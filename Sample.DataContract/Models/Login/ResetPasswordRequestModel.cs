﻿namespace Sample.DataContract.Models.Login
{
    public class ResetPasswordRequestModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Link { get; set; }
    }
}
