using Economy.DataAccess;
using Economy.Models;
using Economy.Models.JsonModels;
using Economy.Models.Utils;
using Economy.Models.ViewModels;
using Economy.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Economy.Business
{
    public class EconomyBusiness
    {
        public static BillListViewModel GetBills(int page, int pageSize)
        {
            int totalBills = 0;
            var model = new BillListViewModel();

            page = page - 1;
            if (page <= 0)
                page = 0;
            model.Bills = EconomyDataService.GetBills(page, pageSize, out totalBills);
            model.Paging = new Paging(totalBills, page+1, pageSize);
            

            return model;
        }

        public static IEnumerable<Bill> GetBills(FilterJson filter)
        {
            return EconomyDataService.GetBills(filter);
        }

        public static Bill GetBillById(int billId)
        {
            return EconomyDataService.GetBillById(billId);
        }

        public static IEnumerable<MonthlyBill> GetMonthlyBills()
        {
            return EconomyDataService.GetMonthlyBills();
        }

        public static MonthlyBill GetMonthlyBillById(int billId)
        {
            return EconomyDataService.GetMonthlyBillById(billId);
        }

        public static IEnumerable<Category> GetAllCategories()
        {
            return EconomyDataService.GetAllCategories();
        }

        public static IEnumerable<SubCategory> GetAllSubCategories()
        {
            return EconomyDataService.GetAllSubCategories();
        }
        public static IEnumerable<SubCategory> GetSubCategoriesByCategory(int categoryId)
        {
            return EconomyDataService.GetAllSubCategoriesByCategory(categoryId);
        }

        public static IEnumerable<Payer> GetAllPayers()
        {
            return EconomyDataService.GetAllPayers();
        }

        private static Bill AddBill(BillJson billJson)
        {           
            var bill = CreateBill(billJson);
           
            if (EconomyDataService.AddBill(bill))
                return bill;
            return null;
        }

        private static MonthlyBill AddMonthlyBill(BillJson billJson)
        {
            var bill = CreateMonthlyBill(billJson);

            if (EconomyDataService.AddMonthlyBill(bill))
                return bill;
            return null;
        }

        public static Bill SaveBill(BillJson billJson)
        {
            if (billJson.CategoryId <= 0 || billJson.SubCategoryId <= 0)
                return null;

            if (billJson.BillId <= 0)
                return AddBill(billJson);

            var bill = GetBillById(billJson.BillId);
            bill.Amount = billJson.Amount;
            bill.Category = null;
            bill.CategoryID = billJson.CategoryId;
            bill.SubCategory = null;
            bill.SubCategoryID = billJson.SubCategoryId;
            bill.Description = billJson.Description;
            bill.Payer = null;
            bill.PayerID = billJson.PayerId;
            bill.DueDate = billJson.DueDate;
            bill.RegDate = DateTime.Now;

            if (EconomyDataService.SaveBill(bill))
                return bill;
            return null;
        }
        public static Bill CopyBill(int id)
        {
            var bill = GetBillById(id);
            bill.BillID = 0;
            bill.Category = null;
            bill.SubCategory = null;
            bill.Payer = null;
            if (EconomyDataService.AddBill(bill))
                return bill;
            return null;
        }

        public static MonthlyBill SaveMonthlyBill(BillJson billJson)
        {
            if (billJson.Amount <= 0 || billJson.CategoryId <= 0 || billJson.SubCategoryId <= 0)
                return null;

            if (billJson.BillId <= 0)
                return AddMonthlyBill(billJson);

            var bill = GetMonthlyBillById(billJson.BillId);
            bill.Amount = billJson.Amount;
            bill.Category = null;
            bill.CategoryID = billJson.CategoryId;
            bill.SubCategory = null;
            bill.SubCategoryID = billJson.SubCategoryId;
            bill.Description = billJson.Description;
            bill.Payer = null;
            bill.PayerID = billJson.PayerId;
           
            if (EconomyDataService.SaveMonthlyBill(bill))
                return bill;
            return null;
        }

        public static IEnumerable<string> GetAllDescriptions()
        {
            return EconomyDataService.GetAllDescriptions();
        }

        public static bool RunMonthlyPayments(DateTime date)
        {
            bool success = true;
            bool added = false;

            //Get last day in month
            DateTime dueDate = DateTimeHelper.GetLastDateInMonth(date);
            // Check if the month already is executed


            var monthlyBills = GetMonthlyBills();
            foreach (var b in monthlyBills)
            {
                added = EconomyDataService.AddBill(CreateBill(b, dueDate));
                if (!added)
                    success = false;
            }

            return success;
        }

        public static bool IsMonthlyPaymentsExecuted(DateTime date)
        {
            DateTime start = DateTimeHelper.GetFirstDateInMonth(date);
            DateTime end = DateTimeHelper.GetLastDateInMonth(date).AddDays(1);
            return EconomyDataService.IsMonthlyPaymentsExecuted(start, end);
        }

        private static Bill CreateBill(BillJson billJson)
        {
            var bill = new Bill();
            bill.Amount = billJson.Amount;
            bill.CategoryID = billJson.CategoryId;
            bill.SubCategoryID = billJson.SubCategoryId;
            bill.Description = billJson.Description;
            bill.PayerID = billJson.PayerId;
            bill.DueDate = billJson.DueDate;
            bill.RegDate = DateTime.Now;

            return bill;
        }
        private static Bill CreateBill(MonthlyBill monthlyBill, DateTime dueDate)
        {
            var bill = new Bill();
            bill.Amount = monthlyBill.Amount;
            bill.CategoryID = monthlyBill.CategoryID;
            bill.SubCategoryID = monthlyBill.SubCategoryID;
            bill.Description = monthlyBill.Description;
            bill.PayerID = monthlyBill.PayerID;
            bill.DueDate = dueDate;
            bill.RegDate = DateTime.Now;

            return bill;
        }
        
        private static MonthlyBill CreateMonthlyBill(BillJson billJson)
        {
            var bill = new MonthlyBill();
            bill.Amount = billJson.Amount;
            bill.CategoryID = billJson.CategoryId;
            bill.SubCategoryID = billJson.SubCategoryId;
            bill.Description = billJson.Description;
            bill.PayerID = billJson.PayerId;

            return bill;
        }
    }
}