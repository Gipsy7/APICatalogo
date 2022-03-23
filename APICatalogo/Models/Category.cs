﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace APICatalogo.Models
{
    public class Category
    {
        public Category()
        {
            Products = new Collection<Product>();
        }
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}