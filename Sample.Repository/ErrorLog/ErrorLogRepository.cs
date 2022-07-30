using Dapper;
using Sample.DataContract.Enums;
using Sample.DataContract.Models.ErrorLog;
using Sample.RepositoryContract.ErrorLog;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Repository.ErrorLog
{
    /// <summary>
    /// Error Log Repository to log errors
    /// </summary>
    public class ErrorLogRepository : BaseRepository, IErrorLogRepository
    {
        public ErrorLogRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Insert new errror log into the database.
        /// </summary>
        /// <param name="model">Error log view model object</param>
        public async Task InsertErrorLogAsync(ErrorLogViewModel model)
        {
            string query = "fn_insertnewerrorlog";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p_errormessage", model.ErrorMessage, DbType.String, ParameterDirection.Input);
            parameter.Add("@p_loggedinuser", model.LoggedInUser, DbType.String, ParameterDirection.Input);
            parameter.Add("@p_stacktrace", model.StackTrace, DbType.String, ParameterDirection.Input);
            parameter.Add("@p_custommessage", model.CustomMessage, DbType.String, ParameterDirection.Input);
            parameter.Add("@p_createdate", model.CreatedDate, DbType.DateTime, ParameterDirection.Input);

            await AddAsync(query, parameter, CommandType.StoredProcedure, DataBaseNameEnum.DataBaseHotel);
        }
    }
}
