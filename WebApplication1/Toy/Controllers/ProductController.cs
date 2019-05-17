using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.Product;
using ToyStore.Controllers;

namespace Toy.Controllers
{
    public class ProductController : GenericController<Product, ProductVM, ProducLIst, PruductFilter, ProductServise>
    {
        ProductServise _productServise = new ProductServise();

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search != null)
            {
                string[] keys = search.Split(" ");
                List<Product> list = new List<Product>();
                try
                {
                    list = _productServise.GetAll(x => x.Title.Contains(keys[0]));
                }
                catch
                {

                }
                Check(keys, list, 1);
            }

            return View();
        }

        private void Check(string[] keys, List<Product> list, int i)
        {
            try
            {
                list = _productServise.GetAll(x => x.Title.Contains(keys[i]));
                if (i == keys.Length - 1)
                {
                    return;
                }
                Check(keys, list, i++);
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }

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

        public override Product PopulateItemToModel(ProductVM model, Product entity)
        {
            throw new NotImplementedException();
        }

        public override ProductVM PopulateModelToItem(Product entity, ProductVM model)
        {
            throw new NotImplementedException();
        }

        public override Product PopulateEditItemToModel(ProductVM model, Product entity, int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      [HttpPost]
        public IActionResult Create(ProductVM model, IFormFile[] photo)
        {
            ImageServise _img = new ImageServise();
            string isUploadet= _img.UploadImages(photo);
            ModelState.AddModelError(string.Empty, isUploadet);
            return View();
        }

      
    }
}