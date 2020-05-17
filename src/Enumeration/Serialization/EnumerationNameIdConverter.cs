using System;
using System.Reflection;
using Newtonsoft.Json;

namespace AnkitVijay.Enumeration.Serialization
{
    public class EnumerationNameConverter : JsonConverter
    {
        private const string NameProperty = "Name";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
            }
            else
            {
                var name = value.GetType().GetProperty(NameProperty, BindingFlags.Public | BindingFlags.Instance);
                if (name == null)
                {
                    throw new JsonSerializationException($"Error while writing JSON for {value}");
                }

                writer.WriteValue(name.GetValue(value));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.String:
                    return GetFromNameOrValue(reader.Value.ToString(), objectType);
                case JsonToken.StartObject:
                case JsonToken.Null:
                    return null;
                default:
                    throw new JsonSerializationException(
                        $"Unexpected token {reader.TokenType} when parsing a smart enum.");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Enumeration));
        }

        private static object GetFromNameOrValue(string nameOrValue, Type objectType)
        {
            try
            {
                object result = default;
                var methodInfo = typeof(Enumeration).GetMethod(
                    nameof(Enumeration.TryGetFromValueOrName)
                    , BindingFlags.Static | BindingFlags.Public);

                if (methodInfo == null)
                {
                    throw new JsonSerializationException("Serialization is not supported");
                }

                var genericMethod = methodInfo.MakeGenericMethod(objectType);

                var arguments = new[] { nameOrValue, result };

                genericMethod.Invoke(null, arguments);
                return arguments[1];
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value '{nameOrValue}' to a enumeration.", ex);
            }
        }
    }
}
