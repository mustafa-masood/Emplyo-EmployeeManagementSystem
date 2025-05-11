using Microsoft.AspNetCore.Mvc;

namespace Emplyo_EMS.Server.Controllers.Manager
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
