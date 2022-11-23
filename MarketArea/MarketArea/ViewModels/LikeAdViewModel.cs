using MarketArea.Data.ModelDb;

namespace MarketArea.ViewModels
{
    public class LikeAdViewModel : SeenAdViewModel
    {
        public Ad Ad { get; set; }
        public int NumberOfLikes { get; set; }
    }
}
