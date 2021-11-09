using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebSite
{
    public static class ControllerExtensionMethods
    {
        public static int? GetCurrentUserId(this Controller controller)
        {
            if (controller.User == null) { return null; }

            var identity = (ClaimsIdentity)controller.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            if (claims == null || claims.Count() == 0) { return null; }

            string userIdStringValue = claims.Where(c => c.Type == "user_id").FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(userIdStringValue)) { return null; }
            int userIdValue = -1;
            if (int.TryParse(userIdStringValue, out userIdValue))
            {
                return userIdValue;
            }
            return null;
        }
    }
}