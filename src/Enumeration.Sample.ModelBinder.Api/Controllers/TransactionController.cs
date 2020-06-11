using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AV.Enumeration.Sample.Version2;

namespace AV.Enumeration.Sample.ModelBinder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private static readonly List<ComplexTransaction> _transactions =
            new List<ComplexTransaction>();

        [HttpGet]
        [Route("code")]
        public string Get(PaymentType paymentType)
        {
            // return paymentType.Code;
            return paymentType.Code;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTransaction(ComplexTransaction transaction)
        {
            _transactions.Add(transaction);
            return Accepted();
        }
    }
}
