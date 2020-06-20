using System;

namespace AV.Enumeration.Sample.Before
{
    public enum PaymentType
    {
        DebitCard = 0,
        CreditCard = 1
    }

    public static class PaymentTypeExtensions
    {
        public static string GetPaymentTypeCode(this PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.CreditCard:
                    return "CC";
                case PaymentType.DebitCard:
                    return "DC";
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(paymentType), paymentType, "Invalid payment type");
            }
        }
    }
}
