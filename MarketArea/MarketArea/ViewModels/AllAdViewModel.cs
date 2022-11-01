using MarketArea.Data.ModelDb;

namespace MarketArea.ViewModels
{
    public class AllAdViewModel
    {
        public IEnumerable<Ad> Ads { get; set; }
        public string CurrentCategory { get; set; }
    }
}
