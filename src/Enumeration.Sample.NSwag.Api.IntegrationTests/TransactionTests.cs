using System.Net.Http;
using MyNamespace;
using Shouldly;
using Xunit;

namespace AV.Enumeration.Sample.NSwag.Api.IntegrationTests
{
    public class TransactionTests
    {
        [Fact]
        public void CreateTransactionIsCalledSuccessfully()
        {
            var client = new Client("https://localhost:5001", new HttpClient());
            Should.NotThrow(async () => await client.CreateAsync(new Transaction()
            {
                Amount = 500,
                PaymentType = PaymentType.CreditCard
            }));
        }
    }
}