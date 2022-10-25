﻿using MarketArea.Contracts;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using MarketArea.Data.Common;

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

        [HttpPost]
        public IActionResult Create(CreateAdViewModel model)
        {
            var user = userManager.GetUserAsync(User).Result;
            var (created, error) = adService.Create(model,user);
            if (!created)
            {
            }

            return Redirect("/");
        }


    }
}
