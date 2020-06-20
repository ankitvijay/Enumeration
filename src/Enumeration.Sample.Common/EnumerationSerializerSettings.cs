using AV.Enumeration.NewtonsoftJson;
using Newtonsoft.Json;

namespace AV.Enumeration.Sample.Common
{
    public class EnumerationSerializerSettings : JsonSerializerSettings
    {
        public EnumerationSerializerSettings()
        {
            Converters = new JsonConverter[]
            {
                new EnumerationJsonConverter()
            };
        }
    }
}
