using Sample.DataContract.Models.Login;
using System.Threading.Tasks;

namespace Sample.RepositoryContract.Login
{
    public interface ILoginRepository
    {
        /// <summary>
        ///<Description>Function to get login Details</Description>
        /// </summary>
        Task<UserLoginDetailViewModel> GetLoginDetailsAsync(string userName, string password);

        /// <summary>
        ///<Description>Function to check User Detail</Description>
        /// </summary>
        Task<UserLoginDetailViewModel> CheckUserDetailsAsync(string username);

        /// <summary>
        ///<Description>Function to insert password detail</Description>
        /// </summary>
        void InsertPasswordDetailsAsync(UserLoginDetailViewModel passwordDetails);

        /// <summary>
        ///<Description>Function to check Password link</Description>
        /// </summary>
        Task<CheckPasswordLinkDetailModel> CheckPasswordLinkAsync(string link);

        /// <summary>
        ///<Description>Function to update Password Detail</Description>
        /// </summary>
        void UpdatePasswordDetailsAsync(string link);

        /// <summary>
        ///<Description>Function to find link for specified User</Description>
        /// </summary>
        Task<ResetPasswordViewModel> FindLinkForSpecifiedUserAsync(string username);

        /// <summary>
        ///<Description>Function to log the Entry in database while the user logged out.</Description>
        /// </summary>
        Task<bool> LogOutAsync(int? userId);
    }
}
