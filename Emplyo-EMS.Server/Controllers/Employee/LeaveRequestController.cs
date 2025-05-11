using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Employee
{
    public class LeaveRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
