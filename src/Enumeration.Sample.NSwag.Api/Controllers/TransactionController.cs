using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AV.Enumeration.Sample.NSwag.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private static readonly List<Transaction> s_Transactions = new List<Transaction>();
        
        [HttpPost]
        [Route("create")]
        public IActionResult CreateTransaction(Transaction transaction)
        {
            s_Transactions.Add(transaction);
            return Ok();
        }

        [HttpPost]
        [Route("get")]
        public ActionResult<IEnumerable<Transaction>> GetTransactions()
        {
            return s_Transactions;
        }
    }
}
