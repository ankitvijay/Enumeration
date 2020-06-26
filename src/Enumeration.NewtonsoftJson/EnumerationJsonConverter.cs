using System;
using System.Reflection;
using Newtonsoft.Json;

namespace AV.Enumeration.NewtonsoftJson
{
    /// <summary>
    /// Converts an <see cref="Enumeration"/> to or from JSON.
    /// </summary>
    public class EnumerationJsonConverter : JsonConverter<Enumeration>
    {
        /// <summary>
        /// Writes a specified <see cref="Enumeration"/> value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The <see cref="Enumeration"/> value to convert to the JSON.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, Enumeration value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.Name);
            }
        }

        /// <summary>
        /// Reads and converts the JSON to type <see cref="Enumeration"/>
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="objectType">The type to convert.</param>
        /// <param name="existingValue">An object that specifies serialization options to use.</param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The converted value of type <see cref="Enumeration"/>.</returns>
        public override Enumeration ReadJson(JsonReader reader,
            Type objectType,
            Enumeration existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.Integer => GetEnumerationFromJson(reader.Value.ToString(), objectType),
                JsonToken.String => GetEnumerationFromJson(reader.Value.ToString(), objectType),
                JsonToken.Null => null,

                _ => throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an enumeration")
            };
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
                    throw new JsonSerializationException("Serialization is not supported");
                }

                var genericMethod = methodInfo.MakeGenericMethod(objectType);

                var arguments = new[] { nameOrValue, result };

                genericMethod.Invoke(null, arguments);
                return arguments[1] as Enumeration;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException($"Error converting value '{nameOrValue}' to a enumeration.", ex);
            }
        }
    }
}
