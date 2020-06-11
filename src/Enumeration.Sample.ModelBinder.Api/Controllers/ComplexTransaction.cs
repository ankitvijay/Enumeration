using System.Collections.Generic;
using AV.Enumeration.Sample.Common;
using AV.Enumeration.Sample.Version2;
using PaymentType = AV.Enumeration.Sample.Common.PaymentType;

namespace AV.Enumeration.Sample.ModelBinder.Api.Controllers
{
    public class ComplexTransaction
    {
        public List<Transaction> Transactions { get; set; }

        public Version2.PaymentType PaymentType { get; set; }

        PaymentType EnumPaymentType { get; set; }
    }
}