using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.WebApp.Infrastructure.Constants;
using BaselineSolution.WebApp.Infrastructure.Models.Authentication;

namespace BaselineSolution.WebApp.Infrastructure.Bases
{
    public class ControllerBase : Controller
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

/*              
 *              EnvironmentInfo.Current.ActiveCultures = accountCultures.Select(a => a.Culture);
                EnvironmentInfo.Current.Culture = User.DefaultCulture;
                */
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