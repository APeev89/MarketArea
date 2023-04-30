using MarketArea.ViewModels;

namespace MarketArea.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<UserEditViewModel> GetUserForEdit(string id);
        Task<bool> UpdateUser(UserEditViewModel model);
    }
}
