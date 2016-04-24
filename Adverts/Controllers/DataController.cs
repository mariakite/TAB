using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adverts.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult getParams(int category_id)
        {
            ViewData["result"] = JSONHelper.toJSON(infoModels.param_name.getList(category_id));
            return PartialView("partials/result");
        }

        public ActionResult getParamValues(int param_id)
        {
            ViewData["result"] = JSONHelper.toJSON(infoModels.param_value.getList(param_id));
            return PartialView("partials/result");
        }

        public ActionResult saveDeliveryParam(string email, int region_id, int category_id, string param_ids, string value_ids)
        {
            ViewData["result"] = entityModels.delivery.saveDelivery(email, region_id, category_id, param_ids, value_ids);
            return PartialView("partials/result");
        }
    }
}