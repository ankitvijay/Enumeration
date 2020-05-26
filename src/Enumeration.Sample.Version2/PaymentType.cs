namespace AV.Enumeration.Sample.Version2
{
    public abstract class PaymentType : Enumeration
    {
        public static readonly PaymentType DebitCard = new DebitCardType();

        public static readonly PaymentType CreditCard = new CreditCardType();

        public abstract string Code { get; }

        private PaymentType(int value, string name = null) : base(value, name)
        {
        }

        private class DebitCardType : PaymentType
        {
            public DebitCardType() : base(0, nameof(DebitCardType))
            {
            }

            public override string Code => "DC";
        }

        private class CreditCardType : PaymentType
        {
            public CreditCardType() : base(1, nameof(CreditCardType))
            {
            }

            public override string Code => "CC";
        }
    }
}
