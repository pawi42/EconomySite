using Economy.Business;
using Economy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Economy.Controllers
{
    [RoutePrefix("faktura")]
    //[Route("{action = index}”)]
    public class InvoiceController : Controller
    {
        // GET: Invoice
        [Authorize]
        [Route("{id:int}")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = "Redigera";

            var model = new EditBillViewModel();
            var bill = EconomyBusiness.GetBillById(id);
            if (bill == null)
                return HttpNotFound("Finns ingen faktura med id: " + id.ToString());

            model.BillId = bill.BillID;
            model.DueDate = bill.DueDate;
            model.Amount = Math.Round(bill.Amount, 2);
            model.SelectedCategoryId = bill.CategoryID;
            model.SelectedSubCategoryId = bill.SubCategoryID;
            model.SelectedPayerId = bill.PayerID;
            model.Description = bill.Description;

            model.Categories = EconomyBusiness.GetAllCategories();
            model.SubCategories = EconomyBusiness.GetAllSubCategories();
            model.Payers = EconomyBusiness.GetAllPayers();

            return View("Index", model);
        }

        [Authorize]
        [Route("skapa")]
        //[ActionName("Skapa")]
        public ActionResult Create()
        {
            ViewBag.Title = "Skapa";

            var model = new EditBillViewModel();
            model.DueDate = DateTime.Now;

            model.Categories = EconomyBusiness.GetAllCategories();
            model.SubCategories = EconomyBusiness.GetAllSubCategories();
            model.Payers = EconomyBusiness.GetAllPayers();

            return View("Index", model);
        }

        //public ActionResult Edit(int id)
        //{
        //    ViewBag.Title = "Redigera";

        //    var model = new EditBillViewModel();
        //    var bill = EconomyBusiness.GetBillById(id);
        //    model.BillId = bill.BillID;
        //    model.DueDate = bill.DueDate;
        //    model.Amount = Math.Round(bill.Amount, 2);
        //    model.SelectedCategoryId = bill.CategoryID;
        //    model.SelectedSubCategoryId = bill.SubCategoryID;
        //    model.SelectedPayerId = bill.PayerID;
        //    model.Description = bill.Description;

        //    model.Categories = EconomyBusiness.GetAllCategories();
        //    model.SubCategories = EconomyBusiness.GetAllSubCategories();
        //    model.Payers = EconomyBusiness.GetAllPayers();

        //    return View("Index", model);
        //}

        [Authorize]
        [Route("kopiera/{id:int}")]
        public ActionResult Copy(int id)
        {
            var model = EconomyBusiness.CopyBill(id);
            if (model != null)
            {
                return RedirectToAction("Index", "Invoice", new
                {
                    id = model.BillID
                });
            }
            return Create();
        }

        [Authorize]
        public ActionResult Detail()
        {
            ViewBag.Message = "Detail";

            return View();
        }
    }
}