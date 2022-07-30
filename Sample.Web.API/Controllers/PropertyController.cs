using Microsoft.AspNetCore.Mvc;

namespace Sample.Web.API.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
