using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
