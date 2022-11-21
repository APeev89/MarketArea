using MarketArea.Contracts;
using MarketArea.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace MarketArea.Services
{
    public class LikeService : ILikeService
    {
        private readonly IRepository repo;
        public LikeService(IRepository _repo)
        {
            repo = _repo;
        }

        public void LikeDislike(string id, IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
