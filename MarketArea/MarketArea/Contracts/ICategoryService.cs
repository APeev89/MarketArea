using MarketArea.ViewModels;

namespace MarketArea.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListViewModel>> GetCategorys();
        Task<CategoryEditViewModel> GetCategoryForEdit(string id);
        Task<bool> UpdateCategory(CategoryEditViewModel model);
        Task<bool> DeleteCategory(string id);
        Task CreateCategory(CategoryAddViewModel model);
    }
}
