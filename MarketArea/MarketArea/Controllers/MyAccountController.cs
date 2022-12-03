using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Controllers
{
    public class MyAccountController : BaseController
    {
        private readonly IRepository repo;
        private readonly UserManager<IdentityUser> userManager;

        public MyAccountController(IRepository _repo, UserManager<IdentityUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        [Authorize]
        public IActionResult MyAdsList()
        {
            IEnumerable<Ad>? myAdsList = null;
            var user = userManager.GetUserAsync(User).Result;
            var myAds = repo.All<Ad>().Where(x => x.UserId == user.Id)
                                      .Include(a => a.City)
                                      .OrderBy(x => x.DateFrom);

            if (myAds.Count() != 0)
            {
                myAdsList = myAds;
            }

            return View(new MyAdsViewModel
            {
                MyAdsList = myAdsList,
            });
        }
    }
}
