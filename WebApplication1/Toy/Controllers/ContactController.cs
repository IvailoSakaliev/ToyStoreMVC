using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using ProjectToyStore.Servise.ProjectServise;
using System;
using Toy.Authentication;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.Contacts;

namespace Toy.Controllers
{
    [AuthenticationFilter]
    public class ContactController 
        :GenericController<Contact,ContactVm, ContactLIst, ContactFilter, ContactServise> 

    {
        private IEncriptServises _encript = new EncriptServises();
        private ContactServise _contact = new ContactServise();
        private static string _emailUser;
        private static string _emailSubjec;
        private static int _emailID;

        public override Contact PopulateEditItemToModel(ContactVm model, Contact entity, int id)
        {
            throw new NotImplementedException();
        }

        public override Contact PopulateItemToModel(ContactVm model, Contact entity)
        {
            throw new NotImplementedException();
        }

        public override ContactVm PopulateModelToItem(Contact entity, ContactVm model)
        {
            model.Email = entity.Email;
            model.Subject = entity.Name;
            model.Message = entity.Message;
            return model;
        }

        internal override string PopulateINdexType(ContactLIst itemVM, int id)
        {
            throw new NotImplementedException();
        }
        public override Contact PopulateIndexContactInfo(Contact model)
        {
            Contact entity = new Contact();
            entity.ID = model.ID;
            entity.Email = _encript.DencryptData(model.Email);
            entity.Name = _encript.DencryptData(model.Name);
            entity.Message = _encript.DencryptData(model.Message);
            entity.Date = model.Date;
            return entity;
        }

        [HttpPost]
        public JsonResult ChangeEmailInformation(int id)
        {
            Contact contact = new Contact();
            Contact item = _contact.GetByID(id);
            contact.Message = _encript.DencryptData(item.Message);
            contact.Email = _encript.DencryptData(item.Email);
            contact.Name = _encript.DencryptData(item.Name);
            contact.Date = item.Date;
            return Json(contact);
        }

        [HttpPost]
        public JsonResult DeleteEmail(int id)
        {
            _contact.DeleteById(id);
            return Json("ok");
        }

        [HttpGet]
        public IActionResult SendEmailTOUser(int id)
        {
            ContactVm model = new ContactVm();
            Contact user = _contact.GetByID(id);
            _emailUser = user.Email;
            _emailSubjec = _encript.DencryptData(user.Name);
            _emailID = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult SendEmailTOUser(ContactVm model)
        {
            Login login = new Login();
            login.Email = _emailUser;
            login.ID = 0;

            Contact entity = new Contact();
            entity.Email = _emailUser;
            entity.Name = _emailSubjec;
            entity.Message = model.Message;
            try
            {
                EmailServises _email = new EmailServises(login);
                _email.SendEmailFromAdmin(3, entity);
                DeleteEmail();
            }
            catch (Exception)
            {

                throw;
            }
           
            
            return Redirect("../Index?Curentpage=1");
        }

        private void DeleteEmail()
        {
            _contact.DeleteById(_emailID);
        }
    }
}