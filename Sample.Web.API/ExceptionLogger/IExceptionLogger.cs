using Microsoft.AspNetCore.Http;
using System;

namespace Sample.Web.API.ExceptionLogger
{
    /// <summary>
    /// Interface for logging exception
    /// </summary>
    public interface IExceptionLogger
    {
        void Log(Exception exception, HttpRequest request);
    }
}
