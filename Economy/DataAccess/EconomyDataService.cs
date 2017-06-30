using Economy.Models;
using Economy.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace Economy.DataAccess
{
    public class EconomyDataService
    {
        public static IEnumerable<Bill> GetBills(int page, int pageSize, out int total)
        {
            var bills = new List<Bill>();
            using (var ctx = new EconomyContext())
            {
                //DateTime date = DateTime.Now.AddMonths(-26);
                var list = ctx.Bills
                         .Include("Category")
                         .Include("SubCategory")
                         .Include("Payer")
                         //.Where(b => EntityFunctions.CreateDateTime(b.dueDate.Year, b.dueDate.Month, b.dueDate.Day, 0,0,0) > DateTime.Now.AddMonths(4))
                         //.Where(b => b.DueDate >= fromDate)
                         //.Where(b => b.CategoryID.Equals(1))
                         .OrderByDescending(b => b.DueDate).ThenByDescending(b => b.RegDate);

                total = list.Count();
                bills = list.Skip(page * pageSize)
                         .Take(pageSize).ToList();
            }
            return bills;
        }

        public static IEnumerable<Bill> GetBills(FilterJson filter)
        {
            //var bills = new List<Bill>()
            using (var ctx = new EconomyContext())
            {
                DateTime fromDate = new DateTime(filter.FromYear, 1, 1);
                //if (filter.CategoryId.Equals(0) && filter.SubCategoryId.Equals(0) && filter.Description.Equals("0"))
                //{
                var bills = ctx.Bills
                        .Include("Category")
                        .Include("SubCategory")
                        .Include("Payer")
                        .AsQueryable();
                //.Where(b => EntityFunctions.CreateDateTime(b.dueDate.Year, b.dueDate.Month, b.dueDate.Day, 0,0,0) > DateTime.Now.AddMonths(4))
                //.Where(b => b.CategoryID.Equals(1))

                //}
                bills = bills.Where(b => b.DueDate >= fromDate);

                if (filter.CategoryId > 0)
                    bills = bills.Where(b => b.CategoryID.Equals(filter.CategoryId));

                if (filter.SubCategoryId > 0)
                    bills = bills.Where(b => b.SubCategoryID.Equals(filter.SubCategoryId));

                if (!string.IsNullOrEmpty(filter.Description) && filter.Description != "0")
                    bills = bills.Where(b => b.Description.Equals(filter.Description));

                return bills.OrderByDescending(b => b.DueDate).ThenByDescending(b => b.RegDate).ToList();
            }
        }

        /// <summary>
        /// Based on if one type of payment already is registrated
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool IsMonthlyPaymentsExecuted(DateTime start, DateTime end)
        {
            using (var ctx = new EconomyContext())
            {

                var bill = ctx.Bills
                        .Include("Category")
                        .Include("SubCategory")
                        .Include("Payer")
                        //.Where(b => EntityFunctions.CreateDateTime(b.DueDate.Year, b.DueDate.Month, b.DueDate.Day, 0,0,0) > DateTime.Now.AddMonths(4))
                        .Where(b => b.DueDate >= start && b.DueDate < end && b.CategoryID.Equals(1) && b.SubCategoryID.Equals(1))
                        .FirstOrDefault();
                return (bill != null && bill.BillID > 0);                        
            }
        }

        public static Bill GetBillById(int billId)
        {
            using (var ctx = new EconomyContext())
            {                
                return ctx.Bills
                        .Include("Category")
                        .Include("SubCategory")
                        .Include("Payer")
                        .Where(b => b.BillID.Equals(billId))
                        .SingleOrDefault();
            }
        }
        public static IEnumerable<MonthlyBill> GetMonthlyBills()
        {
            using (var ctx = new EconomyContext())
            { 
                return ctx.MonthlyBills
                        .Include("Category")
                        .Include("SubCategory")
                        .Include("Payer")                      
                        .ToList();
            }
        }

        public static MonthlyBill GetMonthlyBillById(int billId)
        {
            using (var ctx = new EconomyContext())
            {
                return ctx.MonthlyBills
                        .Include("Category")
                        .Include("SubCategory")
                        .Include("Payer")
                        .Where(b => b.MonthlyBillID.Equals(billId))
                        .SingleOrDefault();
            }
        }

        public static IEnumerable<Category> GetAllCategories()
        {
            using (var ctx = new EconomyContext())
            {
                DateTime date = DateTime.Now.AddMonths(-26);
                return ctx.Categories
                        .Include("CategorySubcategoryRelations")
                        .Include("CategorySubcategoryRelations.SubCategory")
                        .OrderBy(b => b.Name).ToList();
            }
        }

        public static IEnumerable<SubCategory> GetAllSubCategories()
        {
            using (var ctx = new EconomyContext())
            {               
                return ctx.SubCategories
                        .Include("CategorySubcategoryRelations")  
                        .OrderBy(b => b.Name).ToList();
            }
        }
        public static IEnumerable<SubCategory> GetAllSubCategoriesByCategory(int categoryId)
        {
            using (var ctx = new EconomyContext())
            {
                var category = ctx.CategorySubcategoryRelations
                        .Include("SubCategory").AsQueryable();
                //.Include("SubCategory.Bill")
                //.Include("SubCategory.CategorySubcategoryRelation")
                if (categoryId > 0)
                    category = category.Where(c => c.CategoryID.Equals(categoryId));

                var categories = category.ToList();
               // return null;
                //if (category != null)
                var list = categories.Select(x => x.SubCategory);
                return list;
                        
            }
        }

        public static IEnumerable<Payer> GetAllPayers()
        {
            using (var ctx = new EconomyContext())
            {
                DateTime date = DateTime.Now.AddMonths(-26);
                return ctx.Payers.ToList();
            }
        }

        public static bool AddBill(Bill bill)
        {
            using (var ctx = new EconomyContext())
            {
                try {
                    ctx.Bills.Add(bill);
                    ctx.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool AddMonthlyBill(MonthlyBill bill)
        {
            using (var ctx = new EconomyContext())
            {
                try
                {
                    ctx.MonthlyBills.Add(bill);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SaveBill(Bill bill)
        {
            using (var ctx = new EconomyContext())
            {
                try
                {
                    ctx.Bills.Attach(bill);
                    ctx.Entry(bill).State = System.Data.Entity.EntityState.Modified;                                     
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SaveMonthlyBill(MonthlyBill bill)
        {
            using (var ctx = new EconomyContext())
            {
                try
                {
                    ctx.MonthlyBills.Attach(bill);
                    ctx.Entry(bill).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static IEnumerable<string> GetAllDescriptions()
        {
            using (var ctx = new EconomyContext())
            {
                try
                {
                   var descriptions = ctx.Bills.Select(x => x.Description).Distinct().ToList();
                    return descriptions.Where(d => !String.IsNullOrWhiteSpace(d)).ToList();
                }
                catch (Exception ex)
                {
                    return new List<string>();
                }

            }
        }       
    }
}