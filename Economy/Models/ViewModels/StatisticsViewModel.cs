using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Economy.Models.ViewModels
{
    public class StatisticsViewModel
    {
        //public Chart Chart { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<string> Descriptions { get; set; }
        public IEnumerable<int> Years { get; set; }
        public int SelectedYear { get; set; }

        public string ChartSrc { get; set; }
    }
}