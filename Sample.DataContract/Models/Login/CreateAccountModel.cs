namespace Sample.DataContract.Models.Login
{
    public class CreateAccountModel
    {
        public string CompanyName { get; set; }
        public string NumberofRooms { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSampleRequired { get; set; }
    }
}
