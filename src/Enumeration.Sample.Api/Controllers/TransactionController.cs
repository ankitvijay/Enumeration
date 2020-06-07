using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Enumeration.SampleJsonNet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private static readonly List<ComplexTransaction> s_Transactions = new List<ComplexTransaction>();
        
        [HttpPost]
        [Route("create")]
        public IActionResult CreateTransaction(ComplexTransaction transaction)
        {
            s_Transactions.Add(transaction);
            return Accepted();
        }

        [HttpPost]
        [Route("get")]
        public ActionResult<IEnumerable<ComplexTransaction>> GetTransactions()
        {
            return s_Transactions;
        }
    }
}
