using JobPortal.Model;
using Microsoft.AspNet.Identity;
using MyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PayPal.Api;
using MyWeb.App_Start;
using Repository.DbConnection;
using System.Data.SqlClient;
using System.ServiceModel;

namespace MyWeb.Controllers
{
    public class OrderController : Controller
    {
        private User u;
        private OfferReference.IOfferService _offerProxy;
        private UserReference1.IUserService _userProxy;
        private OrderReference.IOrderService _orderProxy;
        private ShoppingCard shoppingCard;

        public OrderController()
        {
            _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
            _userProxy = new UserReference1.UserServiceClient("UserServiceHttpEndpoint1");
            _orderProxy = new OrderReference.OrderServiceClient("OrderServiceHttpEndpoint");


        }
        // GET: Order
        public ActionResult Index(string id, string error)
        {
            if (shoppingCard == null)
            {
                var shoppingcard = _orderProxy.GetShoppingCard(id);
                ShoppingCardView scv = new ShoppingCardView { Card = shoppingcard, Error = error };
                return View(scv);
            }
            else
            {
                return null;
            }
        }

        public ActionResult CreateOrder(string userID)
        {
            if (userID != null)
            {
                try
                {
                    _orderProxy.CreateOrder(userID);
                    CleanCart(userID);
                    return RedirectToAction("Index", "Order", new { id = userID.Trim() });
                }catch(FaultException e)
                {
                   
                    return RedirectToAction("Index", "Order", new { id = userID.Trim(), error = e.Reason.ToString() });
                }
                
            }
            return null;
        }


        public ActionResult AddToCart(string userID, int serviceID, DateTime date, TimeSpan from, TimeSpan to)
        {

            if (userID != null && serviceID > 0)
            {
                try
                {
                    var result = _orderProxy.AddToCart(userID, serviceID, date, from, to);
                    if (result)
                    {
                        return RedirectToAction("Index", "Order", new { id = userID.Trim() });
                    }
                    else
                    {
                        return View("Error", null);
                    }
                }
                catch (InvalidOperationException)
                {
                    return View("Error", null);
                }
            }
            return View("Error", null);
        }
        public ActionResult DeleteFromCard(string idU, int? id, DateTime? date, TimeSpan? from, TimeSpan? to)
        {
            var result = _orderProxy.DeleteFromCart(idU, (int)id, (DateTime)date, (TimeSpan)from, (TimeSpan)to);
            if (result)
            {
                return RedirectToAction("Index", "Order", new { id = idU.Trim() });
            }
            return null;
        }


        public ActionResult CleanCart(string id)
        {
            if (_orderProxy.CleanCart(id))
            {
                return RedirectToAction("Index", "Order", new { id = id.Trim() });
            }
            else
            {
                return null;
            }

        }



        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Order/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var shoppingCard = _orderProxy.GetShoppingCardForPaypal(User.Identity.GetUserId());
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            var lista = shoppingCard.PayPalList;
            //Adding Item Details like name, currency, price etc  
            foreach (var i in lista)
            {
                itemList.items.Add(new Item()
                {
                    name = i.Title,
                    currency = "DKK",
                    price = decimal.Round(i.RatePerHour, 0, MidpointRounding.AwayFromZero).ToString(),
                    quantity = decimal.Round((i.HoursTo - i.HoursFrom).Hours, 0, MidpointRounding.AwayFromZero).ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = decimal.Round(shoppingCard.GetTotalPricePayPal(), 0, MidpointRounding.AwayFromZero).ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "DKK",
                total = decimal.Round(shoppingCard.GetTotalPricePayPal(), 0, MidpointRounding.AwayFromZero).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = shoppingCard.RandomString(),
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}