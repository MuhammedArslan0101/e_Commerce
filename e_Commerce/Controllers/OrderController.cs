using e_Commerce.Entity;
using e_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();

        // GET: Order
        public ActionResult Index()
        {
            // its not nessecery to use filter i want to list every order to admin ( approved and not approved orders)
            var orders = db.orders.Select(i => new AdminOrder()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                Count = i.OrdeLines.Count()
            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var model = db.orders.Where(i => i.Id == id).Select(i => new OrderDetails()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                UserName = i.UserName,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Adres = i.Address,
                City = i.City,
                District = i.District,
                Neghborhood = i.Neghborhood,
                PostaCode = i.PostCode,
                OrderLines = i.OrdeLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.Image,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            }).FirstOrDefault();
            return View(model);
        }
        public ActionResult UpdateOrderState(int OrderId, OrderState Orderstate)
        {
            var order = db.orders.FirstOrDefault(i => i.Id == OrderId);
            if (order != null)
            {
                order.OrderState = Orderstate;
                db.SaveChanges();
                TempData["mesaj"] = "info saved";
                return RedirectToAction("Details", new { id = OrderId });
            }
            return RedirectToAction("Index");
        }

        public ActionResult ExpectedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Expected).ToList();
            return View(model);
                
        }
        public ActionResult CompletedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Completed).ToList();
            return View(model);

        }
        public ActionResult PackagedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Packaged).ToList();
            return View(model);

        }

        public ActionResult ShippedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Shipped).ToList();
            return View(model);

        }
    }
}