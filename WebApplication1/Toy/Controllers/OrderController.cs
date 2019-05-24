using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Models.ViewModels.Login;
using Toy.Models.ViewModels.Order;
using Toy.Models.ViewModels.Product;

namespace Toy.Controllers
{
    public class OrderController
        : Controller
    {
        private ProductServise _product = new ProductServise();
        private ImageServise _image = new ImageServise();
        private OrderServise _order = new OrderServise();

        private int _orderNumber;
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            OrderVM model = new OrderVM();
            Product entity = _product.GetByID(id);
            model.Code = entity.Code;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Title = entity.Title;
            model.FromtImage = entity.Image;

            int quantity = entity.Quantity;
            GenericSelectedList<Order> listUser = new GenericSelectedList<Order>();

            model.Quantity = listUser.GetSelectedListIthemQuantity(quantity);
            List<Images> listImg = _image.GetAll(x => x.Subject_id == id);

            foreach (var item in listImg)
            {
                model.Image.Add(item.Path);
            }


            return View(model);
        }

        [HttpPost]
        public JsonResult SaveOrderInSession(string page, string quantity)
        {
            string orderSession = HttpContext.Session.GetString("OrderProduct");
            string quantityPro = HttpContext.Session.GetString("ProductQuantity");
            if (orderSession == null || orderSession == ""
                 && quantity == null || quantity == "")
            {
                orderSession = page + ",";
                quantityPro = quantity + ",";
            }
            else
            {
                if (orderSession.Contains(page))
                {
                    return Json("colision");
                }
                orderSession += page + ",";
                quantityPro += quantity + ",";

            }

            HttpContext.Session.SetString("OrderProduct", orderSession);
            HttpContext.Session.SetString("ProductQuantity", quantityPro);

            return Json("ok");

        }

        [HttpPost]
        public JsonResult ChangeQuantityOfProduct(string id, string element)
        {
            string orderSession = HttpContext.Session.GetString("OrderProduct");
            string quantityPro = HttpContext.Session.GetString("ProductQuantity");

            string[] keyProduct = orderSession.Split(",");
            string[] keyQuantity = quantityPro.Split(",");

            orderSession = "";
            quantityPro = "";
            for (int i = 0; i < keyProduct.Length -1; i++)
            {
                if (keyProduct[i] == id)
                {
                    keyQuantity[i] = element;
                }
                orderSession += id + ",";
                quantityPro += element + ",";

            }

            HttpContext.Session.SetString("OrderProduct", orderSession);
            HttpContext.Session.SetString("ProductQuantity", quantityPro);

            return Json("ok");

        }        [HttpGet]

        public IActionResult Orders()
        {
            Random rd = new Random();
            _orderNumber = rd.Next(0, 999999);
            ViewData["NumberOfOrder"] = _orderNumber;
            HttpContext.Session.SetString("OrderNumber", _orderNumber.ToString());
            string productsId = HttpContext.Session.GetString("OrderProduct");
            string quantities = HttpContext.Session.GetString("ProductQuantity");
            ProducLIst itemVM = new ProducLIst();

            if (productsId == null || productsId == ""
                && quantities == null || quantities == "")
            {

                return View(itemVM);

            }
            else
            {
                string[] keyProduct = productsId.Split(",");
                string[] keyQuantity = quantities.Split(",");
                
                for (int i = 0; i < keyProduct.Length - 1; i++)
                {
                    Product entity = _product.GetByID(int.Parse(keyProduct[i]));
                    int quantity = entity.Quantity;
                    GenericSelectedList<Order> listUser = new GenericSelectedList<Order>();

                    itemVM.Qua = listUser.GetSelectedListIthemQuantity(quantity);
                    itemVM.Items.Add(_product.GetByID(int.Parse(keyProduct[i])));
                    itemVM.Items[i].Quantity = int.Parse(keyQuantity[i]);
                }
                
                return View(itemVM);
            }
        }

