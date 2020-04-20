using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace HVIT.Security
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        public AuthorizeUserAttribute()
        {
            AccessLevel = "";
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            try
            {
                TokenProvider tokenProvider = new TokenProvider();
                TokenIdentity tokenIdentity = new TokenIdentity();
                tokenIdentity.UserAgent = context.Request.Headers.UserAgent.ToString();

                if (context.Request.Headers.Referrer != null)
                    tokenIdentity.IP = context.Request.Headers.Referrer.Authority;

                if (context.Request.Headers.Contains("access_token"))
                {
                    tokenIdentity.Token = context.Request.Headers.GetValues("access_token").FirstOrDefault();
                }
                if (!tokenProvider.ValidateToken(ref tokenIdentity))
                {
                    context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    HttpContext.Current.User = new System.Security.Claims.ClaimsPrincipal(tokenIdentity);
                }
                base.OnAuthorization(context);
            }
            catch (System.NullReferenceException ex)
            {
                context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthorized = base.IsAuthorized(actionContext);
            if (!isAuthorized)
            {
                return false;
            }

            string privilegeLevels = string.Join("", "abc");

            return privilegeLevels.Contains(this.AccessLevel);
        }
    }
}