using Microsoft.AspNetCore.Mvc;
using PetFinder.Utils;
using System.Threading.Tasks;

namespace PetFinder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Send()
        {
            await EmailUtil.SendEmailAsync("revanepiriyevacom@gmail.com" , "test", "lorem");
                return RedirectToAction("Index");
        }
    }
}
