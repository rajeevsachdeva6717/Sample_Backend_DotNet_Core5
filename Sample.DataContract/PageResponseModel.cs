namespace Sample.DataContract
{
    public class PageResponseModel
    {
        public int Page { get; set; }
        public int Per_page { get; set;}
        public int Total { get; set;}
        public int Total_pages { get; set; }
        public object Data { get; set; }
    }
}
