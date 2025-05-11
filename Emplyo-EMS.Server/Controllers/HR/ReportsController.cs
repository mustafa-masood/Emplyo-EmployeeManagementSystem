using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.HR
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
