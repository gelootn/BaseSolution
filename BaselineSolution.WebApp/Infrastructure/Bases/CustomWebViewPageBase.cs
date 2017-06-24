using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Models.Authentication;

namespace BaselineSolution.WebApp.Infrastructure.Bases
{
    public abstract class CustomWebViewPageBase<T> : WebViewPage<T>
    {
        protected new ICustomPrincipal User
        {
            get { return base.User as ICustomPrincipal ?? new AnonymousCustomPrincipal(); }
        }
    }

    public abstract class CustomWebViewPageBase : WebViewPage
    {

        public new ICustomPrincipal User
        {
            get { return base.User as ICustomPrincipal ?? new AnonymousCustomPrincipal(); }
        }
    }
}