using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using Microsoft.AspNetCore.Identity;

namespace MarketArea.Services
{
    public class CommentService : ICommentService
    {

        private readonly IRepository repo;
        public CommentService(IRepository _repo)
        {
            repo = _repo;
        }

        public void Add(string id, string text, IdentityUser user)
        {
            var comment = new Comment()
            {
                Text = text,
                DateFrom = DateTime.Now,
                UserId = user.Id,
                User = user,
                AdId = id
            };
            try
            {
                repo.Add(comment);
                repo.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Cannot be saved comment!");
            }

        }

        public void Remove(string id)
        {
            var commnet = repo.GetById<Comment>(id);
            repo.Delete(commnet);
            repo.SaveChanges();
        }
    }
}
