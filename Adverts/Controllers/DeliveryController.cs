using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: Delivery
        public ActionResult Index()
        {
            ViewBag.RegionList = entityModels.region.getList();
            ViewBag.CategoryList = entityModels.category.getListToPeople();
            ViewBag.ParamNameList = infoModels.param_name.getList();

            usersModels.user currentUser = new usersModels.user();
            if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
            {
                currentUser = usersModels.user.getUser(Convert.ToString(HttpContext.Session["hash_key"]));
            }
            return View(model: currentUser);
        }
    }
}