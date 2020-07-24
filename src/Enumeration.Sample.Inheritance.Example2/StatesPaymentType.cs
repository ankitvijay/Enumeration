namespace AV.Enumeration.Sample.Inheritance.Example2
{
    public abstract class StatesPaymentType : PaymentType
    {
        public new static readonly PaymentType DebitCard = new DebitCardType();

        public new static readonly PaymentType CreditCard = new CreditCardType();

        public static readonly PaymentType Bitcoin = new BitCoinType();

        private class BitCoinType : PaymentType
        {
            public BitCoinType() : base(3, "Bitcoin")
            {
            }

            public override string Code => "BT";
        }

        protected StatesPaymentType(int value, string name = null) : base(value, name)
        {
        }
    }
}