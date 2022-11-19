using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Controllers
{
    public class FavoriteController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IRepository repo;
        private readonly IFavoriteService favoriteService;

        public FavoriteController(UserManager<IdentityUser> _userManager, IRepository _repo, IFavoriteService _favoriteService)
        {
            userManager = _userManager;
            repo = _repo;
            favoriteService = _favoriteService;
        }



        public IActionResult MyFavorite()
        {
            var user = userManager.GetUserAsync(User).Result;
            FavouritesViewModel favouritesViewModel = favoriteService.MyFavorite(user);

            return View("Views/Ad/All.cshtml", favouritesViewModel);

        }

        [HttpPost]
        public IActionResult AddToFavorite(string id)
        {
            var user = userManager.GetUserAsync(User).Result;
            var message = favoriteService.Add(id, user);

            return Json(new { result = message });
        }

        public IActionResult RemoveFromFavorite(string id)
        {
            
            var user = userManager.GetUserAsync(User).Result;

            var isRemoved = favoriteService.Delete(id, user);
            if (!isRemoved)
            {
                throw new Exception("Cannot be removed");
            }

            return Json(new { result = "success"});
        }
    }
}
