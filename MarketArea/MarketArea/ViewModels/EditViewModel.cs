using MarketArea.Data.ModelDb;

namespace MarketArea.ViewModels
{
    public class EditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string AdCity { get; set; }
        public string AdCategory { get; set; }
    }
}
