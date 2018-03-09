using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Response;
using BaselineSolution.Framework.Utilities;
using BaselineSolution.WebApp.Components.Models.Authentication;
using BaselineSolution.WebApp.Infrastructure.Constants;
using BaselineSolution.WebApp.Infrastructure.Utilities;

namespace BaselineSolution.WebApp.Infrastructure.Bases
{
    public class BaseController : Controller
    {
        private ICustomPrincipal _user;

        protected virtual string Title { get; set; }

        public new ICustomPrincipal User
        {
            get
            {
                if (_user != null || Session == null)
                    return _user;

                _user = Session[SessionVariables.User] as ICustomPrincipal;
                return _user;
            }
            set
            {
                if (Session != null)
                    Session[SessionVariables.User] = value;
                _user = value;
            }
        }

        #region Filters (sealed in case we want to inject more logic here later)

        protected override sealed void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ExecuteAfterInitialize(requestContext);
        }

        /// <summary>
        ///     Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override sealed void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ExecuteBeforeAction(filterContext);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        ///     Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override sealed void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ExecuteAfterAction(filterContext);
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        ///     Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override sealed void OnException(ExceptionContext filterContext)
        {
            ExecuteOnException(filterContext);
            base.OnException(filterContext);
        }

        /// <summary>
        ///     Called after the action result that is returned by an action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action result</param>
        protected override sealed void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ExecuteAfterResult(filterContext);
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        ///     Before any action is executed, this method is called
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override sealed void OnAuthorization(AuthorizationContext filterContext)
        {
            ExecuteBeforeAuthorize(filterContext);
            base.OnAuthorization(filterContext);
            ExecuteAfterAuthorize(filterContext);
        }

        protected override sealed void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Set some menu variables. 
            ViewBag.Title = Title;

            CheckAndHandleFileResult(filterContext);
            base.OnResultExecuting(filterContext);
            ExecuteBeforeResult(filterContext);
        }

        #endregion

        #region Triggers

        /// <summary>
        ///     Call priority: 0  (Lower is called earlier)
        ///     Triggers right after the controller is initialized.
        ///     <br/><strong>This method is only called once in the lifecycle of the controller!</strong>
        /// </summary>
        /// <param name="requestContext">The request context</param>
        protected virtual void ExecuteAfterInitialize(RequestContext requestContext)
        {
        }

        /// <summary>
        ///     Call priority: 1 (Lower is called earlier)
        ///     Triggers before any authorization took place.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteBeforeAuthorize(AuthorizationContext filterContext)
        {
        }

        /// <summary>
        ///     Call priority: 2 (Lower is called earlier)
        ///     Triggers after authorization (if it succeeded!)
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteAfterAuthorize(AuthorizationContext filterContext)
        {
        }

        /// <summary>
        ///     Call priority: 3 (Lower is called earlier)
        ///     Triggers before an action method is called
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteBeforeAction(ActionExecutingContext filterContext)
        {
            // User ophalen als WesAccount
            if (User != null && User.CurrentAccount != null && User.CurrentAccount.AccountCultures != null)
            {
                var accountCultures = User.CurrentAccount.AccountCultures.ToArray();


                EnvironmentInfo.Current.ActiveCultures = accountCultures.Select(a => a.Culture);
                EnvironmentInfo.Current.Culture = User.DefaultCulture;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(User.DefaultCulture ?? "nl-BE");

            }
        }

        /// <summary>
        ///     Call priority: 4 (Lower is called earlier)
        ///     Triggers after an action method is invoked
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteAfterAction(ActionExecutedContext filterContext)
        {
        }

        /// <summary>
        ///     Call priority: 5 (Lower is called earlier)
        ///     Triggers before the ActionResult that was returned by the action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteBeforeResult(ResultExecutingContext filterContext)
        {
        }

        /// <summary>
        ///     Call priority: 6 (Lower is called earlier)
        ///     Triggers after the ActionResult that was returned by the action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteAfterResult(ResultExecutedContext filterContext)
        {
        }

        /// <summary>
        ///     Call priority: 7 (Lower is called earlier)
        ///     Triggers if an unhandled exception is thrown during the execution of the ASP.NET MVC pipeline.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected virtual void ExecuteOnException(ExceptionContext filterContext)
        {
        }

        #endregion


        #region Messages

        /// <summary>
        ///     Displays a success message above the content body
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="parameters">The parameters that need to be used for string.format</param>
        [StringFormatMethod("message")]
        protected void Success(string message, params object[] parameters)
        {
            SetTempData(TempDataEnum.Success, string.Format(message, parameters));
        }

        /// <summary>
        ///     Displays a warning message above the content body
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="parameters">The parameters that need to be used for string.format</param>
        [StringFormatMethod("message")]
        protected void Warning(string message, params object[] parameters)
        {
            SetTempData(TempDataEnum.Warning, string.Format(message, parameters));
        }

        /// <summary>
        ///     Displays an error message above the content body
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="parameters">The parameters that need to be used for string.format</param>
        [StringFormatMethod("message")]
        protected void Error(string message, params object[] parameters)
        {
            SetTempData(TempDataEnum.Error, string.Format(message, parameters));
        }

        /// <summary>
        ///     Displays an info message above the content body
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="parameters">The parameters that need to be used for string.format</param>
        [StringFormatMethod("message")]
        protected void Info(string message, params object[] parameters)
        {
            SetTempData(TempDataEnum.Info, string.Format(message, parameters));
        }

        protected void SetMessage<T>(Response<T> response)
        {
            var errorMessages = response.Messages.Where(x => x.Type == MessageType.Error).Select(x => x.MessageText).ToList();
            var warningMessages = response.Messages.Where(x => x.Type == MessageType.Warning).Select(x => x.MessageText).ToList();
            var validationMessages = response.Messages.Where(x => x.Type == MessageType.Validation).Select(x => x.MessageText).ToList();
            var successMessages = response.Messages.Where(x => x.Type == MessageType.Success).Select(x => x.MessageText).ToList();

            if (errorMessages.Any())
                SetTempData(TempDataEnum.Error, errorMessages);

            if (warningMessages.Any())
                SetTempData(TempDataEnum.Warning, warningMessages);

            if (validationMessages.Any())
                SetTempData(TempDataEnum.Info, validationMessages);

            if (successMessages.Any())
                SetTempData(TempDataEnum.Success, successMessages);
        }

        private void SetTempData(TempDataEnum type, string message)
        {
            if (TempData[type.ToString()] == null)
                TempData[type.ToString()] = message;
            else
                TempData[type.ToString()] += "<br/>" + message;
        }

        private void SetTempData(TempDataEnum type, List<string> messages)
        {
            messages.ForEach(x => SetTempData(type, x));
        }

        #endregion

        private const string FileDownloadCookieName = "fileDownload";

        /// <summary>
        /// If the current response is a FileResult (an MVC base class for files) then write a
        /// cookie to inform jquery.fileDownload that a successful file download has occured
        /// </summary>
        /// <param name="context"></param>
        private void CheckAndHandleFileResult(ResultExecutingContext context)
        {
            if (context.Result is FileResult)
                //jquery.fileDownload uses this cookie to determine that a file download has completed successfully
                Response.SetCookie(new HttpCookie(FileDownloadCookieName, "true") { Path = "/" });
            else
            //ensure that the cookie is removed in case someone did a file download without using jquery.fileDownload
            if (Request.Cookies[FileDownloadCookieName] != null)
                Response.Cookies[FileDownloadCookieName].Expires = DateTime.Now.AddYears(-1);
        }
    }
}