using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Commerce.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var categories = new List<Category>()
            {
                new Category() {Name="Camera" , Description="Categories Of Cameras"},
                new Category() {Name="Telephone" , Description="Categories Of Telephone"},
                new Category() {Name="Pc" , Description="Categories Of Pc's "},


            };
            foreach (var cat in categories)
            {
                context.categories.Add(cat);
            }
            context.SaveChanges();

            var prod = new List<Product>()
            {
                new Product() {Name="Canon" , Description="Categories Of Cameras" , Price=200 , Stock=100 , IsHome = true , IsFeatured=false , IsApproved=true , Slider=true , CategoryId=1 ,Image="1.jpg"},
                new Product() {Name="Asus" , Description="Categories Of Pc" , Price=2000 , Stock=10 , IsHome = true , IsFeatured=true , IsApproved=true , Slider=true , CategoryId=3 ,Image="2.jpg"},
                new Product() {Name="Iphone" , Description="Categories Of Telephone" , Price=7000 , Stock=5 , IsHome = false , IsFeatured=true , IsApproved=false , Slider=false , CategoryId=2 ,Image="3.jpg"},

            };
            foreach (var p in prod)
            {
                context.products.Add(p);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}