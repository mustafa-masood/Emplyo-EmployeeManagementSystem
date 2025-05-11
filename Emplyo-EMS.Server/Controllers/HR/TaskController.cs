using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.HR
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
