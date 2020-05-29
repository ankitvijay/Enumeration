using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Enumeration.Sample.Version2;
using Microsoft.AspNetCore.Mvc;

namespace Enumeration.Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private static readonly List<ComplexTransaction> _transactions = new List<ComplexTransaction>();
        
        [HttpPost]
        [Route("create")]
        public IActionResult CreateTransaction(ComplexTransaction transaction)
        {
            _transactions.Add(transaction);
            return Accepted();
        }

        [HttpPost]
        [Route("get")]
        public ActionResult<IEnumerable<ComplexTransaction>> GetTransactions()
        {
            return _transactions;
        }
    }

    public class ComplexTransaction
    {
        public List<Transaction> Transactions { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }
        public List<EnumPaymentType> EnumPaymentTypes { get; set; }
    }

    public enum EnumPaymentType
    {
        DebitCard = 0,
        CreditCard = 1
    }
}
