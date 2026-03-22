using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class ApiKeyAuthorizeAttribute : AuthorizationFilterAttribute
{
    private const string HeaderName = "X-API-KEY";

    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var expectedKey = ConfigurationManager.AppSettings["ApiKey"];

        if (string.IsNullOrWhiteSpace(expectedKey))
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                "API key not configured"
            );
            return;
        }

        if (!actionContext.Request.Headers.TryGetValues(HeaderName, out var values))
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                "API key missing"
            );
            return;
        }

        var providedKey = values.FirstOrDefault();

        if (!string.Equals(providedKey, expectedKey))
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                "API key invalid"
            );
        }
    }
}
