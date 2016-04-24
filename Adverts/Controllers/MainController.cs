using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Delivery()
        //{
        //    ViewBag.RegionList = entityModels.region.getList();
        //    ViewBag.CategoryList = entityModels.category.getListToPeople();
        //    ViewBag.ParamNameList = infoModels.param_name.getList();

        //    usersModels.user currentUser = new usersModels.user();
        //    if (Convert.ToBoolean(HttpContext.Session["is_auth"]))
        //    {
        //        currentUser = usersModels.user.getUser(Convert.ToString(HttpContext.Session["hash_key"]));
        //    }
        //    return View(model: currentUser);
        //}

        //public ActionResult Adverts(int region_id = 78, int category_id = 8, string param_ids = "1,2,3,4,5,6,7,8,9,10", string value_ids = "", bool free = false)
        //{
        //    ViewBag.RegionList = entityModels.region.getList();
        //    ViewBag.CategoryList = entityModels.category.getListToPeople();
        //    ViewBag.category_id = category_id;
        //    ViewBag.Free = free;
        //    IList<entityModels.advert> advertList = entityModels.advert.getList(region_id, category_id, param_ids, value_ids, free);
        //    return View(model: advertList);
        //}

    }
}