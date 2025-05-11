using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Employee
{
    public class AttendanceViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
