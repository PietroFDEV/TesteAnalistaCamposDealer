using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

public class SwaggerApiKeyHeaderOperationFilter : IOperationFilter
{
    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    {
        if (operation.parameters == null)
            operation.parameters = new List<Parameter>();

        operation.parameters.Add(new Parameter
        {
            name = "X-API-KEY",
            @in = "header",
            type = "string",
            required = true,
            description = "API Key"
        });
    }
}
