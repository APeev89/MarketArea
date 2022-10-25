using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;

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

            var category = new Category()
            {
                Name = model.Category
            };

            var city = new City()
            {
                Name = model.City
            };


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
                repo.Add(category);
                repo.Add(city);
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
    }
}
