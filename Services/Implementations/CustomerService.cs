using System;
using System.Collections.Generic;
using System.Linq;
using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService
{
    private readonly EcommerceContext _context;

    public CustomerService(EcommerceContext context)
    {
        _context = context;
    }

    public Order CreateOrder(int userId)
    {
        var order = new Order
        {
            UserId = userId,
            Date = DateTime.Now,
            OrderProducts = new List<OrderProduct>()
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
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
