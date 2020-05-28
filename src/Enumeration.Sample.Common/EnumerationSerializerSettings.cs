using AV.Enumeration.JsonNet;
using Newtonsoft.Json;

namespace Enumeration.Sample.Common
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
