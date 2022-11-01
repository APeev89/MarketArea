using MarketArea.Data.Common;
using MarketArea.Data.ModelDb;
using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Components
{
    public class Categories : ViewComponent
    {
        private readonly IRepository repo;

        public Categories(IRepository _repo)
        {
            repo = _repo;
        }
        public IViewComponentResult Invoke()
        {
            var categories = repo.All<Category>().OrderBy(x => x.Name);
            return View(categories);
        }
    }
}
    

