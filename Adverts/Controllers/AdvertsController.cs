using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class AdvertsController : Controller
    {
        // GET: Adverts
        public ActionResult Index(int region_id = 78, int category_id = 8, string param_ids = "1,2,3,4,5,6,7,8,9,10", string value_ids = "", bool free = false, bool payment = false)
        {
            ViewBag.RegionList = entityModels.region.getList();
            ViewBag.CategoryList = entityModels.category.getListToPeople();
            ViewBag.category_id = category_id;
            ViewBag.region_id = region_id;
            ViewBag.Free = free;
            ViewBag.Payment = payment;
            IList<entityModels.advert> advertList = new List<entityModels.advert>();
            if (payment)
            {
                string advs = entityModels.advert.getPaymentAdvertIds();
                if (advs.Length < 5)
                {
                    System.Threading.Thread.Sleep(2000);
                    advs = entityModels.advert.getPaymentAdvertIds();
                }
                advertList = entityModels.advert.getList(advs);
            }
            else
            {
                advertList = entityModels.advert.getList(region_id, category_id, param_ids, value_ids, free);
            }
            return View(model: advertList);
        }
    }
}