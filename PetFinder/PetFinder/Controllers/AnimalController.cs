using Microsoft.AspNetCore.Mvc;

namespace PetFinder.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
