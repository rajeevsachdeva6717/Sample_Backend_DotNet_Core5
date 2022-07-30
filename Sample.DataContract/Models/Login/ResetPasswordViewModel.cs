namespace Sample.DataContract.Models.Login
{
    public class ResetPasswordViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsApproved { get; set; }
        public long UserId { get; set; }
        public bool? Inactive { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public string Passwordsalt { get; set; }
        public string NewEncryptedPassword { get; set; }
        public string NewPasswordSalt { get; set; }
        public string PasswordFormat { get; set; }
    }
}
