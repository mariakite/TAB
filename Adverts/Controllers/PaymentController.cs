using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index(int count = 0, string adverts = "", int category_id = 0, int count_day = 0)
        {
            string sum = "";
            string hash_key_order = identity.getHashKey();
            
            //формируем заказ
            if (count >0 && adverts != String.Empty)
            {
                ViewBag.type = "объявления";
                ViewBag.url = "http://tabavi.ru/adverts/?payment=true";
                sum = count.ToString();
                int res = paymentModels.order.createOrder(hash_key_order, count, sum, adverts);
            }
            else if (category_id >0 && count_day > 0 && Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                switch (count_day)
                {
                    case 3:
                        sum = constant.str.delivery_3.ToString();
                        Console.WriteLine("Case 1");
                        break;
                    case 7:
                        sum = constant.str.delivery_7.ToString();
                        break;
                    case 15:
                        sum = constant.str.delivery_15.ToString();
                        break;
                    case 30:
                        sum = constant.str.delivery_30.ToString();
                        break;

                }
                ViewBag.type = "рассылки";
                ViewBag.url = "http://tabavi.ru/account/delivery/";
                int user_id = usersModels.user.getIdFromHash(Convert.ToString(HttpContext.Session["hash_key"]));
                int region_id = 0;
                int res = paymentModels.order.createOrder(hash_key_order, count, sum, region_id, category_id, user_id);
            }
            ViewBag.sum = sum;
            ViewBag.hash_key_order = hash_key_order;

            HttpContext.Session["hash_key_order"] = HttpContext.Session["hash_key_order"] != null ? HttpContext.Session["hash_key_order"].ToString() + "," + hash_key_order : hash_key_order;
             

            return View();
        }

        public ActionResult yaPayment(string withdraw_amount = "0", string operation_id="0", string label="")
        {
            ViewData["result"] = paymentModels.order.updateOrder(label,operation_id, withdraw_amount);
            return PartialView("partials/result");
        }
    }
}