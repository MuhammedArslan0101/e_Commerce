using e_Commerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class Cart
    {
        private List<Cartline> _cartLines = new List<Cartline>();
        public List<Cartline> CartLines
        {
            get { return _cartLines; }
        }
        public void AddProduct(Product product, int quantity)
        {
            var line = _cartLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line == null)
            {
                _cartLines.Add(new Cartline() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void DeleteProduct(Product product)
        {
            _cartLines.RemoveAll(i => i.Product.Id == product.Id);
        }
        public double Total()
        {
            return _cartLines.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            _cartLines.Clear();
        }
    }
    public class Cartline
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
