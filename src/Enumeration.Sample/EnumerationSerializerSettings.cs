using AV.Enumeration.JsonNet;
using Newtonsoft.Json;

namespace AV.Enumeration.Sample.Version1
{
    public class EnumerationSerializerSettings : JsonSerializerSettings
    {
        public EnumerationSerializerSettings()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            Converters = new JsonConverter[]
            {
                new EnumerationNameConverter()
            };
        }
    }
}
