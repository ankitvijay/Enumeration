using System.Collections.Generic;
using AV.Enumeration.Sample.Version2;

namespace Enumeration.SampleJsonNet.Api.Controllers
{
    public class ComplexTransaction
    {
        public List<Transaction> Transactions { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }
        public List<EnumPaymentType> EnumPaymentTypes { get; set; }
    }
}