using Dapper;
using Sample.DataContract.Enums;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Repository
{
    public class BaseRepository
    {
        private readonly IConfiguration Configuration;

        public BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Returns first row of type T based on query parameter
        /// </summary>
        public async Task<T> GetFirstOrDefaultAsync<T>(string query, DynamicParameters parameter = null, CommandType? commandType = null, DataBaseNameEnum? databaseID = null)
        {
            T result = default(T);
            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = await conn.QueryFirstOrDefaultAsync<T>(query, parameter, null, null, commandType);
            }

            return result;
        }

        /// <summary>
        /// Returns a list of type T based on query parameter
        /// </summary>
        public async Task<IEnumerable<T>> GetAsync<T>(string query, DynamicParameters parameters = null, CommandType? commandType = null, DataBaseNameEnum? databaseID = null)
        {
            IEnumerable<T> result = default(IEnumerable<T>);
            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = await conn.QueryAsync<T>(query, parameters, null, null, commandType);
            }

            return result;
        }

        public async Task<int> AddAsync(string sql, DynamicParameters parameters = null, CommandType? commandType = null, DataBaseNameEnum databaseID = DataBaseNameEnum.DataBaseHotel)
        {
            int result = 0;

            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = await conn.ExecuteAsync(sql, parameters, null, null, commandType);
            }

            return result;
        }

        private IDbConnection GetConnection(DataBaseNameEnum? databaseID)
        {
            string connectionString = Configuration.GetSection("Data:" + databaseID).GetSection("ConnectionString").Value;
            return new SqlConnection(connectionString);
        }

        protected async Task DeleteAsync(string sql, object parameters = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = GetConnection(DataBaseNameEnum.DataBaseHotel))
            {
                await conn.ExecuteAsync(sql, parameters, null, null, commandType);
            }
        }

        public async Task<SqlMapper.GridReader> QueryMultiple(string query, DynamicParameters parameters = null, CommandType? commandType = null, DataBaseNameEnum databaseID = DataBaseNameEnum.DataBaseHotel)
        {
            SqlMapper.GridReader result;
            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = await conn.QueryMultipleAsync(query, parameters, null, null, commandType);
            }

            return result;
        }

        /// <summary>
        /// Returns a list of type T based on query parameter
        /// </summary>
        public async Task<List<T>> GetAsyncList<T>(string query, DynamicParameters parameters = null, CommandType? commandType = null, DataBaseNameEnum? databaseID = null)
        {
            List<T> result = default(List<T>);
            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = (await conn.QueryAsync<T>(query, parameters, null, null, commandType)).ToList();
            }

            return result;
        }

        private object GetSqlConnection(DataBaseNameEnum? databaseID)
        {
            string connectionString = Configuration.GetSection("Data:" + databaseID).GetSection("ConnectionString").Value;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        private void ReleaseConnection(object Connection)
        {
            SqlConnection con = (SqlConnection)Connection;
            if (con != null)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

                con.Dispose();
            }
        }

        /// <summary>
        /// To get single or default row of type T based on query parameters.
        /// </summary>
        /// <typeparam name="T">T For Generic Returns Type</typeparam>
        /// <param name="query">Query Name</param>
        /// <param name="parameter">Query Parameters</param>
        /// <param name="commandType">Sql Command Type</param>
        /// <param name="databaseID">Database ID</param>
        /// <returns></returns>
        public async Task<T> GetQuerySingleOrDefaultAsync<T>(string query, DynamicParameters parameter = null, CommandType? commandType = null, DataBaseNameEnum databaseID = DataBaseNameEnum.DataBaseHotel)
        {
            T result = default(T);
            using (IDbConnection conn = GetConnection(databaseID))
            {
                result = await conn.QuerySingleOrDefaultAsync<T>(query, parameter, null, null, commandType);
            }

            return result;
        }
    }
}
