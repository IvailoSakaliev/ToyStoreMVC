using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyStore.Authentication
{
    public class AuthenticationFilter
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (HttpContext.Session["LoggedUser"] == null)
            //{
            //    filterContext.HttpContext.Response.Redirect("~/SingIN/Login");
            //}
        }
    }
}
