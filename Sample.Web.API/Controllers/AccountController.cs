using Sample.Common.Messages;
using Sample.DataContract;
using Sample.DataContract.Common;
using Sample.DataContract.Models.Login;
using Sample.ServiceContract.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService,
            IHttpContextAccessor contextAccessor
            ) : base(contextAccessor)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///<Description>Function to create user account</Description>
        /// </summary>
        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUpAsync(CreateAccountModel model)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.SignUpAsync(model);
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
        ///<Description>Function to get User list</Description>
        /// </summary>
        [HttpGet]
        [Route("/user/list/")]
        [AllowAnonymous]
        public async Task<IActionResult> UserListAsync([FromQuery] PageSortingModel model)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.UserListAsync(model);
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
        ///<Description>Function to get User list</Description>
        /// </summary>
        [HttpGet]
        [Route("/user/detail/")]
        public async Task<IActionResult> UserDetailAsync(Guid userId)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.UserDetailAsync(userId);
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
        ///<Description>Function for Activate User Account</Description>
        /// </summary>
        [HttpGet]
        [Route("/activate/user")]
        [AllowAnonymous]
        public async Task<IActionResult> ActivateUserAsync(Guid userId)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.ActivateUserAsync(userId);
            if (result != null)
            {
                response.Data = result.Result;
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
        ///<Description>Function to update user detail</Description>
        /// </summary>
        [HttpPut]
        [Route("/update/user/")]
        public async Task<IActionResult> UpdateUserDetailAsync(UserLoginDetailViewModel model)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.UpdateUserDetailAsync(model);
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
        /// Delete a user from database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/delete/user/")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            ResponseStatus<dynamic> response = new ResponseStatus<dynamic>();
            var result = await _accountService.DeleteUserAsync(userId);
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
    }
}
