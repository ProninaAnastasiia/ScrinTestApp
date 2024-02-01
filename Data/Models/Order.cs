using System;
using System.Collections.Generic;

namespace ScrinTestApp.Data.Models
{
    public partial class Order
    {
        public Order()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Date { get; set; } = null!;
        public int UserId { get; set; }
        public double Cost { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
