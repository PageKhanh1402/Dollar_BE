using Microsoft.AspNetCore.Mvc;

namespace DollarProject.Areas.Admin.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
