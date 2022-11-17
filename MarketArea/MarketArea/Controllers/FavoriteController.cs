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

        public FavoriteController(UserManager<IdentityUser> _userManager, IRepository _repo)
        {
            userManager = _userManager;
            repo = _repo;
        }



       // public IActionResult MyFavorite()
        //{
        //    var user = userManager.GetUserAsync(User).Result;
        //    var ads = repo.All<Ad>()
        //        .Include(a => a.City)
        //        .OrderBy(x => x.DateFrom);

        //    var userFavoritesAds = repo.All<UserFavorite>().Include(x => x.Ad).Where(x => x.UserId == user.Id);



        //    AllAdViewModel allAdViewModel = new AllAdViewModel()
        //    {
        //        Ads = ads,
        //        CurrentCategory = "All ads"
        //    };

        //    return View("Views/Ad/All.cshtml", allAdViewModel);

        //}


        [HttpPost]
        public IActionResult AddToFavorite(string id, bool setFavourite)
        {
            var user = userManager.GetUserAsync(User).Result;
            repo.Add<UserFavorite>(new UserFavorite
            {
                AdId = id,
                UserId = user.Id
            });

            repo.SaveChanges();


            return Json(new {message="succes"});
        }

        //public IActionResult RemoveFromFavorite()
        //{

        //}
    }
}
