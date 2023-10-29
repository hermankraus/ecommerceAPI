using ecommerceAPI.Entities;
using ecommerceAPI.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ecommerceAPI.DBContexts
{
    public class EcommerceContext : DbContext { 
    public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);

        modelBuilder.Entity<OrderProduct>()
            .Property(op => op.Quantity)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>("Customer")
            .HasValue<Admin>("Admin");

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Order)
            .HasForeignKey(o => o.UserId);
    }
}

}
