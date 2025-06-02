using Microsoft.AspNetCore.Mvc;

namespace DollarProject.Areas.Admin.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
