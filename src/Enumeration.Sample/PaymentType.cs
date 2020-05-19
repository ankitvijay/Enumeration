using System.Runtime.CompilerServices;

namespace AV.Enumeration.Sample.Version1
{
    public class PaymentType : Enumeration
    {
        public static readonly PaymentType DebitCard = new PaymentType(0);

        public static readonly PaymentType CreditCard = new PaymentType(1);

        private PaymentType(int value, [CallerMemberName] string name = null) : base(value, name)
        {
        }
    }
}