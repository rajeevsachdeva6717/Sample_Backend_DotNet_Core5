using Sample.DataContract;
using Sample.DataContract.Models.Login;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.RepositoryContract.Account
{
    public interface IAccountRepository
    {
        /// <summary>
        ///<Description>Function to create user account in application</Description>
        /// </summary>
        Task<Guid> SignupAsync(CreateAccountModel model);

        /// <summary>
        ///<Description>Function to get users list</Description>
        /// </summary>
        Task<IEnumerable<UserLoginDetailViewModel>> UserListAsync(PageSortingModel model);

        /// <summary>
        ///<Description>Function to get users list</Description>
        /// </summary>
        Task<UserLoginDetailViewModel> UserDetailAsync(Guid userId);

        /// <summary>
        ///<Description>Function to activate user account</Description>
        /// </summary>
        Task<string> ActivateUserAsync(Guid userId);

        /// <summary>
        ///<Description>Function to update users list</Description>
        /// </summary>
        Task<bool> UpdateUserDetailAsync(UserLoginDetailViewModel model);

        /// <summary>
        ///<Description>Function to delete users list</Description>
        /// </summary>
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
