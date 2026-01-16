using Microsoft.AspNetCore.Mvc;

namespace SergeevaMaria_Lab6_V2_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
