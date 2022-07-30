using Dapper;
using Sample.DataContract;
using Sample.DataContract.Enums;
using Sample.DataContract.Models.Login;
using Sample.RepositoryContract.Account;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace Sample.Repository.Account
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        ///<Description>Function used to create user account</Description>
        /// </summary>
        public async Task<Guid> SignupAsync(CreateAccountModel model)
        {
            string query = "CreateUserAccount";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@companyName", model.CompanyName, DbType.String, ParameterDirection.Input);
            parameter.Add("@room#", model.NumberofRooms, DbType.String, ParameterDirection.Input);
            parameter.Add("@firstName", model.FirstName, DbType.String, ParameterDirection.Input);
            parameter.Add("@lastName", model.LastName, DbType.String, ParameterDirection.Input);
            parameter.Add("@email", model.Email, DbType.String, ParameterDirection.Input);
            parameter.Add("@userName", model.UserName, DbType.String, ParameterDirection.Input);
            parameter.Add("@password", model.Password, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<Guid>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to get user list</Description>
        /// </summary>
        public async Task<IEnumerable<UserLoginDetailViewModel>> UserListAsync(PageSortingModel model)
        {
            string query = "UserAccountList";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@search", model.Search, DbType.String, ParameterDirection.Input);
            parameter.Add("@pageNumber", model.PageNumber, DbType.String, ParameterDirection.Input);
            parameter.Add("@pageSize", model.PageSize, DbType.String, ParameterDirection.Input);

            return await GetAsync<UserLoginDetailViewModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to get User Details</Description>
        /// </summary>
        public async Task<UserLoginDetailViewModel> UserDetailAsync(Guid userId)
        {
            string query = "UserDetail";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userId", userId, DbType.Guid, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<UserLoginDetailViewModel>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to activate user account</Description>
        /// </summary>
        public async Task<string> ActivateUserAsync(Guid userId)
        {
            string query = "ActivateUserAccount";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userId", userId, DbType.Guid, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<string>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to get User Details</Description>
        /// </summary>
        public async Task<bool> UpdateUserDetailAsync(UserLoginDetailViewModel model)
        {
            string query = "UpdateUserDetail";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userId", model.UserId, DbType.Guid, ParameterDirection.Input);
            parameter.Add("@firstName", model.FirstName, DbType.String, ParameterDirection.Input);
            parameter.Add("@lastName", model.LastName, DbType.String, ParameterDirection.Input);
            parameter.Add("@companyName", model.CompanyName, DbType.String, ParameterDirection.Input);
            parameter.Add("@email", model.Email, DbType.String, ParameterDirection.Input);
            parameter.Add("@phone", model.Phone, DbType.String, ParameterDirection.Input);
            parameter.Add("@userName", model.UserName, DbType.String, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<bool>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }

        /// <summary>
        ///<Description>Function used to delete User</Description>
        /// </summary>
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            string query = "DeleteUser";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@userId", userId, DbType.Guid, ParameterDirection.Input);

            return await GetFirstOrDefaultAsync<bool>(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }
    }
}
