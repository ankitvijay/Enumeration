namespace AV.Enumeration.Sample.Version1
{
    public class Transaction
    {
        public double Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public Transaction(double amount, PaymentType paymentType)
        {
            Amount = amount;
            PaymentType = paymentType;
        }
    }
}
