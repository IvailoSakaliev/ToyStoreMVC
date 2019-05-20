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
        public static string _search { get; set; }
        public static int _priceFrom { get; set; }
        public static int _priceTo { get; set; }
        public static string _subType { get; set; }

        [HttpPost]
        public IActionResult Index(string search)
        {
            ProducLIst itemVM = new ProducLIst();
            if (search != null)
            {
                _search = search;
                string[] keys = search.Split(" ");
                List<Product> list = new List<Product>();

                list = _productServise.GetAll(x => x.Title.ToLower().Contains(keys[0].ToLower()));
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

            return View(itemVM);
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




        public IActionResult ListProduct(string Curentpage)
        {
            ProducLIst itemVM = new ProducLIst();
            itemVM.Filter = new PruductFilter();
            itemVM = GetElement(itemVM, int.Parse(Curentpage));
            return View(itemVM);
        }


        private ProducLIst GetElement(ProducLIst itemVM, int curentPage)
        {
            string controllerName = GetControlerName();
            string actionname = GetActionName();

            itemVM.ControllerName = controllerName;
            itemVM.ActionName = actionname;
            if (_priceTo != 0)
            {
                itemVM.AllItems = _Servise.GetAll(x => x.Price < _priceTo);
            }
            else if (_priceFrom != 0)
            {
                itemVM.AllItems = _Servise.GetAll(x => x.Price > _priceFrom);
            }
            else if (_priceFrom != 0 && _priceTo != 0)
            {
                itemVM.AllItems = _Servise.GetAll(x => x.Price > _priceFrom && x.Price < _priceTo);
            }
            else
            {
                itemVM.AllItems = _Servise.GetAll();
            }
            

            itemVM.Pages = itemVM.AllItems.Count / 12;
            double doublePages = itemVM.AllItems.Count / 12.0;
            if (doublePages > itemVM.Pages)
            {
                itemVM.Pages++;
            }
            itemVM.StartItem = 12 * curentPage;
            try
            {
                for (int i = itemVM.StartItem - 12; i < itemVM.StartItem; i++)
                {
                    itemVM.Items.Add(itemVM.AllItems[i]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }

            return itemVM;
        }
        private string GetActionName()
        {
            return this.ControllerContext.RouteData.Values["action"].ToString();
        }

        private string GetControlerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        public IActionResult GaleryProduct(string Curentpage)
        {
            ProducLIst itemVM = new ProducLIst();
            itemVM.Filter = new PruductFilter();
            itemVM = GetElement(itemVM, int.Parse(Curentpage));
            return View(itemVM);
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

        [HttpPost]
        public JsonResult FilterPriceTo(string element)
        {
            _priceTo = int.Parse(element);
            return Json(Request.Cookies["ViewProducr"]);
        }
        

        [HttpPost]
        public JsonResult FilterPriceFrom(string element)
        {
            _priceFrom = int.Parse(element);
            return Json(Request.Cookies["ViewProducr"]);
        }
        [HttpPost]
        public JsonResult ChangeFiltredResult(int id)
        {
            return Json(Request.Cookies["ViewProducr"]);
        }

    }
}
