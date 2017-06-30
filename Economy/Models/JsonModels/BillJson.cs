using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Economy.Models.JsonModels
{
    public class BillJson
    {
        public int BillId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int PayerId { get; set; }
    }
}