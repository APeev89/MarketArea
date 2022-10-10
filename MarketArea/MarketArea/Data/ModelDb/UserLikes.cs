using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketArea.Data.ModelDb
{
    public class UserLikes
    {
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string AdId { get; set; }

        public Ad Ad { get; set; }
    }
}
