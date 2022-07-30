using System;

namespace Sample.Service.Login
{
    public class UserDetailModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string FullUserName { get; set; }
        public string UserIP { get; set; }
        public string Url { get; set; }
        public string AccessToken { get; set; }
    }
}
