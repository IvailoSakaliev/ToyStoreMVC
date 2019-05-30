using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Toy.Authentivation
{
    public class UserFilter
       : ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string url = context.HttpContext.Request.Path;
            int substring = url.LastIndexOf('/');
            int idlenght = url.Length - substring;
            string urlID = url.Substring(substring, idlenght);
            string id = context.HttpContext.Session.GetString("User_ID");
            if (id != urlID)
            {
                context.HttpContext.Response.Redirect("../Home/Index");
            }
            base.OnActionExecuting(context);
        }
    }
}
