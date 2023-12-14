using Microsoft.AspNetCore.Mvc;
using NewBTL.Infrastructure;
using NewBTL.Models;
using WebAppBigHomeWork.Repository;

namespace NewBTL.ViewComponents
{
    public class CartWidget: ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
