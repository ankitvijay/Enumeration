using System;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AV.Enumeration.NSwag
{
    /// <summary>
    /// Implements <see cref="ISchemaFilter"/> to transform <see cref="Enumeration"/> schema to <see cref="Enum"/>.
    /// </summary>
    public class EnumerationToEnumSchemaFilter : ISchemaFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsSubclassOf(typeof(Enumeration)))
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