using MarketArea.Contracts;
using MarketArea.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        public async Task<IActionResult> ManageCategorys()
        {
            var category = await categoryService.GetCategorys();

            return View(category);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            await categoryService.CreateCategory(model);
            return Redirect("/Admin/Category/ManageCategorys");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var category = await categoryService.GetCategoryForEdit(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.UpdateCategory(model);
            return Redirect("/");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var removed = await categoryService.DeleteCategory(id);
            if (!removed)
            {
                return Redirect($"/Admin/Category/ManageCategorys/");
            }
            return Redirect("/");
        }
    }
}
