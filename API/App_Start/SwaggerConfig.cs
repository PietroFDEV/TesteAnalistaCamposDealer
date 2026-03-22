using System.Web.Http;
using Swashbuckle.Application;

public class SwaggerConfig
{
    public static void Register()
    {
        GlobalConfiguration.Configuration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Campos Dealer API");
                c.ApiKey("apiKey")
                    .Description("API Key")
                    .Name("X-API-KEY")
                    .In("header");
                c.OperationFilter<SwaggerApiKeyHeaderOperationFilter>();
            })
            .EnableSwaggerUi();
    }
}
