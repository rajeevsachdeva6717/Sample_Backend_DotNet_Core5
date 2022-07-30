using Sample.DataContract;
using Sample.DataContract.Models;
using Sample.DataContract.Models.Login;
using System;
using System.Threading.Tasks;

namespace Sample.ServiceContract.Account
{
    public interface IAccountService
    {
        ///<summary>
        ///<Description>Function to create account in application</Description>
        /// </summary>
        Task<APIResponse> SignUpAsync(CreateAccountModel model);

        ///<summary>
        ///<Description>Function to get user List</Description>
        /// </summary>
        Task<APIResponse> UserListAsync(PageSortingModel model);

        ///<summary>
        ///<Description>Function to get user Detail</Description>
        /// </summary>
        Task<APIResponse> UserDetailAsync(Guid userId);

        ///<summary>
        ///<Description>Function used to activate user account</Description>
        /// </summary>
        Task<APIResponse> ActivateUserAsync(Guid userId);

        ///<summary>
        ///<Description>Function to update user detail</Description>
        /// </summary>
        Task<APIResponse> UpdateUserDetailAsync(UserLoginDetailViewModel model);

        ///<summary>
        ///<Description>Function to delete user</Description>
        /// </summary>
        Task<APIResponse> DeleteUserAsync(Guid userId);
    }
}
