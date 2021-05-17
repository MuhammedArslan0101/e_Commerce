using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class StateModel
    {
        public int ProductCount { get; set; }
        public int OrdertCount { get; set; }
        public int ExpectedOrderCount { get; set; }

        public int CompletedOrderCount { get; set; }

        public int PackegedOrderCount { get; set; }
        public int ShippedOrderCount { get; set; }




    }
}