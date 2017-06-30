using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Economy.Models.ViewModels
{
    public class EditBillViewModel
    {
        public int BillId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        //[RegularExpression(@"^-?[0-9]\d*(\.\d+)?$", ErrorMessage = "Ogiltigt belopp")]
        //[Range((double)decimal.MinValue, (double)decimal.MaxValue, ErrorMessage = "UnitPrice must be a valid positive currency")]
        public decimal? Amount { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedSubCategoryId { get; set; }
        public int SelectedPayerId { get; set; }
        public string Description { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Payer> Payers { get; set; }

        public IEnumerable<string> Decsriptions { get; set; }
    }
}