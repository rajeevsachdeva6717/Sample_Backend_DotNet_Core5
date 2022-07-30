using Sample.Common;
using Sample.DataContract;
using Sample.DataContract.Enums;
using Sample.DataContract.Models;
using Sample.DataContract.Models.Email;
using Sample.DataContract.Models.Login;
using Sample.RepositoryContract.Account;
using Sample.ServiceContract.Account;
using Sample.ServiceContract.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sample.Service.Account
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailservice;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        private readonly JwtSettingModel _jwtSetting;
        private IHostingEnvironment _environment;

        public AccountService(IAccountRepository accountRepository,
            IHttpContextAccessor accessor,
            IEmailService emailService,
            IConfiguration configuration,
            IOptions<JwtSettingModel> jwtoption,
            IHostingEnvironment environment) : base(accessor, configuration)
        {
            _jwtSetting = jwtoption.Value;
            _accountRepository = accountRepository;
            _emailservice = emailService;
            _configuration = configuration;
            _accessor = accessor;
            _environment = environment;
        }

        /// <summary>
        /// Function used to create account
        /// </summary>
        public async Task<APIResponse> SignUpAsync(CreateAccountModel model)
        {
            string passwordKey = model.Password;
            model.Password = PasswordSHA512CryptoProvider.CreateHash(model.Password);
            Guid result = await _accountRepository.SignupAsync(model);
            if (result != Guid.Empty)
            {
                var accountVerifyURL = AppSetting.AccountActivationLink + "userId=" + result;
                var webRoot = _environment.WebRootPath;
                var filePath = webRoot + AppSetting.AccountActivationTemplatePath;

                StreamReader streamReader = new StreamReader(filePath);
                string MailText = streamReader.ReadToEnd();
                MailText = MailText.Replace("[Name]", model.FirstName.Trim() + model.LastName.Trim());
                MailText = MailText.Replace("[UserName]", model.UserName.Trim());
                MailText = MailText.Replace("[Password]", passwordKey.Trim());
                MailText = MailText.Replace("[Url]", "<a href='" + accountVerifyURL + "'>" + accountVerifyURL + "</a>");

                EmailSettingModel email = new EmailSettingModel()
                {
                    FromAddress = EmailSetting.FromAddress,
                    ToAddress = model.Email,
                    Subject = EmailSetting.Subject,
                    Text = MailText
                };

                streamReader.Dispose();

                _emailservice.MailObject(email.FromAddress, email.ToAddress, email.Subject, email.Text);
            }

            return new APIResponse(StatusCodesEnum.Success, result);
        }

        /// <summary>
        /// Function used to get user list
        /// </summary>
        public async Task<APIResponse> UserListAsync(PageSortingModel model)
        {
            var result = await _accountRepository.UserListAsync(model);

            return new APIResponse(StatusCodesEnum.Success, result);
        }

        /// <summary>
        /// Function used to get user Detail
        /// </summary>
        public async Task<APIResponse> UserDetailAsync(Guid userId)
        {
            var result = await _accountRepository.UserDetailAsync(userId);

            return new APIResponse(StatusCodesEnum.Success, result);
        }

        /// <summary>
        /// Function used to activate user account
        /// </summary>
        public async Task<APIResponse> ActivateUserAsync(Guid userId)
        {
            var result = await _accountRepository.ActivateUserAsync(userId);

            return new APIResponse(StatusCodesEnum.Success, result);
        }

        /// <summary>
        /// Function used to update user Detail
        /// </summary>
        public async Task<APIResponse> UpdateUserDetailAsync(UserLoginDetailViewModel model)
        {
            var result = await _accountRepository.UpdateUserDetailAsync(model);

            return new APIResponse(StatusCodesEnum.Success, result);
        }

        /// <summary>
        /// Function used to delete user 
        /// </summary>
        public async Task<APIResponse> DeleteUserAsync(Guid userId)
        {
            var result = await _accountRepository.DeleteUserAsync(userId);

            return new APIResponse(StatusCodesEnum.Success, result);
        }
    }
}
