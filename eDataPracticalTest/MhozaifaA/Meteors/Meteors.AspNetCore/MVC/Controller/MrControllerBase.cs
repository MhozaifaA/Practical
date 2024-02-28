#define MroffLanguageRoute
#define MroffDefaultRoute

using Microsoft.AspNetCore.Mvc;


namespace Meteors
{

    //[TypeFilter(typeof(MrExceptionFilter))]

//#if MroffLanguageRoute
//    [LanguageRoute]
//#endif
//#if MroffDefaultRoute
//    [DefaultRoute]
//#endif

    public class MrControllerBase : ControllerBase
    {
      
        protected virtual object? Key => User.CurrentUserId();
      //  protected virtual JwtSecurityToken DecodeToken => _decodeToken();
        //protected virtual string? Lang => RouteData.Values[FixedCommonValue.Lang]?.ToString();


        //private JwtSecurityToken _decodeToken()
        //{
        //    return Token is null ?
        //          (this.Request.Headers.FirstOrDefault(x => x.Key == MrClaimTypes.Token) switch
        //          { { Key: var auth, Value: var value }
        //                when auth is not null && value.Count > 0 => new(value[0].Split(FixedCommonValue.Whitespace)[1]),
        //              _ => null

        //          }) : new JwtSecurityTokenHandler().ReadJwtToken(Token.Split(FixedCommonValue.Whitespace)[1]);
        //}
    }

    public class MrControllerBase<TKey> : MrControllerBase where TKey : struct, IEquatable<TKey>
    {
        protected virtual new  TKey? Key => User.CurrentUserId<TKey>();
    }

    public class MrControllerBase<TKey,TRepository> : MrControllerBase<TKey> where TKey : struct, IEquatable<TKey> where TRepository : IMrRepository
    {
        protected readonly TRepository repository;
        public MrControllerBase(TRepository repository) { this.repository = repository; }
    }
}
