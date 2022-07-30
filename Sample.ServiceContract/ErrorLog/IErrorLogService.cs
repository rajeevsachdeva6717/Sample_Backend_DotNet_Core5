using Sample.DataContract.Models.ErrorLog;
using System.Threading.Tasks;

namespace Sample.ServiceContract.ErrorLog
{
    /// <summary>
    /// Error Log service to log error
    /// </summary>
    public interface IErrorLogService
    {
        /// <summary>
        /// Insert new errror log into the database.
        /// </summary>
        /// <param name="model">Error log view model object</param>
        Task InsertErrorLogAsync(ErrorLogViewModel model);
    }
}