        [HttpPost]
        public JsonResult DeleteOrderProduct(int id)
        {
            string productsId = HttpContext.Session.GetString("OrderProduct");
            string quantities = HttpContext.Session.GetString("ProductQuantity");

            string[] keyProduct = productsId.Split(",");
            string[] keyQuantity = quantities.Split(",");

            productsId = "";
            quantities = "";
            for (int i = 0; i < keyProduct.Length-1; i++)
            {
                if (keyProduct[i] == id.ToString())
                {
                    continue;
                }
                else
                {
                    productsId += keyProduct[i] + ",";
                    quantities += keyQuantity[i] + ",";
                }
            }

            HttpContext.Session.SetString("OrderProduct", productsId);
            HttpContext.Session.SetString("ProductQuantity", quantities);

            return Json("ok");
        }
        [HttpGet]
        public IActionResult MakeOrder()
        {
            RegistrationVM model = new RegistrationVM();
            string userID = HttpContext.Session.GetString("User_ID");
            if (userID != null)
            {
                UserServise _login = new UserServise();
                EncriptServises _encript = new EncriptServises();
                User user = new User();
                user = _login.GetByID(int.Parse(_encript.DencryptData( userID)));
                model.FirstName = _encript.DencryptData(user.Name);
                model.SecondName = _encript.DencryptData(user.SecondName);
                model.City = _encript.DencryptData(user.City);
                model.Adress = _encript.DencryptData(user.Adress);
                model.Telephone = _encript.DencryptData(user.Telephone);
                ViewData["Information"] = "Вие имате регистрация в нашия  сайт! Моля натиснете бътона 'Поръчай', за да направите поръчката си!";

            }
            else
            {
                ViewData["Information"] = "Моля въведете информацията която се изисква за да направите вашата поръчка!";
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult MakeOrder(RegistrationVM model)
        {
            string productsId = HttpContext.Session.GetString("OrderProduct");
            string quantities = HttpContext.Session.GetString("ProductQuantity");
            string userID = HttpContext.Session.GetString("User_ID");

            string[] keyProduct = productsId.Split(",");
            string[] keyQuantity = quantities.Split(",");
            
            User user = new User();
            UserServise _user = new UserServise();

            if (userID != null)
            {
                EncriptServises _encript = new EncriptServises();
                for (int i = 0; i < keyProduct.Length -1; i++)
                {
                    Order entity = new Order();
                    entity.SubjectID = int.Parse(keyProduct[i]);
                    entity.Quantity = int.Parse(keyQuantity[i]);
                    entity.OrderNumber = HttpContext.Session.GetString("OrderNumber");
                    entity.Date = DateTime.Today.ToString();
                    entity.Status = Status.Supplier;
                    entity.UserID = int.Parse(_encript.DencryptData(userID));
                    _order.Save(entity);

                    
                }
            }
            else
            {
                user = AddUserInDB(user, model);
                _user.Save(user);
                user = new User();
                for (int i = 0; i < keyProduct.Length - 1; i++)
                {
                    Order entity = new Order();
                    entity.SubjectID = int.Parse(keyProduct[i]);
                    entity.Quantity = int.Parse(keyQuantity[i]);
                    entity.OrderNumber = HttpContext.Session.GetString("OrderNumber");
                    entity.Date = DateTime.Today.ToString();
                    entity.Status = Status.Supplier;
                    user = _user.GetLastElement();
                    entity.UserID = user.ID;
                    _order.Save(entity);
                }
            }
            DeleteSession();
            return RedirectToAction("CungratOrder");
        }

        private void DeleteSession()
        {
            HttpContext.Session.Remove("OrderNumber");
            HttpContext.Session.Remove("OrderProduct");
            HttpContext.Session.Remove("ProductQuantity");
        }

        private User AddUserInDB(User user, RegistrationVM model)
        {
            EncriptServises _encript = new EncriptServises();
            user.Name = _encript.EncryptData(model.FirstName);
            user.SecondName = _encript.EncryptData(model.SecondName);
            user.City = _encript.EncryptData(model.City);
            user.Adress = _encript.EncryptData(model.Adress);
            user.Telephone = _encript.EncryptData(model.Telephone);
            return user;
        }

        [HttpGet]
        public IActionResult CungratOrder()
        {
            return View();
        }


    }
}