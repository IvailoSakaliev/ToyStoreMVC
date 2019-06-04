using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Enum;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Authentivation;
using Toy.Models.ViewModels;
using Toy.Models.ViewModels.Login;

namespace Toy.Controllers
{
    public class LoginController : Controller
    {
        private AuthenticationServises _aut = new AuthenticationServises();
        private IEncriptServises _encript = new EncriptServises();
        private LoginServise _servise = new LoginServise();
        private static int userID;

        [HttpGet]
        public IActionResult Index()
        {
            LoginVM login = new LoginVM();
            try
            {
                if (Request.Cookies["UserEmail"] != null
                || Request.Cookies["UserEmail"] != "")
                {
                    login.Email = _encript.DencryptData(Request.Cookies["UserEmail"]);
                    login.Password = _encript.DencryptData(Request.Cookies["Userpassword"]);
                }
            }
            catch (ArgumentNullException ex)
            {

                return View(login);
            }
            
            return View(login);
        }

        [HttpPost]
        public IActionResult Index(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                int id = _aut.AuthenticateUser(login.Email, login.Password, 1,1);
                if ( id != 0)
                {
                    if (id == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Please go to your email to active yout account!");
                        return View();
                    }
                    else
                    {
                        GoToSession();
                        if (login.RememberME)
                        {
                            CreateCookie(login);
                        }
                        return Redirect("../");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This user is't exist in this site. Please go ot Create account to registed");
                    
                }
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassM model)
        {
            if (ModelState.IsValid)
            {
                Login login = new Login();
                List<Login> list = _servise.GetAll(x => x.Email == _encript.EncryptData(model.Email));
                if (list.Count != 0)
                {
                     EmailServises _email = new EmailServises(list[0]);
                   _email.SendEmail(2);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Тhis email is not in our system.");
                    return View();

                }
            }
            return RedirectToAction("GoToEmail");
        }
        [HttpGet]
        public IActionResult GoToEmail()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Confirm()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationVM reg)
        {
            if(ModelState.IsValid)
            {
                if (CheckForExitedUserInDB(reg))
                {
                    ModelState.AddModelError(string.Empty, "Email already exist in site. Please enter anoither emeil");
                    return View(reg);
                }
                else {
                   
                    string error = EnterLoginInformation(reg);
                    if (error != "OK")
                    {
                        ModelState.AddModelError(string.Empty, error);
                        return View(reg);
                    }
                    else
                    {
                        AddUSerINformation(reg);
                        
                    }
                 }
                    
            }
            return RedirectToAction("Confirm");
        }

        private User AddUSerINformation(RegistrationVM reg)
        {
            List<Login> list = new List<Login>();
            list = _servise.GetAll(x => x.Email == _encript.EncryptData(reg.Email));
            User user = new User();
            user.LoginID = list[0].ID;
            user.Name = _encript.EncryptData(reg.FirstName);
            user.SecondName = _encript.EncryptData(reg.SecondName);
            user.City = _encript.EncryptData(reg.City);
            user.Adress = _encript.EncryptData(reg.Adress);
            user.Telephone = _encript.EncryptData(reg.Telephone);
            user.Image = "../images/userIcon.png";
            UserServise _userServise = new UserServise();
            _userServise.Save(user);
            return user;
        }

        private string EnterLoginInformation( RegistrationVM reg)
        {
            Login login = new Login();
            LoginVM loginVm = new LoginVM();
            login.Email = _encript.EncryptData(reg.Email);
            if (reg.Password == reg.ConfirmPassword)
            {
                login.Password = _encript.EncryptData(reg.Password);
                
                if (!_servise.CheckForAdmin())
                {
                    login.isRegisted = true;
                    login.Role = 1;
                    _servise.Save(login);

                    loginVm.Email = reg.Email;
                    loginVm.Password = reg.Password;
                    CreateCookie(loginVm);

                    

                }
                else
                {
                    login.isRegisted = false;
                    login.Role = 2;
                    _servise.Save(login);

                    login = new Login();
                    login = _servise.GetLastElement();
                    EmailServises _email = new EmailServises(login);
                    _email.SendEmail(1);
                }
                
               

            }
            else
            {
                return "Password no match";
            }
           
            return "OK";
        }

        [HttpGet]
        public IActionResult EnableAccount(int id)
        {
            Login entity = new Login();
            entity = _servise.GetByID(id);
            entity.isRegisted = true;
            _servise.Save(entity);
            return View();
        }

        [HttpGet]
        public IActionResult RestorePassword(string id)
        {
            userID = int.Parse(_encript.DencryptData(id));
            RegistrationVM model = new RegistrationVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult RestorePassword(RegistrationVM model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                Login entity = _servise.GetByID(userID);
                entity.Password =_encript.EncryptData(model.Password);
                _servise.Save(entity);
                return RedirectToAction("SuccessChangePassword");
            }
           else
            {
                ModelState.AddModelError(string.Empty, "Паролите не съвпадат !");
                return View(model);
            }

            
        }

        [HttpGet]
        public IActionResult SuccessChangePassword()
        {
            return View();
        }
        private void GoToSession()
        {
            HttpContext.Session.SetString("LoggedUser", _encript.EncryptData(_aut._LoggedUser.Role.ToString()));
            
            HttpContext.Session.SetString("UserFirstName", _encript.EncryptData(_aut._LoggedUser.Email));

            HttpContext.Session.SetString("User_ID", _aut._LoggedUser.ID.ToString());
        }

        [HttpGet]
        public IActionResult LoggOut()
        {

            _aut._LoggedUser = null;
            HttpContext.Session.Remove("LoggedUser");

            HttpContext.Session.Remove("UserFirstName");

            HttpContext.Session.Remove("User_ID");
            
            return RedirectToAction("Index");

        }

        public bool CheckForExitedUserInDB(RegistrationVM model)
        {
            if (_aut.AuthenticateUser(model.Email, model.Password, 2, 2) != 0)
            {
                return true;
            }
            return false;
        }

        public void CreateCookie(LoginVM model)
        {
            var cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("UserEmail", _encript.EncryptData(model.Email), cookie);
            Response.Cookies.Append("Userpassword", _encript.EncryptData(model.Password));
        }
        public LoginVM GetInformation()
        {
            LoginVM login = new LoginVM();
            var cookie = new CookieOptions();
            login.Email = Request.Cookies["UserEmail"];
            login.Password = Request.Cookies["Userpassword"];
            return login;
        }

        [HttpGet]
        [UserFilter]
        public IActionResult Details(int id)
        {
            //TEntity entity = new TEntity();
            //TeidtVM model = new TeidtVM();
            //entity = _Servise.GetByID(id);
            //model = PopulateModelToItem(entity, model);
            return View();
        }


    }
}
