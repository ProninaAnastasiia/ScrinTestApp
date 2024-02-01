using System;
using System.Collections.Generic;

namespace ScrinTestApp.Data.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
