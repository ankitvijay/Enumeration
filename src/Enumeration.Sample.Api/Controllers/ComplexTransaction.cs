using System.Collections.Generic;
using AV.Enumeration.Sample.Common;
using AV.Enumeration.Sample.Version2;
using PaymentType = AV.Enumeration.Sample.Common.PaymentType;

namespace AV.Enumeration.SampleJsonNet.Api.Controllers
{
    public class ComplexTransaction
    {
        public List<Transaction> Transactions { get; set; }

        public List<Sample.Version2.PaymentType> PaymentTypes { get; set; }

        public List<PaymentType> EnumPaymentTypes { get; set; }
    }
}