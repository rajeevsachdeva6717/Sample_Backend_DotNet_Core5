using Dapper;
using Sample.DataContract.Enums;
using Sample.DataContract.Models.Login;
using Sample.RepositoryContract.Login;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Repository.Login
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        ///<Description>Function used to get Login Details</Description>
        /// </summary>
        public async Task<UserLoginDetailViewModel> GetLoginDetailsAsync(string userName, string password)
        {
            string query = "CheckLoginCredential";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userName", userName, DbType.String, ParameterDirection.Input);
            parameter.Add("@password", password, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<UserLoginDetailViewModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to check user Details</Description>
        /// </summary>
        public async Task<UserLoginDetailViewModel> CheckUserDetailsAsync(string username)
        {
            string query = "CheckUserExist";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userName", username, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<UserLoginDetailViewModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to Reset Password Details</Description>
        /// </summary>
        public async void InsertPasswordDetailsAsync(UserLoginDetailViewModel passwordDetails)
        {
            string query = "ResetUserPassword";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("userId", passwordDetails.UserId, DbType.Guid, ParameterDirection.Input);

            await AddAsync(query, parameters, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to check Password Link</Description>
        /// </summary>
        public async Task<CheckPasswordLinkDetailModel> CheckPasswordLinkAsync(string link)
        {
            string query = "fn_checkpasswordlinkdetails";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p_link", link, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<CheckPasswordLinkDetailModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to update Password</Description>
        /// </summary>
        public async void UpdatePasswordDetailsAsync(string link)
        {
            string query = "fn_UpdatePasswordDetails";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p_link", link, DbType.String, ParameterDirection.Input);

            await AddAsync(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to get link for specified user</Description>
        /// </summary>
        public async Task<ResetPasswordViewModel> FindLinkForSpecifiedUserAsync(string username)
        {
            string query = "fn_FindLinkForSpecifiedUser";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p_username", username, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<ResetPasswordViewModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function to log the Entry in Database while the user logged Out.</Description>
        /// </summary>
        public async Task<bool> LogOutAsync(int? userId)
        {
            string query = "fn_updatelogindetails";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p_userid", userId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("p_checkid", (int)DefaultNumbers.Three, DbType.Int32, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<bool>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }
    }
}
