﻿namespace Sample.DataContract
{
    public class PageSortingModel
    {       
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
