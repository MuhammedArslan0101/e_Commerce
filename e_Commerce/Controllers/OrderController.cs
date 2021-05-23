using e_Commerce.Entity;
using e_Commerce.Identity;
using e_Commerce.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.Controllers
{
   
    public class OrderController : Controller
    {
       
        private UserManager<ApplicationUser> UserManager;
        DataContext db = new DataContext();
        public OrderController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult ExpectedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Expected).ToList();
            return View(model);
                
        }
        [Authorize(Roles = "admin")]
        public ActionResult CompletedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Completed).ToList();
            return View(model);

        }
        [Authorize(Roles = "admin")]
        public ActionResult PackagedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Packaged).ToList();
            return View(model);

        }
        [Authorize(Roles = "admin")]
        public ActionResult ShippedOrders()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Shipped).ToList();
            return View(model);

        }
        public ActionResult PaymentSuccess()
        {
            return View();

        }

      

        [Authorize]
        //[HttpPost]
        public ActionResult pay(int OrderId)
        {

            var order = db.orders.Where(i => i.Id == OrderId).Select(i => new OrderDetails()
            {
                // Order Details
                OrderId = OrderId,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                UserName=i.UserName,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Adres = i.Address,
                City = i.City,
                District = i.District,
                Neghborhood = i.Neghborhood,
                PostaCode = i.PostCode,
                //Product Details
                OrderLines = i.OrdeLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.Image,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()


            }).FirstOrDefault();// Tek bir sipariş gelecektir
            var user = UserManager.Users.SingleOrDefault(i => i.UserName == order.UserName);
           
            if (order != null)
            {

                Options options = new Options();
                options.ApiKey = "sandbox-44hnQUuXX9etK58J2ljXPbzrJnCS8hEm";
                options.SecretKey = "sandbox-YEZaTATPWJoEG6SpxdvEs9RiUz6b4vtx";
                options.BaseUrl = "https://sandbox-api.iyzipay.com";

                CreatePaymentRequest request = new CreatePaymentRequest();
                request.Locale = Locale.TR.ToString();
                // request.ConversationId = "123456789";
                request.Price = order.Total.ToString();
                request.PaidPrice = order.Total.ToString();
                request.Currency = Currency.TRY.ToString();
                request.Installment = 1;
                request.BasketId = order.OrderNumber.ToString();
                request.PaymentChannel = PaymentChannel.WEB.ToString();
                request.PaymentGroup = PaymentGroup.PRODUCT.ToString();


                PaymentCard paymentCard = new PaymentCard();
                paymentCard.CardHolderName = "Muhammed ARSLAN";
                paymentCard.CardNumber = "5528790000000008";
                paymentCard.ExpireMonth = "12";
                paymentCard.ExpireYear = "2030";
                paymentCard.Cvc = "123";
                paymentCard.RegisterCard = 0;
                request.PaymentCard = paymentCard;


                Buyer buyer = new Buyer();
                buyer.Id = user.Id.ToString();
                buyer.Name = user.Name.ToString();
                buyer.Surname = user.Surname.ToString();
                buyer.GsmNumber = "05316955558";
                buyer.Email = user.Email.ToString();
                buyer.IdentityNumber = user.Id.ToString();
                //buyer.LastLoginDate = "2015-10-05 12:43:35";
                //buyer.RegistrationDate = "2013-04-21 15:12:09";
                buyer.RegistrationAddress = order.Adres.ToString();
                buyer.Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                buyer.City = order.City.ToString();
                buyer.Country = order.City.ToString();
                buyer.ZipCode = order.PostaCode.ToString();
                request.Buyer = buyer;

                Address shippingAddress = new Address();
                shippingAddress.ContactName = order.UserName.ToString();
                shippingAddress.City = order.City.ToString();
                shippingAddress.Country = order.City.ToString();
                shippingAddress.Description = order.Adres.ToString();
                shippingAddress.ZipCode = order.PostaCode.ToString();
                request.ShippingAddress = shippingAddress;

                Address billingAddress = new Address();
                billingAddress.ContactName = order.UserName.ToString();
                billingAddress.City = order.City.ToString();
                billingAddress.Country = order.City.ToString();
                billingAddress.Description = order.Adres.ToString();
                billingAddress.ZipCode = order.PostaCode.ToString();
                request.BillingAddress = billingAddress;

                var orderline = db.orderLines.Where(i => i.OrderId == OrderId).ToList();
                List<BasketItem> basketItems = new List<BasketItem>();
                foreach (var item in orderline)
                {
                    BasketItem firstBasketItem = new BasketItem();
                    firstBasketItem.Id = item.ProductId.ToString();
                    firstBasketItem.Name = item.Product.Name;
                    firstBasketItem.Category1 = item.Product.CategoryId.ToString();
                    // firstBasketItem.Category2 = "Accessories";
                    firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                    firstBasketItem.Price = item.Price.ToString();
                    basketItems.Add(firstBasketItem);
                }



                request.BasketItems = basketItems;

                Payment payment = Payment.Create(request, options);


            }
            return RedirectToAction("PaymentSuccess");

        }
    }
}