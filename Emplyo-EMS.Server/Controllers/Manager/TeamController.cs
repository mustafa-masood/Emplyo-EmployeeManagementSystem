using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Manager
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
