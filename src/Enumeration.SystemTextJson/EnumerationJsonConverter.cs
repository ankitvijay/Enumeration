using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AV.Enumeration.SystemTextJson
{
    public class EnumerationJsonConverter : JsonConverter<Enumeration>
    {
        private const string NameProperty = "Name";

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Enumeration));
        }

        public override Enumeration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                case JsonTokenType.String:
                    return GetEnumerationFromJson(reader.GetString(), typeToConvert);
                case JsonTokenType.Null:
                    return null;
                default:
                    throw new JsonException(
                        $"Unexpected token {reader.TokenType} when parsing the enumeration.");
            }
        }

        public override void Write(Utf8JsonWriter writer, Enumeration value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull(NameProperty);
            }
            else
            {
                var name = value.GetType().GetProperty(NameProperty, BindingFlags.Public | BindingFlags.Instance);
                if (name == null)
                {
                    throw new JsonException($"Error while writing JSON for {value}");
                }

                writer.WriteStringValue(name.GetValue(value).ToString());
            }
        }

        private static Enumeration GetEnumerationFromJson(string nameOrValue, Type objectType)
        {
            try
            {
                object result = default;
                var methodInfo = typeof(Enumeration).GetMethod(
                    nameof(Enumeration.TryGetFromValueOrName)
                    , BindingFlags.Static | BindingFlags.Public);

                if (methodInfo == null)
                {
                    throw new JsonException("Serialization is not supported");
                }

                var genericMethod = methodInfo.MakeGenericMethod(objectType);

                var arguments = new[] { nameOrValue, result };

                genericMethod.Invoke(null, arguments);
                return arguments[1] as Enumeration;
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error converting value '{nameOrValue}' to a enumeration.", ex);
            }
        }
    }
}
