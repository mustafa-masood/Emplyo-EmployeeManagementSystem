using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Manager
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
