using System;

namespace Economy.Models.JsonModels
{
    public class FilterJson
    {        
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }        
        public int FromYear { get; set; }
    }
}