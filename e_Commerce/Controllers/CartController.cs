using e_Commerce.Entity;
using e_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.Controllers
{
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        private void SaveOrder(Cart cart, ShippingDetails model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;

            order.Address = model.Address;
            order.City = model.City;
            order.District = model.District;
            order.Neghborhood = model.Neghborhood;
            order.PostCode = model.PostaCode;
            order.OrdeLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.Id;
                order.OrdeLines.Add(orderline);
            }
            db.orders.Add(order);
            db.SaveChanges();
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails model)
        {
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoProductYet", "There are no products in your cart..");
            }
            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);
                cart.Clear();
                return View("OrderCompleted");
            }
            else
            {
                return View(model);
            }

        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        public PartialViewResult Summary1()
        {
            return PartialView(GetCart());
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.products.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.products.FirstOrDefault(i => i.Id == Id);
            //Add product to Cart from database 
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }


            return RedirectToAction("Index");

        }
        // Create cart For User for first Time 
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                // Add new cart To session
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}