using Economy.Business;
using Economy.Configurations;
using Economy.Models.JsonModels;
using Economy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Economy.Controllers
{
    [RoutePrefix("statistik")]
    public class StatisticsController : Controller
    {
        // GET: Invoice
        [Route]
        public ActionResult Index()
        {
            var model = new StatisticsViewModel();

            model.Categories = EconomyBusiness.GetAllCategories();
            model.SubCategories = EconomyBusiness.GetAllSubCategories();
            model.Descriptions = EconomyBusiness.GetAllDescriptions();
            
            model.Years = StatisticsBusiness.GetYears();

            int selectedYear = -AppSettings.GetAppSettingsInteger("YearsBack");//DateTime.Now.AddYears(-AppSettings.GetAppSettingsInteger("YearsBack")).Year;
            model.SelectedYear = DateTime.Now.AddYears(selectedYear).Year;

            model.ChartSrc = StatisticsBusiness.GetChart(new FilterJson { CategoryId = 0, SubCategoryId = 0,
                                                                          Description = string.Empty,
                                                                          FromYear = model.SelectedYear
            });
            return View(model);
        }
    }
}