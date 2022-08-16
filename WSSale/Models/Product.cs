using System;
using System.Collections.Generic;

#nullable disable

namespace WSSale.Models
{
    public partial class Product
    {
        public Product()
        {
            Concepts = new HashSet<Concept>();
        }

        public long IdProduct { get; set; }
        public string NameProduct { get; set; }
        public decimal UnitPriceProduct { get; set; }
        public decimal CostProduct { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
