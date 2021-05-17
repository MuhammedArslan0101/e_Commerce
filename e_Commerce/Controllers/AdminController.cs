using e_Commerce.Entity;
using e_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.Controllers
{
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            StateModel model = new StateModel();
            model.ExpectedOrderCount = db.orders.Where(i => i.OrderState == OrderState.Expected).ToList().Count();
            model.CompletedOrderCount = db.orders.Where(i => i.OrderState == OrderState.Completed).ToList().Count();
            model.PackegedOrderCount = db.orders.Where(i => i.OrderState == OrderState.Packaged).ToList().Count();
            model.ShippedOrderCount = db.orders.Where(i => i.OrderState == OrderState.Shipped).ToList().Count();

            model.ProductCount = db.products.Count();
            model.OrdertCount = db.orders.Count();

            return View(model);
        }
    }
}