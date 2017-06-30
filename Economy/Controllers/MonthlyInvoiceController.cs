using Economy.Business;
using Economy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Economy.Controllers
{
    [RoutePrefix("manadsbetalningar")]
    public class MonthlyInvoiceController : Controller
    {
        [Route]
        public ActionResult Index()
        {            
            var model = EconomyBusiness.GetMonthlyBills();
            ViewBag.RunButtonDisabled = EconomyBusiness.IsMonthlyPaymentsExecuted(DateTime.Now);

            return View(model);
        }

        [Route("skapa")]
        public ActionResult Create()
        {
            ViewBag.Title = "Skapa";

            var model = new EditBillViewModel();
           

            model.Categories = EconomyBusiness.GetAllCategories();
            model.SubCategories = EconomyBusiness.GetAllSubCategories();
            model.Payers = EconomyBusiness.GetAllPayers();

            return View("Edit", model);
        }

        [Route("redigera/{id:int}")]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Redigera";

            var model = new EditBillViewModel();
            var bill = EconomyBusiness.GetMonthlyBillById(id);
            model.BillId = bill.MonthlyBillID;
            model.Amount = bill.Amount;
            model.SelectedCategoryId = bill.CategoryID;
            model.SelectedSubCategoryId = bill.SubCategoryID;
            model.SelectedPayerId = bill.PayerID;
            model.Description = bill.Description;

            model.Categories = EconomyBusiness.GetAllCategories();
            model.SubCategories = EconomyBusiness.GetAllSubCategories();
            model.Payers = EconomyBusiness.GetAllPayers();

            return View("Edit", model);
        }
    }
}