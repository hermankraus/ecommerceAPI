using System;
using System.Collections.Generic;
using System.Linq;
using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;
using ecommerceAPI.Enums;
using ecommerceAPI.Models;
using ecommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        
        Order newOrder = new Order
        {
            UserId = userId,
            TotalPrice = TotalPrice(products),
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

    
    public Order GetOrderByOrderId(int orderId)
    {

        var order = _context.Orders.FirstOrDefault(u => u.Id == orderId);
        
        if (order != null)
        {
            return order;
        }

        return null;
        
    }

    private double TotalPrice(List<ProductOrderDTO> products)
    {
        double totalPrice = 0;

        foreach (var productDTO in products)
        {
            Product product = _context.Products.Find(productDTO.ProductId);

            if (product != null && product.Stock)
            {
                totalPrice += product.Price * productDTO.Quantity;
            }
        }

        return totalPrice;
    }

    public List<ShowProductsOrderDTO> GetOrderHistory(int userId)
    {
        var orderHistory = _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .OrderByDescending(o => o.Date)
            .ToList();

        List<ShowProductsOrderDTO> orderDTOs = new List<ShowProductsOrderDTO>();

        foreach (var order in orderHistory)
        {
            ShowProductsOrderDTO orderDTO = new ShowProductsOrderDTO
            {
                Id = order.Id,
                Date = order.Date,
                StatusOrder = order.StatusOrder,
                Products = order.OrderProducts.Select(op => new ProductOrderDTO
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                }).ToList(),
                TotalPrice= order.TotalPrice,
            };

            orderDTOs.Add(orderDTO);
        }

        return orderDTOs;
    }

    public void CancelOrder(Order canceledOrder)
    {
        _context.Update(canceledOrder);
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

}
