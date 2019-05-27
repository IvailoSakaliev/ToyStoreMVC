using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Authentication;
using Toy.Models.ViewModels.Product;

namespace Toy.Controllers
{
    [AuthenticationFilter]
    public class AdminController : Controller
    {
        private ProductServise _product { get; set; }
        private ImageServise _image { get; set; }
        private TypeServise _type { get; set; }
        private BaseTypeServise _basetype { get; set; }
        private static int type;
        private static string frontImage;
        private static int _idElement;

        public AdminController()
        {
            _product = new ProductServise();
            _image = new ImageServise();
            _type = new TypeServise();
            _basetype = new BaseTypeServise();
        }
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

            var baseType = _basetype.GetAll();
            GenericSelectedList<BaseType> listBAseType = new GenericSelectedList<BaseType>();

            model.BaseType = listBAseType.GetSelectedListIthem(baseType);
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
            entity.Type = int.Parse(Request.Form["type"]);
            entity.Basetype = int.Parse(Request.Form["basetype"]);

            if (entity.Type == -1 || entity.Basetype == -1)
            {
                ModelState.AddModelError(string.Empty, "Please select type or basetype!");
                return View(model);
            }
            entity.Date = DateTime.Today.ToString();
            entity.Image = GetImagePath(photo);

            _product.Save(entity);
            Product item = _product.GetLastElement();

            Addimage(photo, item.ID);


            return Redirect("Product");
        }

        private string GetImagePath(IFormFile[] photo)
        {
            return "../images/Galery/" + photo[0].FileName;
        }

        private void Addimage(IFormFile[] photo, int id)
        {
            ImageServise _img = new ImageServise();
            string isUploadet = _img.UploadImagesForUser(photo);
            ModelState.AddModelError(string.Empty, isUploadet);

        }

        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            string result = "";
            try
            {
                _image.DeleteById(id);
                result = "ok";
            }
            catch (Exception)
            {
                result = "no";
            }
            return Json(result);
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

                    itemVM.BaseTypeName.Add(AddNameOftypes(itemVM, i,1));
                    itemVM.TypeName.Add(AddNameOftypes(itemVM, i,2));


                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }

            return itemVM;
        }

        private string AddNameOftypes(ProducLIst itemVM, int id, int v)
        {
           
            if (v == 2)
            {
                TypeServise servise = new TypeServise();
                string items = "";
                var element = servise.GetByID(itemVM.Items[id].Type);
                items = element.Name;
                return items;
            }
            else
            {
                BaseTypeServise servise = new BaseTypeServise();
                string items = "";
                var element = servise.GetByID(itemVM.Items[id].Basetype);
                items = element.Name;
                return items;
            }
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
            _idElement = id;
            model.Code = entity.Code;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            model.Title = entity.Title;
            model.DateOfEdit = entity.Date;
            type = entity.Type;
            frontImage = entity.Image;

            List<Images> img = new List<Images>();
            img = _image.GetAll(x => x.Subject_id == id);
            foreach (var item in img)
            {
                model.ImageS.Add(item);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductVM model, IFormFile[] photo)
        {
            Product entity = new Product();
            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.Date = DateTime.Today.ToString();
            entity.ID = _idElement;
            entity.Type = type;
            entity.Image = frontImage;

            _product.Save(entity);
            if (photo.Length != 0)
            {
                Addimage(photo, _idElement);
            }

            ViewBag.success = "Product is updated successfuly !";
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