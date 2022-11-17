namespace MarketArea.ViewModels
{
    public class FavouritesViewModel
    {
        public FavouritesViewModel()
        {
            this.FavouriteAds = new List<FavouriteAdViewModel>();
        }

        public ICollection<FavouriteAdViewModel> FavouriteAds { get; set; }

        public string CurrentCategory { get; set; }
    }
}
