using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Enum;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Authenticatiion;
using Toy.Authentication;
using Toy.Filters;
using Toy.Models;

namespace Toy.Controllers
{
    public abstract class GenericController<TEntity, TeidtVM, TlistVM, Tfilter, Tservise> : Controller
         where TEntity : BaseModel, new()
         where TeidtVM : new()
         where Tfilter : GenericFiler<TEntity>, new()
         where TlistVM : IGenericList<TEntity, Tfilter>, new()
         where Tservise : IGenericServise<TEntity>, new()
    {

        public Tservise _Servise { get; set; }
        public TEntity entity { get; set; }
        protected int login_id { get; set; }
        private LoginServise _singin { get; set; }
        private IEncriptServises _encript;
        private BaseTypeServise _baseType { get; set; }
        private TypeServise _typeServise { get; set; }


        public GenericController()
        {
            _Servise = new Tservise();
            _singin = new LoginServise();
            _baseType = new BaseTypeServise();
            _typeServise = new TypeServise();
        }

        [HttpGet]
        public ActionResult Index(int Curentpage)
        {
            TlistVM itemVM = new TlistVM();
            itemVM.Filter = new Tfilter();
            itemVM = PopulateIndex(itemVM, Curentpage);
            string controllerNAme = GetControlerName();

            return View(itemVM);
        }



        protected virtual TlistVM PopulateIndex(TlistVM itemVM, int curentPage)
        {
            string controllerName = GetControlerName();
            string actionname = GetActionName();

            itemVM.ControllerName = controllerName;
            itemVM.ActionName = actionname;
            itemVM.AllItems = _Servise.GetAll();
            itemVM.Pages = itemVM.AllItems.Count / 12;
            double doublePages = itemVM.AllItems.Count / 12.0;
            if (doublePages > itemVM.Pages)
            {
                itemVM.Pages++;
            }
            itemVM.StartItem = 12 * curentPage;
            try
            {
                if (controllerName == "Type")
                {
                    for (int i = itemVM.StartItem - 12; i < itemVM.StartItem; i++)
                    {    
                        itemVM.Items.Add(itemVM.AllItems[i]);
                        itemVM.BaseTypeName.Add(PopulateINdexType(itemVM , i));
                    }
                }
                else {

                    for (int i = itemVM.StartItem - 12; i < itemVM.StartItem; i++)
                    {
                        itemVM.Items.Add(itemVM.AllItems[i]);
                    }
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
        [AuthenticationFilter]
        public ActionResult Edit(int id)
        {
            TEntity entity = new TEntity();
            TeidtVM model = new TeidtVM();
            Tservise servise = new Tservise();
            entity = servise.GetByID(id);
            model = PopulateModelToItem(entity, model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TeidtVM model, int id)
        {
            if (ModelState.IsValid == true)
            {
                TEntity entity = new TEntity();
                Tservise servise = new Tservise();
                entity = PopulateEditItemToModel(model, entity, id);
                servise.Save(entity);
                string controllername = GetControlerName();
                if (controllername == "Type")
                {
                    return Redirect("../Index?Curentpage=1");
                }
                return Redirect("Index?Curentpage=1");
            }
            return View(model);
        }

        [HttpGet]
        [AuthenticationFilter]
        public ActionResult Add()
        {
            TeidtVM model = new TeidtVM();
            string controllerName = GetControlerName();
            if (controllerName == "Type")
            {
                model = PopilateSelectListIthem(model);
                
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Add(TeidtVM model)
        {
            if (ModelState.IsValid)
            {
                TEntity entity = new TEntity();
                entity = PopulateItemToModel(model, entity);
                if (entity == null)
                {
                    return View(model);
                }
                else
                {
                    _Servise.Save(entity);
                }

            }
            return Redirect("Index?CurentPage=1");
        }

        private void AddInformation(TEntity entity, TeidtVM model)
        {
            entity = PopulateItemToModel(model, entity);
            Tservise servise = new Tservise();
            servise.Save(entity);
        }

        [HttpGet]
        [UserFilter]
        public ActionResult Details(int id)
        {
            TEntity entity = new TEntity();
            TeidtVM model = new TeidtVM();
            entity = _Servise.GetByID(id);
            model = PopulateModelToItem(entity, model);
            return View(model);
        }

       

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string controllerName = GetControlerName();
            if (controllerName == "BaseType")
            {
                ProductServise _product = new ProductServise();
                var elementID = _typeServise.GetAll(x => x.BaseTypeID == id);
                var productElement = _product.GetAll(x => x.Basetype == id);
                foreach (var item in productElement)
                {
                    item.Basetype = 0;
                    item.Type = 0;
                    _product.Save(item);
                }
                foreach (var item in elementID)
                {
                    _typeServise.DeleteById(item.ID);
                    
                }
            }
            _Servise.DeleteById(id);
            return Redirect("../Index?Curentpage=1");
        }

        


        // abstract and viirtual classess 
        public abstract TEntity PopulateItemToModel(TeidtVM model, TEntity entity);
        public abstract TeidtVM PopulateModelToItem(TEntity entity, TeidtVM model);
        public abstract TEntity PopulateEditItemToModel(TeidtVM model, TEntity entity, int id);
        public virtual TeidtVM PopilateSelectListIthem(TeidtVM model)
        {
            throw new NullReferenceException();
        }
        public virtual Login PopulateRegisterInfomationInModel(Login entity, TeidtVM model)
        {
            throw new NullReferenceException();
        }
        public virtual bool CheckForExitedUserInDB(TeidtVM model)
        {
            throw new NullReferenceException();

        }
        internal abstract string PopulateINdexType(TlistVM itemVM, int id);
    }
}