using MarketArea.Data.ModelDb;

namespace MarketArea.ViewModels
{
    public class LikeAdViewModel : SeenAdViewModel
    {

        public LikeAdViewModel()
        {
            Comments = new List<Comment>();
        }
        public Ad Ad { get; set; }
        public int NumberOfLikes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
