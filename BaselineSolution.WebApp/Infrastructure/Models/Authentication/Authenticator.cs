using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Infrastructure.Extensions;
using BaselineSolution.WebApp.Infrastructure.Utilities;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication
{
    [Serializable]
    public class Authenticator
    {
        private readonly RequestContext _requestContext;
        private readonly AuthenticatorCache _cache;

        public Authenticator(RequestContext requestContext)
        {
            _requestContext = requestContext;
            _cache = new AuthenticatorCache(requestContext.GetUsername());
        }

        public Authenticator(RequestContext requestContext, UserSecurityBo user)
        {
            _requestContext = requestContext;
            _cache = new AuthenticatorCache(user.UserName);
            _cache.SetUser(user);
        }

        [Serializable]
        private class AuthenticatorCache
        {
            private readonly string _username;
            private const string UserPrefix = "user";
            private const string RightPrefix = "right";

            public AuthenticatorCache(string username)
            {
                _username = username;
            }

            public string GetUserCacheKey(string username)
            {
                return string.Format("{0}.{1}", UserPrefix, username);
            }

            public bool ContainsUser(string username)
            {
                return HttpContextSession.Has(GetUserCacheKey(username));
            }

            public UserSecurityBo GetUser(string username)
            {
                return HttpContextSession.Get<UserSecurityBo>(GetUserCacheKey(username));
            }

            public void SetUser(UserSecurityBo user)
            {
                HttpContextSession.Put(GetUserCacheKey(user.UserName), user);
            }

            public string GetRightCacheKey(string rightKey)
            {
                return string.Format("{0}.{1}.{2}", RightPrefix, _username, rightKey);
            }

            public bool ContainsRight(string rightKey)
            {
                return HttpContextSession.Has(GetRightCacheKey(rightKey));
            }

            public bool GetRight(string rightKey)
            {
                return HttpContextSession.Get<bool>(GetRightCacheKey(rightKey));
            }

            public void SetRight(string rightKey, bool isAllowed)
            {
                HttpContextSession.Put(GetRightCacheKey(rightKey), isAllowed);
            }
        }

        public bool Authenticate()
        {
            var area = _requestContext.RouteData.GetArea();
            var controller = _requestContext.RouteData.GetController();
            var action = _requestContext.RouteData.GetAction();
            return _requestContext.Authenticate(area, controller, action);
        }

        /// <summary>
        /// Returns true if the current user has access to the given rightkey
        /// </summary>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        public bool Authenticate(string rightKey)
        {
            // user needs to be logged in to access rightkeys
            if (!_requestContext.HttpContext.Request.IsAuthenticated)
                return false;

            // check if rightKey exists in cache
            if (_cache.ContainsRight(rightKey))
                return _cache.GetRight(rightKey);

            // no cached result found, get securityservice from dependencyresolver and check with persisted data
            var security = DependencyResolver.Current.GetService<ISecurityService>();
            var user = GetUser(security);
            var userHasRight = security.CheckUserRight(user, rightKey);
            if (userHasRight.IsSuccess)
            {
                bool hasRight = user != null && (user.IsAdministrator() || userHasRight.GetValue());

                // cache this information to avoid multiple service calls for the same rightkey
                _cache.SetRight(rightKey, hasRight);

                return hasRight;
            }
            return false;

        }

        public bool Authenticate([AspMvcArea] string areaName, [AspMvcController] string controllerName, [AspMvcAction] string actionName)
        {
            // prepare rightKey
            string rightKey = string.Join(".", new[] { areaName, controllerName + "Controller", actionName }).Trim('.');

            // check if rightKey exists in cache
            if (_cache.ContainsRight(rightKey))
                return _cache.GetRight(rightKey);

            if (AuthenticateControllerAndAction(areaName, controllerName, actionName))
            {
                _cache.SetRight(rightKey, true);
                return true;
            }

            // Controller and method were both not tagged with AllowAnonymous, continue checking with rightKey
            return Authenticate(rightKey);
        }

        private bool AuthenticateControllerAndAction(string areaName, string controllerName, string actionName)
        {
            var controllerDictionaryKey = GetControllerDictionaryKey(areaName, controllerName);
            var controllerDictionary = GetControllerDictionary();
            if (!controllerDictionary.ContainsKey(controllerDictionaryKey))
            {
                return false;
            }

            var controllerType = controllerDictionary[controllerDictionaryKey];
            return AuthenticateController(controllerType) || AuthenticateAction(controllerType, actionName);
        }

        private bool AuthenticateController(Type controllerType)
        {
            if (controllerType == null)
                return false;
            return controllerType.HasAttribute(typeof(AllowAnonymousAttribute))
                   || (_requestContext.HttpContext.Request.IsAuthenticated && controllerType.HasAttribute(typeof(AllowAuthenticatedAttribute)));

        }

        private bool AuthenticateAction(Type controllerType, string actionName)
        {
            var actionsWithSameName = controllerType.GetMethods().Where(m => m.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!actionsWithSameName.Any())
                return false;

            return actionsWithSameName.Any(m => m.HasAttribute(typeof(AllowAnonymousAttribute))
                   || (_requestContext.HttpContext.Request.IsAuthenticated && m.HasAttribute(typeof(AllowAuthenticatedAttribute))));
        }

        private UserSecurityBo GetUser(ISecurityService security)
        {
            var username = _requestContext.GetUsername();
            if (_cache.ContainsUser(username))
                return _cache.GetUser(username);

            var userResponse = security.FindUserByUsername(username);
            if (userResponse.IsSuccess)
            {
                UserSecurityBo user = userResponse.GetValue();
                _cache.SetUser(user);
                return user;
            }
            return null;
        }

        private IDictionary<string, Type> GetControllerDictionary()
        {
            const string key = "controller.dictionary";
            if (HttpContextSession.Has(key))
                return HttpContextSession.Get<IDictionary<string, Type>>(key);

            IDictionary<string, Type> controllerDictionary = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("Wes"))
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null
                    && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    && t.IsPublic
                    && !t.IsAbstract
                    && typeof(IController).IsAssignableFrom(t))
                .ToDictionary(GetControllerDictionaryKey, t => t);
            HttpContextSession.Put(key, controllerDictionary);
            return controllerDictionary;
        }

        private string GetControllerDictionaryKey(string areaName, string controllerName)
        {
            areaName = string.IsNullOrEmpty(areaName) ? "Base" : areaName;
            return string.Format("{0}.{1}", areaName, controllerName);
        }

        private string GetControllerDictionaryKey(Type controllerType)
        {
            var controllerDictionaryKey = string.Join(".", controllerType.FullName.Split('.').Reverse().Take(3).Reverse().Where(s => !string.Equals("Controllers", s) && !string.Equals("Demo", s)));
            return controllerDictionaryKey.Replace("Controller", string.Empty);
        }

        public void BuildCache(UserSecurityBo user)
        {
            var security = DependencyResolver.Current.GetService<ISecurityService>();
            var rights = security.GetFullRightList();
            var isAdministrator = user.IsAdministrator();


            foreach (var right in rights.Values)
            {
                _cache.SetRight(right.Key, isAdministrator || user.HasRight(right));
            }
        }
    }
}