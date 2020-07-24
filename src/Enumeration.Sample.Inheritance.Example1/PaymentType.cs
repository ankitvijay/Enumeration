namespace AV.Enumeration.Sample.Inheritance.Example1
{
    public abstract class PaymentType : Enumeration
    {
        public static readonly PaymentType DebitCard = new DebitCardType();

        public static readonly PaymentType CreditCard = new CreditCardType();

        public abstract string Code { get; }

        protected PaymentType(int value, string name = null) : base(value, name)
        {
        }

        private class DebitCardType : PaymentType
        {
            public DebitCardType() : base(0, "DebitCard")
            {
            }

            public override string Code => "DC";
        }

        private class CreditCardType : PaymentType
        {
            public CreditCardType() : base(1, "CreditCard")
            {
            }

            public override string Code => "CC";
        }
    }
}