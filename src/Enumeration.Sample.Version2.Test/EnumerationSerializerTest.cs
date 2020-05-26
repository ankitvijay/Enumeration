using AV.Enumeration.Sample.Version2;
using Enumeration.Sample.Common;
using Newtonsoft.Json;
using Xunit;

namespace Enumeration.Sample.Version2.Test
{
    public class EnumerationSerializerTest
    {
        [Fact]
        public void EnumerationSerializesCorrectly()
        {
            var expected = new Transaction(300, PaymentType.CreditCard);
            var settings = new EnumerationSerializerSettings();

            // Act
            var json = JsonConvert.SerializeObject(expected, settings);
            var actual = JsonConvert.DeserializeObject<Transaction>(json, settings);

            Assert.NotNull(actual);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.PaymentType, actual.PaymentType);
        }
    }
}
