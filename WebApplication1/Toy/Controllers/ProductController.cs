using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Toy.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("ViewProducr", "2");
            return View();
        }

        public IActionResult ListProduct()
        {
            return View();
        }
        public IActionResult GaleryProduct()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangeViewProducts(int id)
        {
            if (id == 1)
            {
                Response.Cookies.Append("ViewProducr", "2");
                RedirectToAction("ListProduct");
            }
            else
            {
                Response.Cookies.Append("ViewProducr", "1");
            }
            return Json(id);
        }

    }
}