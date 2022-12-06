﻿using MarketArea.Contracts;
using MarketArea.Data.Common;
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

        public IActionResult AllComments(string id)
        {
            return Json(new { result = "success" });
        }
    }
}
