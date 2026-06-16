using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class UserBookApp:ViewComponent
    {
        IUser user;
        public UserBookApp(IUser user)
        {
            this.user = user;
        }
        public async Task<IViewComponentResult> InvokeAsync(Int64 UserID)
        {
            var res = await this.user.GetUserAppoint(UserID);
            return View(res);
        }
    }
}
