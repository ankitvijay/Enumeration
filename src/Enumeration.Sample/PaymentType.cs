namespace AV.Enumeration.Sample.Version1
{
    public class PaymentType : Enumeration
    {
        public static readonly PaymentType DirectDebit = new PaymentType(1, nameof(DirectDebit));

        public static readonly PaymentType CreditCard = new PaymentType(1, nameof(CreditCard));

        private PaymentType(int value, string name) : base(value, name)
        {
        }
    }
}