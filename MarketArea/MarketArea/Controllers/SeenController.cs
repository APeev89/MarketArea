using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Controllers
{
    public class SeenController : BaseController
    {

        public SeenController()
        {

        }


        [HttpPost]
        public IActionResult Seen(string id)
        {
            return Json(new { result = "success" });
        }
    }
}
