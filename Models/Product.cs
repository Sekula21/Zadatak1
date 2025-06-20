﻿using System.ComponentModel.DataAnnotations;

namespace Zadatak1.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Producer { get; set; }
        public string Adress { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
    }
}
