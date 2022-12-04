using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Controllers
{
    public class ContactController : BaseController
    {
        public IActionResult Connection()
        {
            return View();
        }
    }
}
