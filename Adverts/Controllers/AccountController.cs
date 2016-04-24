using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            int result = usersModels.user.authorization(email, password);
            if (result == (int)constant.status.ok)
            {
                return Redirect(Url.Action("Delivery", "Account"));
            }
            else
            {
                ViewBag.result = result;
                return View();
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            int result = usersModels.user.setPassword(email);
            ViewBag.result = result;
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session["hash_key"] = "";
            HttpContext.Session["is_admin"] = false;
            HttpContext.Session["is_auth"] = false;
            return Redirect("/");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(string email, string password)
        {
            int result = usersModels.user.saveUser(email, password);
            if (result == (int)constant.status_registrate.success)
            {
                return Redirect(Url.Action("Delivery", "Account"));
            }
            else
            {
                ViewBag.result = result;
                return View();
            }
        }

        public ActionResult Settings()
        {
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                usersModels.user currentUser = usersModels.user.getUser(Convert.ToString(HttpContext.Session["hash_key"]));
                return View(model: currentUser);
            }
            else
            {
                return Redirect(Url.Action("Login","Account"));
            }
        }

        [HttpPost]
        public ActionResult Settings(string email)
        {
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                ViewData["result"] = usersModels.user.updateEmail(Convert.ToString(HttpContext.Session["hash_key"]),email);
            }
            else
            {
                ViewData["result"] = (int)constant.status_registrate.error;
            }
            
            return PartialView("partials/result");
        }

        [HttpPost]
        public ActionResult UpdatePassword(string old_password, string new_password)
        {
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                ViewData["result"] = usersModels.user.updatePassword(Convert.ToString(HttpContext.Session["hash_key"]), old_password, new_password);
            }
            else
            {
                ViewData["result"] = (int)constant.status_registrate.error;
            }

            return PartialView("partials/result");
        }

        public ActionResult Delivery()
        {

            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                int user_id = usersModels.user.getIdFromHash(Convert.ToString(HttpContext.Session["hash_key"]));
                IList<entityModels.delivery> deliveryList = entityModels.delivery.getList(user_id);
                ViewBag.deliveryAccess = paymentModels.delivery_access.getActualAccessForUser(user_id);
                return View(model: deliveryList);
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        [HttpPost]
        public ActionResult DeleteDelivery(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                ViewData["result"] = entityModels.delivery.deleteDelivery(Convert.ToString(HttpContext.Session["hash_key"]), id);
                return PartialView("partials/result");
            }
            else
            {
                ViewData["result"] = (int)constant.status.error;
                return PartialView("partials/result");
            }
        }

        [HttpPost]
        public ActionResult SetStatusDelivery(int id, int status)
        {
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                ViewData["result"] = entityModels.delivery.updateStatusDelivery(Convert.ToString(HttpContext.Session["hash_key"]), id, status);
                return PartialView("partials/result");
            }
            else
            {
                ViewData["result"] = (int)constant.status.error;
                return PartialView("partials/result");
            }
        }
    }
}   