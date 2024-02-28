using System.Security.Claims;


namespace Meteors
{
    public static class ClaimsPrincipalExtension
    {
        //MrClaimTypes.NameIdentifier remove MrClaimTypes from this eData test project

        public static object? CurrentUserId(this ClaimsPrincipal user) =>
             user.FindFirst(ClaimTypes.NameIdentifier)?.Value??null;

        public static T? CurrentUserId<T>(this ClaimsPrincipal user)
        {
            var obId = user.FindFirst(ClaimTypes.NameIdentifier);
            if (obId is null)
                return default(T);
            return obId.Value.ChangeType<T>();
        }

        public static T CurrentUserId<T>(this IEnumerable<Claim> claims)
            => (T)Convert.ChangeType(claims.FirstOrDefault(claim => claim.Type == (ClaimTypes.NameIdentifier))?.Value, typeof(T));

        //public static T CurrentUserRole<T>(this IEnumerable<Claim> claims) =>
        //    claims.First(claim => claim.Type == (MrClaimTypes.Role)).Value.ToEnum<T>();

    }
}
