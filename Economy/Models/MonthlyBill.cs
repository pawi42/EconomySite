using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Economy.Models
{
    [Table("MonthlyBills")]
    public class MonthlyBill
    {
        [Key]
        public int MonthlyBillID { get; set; }

        //public DateTime DueDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int PayerID { get; set; }

        public int SubCategoryID { get; set; }

        //public DateTime RegDate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [ForeignKey("PayerID")]
        public virtual Payer Payer { get; set; }

        [ForeignKey("SubCategoryID")]
        public virtual SubCategory SubCategory { get; set; }
    }
}