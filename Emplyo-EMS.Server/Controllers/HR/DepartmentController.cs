using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.HR
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
