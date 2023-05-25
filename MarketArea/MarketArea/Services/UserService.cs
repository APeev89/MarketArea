using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketArea.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;
        public UserService(IRepository _repo )
        {
            repo = _repo;
        }

        public async Task<IdentityUser> GetUserById(string id)
        {
           return repo.GetById<IdentityUser>(id);
           
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user =  repo.GetById<IdentityUser>(id);
            return new UserEditViewModel()
            {
                Id = user.Id,
                Name = user.UserName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {


            return repo.All<IdentityUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.UserName,
                })
                .ToList();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = repo.GetById<IdentityUser>(model.Id);
            if (user != null)
            {
                user.UserName = model.Name;

                repo.SaveChanges();
                result = true;
            }
            return result;

        }
    }
}
