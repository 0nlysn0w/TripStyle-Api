using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class TripStyleContext : DbContext
    {
        public TripStyleContext() { }
        public TripStyleContext(DbContextOptions<TripStyleContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseLine> PurchaseLines { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=145.24.222.139,8080;" +
                                           "Database=TripStyleProduction;Persist Security Info=True;" +
                                           "User ID=sa; Password=Tripstyle2018");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product and Purchase many to many
            modelBuilder.Entity<PurchaseLine>(entity =>
            {
                // Foreign keys of PurchaseLine
                entity
                    .HasKey(pl => new
                    {
                        pl.PurchaseId,
                        pl.ProductId
                    });

                entity
                    .HasOne(pl => pl.Purchase)
                    .WithMany(pu => pu.PurchaseLines)
                    .HasForeignKey(pl => pl.PurchaseId);

                entity
                    .HasOne(pl => pl.Product)
                    .WithMany(li => li.PurchaseLines)
                    .HasForeignKey(pl => pl.ProductId);
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity
                    .HasKey(f => new
                    {
                        f.ProductId,
                        f.UserId
                    });

                entity
                    .HasOne(f => f.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(f => f.ProductId);

                entity
                    .HasOne(f => f.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(f => f.UserId);
            });



        }
    }
}