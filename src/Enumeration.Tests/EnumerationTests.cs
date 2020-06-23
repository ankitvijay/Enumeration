using System;
using System.Collections.Generic;
using AV.Enumeration.Sample.Version2;
using Shouldly;
using Xunit;

namespace AV.Enumeration.Tests
{
    public class EnumerationTests
    {
        [Fact]
        public void EnumerationPropertiesAreSetCorrectly()
        {
            PaymentType.DebitCard.ShouldNotBeNull();
            PaymentType.DebitCard.Code.ShouldBe("DC");
            PaymentType.DebitCard.Name.ShouldBe(nameof(PaymentType.DebitCard));
            PaymentType.DebitCard.Value.ShouldBe(0);

            PaymentType.CreditCard.ShouldNotBeNull();
            PaymentType.CreditCard.Code.ShouldBe("CC");
            PaymentType.CreditCard.Name.ShouldBe(nameof(PaymentType.CreditCard));
            PaymentType.CreditCard.Value.ShouldBe(1);
        }

        [Fact]
        public void ToStringReturnsEnumerationName()
        {
            PaymentType.DebitCard.ToString().ShouldBe(nameof(PaymentType.DebitCard));
            PaymentType.CreditCard.ToString().ShouldBe(nameof(PaymentType.CreditCard));
        }

        [Fact]
        public void GetAllReturnsAllEnumerations()
        {
            var validEnumerations = new List<PaymentType> {PaymentType.DebitCard, PaymentType.CreditCard};
            Enumeration.GetAll<PaymentType>().ShouldAllBe(type => validEnumerations.Contains(type));
        }

        [Fact]
        public void EqualsComparesCorrectEnumeration()
        {
            var debitCard = PaymentType.DebitCard;

            PaymentType.DebitCard.Equals(debitCard).ShouldBeTrue();
            PaymentType.CreditCard.Equals(debitCard).ShouldBeFalse();

            (PaymentType.DebitCard == debitCard).ShouldBeTrue();
            (PaymentType.CreditCard == debitCard).ShouldBeFalse();
        }

        [Fact]
        public void EqualComparesCorrectEnumeration()
        {
            var debitCard = PaymentType.DebitCard;

            (debitCard != PaymentType.CreditCard).ShouldBeTrue();
            (debitCard != PaymentType.DebitCard).ShouldBeFalse();
        }

        [Fact]
        public void TryGetFromValueOrNameReturnsCorrectEnumeration()
        {
            Enumeration.TryGetFromValueOrName<PaymentType>(PaymentType.CreditCard.Value.ToString(),
                out var paymentTypeFromValue).ShouldBeTrue();
            paymentTypeFromValue.ShouldBe(PaymentType.CreditCard);

            Enumeration.TryGetFromValueOrName<PaymentType>(nameof(PaymentType.CreditCard), out var paymentTypeFromName).ShouldBeTrue();
            paymentTypeFromName.ShouldBe(PaymentType.CreditCard);

            Enumeration.TryGetFromValueOrName<PaymentType>("1000", out var invalidPaymentTypeValue).ShouldBeFalse();
            invalidPaymentTypeValue.ShouldBeNull();
        }

        [Fact]
        public void FromValueReturnsCorrectEnumeration()
        {
            Enumeration.FromValue<PaymentType>(0).ShouldBe(PaymentType.DebitCard);
            Enumeration.FromValue<PaymentType>(1).ShouldBe(PaymentType.CreditCard);

            Should.Throw<InvalidOperationException>(() => Enumeration.FromValue<PaymentType>(100));
        }

        [Fact]
        public void FromNameReturnsCorrectEnumeration()
        {
            Enumeration.FromName<PaymentType>(nameof(PaymentType.DebitCard)).ShouldBe(PaymentType.DebitCard);
            Enumeration.FromName<PaymentType>(nameof(PaymentType.CreditCard)).ShouldBe(PaymentType.CreditCard);

            Should.Throw<InvalidOperationException>(() => Enumeration.FromName<PaymentType>("INVALID_PAYMENT_TYPE"));
        }

        [Fact]
        public void CompareToReturnsCorrectValue()
        {
            var debitCard = PaymentType.DebitCard;
            var creditCard = PaymentType.CreditCard;

            PaymentType.DebitCard.CompareTo(debitCard).ShouldBe(0);
            PaymentType.CreditCard.CompareTo(debitCard).ShouldBe(1);
            PaymentType.DebitCard.CompareTo(creditCard).ShouldBe(-1);
        }
    }
}
