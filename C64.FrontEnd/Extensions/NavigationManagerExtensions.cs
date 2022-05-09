using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.FrontEnd.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static Dictionary<string, string> GetQueryStrings(this NavigationManager navigationManager, string url)
        {
            return ParseUrl(url);
        }

        public static Dictionary<string, string> GetQueryStrings(this NavigationManager navigationManager)
        {
            return ParseUrl(navigationManager.Uri);
        }

        public static T GetFromQueryString<T>(this NavigationManager navigationManager, string key)
        {
            var queryStrings = navigationManager.GetQueryStrings();

            if (queryStrings == null || !queryStrings.ContainsKey(key))
                return default;

            try
            {
                var value = queryStrings[key];
                var retVal = (T)Convert.ChangeType(value, typeof(T));
                return retVal;
            }
            catch
            {
                var value = queryStrings[key];
                return GetValue<T>(value);
            }
        }

        private static T GetValue<T>(object value)
        {
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;

            try
            {
                return (value == null || DBNull.Value.Equals(value)) ? default(T) : (T) Convert.ChangeType(value, t);
            }
            catch
            {
                return default(T);
            }
        }

        private static Dictionary<string, string> ParseUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || !url.Contains("?") || url.Substring(url.Length - 1) == "?")
                return null;

            var queryStrings = url.Split(new string[] { "?" }, System.StringSplitOptions.None)[1];

            var dicQueryString = queryStrings.Split('&').ToDictionary(p => p.Split("=")[0], p => Uri.UnescapeDataString(p.Split('=')[1]));

            return dicQueryString;
        }
    }
}