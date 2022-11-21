using MarketArea.Contracts;
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
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILikeService likeService;
        public LikeController(UserManager<IdentityUser> _userManager, ILikeService _likeService)
        {
            userManager = _userManager;
            likeService = _likeService;
        }

        public IActionResult LikeDislike(string id)
        {
            var user = userManager.GetUserAsync(User).Result;
            likeService.LikeDislike(id, user);

            return Json(new { result = "success" });
        }
    }
}
