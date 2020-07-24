namespace AV.Enumeration.Sample.Inheritance.Example1
{
    public abstract class StatesPaymentType : PaymentType
    {
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