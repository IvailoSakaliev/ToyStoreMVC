using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Authenticatiion
{
    public class UserFilter
       : ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var url = context.HttpContext.Request.Path;
            
            if (true)
            {

            }
        }
    }
}
