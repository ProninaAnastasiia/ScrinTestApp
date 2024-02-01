using System;
using System.Collections.Generic;

namespace ScrinTestApp.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
