using AV.Enumeration.JsonNet;
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
