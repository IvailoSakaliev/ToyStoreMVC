using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using Toy.Models;
using Toy.Models.ViewModels.Contacts;
using System;
using System.Linq;
using Toy.Models.ViewModels.Product;

namespace Toy.Controllers
{
    public class HomeController : Controller
    {

        private IEncriptServises _encript;
        private ContactServise _contact;
        private ProductServise _product;
        public HomeController()
        {
            _encript = new EncriptServises();
            _contact = new ContactServise();
            _product = new ProductServise();
        }
        public IActionResult Index()
        {
           
            
            LoginServise _login = new LoginServise();
            if (!_login.CheckForAdmin())
            {
                return Redirect("Login/Registration");
            }

            ProducLIst itemVM = new ProducLIst();
            itemVM = PopulateIndex(itemVM);

            return View(itemVM);
        }

        private ProducLIst PopulateIndex(ProducLIst itemVM)
        {
            itemVM.Items = _product.GetAll(x => x.Front == 1);
            return itemVM;
        }

        public IActionResult About()
        {
            
            return Redirect("Error");
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactVm model)
        {
            Contact entity = new Contact();
            entity.Email = _encript.EncryptData(model.Email);
            entity.Name = _encript.EncryptData(model.Subject);
            entity.Message = _encript.EncryptData(model.Message);
            entity.Date = DateTime.Today.ToString("dd/MM/yyyy");
            _contact.Save(entity);
            return Redirect("SuccessSendEmail");
        }

        public IActionResult SuccessSendEmail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public JsonResult CookieAcsept(int id)
        {
            if (id != 0)
            {
                Response.Cookies.Append("CookieUsing", "1");
            }
            return Json("ok");
        }
    }
    
}
