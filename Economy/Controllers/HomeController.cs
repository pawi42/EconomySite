using Economy.Business;
using System.Web.Mvc;

namespace Economy.Controllers
{   
    [Route]
    public class HomeController : Controller
    {
        [Route("{page:int?}")]
        public ActionResult Index(int? page)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "User");

            page = page ?? 0;

            ViewBag.Title = "Skapa";

            var model = EconomyBusiness.GetBills(page.Value, 50);
            return View(model);
        }
    }
}