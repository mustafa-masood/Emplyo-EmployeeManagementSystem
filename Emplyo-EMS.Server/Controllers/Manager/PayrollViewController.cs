using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Manager
{
    public class PayrollViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
