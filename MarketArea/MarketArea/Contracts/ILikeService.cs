using Microsoft.AspNetCore.Identity;

namespace MarketArea.Contracts
{
    public interface ILikeService
    {
        void LikeDislike(string id, IdentityUser user);
    }
}
