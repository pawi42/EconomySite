using Economy.Models.Utils;
using System.Collections.Generic;

namespace Economy.Models.ViewModels
{
    public class BillListViewModel
    {
        public IEnumerable<Bill> Bills { get; set; }

        public Paging Paging { get; set; }
    }
}