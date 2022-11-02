using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Services
{
    public class AdService : IAdService
    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;
        public AdService(IRepository _repo, IValidationService _validationService)
        {
            repo = _repo;
            validationService = _validationService;
        }
        public (bool create, string error) Create(CreateAdViewModel model, IdentityUser user)
        {
            bool created = false;
            string error = string.Empty;

            var(isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            var city = repo.All<City>().FirstOrDefault(x => x.Name == model.AdCity);
            var category = repo.All<Category>().FirstOrDefault(x => x.Name == model.AdCategory);

            if (category is null || city is null)
            {
                return (created, "Can not be found city or category!");
            }
            var ad = new Ad()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                PhoneNumber = model.PhoneNumber,
                Email = user.Email,
                UserId = user.Id,
                CityId = city.Id,
                CategoryId = category.Id,
                Description = model.Description,
            };

            try
            {
                repo.Add(ad);
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
                error = "Could not save product";
            }

            return (created, error);
        }

        public Ad Details(string id)
        {
            return repo.All<Ad>().Include(x => x.City).Include(x => x.Category).Single(x => x.Id == id);

        }

        public bool Edit(EditViewModel model)
        {
            bool created = false;

            var ad = repo.GetById<Ad>(model.Id);
            var category = repo.All<Category>().FirstOrDefault(x=>x.Name == model.AdCategory);
            var city = repo.All<City>().FirstOrDefault(x=>x.Name == model.AdCity);

            if (category is null || city is null)
            {
                return created;
            }
            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return isValid;
            }
            try
            {
                ad.Name = model.Name;
                ad.ImageUrl = model.ImageUrl;
                ad.Price = model.Price;
                ad.PhoneNumber = model.PhoneNumber;
                ad.Description = model.Description;
                ad.Category = category;
                ad.City = city;
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
            }

            return created;

        }

        
    }
}
