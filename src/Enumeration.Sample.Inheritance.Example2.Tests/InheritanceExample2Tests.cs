using System;
using AV.Enumeration.Sample.Inheritance.Example2;
using Xunit;
namespace Enumeration.Sample.Inheritance.Example2.Tests
{
    public class InheritanceExample2Tests
    {
        [Fact]
        public void CanReadAllPaymentTypesForUnitedStates()
        {
            Assert.Equal("CC", StatesPaymentType.CreditCard.Code);
            Assert.Equal("DC", StatesPaymentType.DebitCard.Code);
            Assert.Equal("BT", StatesPaymentType.Bitcoin.Code);
        }

        [Fact]
        public void CommonPaymentTypesAreEqual()
        {
            Assert.Equal(PaymentType.CreditCard, StatesPaymentType.CreditCard);
            Assert.Equal(PaymentType.DebitCard, StatesPaymentType.DebitCard);
        }

        [Fact]
        public void CanReadAllPaymentTypesForRestOfTheWorld()
        {
            Assert.Equal("CC", PaymentType.CreditCard.Code);
            Assert.Equal("DC", PaymentType.DebitCard.Code);
        }
    }
}