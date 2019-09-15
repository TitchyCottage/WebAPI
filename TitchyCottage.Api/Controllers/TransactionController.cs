using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using TitchyCottage.Service.Transaction;

namespace TitchyCottage.Api.Controllers
{
    [RoutePrefix("api/Transaction")]
    public class TransactionController : ApiController
    {
        private TransactionService _transaction;
        private string userId = string.Empty;
        public TransactionController()
        {
            _transaction = new TransactionService();
            userId = ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId") != null ? ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId").Value : string.Empty;
        }

        [Route("CheckInByShop")]
        [HttpPost]
        public IHttpActionResult CheckInByShop(ShopTransactionModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                request.CreatedBy = userId;
            }
            return Ok(_transaction.CheckInByShop(request));

        }

        [Route("CheckOutFromShop")]
        [HttpPost]
        public IHttpActionResult CheckOutFromShop(CheckOutRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                request.CreatedBy = userId;
            }
            return Ok(_transaction.CheckOutFromShop(request));

        }

        [HttpPost]
        [Route("GetShopInformationForDistibutor")]
        public IHttpActionResult GetShopInformationForDistibutor(DistibutorDashboardRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(request.DistibutorId))
            {
                request.DistibutorId = userId;
            }
            return Ok(_transaction.GetShopInformationForDistibutor(request));

        }
    }
}
