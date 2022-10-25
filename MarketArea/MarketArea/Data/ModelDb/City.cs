using System.ComponentModel.DataAnnotations;

namespace MarketArea.Data.ModelDb
{
    public class City
    {
        public City()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Ads = new HashSet<Ad>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Ad> Ads { get; set; }

    }
}
