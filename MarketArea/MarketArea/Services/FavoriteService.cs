using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepository repo;

        public FavoriteService(IRepository _repo)
        {
            repo = _repo;
        }

        public FavouritesViewModel MyFavorite(IdentityUser user)
        {
            var ads = repo.All<Ad>()
                            .Include(a => a.City)
                            .OrderBy(x => x.DateFrom);

            var userFavourtes = repo.All<UserFavorite>().Where(u => u.UserId == user.Id).ToDictionary(u => u.AdId);

            FavouritesViewModel favouritesViewModel = new FavouritesViewModel()
            {
                CurrentCategory = "All ads"
            };

            foreach (var ad in ads)
            {
                FavouriteAdViewModel favouriteAdViewModel = new FavouriteAdViewModel()
                {
                    Ad = ad,
                    IsChecked = userFavourtes.ContainsKey(ad.Id)
                };
                if (favouriteAdViewModel.IsChecked)
                {
                    favouritesViewModel.FavouriteAds.Add(favouriteAdViewModel);
                }
            }

            return favouritesViewModel;
        }


        public string Add(string id, IdentityUser user)
        {
            var message = "Cannot be Added";
            try
            {
                repo.Add<UserFavorite>(new UserFavorite
                {
                    AdId = id,
                    UserId = user.Id
                });

                repo.SaveChanges();
                message = "success";
            }
            catch (Exception)
            {
                throw new Exception(message);
            }

            return message;
        }
        public bool Delete(string id, IdentityUser user)
        {
            bool removed = false;
            var favoriteAd = repo.All<UserFavorite>().Where(uf => uf.UserId == user.Id && uf.AdId == id).FirstOrDefault();
            if (favoriteAd == null)
            {
                return removed;
            }
            try
            {
                repo.Delete<UserFavorite>(favoriteAd);
                repo.SaveChanges();
                removed = true;
            }catch (Exception)
            {  
            }
            

            return removed;
        }

    }
}
