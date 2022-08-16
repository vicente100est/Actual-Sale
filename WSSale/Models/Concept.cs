using System;
using System.Collections.Generic;

#nullable disable

namespace WSSale.Models
{
    public partial class Concept
    {
        public long IdConcept { get; set; }
        public long IdSale { get; set; }
        public int AmountConcept { get; set; }
        public decimal UnitPriceConcept { get; set; }
        public decimal TotalProductsConcept { get; set; }
        public long IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual Sale IdSaleNavigation { get; set; }
    }
}
