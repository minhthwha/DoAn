using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
