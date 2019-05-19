using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.Product;
using ToyStore.Controllers;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Toy.Controllers
{
    public class ProductController : GenericController<Product, ProductVM, ProducLIst, PruductFilter, ProductServise>
    {
        ProductServise _productServise = new ProductServise();
        private List<Product> list;
        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search != null)
            {
                string[] keys = search.Split(" ");
                list = new List<Product>();
                
                list = _productServise.GetAll(x => x.Title.Contains(keys[0]));
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
                
                return View(itemVM);
            }

            return View();
        }

        public ProducLIst Check(string[] keys, List<Product> list, int i, ProducLIst itemVM)
        {
            try
            {
                List<Product> newResult = new List<Product>();
                newResult = list.Where(x => x.Title.Contains(keys[i])).ToList();
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




        public IActionResult ListProduct(string Curentpage)
        {
            int page = int.Parse(Curentpage);
            return Index(page);
        }
        public IActionResult GaleryProduct(string Curentpage)
        {
            int page = int.Parse(Curentpage);
            return Index(page);
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
            string isUploadet= _img.UploadImages(photo);
            ModelState.AddModelError(string.Empty, isUploadet);
            return View();
        }

      
    }
}