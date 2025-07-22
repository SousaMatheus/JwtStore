using System.Security.Claims;

namespace AspNetJWT.Extensions
{
    public static class ClaimsTypeExtension
    {
        public static int GetUserId(this ClaimsPrincipal claims)
        {
			try
			{
				var id = claims.Claims.FirstOrDefault(x => x.Type == "id").Value;
				return int.Parse(id);
			}
			catch(Exception)
			{
				return 0;
			}
        }

        public static string GetUserName(this ClaimsPrincipal claims)
        {
            try
            {
               return claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value ?? "";
            }
            catch(Exception)
            {
                return "";
            }
        }

        public static string GetUserGivenName(this ClaimsPrincipal claims)
        {
            try
            {
                return claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value ?? "";
            }
            catch(Exception)
            {
                return "";
            }
        }

        public static string GetUserEmail(this ClaimsPrincipal claims)
        {
            try
            {
                return claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value ?? "";
            }
            catch(Exception)
            {
                return "";
            }
        }

        public static string GetUserImage(this ClaimsPrincipal claims)
        {
            try
            {
                return claims.Claims.FirstOrDefault(x => x.Type == "image").Value ?? "";
            }
            catch(Exception)
            {
                return "";
            }
        }
    }
}
