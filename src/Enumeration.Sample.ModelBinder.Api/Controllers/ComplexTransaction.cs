using System.Collections.Generic;
using AV.Enumeration.Sample.Version2;

namespace AV.Enumeration.Sample.ModelBinder.Api.Controllers
{
    public class ComplexTransaction
    {
        public List<Transaction> Transactions { get; set; }

        public PaymentType PaymentType { get; set; }
        EnumPaymentType EnumPaymentType { get; set; }
    }
}