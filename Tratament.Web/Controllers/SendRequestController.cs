using Microsoft.AspNetCore.Mvc;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {

        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }
    }
}
