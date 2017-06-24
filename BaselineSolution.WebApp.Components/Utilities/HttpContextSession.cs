using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaselineSolution.WebApp.Components.Utilities
{
    public static class HttpContextSession
    {
        private const string Keys = "HttpContextSession.Keys";

        public static bool IsSessionAvailable()
        {
            return HttpContext.Current != null && HttpContext.Current.Session != null;
        }

        /// <summary>
        ///     Returns true if the HttpContext.Current.Session contains a value with a key equal to <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Has(string key)
        {
            if (!IsSessionAvailable())
                return false;
            return HttpContext.Current.Session[key] != null;
        }

        /// <summary>
        ///     Puts a value in the HttpContext.Current.Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Put<T>(string key, T value)
        {
            if (!IsSessionAvailable())
                throw new InvalidOperationException("No HttpContext Session available");
            HttpContext.Current.Session[key] = value;
            if (!string.Equals(key, Keys))
                AddKeyToSessionKeys(key);
        }

        /// <summary>
        ///     Gets a value from the HttpContext.Current.Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (!IsSessionAvailable())
                throw new InvalidOperationException("No HttpContext Session available");
            if (!Has(key))
                throw new ArgumentException(string.Format("Key [{0}] is not present in HttpContext.Current.Session", key), "key");
            return (T)HttpContext.Current.Session[key];
        }

        /// <summary>
        ///     Gets a value from the HttpContext.Current.Session or the <paramref name="defaultValue"/> if the value could not be fetched
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            if (!IsSessionAvailable() || !Has(key))
                return defaultValue;
            return Get<T>(key);
        }

        /// <summary>
        ///     Removes a value from the HttpContext.Current.Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            if (!IsSessionAvailable())
                throw new InvalidOperationException("No HttpContext Session available");
            if (!Has(key))
                return false;
            HttpContext.Current.Session.Remove(key);
            return true;
        }

        public static IEnumerable<string> GetKeys()
        {
            return !Has(Keys) ? Enumerable.Empty<string>() : Get<ISet<string>>(Keys);
        }

        private static void AddKeyToSessionKeys(string key)
        {
            ISet<string> sessionKeyList = Has(Keys)
                                     ? Get<ISet<string>>(Keys)
                                     : new HashSet<string>();
            sessionKeyList.Add(key);
            Put(Keys, sessionKeyList);
        }
    }
}