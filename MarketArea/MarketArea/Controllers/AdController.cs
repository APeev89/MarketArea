using MarketArea.Contracts;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using MarketArea.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Controllers
{
    public class AdController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAdService adService;
        private readonly IRepository repo;
        public AdController(UserManager<IdentityUser> _userManager, IAdService _adService, IRepository _repo)
        {
            userManager = _userManager;
            adService = _adService;
            repo = _repo;
        }

        public IActionResult Create()
        {
            List<Category> categories = repo.All<Category>().OrderBy(x=>x.Name).ToList();
            List<City> cities = repo.All<City>().OrderBy(x => x.Name).ToList();

            dynamic myDynamicModel = new System.Dynamic.ExpandoObject();
            myDynamicModel.Category = categories;
            myDynamicModel.City = cities;
            return View(myDynamicModel);
        }

        public IActionResult All(string category)
        {
            IEnumerable<Ad> ads;
            string currentCategory;
            if (category == null)
            {
                ads = repo.All<Ad>().Include(a=>a.City).OrderBy(x => x.DateFrom);
                currentCategory = "All ads";
            }
            else
            {
                ads = repo.All<Ad>().Include(a => a.City).Where(x => x.Category.Name == category).OrderBy(x => x.DateFrom);
                currentCategory = category;
            }

            return View(new AllAdViewModel
            {
                Ads = ads,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Detail(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAdViewModel model)
        {
            var user = userManager.GetUserAsync(User).Result;
            var (created, error) = adService.Create(model,user);
            if (!created)
            {
                return Redirect("/Ad/Create");
            }

            return Redirect("/");
        }

        

    }
}
