using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Controllers
{
    public class LikeController : BaseController
    {
        private readonly IRepository repo;
        private readonly UserManager<IdentityUser> userManager;
        public LikeController(IRepository _repo, UserManager<IdentityUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public IActionResult LikeDislike(string id)
        {


            return Json(new { result = "success" });
        }

       
    }
}
