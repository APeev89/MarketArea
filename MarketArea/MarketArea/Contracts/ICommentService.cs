using Microsoft.AspNetCore.Identity;

namespace MarketArea.Contracts
{
    public interface ICommentService
    {
        void Add(string id, string text, IdentityUser user);
    }
}
