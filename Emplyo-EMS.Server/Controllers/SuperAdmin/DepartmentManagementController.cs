using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.SuperAdmin
{
    public class DepartmentManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
