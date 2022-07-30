namespace Sample.DataContract.Models.Email
{
    public class EmailSettingModel
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string MailServer { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string SMTPPort { get; set; }
        public string EnableSSL { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string ActivationAccountText { get; set; }
        public string ResetPasswordText { get; set; }
    }
}
