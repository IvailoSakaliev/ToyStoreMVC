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
using Toy.Models.ViewModels;
using Toy.Models.ViewModels.Login;

namespace Toy.Controllers
{
    public class LoginController : Controller
    {
        private AuthenticationServises _aut = new AuthenticationServises();
        private IEncriptServises _encript = new EncriptServises();
        private LoginServise _servise = new LoginServise();
        

        [HttpGet]
        public IActionResult Index()
        {

            return View();
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
                List<Login> list = _servise.GetAll(x => x.Email == model.Email);
                if (list.Count != 0)
                {
                     //EmailServises _email = new EmailServises(list[0]);
                    //_email.SendEmail(2);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Тhis email is not in our system.");
                    return View();

                }
            }
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

        private void AddUSerINformation(RegistrationVM reg)
        {
            List<Login> list = new List<Login>();
            list = _servise.GetAll(x => x.Email == _encript.EncryptData(reg.Email));
            User user = new User();
            user.LoginID = list[0].ID;
            user.FirstName = _encript.EncryptData(reg.FirstName);
            user.SecondName = _encript.EncryptData(reg.SecondName);
            user.City = _encript.EncryptData(reg.City);
            user.Adress = _encript.EncryptData(reg.Adress);
            user.Telephone = _encript.EncryptData(reg.Telephone);
            UserServise _userServise = new UserServise();
            _userServise.Save(user);
        }

        private string EnterLoginInformation( RegistrationVM reg)
        {
            Login login = new Login();
            login.Email = _encript.EncryptData(reg.Email);
            if (reg.Password == reg.ConfirmPassword)
            {
                login.Password = _encript.EncryptData(reg.Password);
                login.isRegisted = false;
                login.Role = 2;
                _servise.Save(login);
            }
            else
            {
                return "Password no match";
            }
            
            return "OK";
        }

        private void GoToSession()
        {
            HttpContext.Session.SetString("LoggedUser", _aut._LoggedUser.Role.ToString());
            
            HttpContext.Session.SetString("UserFirstName",_aut._LoggedUser.Email);

            HttpContext.Session.SetString("User_ID", _aut._LoggedUser.ID.ToString());
        }

        public void LoggOut()
        {

            _aut._LoggedUser = null;
            HttpContext.Session.SetString("LoggedUser", null);

            HttpContext.Session.SetString("UserFirstName", null);

            HttpContext.Session.SetString("User_ID", null);
            //CoockieServises cookie = new CoockieServises();
            //cookie.DeleteCoockie("UserInformation");

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
            cookie.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("UserEmail", model.Email, cookie);
            Response.Cookies.Append("Userpassword", model.Password);
        }
        public LoginVM GetInformation()
        {
            LoginVM login = new LoginVM();
            var cookie = new CookieOptions();
            login.Email = Request.Cookies["UserEmail"];
            login.Password = Request.Cookies["Userpassword"];
            return login;
        }
    }
}
