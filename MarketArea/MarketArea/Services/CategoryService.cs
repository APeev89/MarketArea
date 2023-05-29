using MarketArea.Contracts;
using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using MarketArea.ViewModels;

namespace MarketArea.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IRepository repo;
        private readonly IAdService adService;

        public CategoryService(IRepository _repo, IAdService _adService)
        {
            repo = _repo;
            adService = _adService;
        }

        public async Task CreateCategory(CategoryAddViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
            };
            repo.Add(category);
            repo.SaveChanges();

        }

        public async Task<bool> DeleteCategory(string id)
        {
            bool result = false;
            var category = repo.GetById<Category>(id);
            if (category != null)
            {
                var ListOfAds = repo.All<Ad>().Where(c => c.CategoryId == category.Id).ToList();

                if (ListOfAds.Count > 0)
                {
                    foreach (var ad in ListOfAds)
                    {
                        adService.Delete(ad.Id);
                    }
                }

                repo.Delete(category);
                repo.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<CategoryEditViewModel> GetCategoryForEdit(string id)
        {
            var category = repo.GetById<Category>(id);
            return new CategoryEditViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<IEnumerable<CategoryListViewModel>> GetCategorys()
        {
            return repo.All<Category>().Select(c => new CategoryListViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        public async Task<bool> UpdateCategory(CategoryEditViewModel model)
        {
            bool result = false;
            var category = repo.GetById<Category>(model.Id);
            if (category != null)
            {
                category.Name = model.Name;

                repo.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
