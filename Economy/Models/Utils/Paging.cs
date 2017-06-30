using System;

namespace Economy.Models.Utils
{
    public class Paging
    {
        public Paging(int totalItems, int? page, int pageSize = 50)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? page.Value : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
                        
            if (page.Value <= 1 || page.Value > totalPages)            
                currentPage = 1; 
            
            else            
                currentPage = page.Value;            
            
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            NextPage = currentPage+1;
            PreviousPage = currentPage -1;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int NextPage { get; private set; }
        public int PreviousPage { get; private set; }
    }
}