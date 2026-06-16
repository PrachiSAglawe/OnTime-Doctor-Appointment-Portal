using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.CuthAuth
{
    public class ClinicAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("ClinicOwnerID") == null)
            {
                context.Result = new RedirectToActionResult("SignIn", "ManageClinicOwner", new { area = "" });
            }
        }
    }
}
