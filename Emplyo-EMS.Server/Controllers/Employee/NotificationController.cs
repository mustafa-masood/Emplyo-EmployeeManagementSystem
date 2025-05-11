using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Employee
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
