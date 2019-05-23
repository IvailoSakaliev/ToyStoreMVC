using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
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
                && quantity == null || quantity =="")
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
                else
                {
                    orderSession += page + ",";
                    quantityPro += quantity + ",";
                }
                
            }
            
             HttpContext.Session.SetString("OrderProduct", orderSession);
            HttpContext.Session.SetString("ProductQuantity", quantityPro);

            return Json("ok");
            
        }
        [HttpGet]
        public IActionResult Orders()
        {
            Random rd = new Random();
            ViewData["NumberOfOrder"] = rd.Next(0, 999999);
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
                    itemVM.Items.Add(_product.GetByID(int.Parse(keyProduct[i])));
                    itemVM.Items[i].Quantity = int.Parse(keyQuantity[i]);
                }

                string cookieValue = Request.Cookies["ViewProducr"];
                ViewBag.Cookie = cookieValue;
                return View(itemVM);
            }
        }

       
    }
}