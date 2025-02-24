using System.Text.Json;
using Tratament.Web.LoggerSetup;

namespace Tratament.Web.Core
{
    public static class CookieHelper
    {
        // Method to save an object to a cookie
        public static void SetObjectAsJson(this IHttpContextAccessor httpContextAccessor, string key, object value, int expireDays = 1)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(expireDays),
                HttpOnly = true, // Prevents JavaScript access (optional for security)
                Secure = true,    // Only send over HTTPS
                SameSite = SameSiteMode.Strict
            };

            string json = JsonSerializer.Serialize(value);
            httpContextAccessor.HttpContext.Response.Cookies.Append(key, json, options);
        }


        public static T GetObjectFromJson<T>(this IHttpContextAccessor httpContextAccessor, string key)
        {
            var cookie = httpContextAccessor.HttpContext.Request.Cookies[key];

            var deserializedObject = JsonSerializer.Deserialize<T>(cookie);

            WriteLog.Common.Debug("Cookie Object: " + JsonSerializer.Serialize(deserializedObject));

            return cookie == null ? default : JsonSerializer.Deserialize<T>(cookie);
        }

        public static void DeleteCookie(this IHttpContextAccessor httpContextAccessor, string key)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Set a past expiration date to delete it
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append(key, "", options); // Empty cookie value to delete it
        }
    }
}
