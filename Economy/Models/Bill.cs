namespace Economy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bills")]
    public partial class Bill
    {
        [Key]
        public int BillID { get; set; }
                      
        public DateTime DueDate { get; set; }

        [Column(TypeName = "money")]        
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int PayerID { get; set; }

        public int SubCategoryID { get; set; }

        public DateTime RegDate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [ForeignKey("PayerID")]
        public virtual Payer Payer { get; set; }

        [ForeignKey("SubCategoryID")]
        public virtual SubCategory SubCategory { get; set; }
    }
}
