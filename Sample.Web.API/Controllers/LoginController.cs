using Sample.Common.Messages;
using Sample.DataContract.Common;
using Sample.DataContract.Models.Login;
using Sample.ServiceContract.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Sample.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService,
            IHttpContextAccessor contextAccessor
            ) : base(contextAccessor)
        {
            _loginService = loginService;
        }

        /// <summary>
        ///<Description>Function to check the user credentials</Description>
        /// </summary>
        [HttpPost]
        [Route("/signin")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginInputApiModel model)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _loginService.LoginAsync(model.UserName, model.Password);
            if (result != null)
            {
                response.Data = result;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            else
            {
                response.Messages = CommonErrorMessages.CommonError;
                response.StatusCode = HttpStatusCode.NoContent;
                return NoContent();
            }
        }

        /// <summary>
        ///<Description>Function for Password Reset</Description>
        /// </summary>
        [HttpGet]
        [Route("/password/reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(string username)
        {
            return Ok(await _loginService.ResetPasswordAsync(username));
        }

        /// <summary>
        ///<Description>Function to log the last-activity of the user when the user Logout.</Description>
        /// </summary>
        [HttpGet]
        [Route("/logOut")]
        [AllowAnonymous]
        public async Task<IActionResult> LogOutAsync(int? userid)
        {
            return Ok(await _loginService.LogOutAsync(userid));
        }
    }
}