using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TitchyCottage.Service.Transaction;

namespace TitchyCottage.Api.Controllers
{
    [RoutePrefix("api/Product")]
    public class TransactionController : ApiController
    {
        private TransactionService _transaction;
        public TransactionController()
        {
            _transaction = new TransactionService();
        }

        [Route("CheckInByShop")]
        public IHttpActionResult CheckInByShop(ShopTransactionModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_transaction.CheckInByShop(request));

        }

        [Route("CheckOutFromShop")]
        public IHttpActionResult CheckOutFromShop(CheckOutRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_transaction.CheckOutFromShop(request));

        }
    }
}
