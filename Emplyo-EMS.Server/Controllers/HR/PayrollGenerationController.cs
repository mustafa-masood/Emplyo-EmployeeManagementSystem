using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.HR
{
    public class PayrollGenerationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
