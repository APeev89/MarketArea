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

        public FavouritesViewModel all(string category, IdentityUser user)
        {
            IEnumerable<Ad> ads;
            string currentCategory;

            if (category == null)
            {
                ads = repo.All<Ad>().Include(a => a.City).OrderBy(x => x.DateFrom);
                currentCategory = "All ads";
            }
            else
            {
                ads = repo.All<Ad>().Include(a => a.City).Where(x => x.Category.Name == category).OrderBy(x => x.DateFrom);
                currentCategory = category;
            }
            
            var userFavourtes = repo.All<UserFavorite>().Where(u => u.UserId == user.Id).ToDictionary(u => u.AdId);
            FavouritesViewModel favouritesViewModel = new FavouritesViewModel()
            {
                CurrentCategory = currentCategory
            };


            foreach (var ad in ads)
            {
                FavouriteAdViewModel favouriteAdViewModel = new FavouriteAdViewModel()
                {
                    Ad = ad,
                    IsChecked = userFavourtes.ContainsKey(ad.Id)
                };
                favouritesViewModel.FavouriteAds.Add(favouriteAdViewModel);
            }

            return favouritesViewModel;
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

        public bool Delete(string id)
        {
            var ad = repo.GetById<Ad>(id);
            if (ad == null)
            {
                return false;
            }
               

            var getUserFavorite = repo.All<UserFavorite>().FirstOrDefault(uf=>uf.AdId == ad.Id);
            if (getUserFavorite != null)
            {
                repo.Delete(getUserFavorite);
            }

            var getUserLies = repo.All<UserLikes>().FirstOrDefault(uf => uf.AdId == ad.Id);
            if (getUserLies != null)
            {

                repo.Delete(getUserLies);
            }

            var getUserSeens = repo.All<UserSeens>().FirstOrDefault(uf => uf.AdId == ad.Id);
            if (getUserSeens != null)
            {
                repo.Delete(getUserSeens);
            }

            var getComment = repo.All<Comment>().FirstOrDefault(uf => uf.AdId == ad.Id);
            if (getComment != null)
            {
                repo.Delete(getComment);
            }
            try
            {
                repo.Delete(ad);
                repo.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Could not delete product");
            }
            
            return true; 
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
