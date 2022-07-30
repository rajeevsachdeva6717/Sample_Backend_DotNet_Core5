using System;
namespace Sample.DataContract.Models.Login
{
    public class UserLoginDetailViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }
    }
}
