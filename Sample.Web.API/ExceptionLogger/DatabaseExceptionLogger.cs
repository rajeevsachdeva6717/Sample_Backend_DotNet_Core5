using Sample.DataContract.Models.ErrorLog;
using Sample.ServiceContract.ErrorLog;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using Wangkanai.Detection;

namespace Sample.Web.API.ExceptionLogger
{
    /// <summary>
    /// Database Logger for logging the exception
    /// </summary>
    public class DatabaseExceptionLogger : IExceptionLogger
    {
        private readonly IBrowserResolver _browser;
        private readonly IPlatformResolver _platform;
        private readonly IErrorLogService _errorLogService;

        public DatabaseExceptionLogger(IBrowserResolver browser,
            IPlatformResolver platform,
            IErrorLogService errorLogService)
        {
            _browser = browser;
            _platform = platform;
            _errorLogService = errorLogService;
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exception">Exception object</param>
        /// <param name="request">Request object</param>
        public void Log(Exception exception, HttpRequest request)
        {
            string customMessage = "";
            System.Diagnostics.StackTrace objectTrace = new System.Diagnostics.StackTrace(exception, true);
            string errorMessage = string.Empty;

            if (objectTrace != null)
            {
                string filePath = objectTrace.GetFrame(0).GetFileName();
                string fileName = filePath == null ? "" : filePath.Substring(filePath.LastIndexOf("\\") + 1);
                string methodName = objectTrace.GetFrame(0).GetMethod().Name == "MoveNext" ? objectTrace.GetFrame(0).GetMethod().DeclaringType.Name : objectTrace.GetFrame(0).GetMethod().Name;
                string lineNumber = objectTrace.GetFrame(0).GetFileLineNumber().ToString();
                string columnNumber = objectTrace.GetFrame(0).GetFileColumnNumber().ToString();
                string exceptionMessage = exception.Message;
                errorMessage = string.Format("FileName ={0}, MethodName = {1}, Line Number= {2}, ColumnNumber ={3}, ErrorMessage:- {4}", fileName, methodName, lineNumber, columnNumber, exceptionMessage);
            }

            customMessage = customMessage ?? string.Empty;
            customMessage += "[practiceid : " + (request.Query["practiceid"].FirstOrDefault() == null ? "-" : Convert.ToString(request.Query["practiceid"].FirstOrDefault())) + "], ";

            if (request != null)
            {
                if (request.Query["userid"].FirstOrDefault() != null)
                {
                    string encryptedquerystring = request.Query["userid"].FirstOrDefault();
                    customMessage += "[User Id: " + encryptedquerystring + "]";
                }
                else
                {
                    foreach (System.Collections.Generic.KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> item in request.Query)
                        customMessage += "[" + item.Key + " : " + item.Value.FirstOrDefault() + "], ";
                }

                if (request.Body.CanSeek)
                {
                    request.Body.Position = 0;
                }

                StreamReader stream = new StreamReader(request.Body);
                string body = stream.ReadToEnd();
                customMessage += "[Body : " + body + "], ";

                if (_browser.Browser != null && _browser.Browser.Version != null)
                {
                    string browserinformation = "browser details: "
                               + "[type = " + _browser.Browser.Type
                               + "] [name = " + _browser.Browser.Name
                               + "] [version = " + string.Format("{0}.{1}.{2}.{3}", _browser.Browser.Version.Major, _browser.Browser.Version.Minor, _browser.Browser.Version.Build, _browser.Browser.Version.Revision)
                               + "] [major version = " + _browser.Browser.Version.Major
                               + "] [minor version = " + _browser.Browser.Version.Minor
                               + "] [platform = " + _platform.Platform?.Type
                               + "]";
                    customMessage = customMessage + browserinformation;
                }
            }

            string loggedInUser = null;
            if (request.HttpContext.User.Identity.IsAuthenticated)
            {
                loggedInUser = request.HttpContext.User.Claims.Where(x => x.Type == "FullUserName").FirstOrDefault().Value;
            }

            ErrorLogViewModel model = new ErrorLogViewModel()
            {
                ErrorMessage = errorMessage,
                CustomMessage = customMessage,
                CreatedDate = DateTime.Now,
                StackTrace = exception.StackTrace,
                LoggedInUser = loggedInUser
            };

            _errorLogService.InsertErrorLogAsync(model).GetAwaiter().GetResult();
        }
    }
}
