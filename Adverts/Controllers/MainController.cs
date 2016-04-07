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

        public ActionResult Delivery()
        {
            ViewBag.RegionList = entityModels.region.getList();
            ViewBag.CategoryList = entityModels.category.getList();
            ViewBag.ParamNameList = infoModels.param_name.getList();
            return View();
        }
    }
}