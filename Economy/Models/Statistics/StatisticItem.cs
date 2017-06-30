using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Economy.Models.Statistics
{
    public class StatisticItem
    {
        public int Year { get; set; }
        public ICollection<Month> Months { get; set; }
    }

    public class Month
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public decimal Sum { get; set; }
    }
}