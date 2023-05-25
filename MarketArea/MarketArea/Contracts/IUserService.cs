using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketArea.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<UserEditViewModel> GetUserForEdit(string id);
        Task<bool> UpdateUser(UserEditViewModel model);
        Task<IdentityUser> GetUserById(string id);
    }
}
