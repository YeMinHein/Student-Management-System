using Microsoft.AspNetCore.Mvc;

namespace Student_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
