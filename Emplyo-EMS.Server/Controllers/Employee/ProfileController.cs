using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Employee
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
