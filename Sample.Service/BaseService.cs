using Sample.Common;
using Sample.DataContract.Models.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Sample.Service
{
    public class BaseService
    {
        protected IConfiguration Configuration { get; set; }
        protected string ClientIPAddress { get; set; } = string.Empty;
        protected int LoggedInUserId { get; set; }
        protected string FullUserName { get; set; }
        protected string ApplicationHostUrl { get; set; }
        protected AppSettingModel AppSetting { get; set; }
        public static EmailSettingModel EmailSetting { get; set; }
        protected JwtSettingModel JwtSetting { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; private set; }

        public BaseService(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            HttpContextAccessor = accessor;
            if (accessor != null)
            {
                ApplicationHostUrl = HttpContextAccessor.HttpContext.Request.Scheme + "://" + HttpContextAccessor.HttpContext.Request.Host;
                ClientIPAddress = accessor.HttpContext.Connection.RemoteIpAddress.ToString() == "::1" ? "172.24.2.105" : accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                if (accessor.HttpContext.User.Identity != null && accessor.HttpContext.User.Identity.AuthenticationType == "JwtAuthentication")
                {
                    LoggedInUserId = Convert.ToInt32(accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                    FullUserName = Convert.ToString(accessor.HttpContext.User.Claims.Where(x => x.Type == "UserName").FirstOrDefault().Value);
                }

                AppSetting = accessor.HttpContext.RequestServices.GetService<IOptions<AppSettingModel>>().Value;
                EmailSetting = accessor.HttpContext.RequestServices.GetService<IOptions<EmailSettingModel>>().Value;
                JwtSetting = accessor.HttpContext.RequestServices.GetService<IOptions<JwtSettingModel>>().Value;
            }

            if (configuration != null)
            {
                Configuration = configuration;
            }
        }
    }
}
