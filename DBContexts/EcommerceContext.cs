using ecommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;


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
            .HasKey(op => new { op.OrderId, op.ProductId }); // agrega key para customer service

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
                .HasDiscriminator<string>("UserRole")
                .HasValue<Customer>("Customer")
                .HasValue<Admin>("Admin");
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {   Id = 1,
                    Name = "Ezequias",
                    Email = "ezequias@test.com",
                    Password = "Password"

                },
                 new Admin
                 {
                     Id=2,
                     Name = "Herman",
                     Email = "Herman@test.com",
                     Password = "Password"
                 });
               modelBuilder.Entity<Customer>()
               .HasData(
               new Customer
               {   
                   Id = 3,
                   Name = "Juan",
                   Email = "juan@test.com",
                   Password = "Password",
                   Address = "Fake Street 123"
               });

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Order)
            .HasForeignKey(o => o.UserId);


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test desc",
                    Price = 1.25
                }
                );
            
    }
}

}
