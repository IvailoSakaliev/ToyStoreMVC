using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.Product;
using ToyStore.Authentication;
using ToyStore.Controllers;

namespace Toy.Controllers
{
    public class ProductController : GenericController<Product, ProductVM, ProducLIst, PruductFilter, ProductServise>
    {
        ProductServise _productServise = new ProductServise();
        public static List<Product> list = new List<Product>();


        [HttpGet]
        [AuthenticationFilter]
        public ActionResult Index(int Curentpage, string search)
        {
            ProducLIst itemVM = new ProducLIst();
            itemVM.Filter = new PruductFilter();
            if (search != null)
            {
                itemVM = GetProduct(search);
            }
            else
            {
                itemVM = PopulateIndex(itemVM, Curentpage);
            }
            


            string cookieValue = Request.Cookies["ViewProducr"];
            ViewBag.Cookie = cookieValue;
            return View(itemVM);
        }
        
        public ProducLIst GetProduct(string search)
        {
            
                string[] keys = search.Split(" ");
                list = new List<Product>();

                list = _productServise.GetAll(x => x.Title.ToLower().Contains(keys[0].ToLower()));
                ProducLIst itemVM = new ProducLIst();
                itemVM.Filter = new PruductFilter();
                if (keys.Length == 1)
                {
                    foreach (var item in list)
                    {
                        itemVM.Items.Add(item);
                    }
                }
                else
                {
                    itemVM = Check(keys, list, 1, itemVM);
                }

                return itemVM;
           
        }

        public ProducLIst Check(string[] keys, List<Product> list, int i, ProducLIst itemVM)
        {
            try
            {
                List<Product> newResult = new List<Product>();
                newResult = list.Where(x => x.Title.ToLower().Contains(keys[i].ToLower())).ToList();
                if (i == keys.Length - 1)
                {
                    foreach (var item in newResult)
                    {
                        itemVM.Items.Add(item);
                    }
                    return itemVM;
                }
                i++;
                Check(keys, newResult, i, itemVM);


            }
            catch (ArgumentOutOfRangeException ex)
            {

            }
            return itemVM;

        }




        public IActionResult ListProduct(string Curentpage, string search)
        {
            int page = int.Parse(Curentpage);
            return Index(page, search);
        }
        public IActionResult GaleryProduct(string Curentpage, string search)
        {
            int page = int.Parse(Curentpage);
            return Index(page, search);
        }
        [HttpPost]
        public JsonResult ChangeViewProducts(int id)
        {
            if (id == 1)
            {
                Response.Cookies.Append("ViewProducr", "1");
            }
            else
            {
                Response.Cookies.Append("ViewProducr", "2");
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
            string isUploadet = _img.UploadImages(photo);
            ModelState.AddModelError(string.Empty, isUploadet);
            return View();
        }


      
    }
}
