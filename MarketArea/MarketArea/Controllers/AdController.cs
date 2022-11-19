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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var categories = repo.All<Category>().OrderBy(x => x.Name);
            var cities = repo.All<City>().OrderBy(x => x.Name);

            List<SelectListItem> itemsCategories = DropDownCategoriesMenu(categories);
            List<SelectListItem> itemsCities = DropDownCitiesMenu(cities);

            ViewBag.AdCategory = itemsCategories;
            ViewBag.AdCity = itemsCities;
            return View();
        }

        public IActionResult All(string category)
        {
            var user = userManager.GetUserAsync(User).Result;
            FavouritesViewModel favouritesViewModel = adService.all(category, user);

            return View(favouritesViewModel);
        }

        public IActionResult Details(string id)
        {
            var ad = adService.Details(id);
            return View(ad);
        }

        public IActionResult Edit(string id)
        {
            var ad = repo.All<Ad>().Include(c => c.Category).Include(c => c.City).FirstOrDefault(x => x.Id == id);
            if (ad == null)
            {
                return NotFound();
            }
            var categories = repo.All<Category>().OrderBy(x => x.Name);
            var cities = repo.All<City>().OrderBy(x => x.Name);

            List<SelectListItem> itemsCategories = DropDownCategoriesMenu(categories);
            itemsCategories.Single(x => x.Text == ad.Category.Name).Selected = true;

            List<SelectListItem> itemsCities = DropDownCitiesMenu(cities);
            itemsCities.Single(x => x.Text == ad.City.Name).Selected = true; ;

            ViewBag.AdCategory = itemsCategories;
            ViewBag.AdCity = itemsCities;
            


            return View(new EditViewModel()
            {
                Id = ad.Id,
                Name = ad.Name,
                ImageUrl = ad.ImageUrl,
                PhoneNumber = ad.PhoneNumber,
                Price = ad.Price,
                Description = ad.Description,

            });
        }

       

        [HttpPost]
        public IActionResult Create(CreateAdViewModel model)
        {
            var user = userManager.GetUserAsync(User).Result;
            var (created, error) = adService.Create(model, user);
            if (!created)
            {
                return Redirect("/Ad/Create");
            }

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            adService.Edit(model);
            
            return Redirect($"/Ad/Details/{model.Id}");

        }

        public IActionResult Delete(string id)
        {
            var removed = adService.Delete(id);
            if (!removed)
            {
                return Redirect($"/Ad/Details/{id}");
            }
            return Redirect("/");
        }


        private static List<SelectListItem> DropDownCategoriesMenu(IOrderedQueryable<Category> categories)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem { Text = category.Name });
            }

            return items;
        }
        private static List<SelectListItem> DropDownCitiesMenu(IOrderedQueryable<City> cities)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var city in cities)
            {
                items.Add(new SelectListItem { Text = city.Name });
            }

            return items;
        }

    }
}
