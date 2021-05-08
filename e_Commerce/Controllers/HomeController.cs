﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Commerce.Entity;

namespace e_Commerce.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        public PartialViewResult  _FeaturedProductList()
        {
            return PartialView(db.products.Where(i => i.IsApproved && i.IsFeatured).Take(5).ToList());

        }
        // GET: Home
        public ActionResult Index()
        {
            return View(db.products.Where(i=> i.IsHome && i.IsApproved).ToList());
        }

        public ActionResult Product()
        {
            return View(db.products.ToList());
        }
        public ActionResult ProductDetails(int id)
        {
            return View(db.products.Where(i => i.Id == id).FirstOrDefault());
        }

        public ActionResult ProductList(int id)
        {
            return View(db.products.Where(i=>i.CategoryId == id).ToList());
        }
    }
}