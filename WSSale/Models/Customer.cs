using System;
using System.Collections.Generic;

#nullable disable

namespace WSSale.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }

        public int IdCustomer { get; set; }
        public string NameCustomer { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
