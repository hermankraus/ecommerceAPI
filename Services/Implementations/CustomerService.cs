using System;
using System.Collections.Generic;
using System.Linq;
using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using ecommerceAPI.Enums;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService, IUserService
{
    private readonly EcommerceContext _context;
    
    public CustomerService(EcommerceContext context)
    {
        _context = context;
        
        
    }

    public void CreateOrder(int userId, List<ProductOrderDTO> products)
    {
        // Crear una nueva orden
        Order newOrder = new Order
        {
            UserId = userId,
            TotalPrice = 0,//CalculateTotalPrice(products)
            StatusOrder = OrderStatus.Waiting, 
            Date = DateTime.Now.ToUniversalTime(),
            OrderProducts = new List<OrderProduct>()
        };


        foreach (var productDTO in products)
        {
            Product product = _context.Products.Find(productDTO.ProductId);

            if (product != null && product.Stock)
            {
                OrderProduct orderProduct = new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = productDTO.Quantity,
                    Product = product
                };

                newOrder.OrderProducts.Add(orderProduct);

            }
        }

        _context.Orders.Add(newOrder);
        _context.SaveChanges();
    }

    public void AddProductsToOrder(int orderId, List<Product> products, List<int> quantities)
    {
        if (products == null || quantities == null || products.Count != quantities.Count)
        {
            throw new ArgumentException("La lista de productos y cantidades no coincide.");
        }

        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            throw new ArgumentException($"No se encontró la orden con ID {orderId}.");
        }

        for (int i = 0; i < products.Count; i++)
        {
            var orderProduct = new OrderProduct
            {
                Product = products[i],
                Quantity = quantities[i]
            };
            order.OrderProducts.Add(orderProduct);
        }

        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void UpdateUser(User userToUpdate)
    {
       
        _context.Update(userToUpdate);
        _context.SaveChanges();

    }

    public void DeleteUser(int userId)
    {
        User userToDelete = _context.Users.FirstOrDefault(u => u.Id == userId);
        userToDelete.State = false;
        _context.Update(userToDelete);
        _context.SaveChanges();

    }

    public User GetUser(int userId)
    {

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return null;
        }
        return(user);
    }

    public List<Order> GetOrderHistory(int userId)
    {
        var orders = _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToList();

        return orders;
    }
}
