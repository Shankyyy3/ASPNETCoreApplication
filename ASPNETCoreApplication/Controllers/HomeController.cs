using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
