using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace e_Commerce.Entity
{
    public class DataContext:DbContext
    {
        //To give dataContext permison to add initializer to database
        public DataContext() :base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderLine> orderLines { get; set; }


    }
}