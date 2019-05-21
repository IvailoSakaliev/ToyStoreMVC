using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Servise.ProjectServise;

namespace ToyStore.Authentication
{
    public class AuthenticationFilter
        : ActionFilterAttribute
    {
        private IEncriptServises _encript = new EncriptServises();
        public override void OnActionExecuting(ActionExecutingContext context)


        {
            if (context.HttpContext.Session.GetString("LoggedUser") == null)
            {
                context.HttpContext.Response.Redirect("../Error/Login");
            }
            else
            {
                string id = _encript.DencryptData(context.HttpContext.Session.GetString("LoggedUser"));
                if (id != "1")
                {
                    context.HttpContext.Response.Redirect("../Home/Index");
                }
            }
            base.OnActionExecuting(context);
           
        }
    }
}
