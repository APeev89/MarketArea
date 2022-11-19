using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MarketArea.Contracts
{
    public interface IFavoriteService
    {
        string Add(string id, IdentityUser user);
        FavouritesViewModel MyFavorite(IdentityUser user);
        bool Delete(string id, IdentityUser user);
    }
}
