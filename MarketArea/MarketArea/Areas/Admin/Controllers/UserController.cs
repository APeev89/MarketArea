using MarketArea.Contracts;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager, IUserService _userService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<ActionResult> ManageUsers()
        {
            var user = await userService.GetUsers();

            return View(user);
        }


        public async Task<ActionResult> Roles(string id)
        {
            
            return Ok(id);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await userService.GetUserForEdit(id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string id, UserEditViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return View(model);
            }

            await userService.UpdateUser(model);
            var user = await userService.GetUserForEdit(id);
            return View(user);
        }

        public async Task<ActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}
