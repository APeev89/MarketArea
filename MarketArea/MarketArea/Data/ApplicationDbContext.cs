using MarketArea.Data.ModelDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketArea.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<UserLikes> UserLikes { get; set; }
        public DbSet<UserSeens> UserSeens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ad>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Ad>()
                .HasOne(a =>a.Category)
                .WithMany(c=>c.Ads)
                .HasForeignKey(a =>a.CategoryId);

            modelBuilder.Entity<Ad>()
                .HasOne(a => a.City)
                .WithMany(c => c.Ads)
                .HasForeignKey(a => a.CityId);

            modelBuilder.Entity<UserLikes>()
                .HasOne(ul => ul.Ad)
                .WithMany(a => a.Likes)
                .HasForeignKey(ul => ul.AdId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLikes>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserSeens>()
               .HasOne(us => us.Ad)
               .WithMany(a => a.Seens)
               .HasForeignKey(us => us.AdId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserSeens>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.Ad)
                .WithMany(a => a.UserFavorites)
                .HasForeignKey(uf => uf.AdId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFavorite>()
                .HasOne(uf => uf.User)
                .WithMany()
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFavorite>()
              .HasKey(uf => new { uf.UserId, uf.AdId });

            modelBuilder.Entity<UserLikes>()
               .HasKey(ua => new { ua.UserId, ua.AdId });

            modelBuilder.Entity<UserSeens>()
               .HasKey(at => new { at.AdId, at.UserId });

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Ad)
                .WithMany(a=> a.Comments)
                .HasForeignKey(c => c.AdId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}