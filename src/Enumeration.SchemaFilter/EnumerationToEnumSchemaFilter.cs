using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Enumeration.SchemaFilter
{
    /// <inheritdoc />
    public class EnumerationToEnumSchemaFilter : ISchemaFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsSubclassOf(typeof(AV.Enumeration.Enumeration)))
            {
                return;
            }

            var fields = context.Type.GetFields(BindingFlags.Static | BindingFlags.Public);

            schema.Enum = fields.Select(field => new OpenApiString(field.Name)).Cast<IOpenApiAny>().ToList();
            schema.Type = "string";
            schema.Properties = null;
            schema.AllOf = null;
        }
    }
}
