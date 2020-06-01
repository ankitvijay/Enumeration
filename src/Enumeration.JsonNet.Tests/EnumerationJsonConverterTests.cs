using AV.Enumeration.Sample.Version2;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace AV.Enumeration.JsonNet.Tests
{
    public class EnumerationJsonConverterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public EnumerationJsonConverterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EnumerationIsSerializesAndDeserializesCorrectly()
        {
            var expected = new Transaction
            {
                Amount = 100,
                PaymentType = PaymentType.CreditCard
            };

            var json = JsonConvert.SerializeObject(expected, Formatting.Indented, new EnumerationJsonConverter());

            _testOutputHelper.WriteLine(json);

            var actual = JsonConvert.DeserializeObject<Transaction>(json, new EnumerationJsonConverter());

            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.PaymentType, actual.PaymentType);
        }

        [Fact]
        public void CanConvertReturnsTrueForEnumeration()
        {
            var converter = new EnumerationJsonConverter();

            Assert.True(converter.CanConvert(typeof(PaymentType)));
            Assert.False(converter.CanConvert(typeof(string)));
            Assert.False(converter.CanConvert(typeof(int)));
        }
    }
}
