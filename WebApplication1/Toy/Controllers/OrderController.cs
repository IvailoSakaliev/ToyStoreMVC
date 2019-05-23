using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Models.ViewModels.Order;

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
                orderSession = page;
                quantityPro = quantity; 
            }
            else
            {
                orderSession += "," + page;
                quantityPro += "," + quantity;
            }
            
             HttpContext.Session.SetString("OrderProduct", orderSession);
            HttpContext.Session.SetString("ProductQuantity", quantityPro);

            return Json("ok");
            
        }
        [HttpGet]
        public IActionResult Orders()
        {
            return View();
        }
    }
}