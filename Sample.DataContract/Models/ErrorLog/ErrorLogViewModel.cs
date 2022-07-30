using System;

namespace Sample.DataContract.Models.ErrorLog
{
    public class ErrorLogViewModel
    {
        public int ErrorLogId { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string CustomMessage { get; set; }
        public string ErrorType { get; set; }
        public string LoggedInUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
