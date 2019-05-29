using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using System;
using System.Collections.Generic;
using Toy.Authenticatiion;
using Toy.Models.ViewModels.Order;
using Toy.Models.ViewModels.User;

namespace Toy.Controllers
{
    public class UserController : Controller
    {
        UserServise _servise = new UserServise();
        EncriptServises _encript = new EncriptServises();
        OrderServise _order = new OrderServise();
        ProductServise _product = new ProductServise();

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
            id = int.Parse(HttpContext.Session.GetString("User_ID"));
            OrderList itemVM = new OrderList();
            User user =  _servise.GetByID(id);
            List<Order> orders = _order.GetAll(x => x.UserID == id);
            itemVM = PopulateOrdersInformation(itemVM, orders);
            itemVM.CurrentUser = PopulateUserInformation(itemVM.CurrentUser, user);
            itemVM.Product = PopulateProductINformation(itemVM.Product, orders);
            itemVM.QuantityOrderList = Populatequntity(itemVM.QuantityOrderList, orders);

            return View(itemVM);
        }

        private List<int> Populatequntity(List<int> quantityOrderList, List<Order> orders)
        {
            foreach (var item in orders)
            {
                quantityOrderList.Add(item.Quantity);
            }
            return quantityOrderList;
        }

        private IList<Product> PopulateProductINformation(IList<Product> product, List<Order> orders)
        {
            foreach (var item in orders)
            {
                product.Add(_product.GetByID(item.SubjectID));
            }
            return product;
        }

        private OrderList PopulateOrdersInformation(OrderList itemVM, List<Order> orders)
        {
            string OrderNumber = "";
            int count = 0;
            int quantityOfOrder = 0;
            double totalPrice = 0;
            List<Order> list = new List<Order>();

            for (int i = 0; i < orders.Count; i++)
            {
                if (OrderNumber == orders[i].OrderNumber)
                {
                    count++;
                    quantityOfOrder += orders[i].Quantity;
                    totalPrice += orders[i].Total;
                    OrderNumber = orders[i].OrderNumber;
                }
                else
                {
                    list.Add(orders[i]);
                    OrderNumber = orders[i].OrderNumber;
                    if (i == 0)
                    {
                        count++;
                        quantityOfOrder += orders[i].Quantity;
                        totalPrice += orders[i].Total;
                    }
                    else
                    {
                        itemVM.ProductCount.Add(count);
                        itemVM.QuantityList.Add(quantityOfOrder);
                        itemVM.TotalPriceList.Add(totalPrice);
                        count = 1;
                        quantityOfOrder = orders[i].Quantity;
                        totalPrice = orders[i].Total;
                    }

                }


            }

            itemVM.ProductCount.Add(count);
            itemVM.QuantityList.Add(quantityOfOrder);
            itemVM.TotalPriceList.Add(totalPrice);

            for (int i = 0; i < list.Count; i++)
            {
                itemVM.Items.Add(list[i]);
            }
            return itemVM;
        }

        private UserEditVm PopulateUserInformation(UserEditVm currentUser, User user)
        {
            currentUser.FirstName = _encript.DencryptData(user.Name);
            currentUser.SecondName = _encript.DencryptData(user.SecondName);
            currentUser.City = _encript.DencryptData(user.City);
            currentUser.Adress = _encript.DencryptData(user.Adress);
            currentUser.Telephone = _encript.DencryptData(user.Telephone);
            currentUser.Image = user.Image;
            return currentUser;
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