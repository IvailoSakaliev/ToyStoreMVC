using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Models.ViewModels.Product;
using ToyStore.Authentication;

namespace Toy.Controllers
{
    [AuthenticationFilter]
    public class AdminController : Controller
    {
        private TypeServise _type = new TypeServise();
        private ProductServise _product = new ProductServise();
        private ImageServise _image = new ImageServise();
        private string type;


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Product()
        {
            ProductVM model = new ProductVM();
            var type = _type.GetAll();
            GenericSelectedList<TypeSubject> listUser = new GenericSelectedList<TypeSubject>();

            model.Type = listUser.GetSelectedListIthem(type);
            return View(model);
        }

        [HttpPost]
        public IActionResult Product(ProductVM model, IFormFile[] photo)
        {
            Product entity = new Product();
            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.Type = Request.Form["type"].ToString();
            entity.Date = DateTime.Today.ToString();
            entity.Image = GetImagePath(photo);
            _product.Save(entity);
            Addimage(photo);
            ViewBag.success = "Product is added successfuly !";
            return Redirect("Product");
        }

        private string GetImagePath(IFormFile[] photo)
        {
            return "/images/Galery/" + photo[0].FileName;
        }

        private void Addimage(IFormFile[] photo)
        {
            Product item = _product.GetLastElement();
            ImageServise _img = new ImageServise();
            string isUploadet = _img.UploadImages(photo, item.ID);
            ModelState.AddModelError(string.Empty, isUploadet);

        }

        [HttpGet]
        public ActionResult ProductIndex(int Curentpage)
        {
            ProducLIst itemVM = new ProducLIst();
            itemVM = PopulateIndex(itemVM, Curentpage);
            string controllerNAme = GetControlerName();


            string cookieValue = Request.Cookies["ViewProducr"];
            ViewBag.Cookie = cookieValue;
            return View(itemVM);
        }



        protected virtual ProducLIst PopulateIndex(ProducLIst itemVM, int curentPage)
        {
            string controllerName = GetControlerName();
            string actionname = GetActionName();

            itemVM.ControllerName = controllerName;
            itemVM.ActionName = actionname;
            itemVM.AllItems = _product.GetAll();
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

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            ProductVM model = new ProductVM();
            Product entity = _product.GetByID(id);
            model.Code = entity.Code;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            type = entity.Type;

            List<Images> img = new List<Images>();
            img = _image.GetAll(x => x.Subject_id == id);
            foreach (var item in img)
            {
                model.Image.Add(item.Path);
            }


            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductVM model, IFormFile[] photo)
        {
            return Redirect("../ProductIndex?Curentpage=1");
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            _product.DeleteById(id);
            _image.Delete(x => x.Subject_id == id);
            return Redirect("../ProductIndex?Curentpage=1");
        }



    }
}