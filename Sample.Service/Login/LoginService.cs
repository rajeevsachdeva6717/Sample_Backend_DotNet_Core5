using Sample.Common;
using Sample.DataContract.Enums;
using Sample.DataContract.Models;
using Sample.DataContract.Models.Email;
using Sample.DataContract.Models.Login;
using Sample.RepositoryContract.Login;
using Sample.ServiceContract.Email;
using Sample.ServiceContract.Login;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Login
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IEmailService _emailservice;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        private readonly JwtSettingModel _jwtSetting;
        private IHostingEnvironment _environment;

        public LoginService(ILoginRepository loginRepo,
            IHttpContextAccessor accessor,
            IEmailService emailService,
            IConfiguration configuration,
            IOptions<JwtSettingModel> jwtoption,
            IHostingEnvironment environment) : base(accessor, configuration)
        {
            _jwtSetting = jwtoption.Value;
            _loginRepository = loginRepo;
            _emailservice = emailService;
            _configuration = configuration;
            _accessor = accessor;
            _environment = environment;
        }

        /// <summary>
        /// Function used to check user Credentials in DB
        /// </summary>
        public async Task<APIResponse> LoginAsync(string username, string password)
        {
            UserDetailModel sessionModel = null;
            string hashPassword = PasswordSHA512CryptoProvider.CreateHash(password);
            UserLoginDetailViewModel loginDetails = await _loginRepository.GetLoginDetailsAsync(username, hashPassword);

            if (loginDetails != null)
            {
                sessionModel = new UserDetailModel()
                {
                    UserID = loginDetails.UserId,
                    UserName = loginDetails.UserName,
                    FullUserName = loginDetails.FirstName + ' ' + loginDetails.LastName,
                    UserIP = string.Empty
                };

                APIResponse response = new APIResponse(StatusCodesEnum.LoginSucess, sessionModel);
                if (response.StatusCode == StatusCodesEnum.LoginSucess)
                {
                    response.Result = GenerateToken(response.Result as UserDetailModel);
                }

                return response;
            }
            else
            {
                return new APIResponse(StatusCodesEnum.InvalidUserIDPassword, sessionModel);
            }
        }

        /// <summary>
        /// Function used to reset user password 
        /// </summary>
        public async Task<APIResponse> ResetPasswordAsync(string username)
        {
            UserLoginDetailViewModel passwordDetails = await _loginRepository.CheckUserDetailsAsync(username);
            StatusCodesEnum statusCode;
            if (passwordDetails == null)
            {
                statusCode = StatusCodesEnum.UserNameDoesNotExists;
            }
            else if (!passwordDetails.IsActive)
            {
                statusCode = StatusCodesEnum.InActiveUser;
            }
            else
            {
                _loginRepository.InsertPasswordDetailsAsync(passwordDetails);
                var accountVerifyURL = AppSetting.ResetPasswordLink + "userId=" + passwordDetails.UserId;
                var webRoot = _environment.WebRootPath;
                var filePath = webRoot + AppSetting.ResetPasswordLink;

                StreamReader streamReader = new StreamReader(filePath);
                string MailText = streamReader.ReadToEnd();
                MailText = MailText.Replace("[Name]", passwordDetails.FirstName.Trim() + passwordDetails.LastName.Trim());
                MailText = MailText.Replace("[Url]", "<a href='" + accountVerifyURL + "'>" + accountVerifyURL + "</a>");

                EmailSettingModel email = new EmailSettingModel()
                {
                    FromAddress = EmailSetting.FromAddress,
                    ToAddress = passwordDetails.Email,
                    Subject = EmailSetting.Subject,
                    Text = MailText
                };

                streamReader.Dispose();

                _emailservice.MailObject(EmailSetting.FromAddress, passwordDetails.Email, EmailSetting.Subject, EmailSetting.Text);
                statusCode = StatusCodesEnum.VisitGmailForResetPasswordLink;
            }

            return new APIResponse(statusCode);
        }

        /// <summary>
        /// Function used to get reset user password link
        /// </summary>
        public async Task<APIResponse> ResetPasswordLinkAsync(string link)
        {
            StatusCodesEnum statusCode;
            UserDetailModel result = new UserDetailModel();
            CheckPasswordLinkDetailModel passwordDetails = await _loginRepository.CheckPasswordLinkAsync(link);

            if (passwordDetails == null)
            {
                statusCode = StatusCodesEnum.LinkNotFound;
            }
            else if (passwordDetails.InActive)
            {
                statusCode = StatusCodesEnum.LinkExpired;
            }
            else
            {
                _loginRepository.UpdatePasswordDetailsAsync(link);
                result = new UserDetailModel() { Url = link };
                statusCode = StatusCodesEnum.LinkSuccessFullySent;
            }

            return new APIResponse(statusCode, result);
        }

        /// <summary>
        /// Function used to generate token key
        /// </summary>
        public UserDetailModel GenerateToken(UserDetailModel model)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("UserId", model.UserID.ToString()),
                new Claim("FullUserName", model.FullUserName==null?"":model.FullUserName),
                new Claim("UserIP", model.UserIP==null?"":model.UserIP),
                new Claim("Url", model.Url==null?"":model.Url),
            });

            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            System.IdentityModel.Tokens.Jwt.JwtSecurityToken token = handler.CreateJwtSecurityToken(
               issuer: _jwtSetting.Issuer,
               audience: _jwtSetting.Audience,
               issuedAt: DateTime.Now,
               expires: DateTime.Now.AddMinutes(_jwtSetting.ExpireIn),
               subject: claimsIdentity,
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSetting.Secret)), SecurityAlgorithms.HmacSha256)
               );

            model.AccessToken = handler.WriteToken(token);

            return model;
        }

        /// <summary>
        /// Function used to set user logged out of application
        /// </summary>
        public async Task<bool> LogOutAsync(int? userId)
        {
            return await _loginRepository.LogOutAsync(userId);
        }
    }
}
