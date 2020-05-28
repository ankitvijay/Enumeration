using System.Text.Json;
using AV.Enumeration.Sample.Version2;
using Xunit;

namespace AV.Enumeration.SystemTextJson.Tests
{
    public class EnumerationJsonConverterTests
    {
        [Fact]
        public void CanConvertReturnsTrueForEnumeration()
        {
            var converter = new EnumerationJsonConverter();

            Assert.True(converter.CanConvert(typeof(PaymentType)));
            Assert.False(converter.CanConvert(typeof(string)));
            Assert.False(converter.CanConvert(typeof(int)));
        }

        [Fact]
        public void EnumerationIsSerializesAndDeserializesCorrectly()
        {
            //Arrange
            var expected = new Transaction
            {
                Amount = 100,
                PaymentType = PaymentType.CreditCard
            };

            // Act
            var json = JsonSerializer.Serialize(expected,
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new EnumerationJsonConverter()
                    }
                });

            // Act
            var actual= JsonSerializer.Deserialize<Transaction>(json, new JsonSerializerOptions()
            {
                Converters = { new EnumerationJsonConverter() }
            });

            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.PaymentType, actual.PaymentType);
        }
    }
}
