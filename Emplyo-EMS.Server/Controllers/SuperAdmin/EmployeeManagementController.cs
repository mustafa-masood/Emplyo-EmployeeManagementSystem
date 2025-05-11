using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.SuperAdmin
{
    public class EmployeeManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
