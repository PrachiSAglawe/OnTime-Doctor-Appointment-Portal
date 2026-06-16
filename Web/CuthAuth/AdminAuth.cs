using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.CuthAuth
{
    public class AdminAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("AdminID") == null)
            {
                context.Result = new RedirectToActionResult("Login", "ManageAdmin", new{ area = "" });
            }
        }
    }
}
