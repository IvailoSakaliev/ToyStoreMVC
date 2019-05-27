using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using System.Collections.Generic;
using Toy.Authenticatiion;
using Toy.Models.ViewModels.User;

namespace Toy.Controllers
{
    public class UserController : Controller
    {
        UserServise _servise = new UserServise();
        EncriptServises _encript = new EncriptServises();
        private static int id;
        private static int UserID;
        private static int loginID;
        public IActionResult Index()
        {
            return View();
        }
        //[UserFilter]
        [HttpGet]
        public IActionResult Details()
        {
            id = int.Parse(_encript.DencryptData(HttpContext.Session.GetString("User_ID")));
            User entity = new User();
            entity = _servise.GetByID(id);
            UserEditVm model = new UserEditVm();
            model.FirstName = _encript.DencryptData(entity.Name);
            model.SecondName = _encript.DencryptData(entity.SecondName);
            model.City = _encript.DencryptData(entity.City);
            model.Adress = _encript.DencryptData(entity.Adress);
            model.Telephone = _encript.DencryptData(entity.Telephone);
            model.Image = entity.Image;
            
            //OrderServise _orderServise = new OrderServise();
            //List<Order> orders = new List<Order>();
            //orders = _orderServise.GetAll(x => x.UserID == id);
            //foreach (var item in orders)
            //{
            //    model.Orders.Add(item);
            //}
            // add list in user vm
            return View(model);
        }

        [HttpPost]
        public JsonResult GetInfoForUser()
        {
            return Json("ok");
        }

        [HttpGet]
        public IActionResult ChangeDetails()
        {
            UserEditVm model = new UserEditVm();
            User entity = new User();
            entity = _servise.GetByID(id);
            model.FirstName = _encript.DencryptData(entity.Name);
            model.SecondName = _encript.DencryptData(entity.SecondName);
            model.City = _encript.DencryptData(entity.City);
            model.Adress = _encript.DencryptData(entity.Adress);
            model.Telephone = _encript.DencryptData(entity.Telephone);
            UserID = entity.ID;
            model.Image = entity.Image;
            loginID = entity.LoginID;

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeDetails(UserEditVm model , IFormFile[] photo)
        {
            
            User entity = new User();
            entity.ID = UserID;
            entity.Name = _encript.EncryptData(model.FirstName);
            entity.SecondName = _encript.EncryptData(model.SecondName);
            entity.City = _encript.EncryptData(model.City);
            entity.Adress = _encript.EncryptData(model.Adress);
            entity.Telephone = _encript.EncryptData(model.Telephone);
            entity.Image = GetImagePath(photo);
            entity.LoginID = loginID;
            Addimage(photo);

            _servise.Save(entity);

            return RedirectToAction("Details");
        }
        private string GetImagePath(IFormFile[] photo)
        {
            return "../images/Galery/" + photo[0].FileName;
        }

        private void Addimage(IFormFile[] photo)
        {
            ImageServise _img = new ImageServise();
            string isUploadet = _img.UploadImagesForUser(photo);
            ModelState.AddModelError(string.Empty, isUploadet);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string password)
        {
            Login user = new Login();
            LoginServise _login = new LoginServise();
            user = _login.GetByID(id);
            user.Password = _encript.EncryptData(password);
            _login.Save(user);

            return Json("ok");
        }
        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangeEmail(string email)
        {
            Login user = new Login();
            LoginServise _login = new LoginServise();
            user = _login.GetByID(id);
            user.Email = _encript.EncryptData(email);
            _login.Save(user);

            return Json("ok");
        }

        [HttpPost]
        public JsonResult ChangeEmailUser()
        {
            return Json("ok");
        }

        [HttpPost]
        public JsonResult ChangePasswordlUser()
        {
            return Json("ok");
        }

    }
}