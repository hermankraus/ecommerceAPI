﻿using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();

        public OrderStatus StatusOrder { get; set; }


        public ICollection<AddProductToTableDTO> Products { get; set; } = new List<AddProductToTableDTO>(); 
    }
}
