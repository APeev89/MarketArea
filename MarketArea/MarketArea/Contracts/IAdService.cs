using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MarketArea.Contracts
{
    public interface IAdService
    {
        (bool create, string error) Create(CreateAdViewModel model, IdentityUser user);
    }
}
