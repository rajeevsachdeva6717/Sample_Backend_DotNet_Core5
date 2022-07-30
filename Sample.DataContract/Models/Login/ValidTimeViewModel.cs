using System;

namespace Sample.DataContract.Models.Login
{
    public class ValidTimeViewModel
    {
        public DateTime? MFLoginStart { get; set; }
        public DateTime? MFLoginEnd { get; set; }
        public DateTime? SatLoginStart { get; set; }
        public DateTime? SatLoginEnd { get; set; }
        public DateTime? SunLoginStart { get; set; }
        public DateTime? SunLoginEnd { get; set; }
    }
}
