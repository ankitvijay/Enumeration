using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Enumeration.Sample.Version2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enumeration.Sample.ModelBinder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        [Route("code")]
        public string Get(PaymentType paymentType)
        {
            // return paymentType.Code;
            return paymentType.Code;
        }
    }
}
