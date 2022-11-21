using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var ad = repo.All<Ad>().Include(c => c.City).First(a => a.Id == id);
            var adLikes = repo.All<UserLikes>().Where(a => a.AdId == ad.Id).ToDictionary(u => u.UserId);

            int numberOfLikes = 0;
            if (adLikes.Count() == 0 || !adLikes.ContainsKey(user.Id))
            {
                Like(user, ad);
            }
            else if (adLikes.ContainsKey(user.Id))
            {
                Dislike(user, ad);
            }
            numberOfLikes = repo.All<UserLikes>().Where(a => a.AdId == ad.Id).Count();
            LikeAdViewModel likeAd = new LikeAdViewModel()
            {
                Ad = ad,
                NumberOfLikes = numberOfLikes

            };
        }
        private void Dislike(IdentityUser user, Ad ad)
        {
            var findLike = repo.All<UserLikes>().Where(ul => ul.UserId == user.Id && ul.AdId == ad.Id).FirstOrDefault();
            if (findLike != null)
            {
                try
                {
                    repo.Delete<UserLikes>(findLike);
                    repo.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Cannot be dislike, maybe the ad or user id can't be found");
                }
            }
        }

        private void Like(IdentityUser user, Ad ad)
        {
            repo.Add<UserLikes>(new UserLikes
            {
                UserId = user.Id,
                AdId = ad.Id
            });
            repo.SaveChanges();
        }
    }
}
