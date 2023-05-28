using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Controllers
{
    public class CommentController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICommentService commentService;
        public CommentController(UserManager<IdentityUser> _userManager, ICommentService _commentService)
        {
            userManager = _userManager;
            commentService = _commentService;
        }

        [HttpPost]
        public IActionResult Add(string id, string text)
        {
            var user = userManager.GetUserAsync(User).Result;
            commentService.Add(id, text, user);

            return Json(new { result = "success" });
        }

        public IActionResult Remove(Dictionary<string,string> ids)
        {
            var adId = ids.Values.First();
            var commnetId = ids.Values.Last();
            commentService.Remove(commnetId);

             return Redirect($"/Ad/Details/{adId}");
        }

    }
}
