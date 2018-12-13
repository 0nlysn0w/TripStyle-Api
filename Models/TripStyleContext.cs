using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class TripStyleContext : DbContext
    {
        public TripStyleContext() {}
        public TripStyleContext(DbContextOptions<TripStyleContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseLine> PurchaseLines { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             //optionsBuilder.UseSqlServer("Data Source=145.24.222.139,8080;" +
                                            // "Database=TripStyleDB;Persist Security Info=True;" +
                                            // "User ID=sa; Password=Tripstyle2018");
             optionsBuilder.UseSqlite("Data Source=../TripStyle.Api/tripstyle.db");
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(a => new { a.AddressId });

            // User has one basket
            // modelBuilder.Entity<User>()
            //     .HasOne(u => u.Basket)
            //     .WithOne(b => b.User)
            //     .HasForeignKey(u => u.BasketId);

            // modelBuilder.Entity<Basket>()
            //     .HasOne(b => b.User)
            //     .WithOne(u => u.Basket)
            //     .HasForeignKey<User>(u => u.BasketId);

            // Basket and products 
            // modelBuilder.Entity<BasketProduct>()
            //     .HasKey(bp => new
            //     {
            //         bp.BasketId,
            //         bp.ProductId
            //     });

            // modelBuilder.Entity<BasketProduct>()
            //     .HasOne(bp => bp.Basket)
            //     .WithMany(b => b.BasketProducts)
            //     .HasForeignKey(bp => bp.BasketId);

            // modelBuilder.Entity<BasketProduct>()
            //     .HasOne(bp => bp.Product)
            //     .WithMany(p => p.BasketProducts)
            //     .HasForeignKey(bp => bp.ProductId);

            // Product has many images
            // modelBuilder.Entity<Product>()
            //     .HasMany(p => p.Images)
            //     .WithOne(i => i.Product);

            // // Purchase has one address
            // modelBuilder.Entity<Purchase>()
            //     .HasOne(p => p.DeliveryAddress)
            //     .WithMany(a => a.Purchases);

            // // Product has one category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // // User has many addresses
            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.Addresses)
            //     .WithOne(a => a.User);

            // // Address belongs to one user, optional
            // modelBuilder.Entity<Address>()
            //     .HasOne(a => a.User)
            //     .WithMany(u => u.Addresses)
            //     .HasForeignKey(a => a.UserId);

            // User can have many purchases
            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.Purchases)
            //     .WithOne(p => p.User);
                

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



        }
    }
}