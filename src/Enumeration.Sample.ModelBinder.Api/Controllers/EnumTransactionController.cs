using AV.Enumeration.Sample.Common;
using Microsoft.AspNetCore.Mvc;

namespace AV.Enumeration.Sample.ModelBinder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnumTransactionController
    {
        [HttpGet]
        [Route("code")]
        public string Get(PaymentType paymentType)
        {
            return paymentType == PaymentType.CreditCard ? "CC" : "DC";
        }
    }
}
