using Economy.Business;
using Economy.Models.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Economy.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax

        [HttpPost]
        public JsonResult SaveBill(BillJson billJson)
        {
            var model = EconomyBusiness.SaveBill(billJson);
            return Json(model);
        }

        [HttpPost]
        public JsonResult SaveMonthlyBill(BillJson billJson)
        {
            var model = EconomyBusiness.SaveMonthlyBill(billJson);
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetSubCategories(CategoryJson categoryJson)
        {
            var model = EconomyBusiness.GetSubCategoriesByCategory(categoryJson.CategoryId).ToList();

            var list = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return Json(list, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteDescription()
        {
            var model = EconomyBusiness.GetAllDescriptions().ToList();

            return Json(model, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RunMonthlyPayments(DateTime date)
        {
            var model = EconomyBusiness.RunMonthlyPayments(date);
            return Json(model);
        }

        [HttpPost]
        public JsonResult IsMonthlyPaymentsExecuted(DateTime date)
        {
            var model = EconomyBusiness.IsMonthlyPaymentsExecuted(date);
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetChart(FilterJson filterJson)
        {   
            var chartImageSrc = StatisticsBusiness.GetChart(filterJson);            
            return Json(chartImageSrc);
        }
    }
}