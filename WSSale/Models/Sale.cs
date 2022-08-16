using System;
using System.Collections.Generic;

#nullable disable

namespace WSSale.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Concepts = new HashSet<Concept>();
        }

        public long IdSale { get; set; }
        public DateTime DateSale { get; set; }
        public decimal? TotalSale { get; set; }
        public int IdCustomer { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
