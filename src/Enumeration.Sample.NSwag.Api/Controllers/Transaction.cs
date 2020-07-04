using AV.Enumeration.Sample.Version2;

namespace AV.Enumeration.Sample.NSwag.Api.Controllers
{
    public class Transaction
    {
        public PaymentType PaymentType { get; set; }
        
        public decimal Amount { get; set; }
    }
}