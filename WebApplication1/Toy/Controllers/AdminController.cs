using System;
using System.Collections.Generic;
using System.Globalization;
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

        private static string _code;

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
            entity.Date = DateTime.Today.ToString("dd/MM/yyyy");
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
            string isUploadet = _img.UploadImages(photo, id);
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
            if (_code != null)
            {
                itemVM.AllItems = _product.GetAll(x=> x.Code.Contains(_code));
            }
            else
            {
                itemVM.AllItems = _product.GetAll();
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
                if (itemVM.Items[id].Type == 0)
                {
                    items = "";
                }
                else
                {
                    var element = servise.GetByID(itemVM.Items[id].Type);
                    items = element.Name;
                }

                return items;
            }
            else
            {
                BaseTypeServise servise = new BaseTypeServise();
                string items = "";

                if (itemVM.Items[id].Basetype == 0)
                {
                    items = "";
                }
                else
                {
                    var element = servise.GetByID(itemVM.Items[id].Basetype);
                    items = element.Name;
                }
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
            model.TypeString = entity.Type;
            model.BaseTyoeString = entity.Basetype;

            frontImage = entity.Image;
            var type = _type.GetAll();
            GenericSelectedList<TypeSubject> listUser = new GenericSelectedList<TypeSubject>();

            model.Type = listUser.GetSelectedListIthem(type);

            var baseType = _basetype.GetAll();
            GenericSelectedList<BaseType> listBAseType = new GenericSelectedList<BaseType>();

            model.BaseType = listBAseType.GetSelectedListIthem(baseType);

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
            entity.Date = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            entity.ID = _idElement;
            if (int.Parse(Request.Form["basetype"]) == -1 || int.Parse(Request.Form["type"]) == -1)
            {
                ModelState.AddModelError(string.Empty, "Please select type or base type!!!");
                return View(model);
            }
            entity.Type = int.Parse(Request.Form["type"]);
            entity.Basetype = int.Parse(Request.Form["basetype"]);
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

        [HttpGet]
        public IActionResult ListProducts(int Curentpage)
        {
            ProducLIst itemVM = new ProducLIst();
            itemVM = PopulateIndex(itemVM, Curentpage);
            return View(itemVM);
        }

        [HttpPost]
        public JsonResult FilterProduct(int id , string element)
        {
            _code = element;
            return Json("ok");
        }

        [HttpPost]
        public JsonResult ChangetopProduct(int id , int mode)
        {
            string error = "";
            if (mode == 1)
            {
                var list = _product.GetAll(x => x.Front == 1);
                if (list.Count == 4)
                {
                    error = "4";
                }
                else
                {
                    Product product = _product.GetByID(id);
                    product.Front = 1;
                    _product.Save(product);
                    error = "ok";
                }
            }
            else
            {
                Product product = _product.GetByID(id);
                product.Front = 0;
                _product.Save(product);
                error = "ok";
            }
            return Json(error);
        }
        [HttpPost]
        public JsonResult Restore(int id)
        {
            _code = null;
            return Json("ok");
        }
    }
}