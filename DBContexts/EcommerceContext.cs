using ecommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerceAPI.DBContexts
{
    public class EcommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(r => r.Role);

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Name = "admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Role = "Administrator",
                    Id = 1,
                }
                );

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Order)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(j => j.ToTable("OrderProduct"));

        }
    }
}
