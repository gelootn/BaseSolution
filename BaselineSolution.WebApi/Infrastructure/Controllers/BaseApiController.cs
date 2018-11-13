using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using System.Web.Http;

namespace BaselineSolution.WebApi.Infrastructure.Controllers
{
    /// <summary>
    /// Base controller for API controllers
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Logging provider
        /// </summary>
        protected readonly ILogging Logging;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logging"></param>
        protected BaseApiController(ILogging logging)
        {
            Logging = logging;
        }

        /// <summary>
        /// Log the response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        protected void LogResponse<T>(Response<T> response)
        {
            var message = $"Is Success:{response.IsSuccess}\t Has Value:{response.HasValue} \t Has warnings:{response.HasWarnings}";
            Logging.Info(message);
        }
    }
}
