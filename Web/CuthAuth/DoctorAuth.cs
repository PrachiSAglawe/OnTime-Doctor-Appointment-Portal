using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.CuthAuth
{
    public class DoctorAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("DoctorID") == null)
            {
                context.Result = new RedirectToActionResult("SignIn", "ManageDoctor", new { area = " " });
            }
        }
    }
}
