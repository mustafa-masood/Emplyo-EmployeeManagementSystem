using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.SuperAdmin
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
