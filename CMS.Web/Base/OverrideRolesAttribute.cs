using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HVITCore.Controllers
{
    public class OverrideRolesAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (principal.Identity.IsAuthenticated)
            {
                return Task.FromResult<object>(null);
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Access Denied");
            return Task.FromResult<object>(null);
        }
    }
}