using Microsoft.AspNetCore.Mvc;

namespace PetFinder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
