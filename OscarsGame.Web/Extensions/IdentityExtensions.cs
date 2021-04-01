using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace OscarsGame.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetOpenIdSurname(this IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                var claimsIdentity = identity as ClaimsIdentity;
                return claimsIdentity?.FindFirst(ClaimTypes.Surname)?.Value;
            }

            return null;
        }

        public static string GetOpenIdGivenName(this IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                var claimsIdentity = identity as ClaimsIdentity;
                return claimsIdentity?.FindFirst(ClaimTypes.GivenName)?.Value;
            }

            return null;
        }

        public static string GetOpenIdName(this IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                var claimsIdentity = identity as ClaimsIdentity;
                return claimsIdentity?.FindFirst("Name")?.Value
                    ?? claimsIdentity?.FindAll(ClaimTypes.Name)?.Skip(1)?.FirstOrDefault()?.Value;
            }

            return null;
        }
    }
}