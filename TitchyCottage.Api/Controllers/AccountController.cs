using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using TitchyCottage.Security.Model;
using TitchyCottage.Security.Repository;
using AllowAnonymousAttribute = System.Web.Http.AllowAnonymousAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using TitchyCottage.Api.Models;
using NLog;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using System.Security.Claims;

namespace TitchyCottage.Api.Controllers
{
    /// <summary>
    /// Account
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        private string userId = string.Empty;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Account
        /// </summary>
        public AccountController()
        {
            userId = ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId") !=null ? ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId") .Value: string.Empty; 
            _repo = new AuthRepository();
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //[Authorize]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel request)
        {
            logger.Info("Hell You have visited the Index view" + Environment.NewLine + DateTime.Now);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.Role))
            {
                return BadRequest("ID or Role is empty");
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email.Substring(0, request.Email.IndexOf("@")),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ShopName = request.ShopName,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Pincode= request.Pincode,
                City = request.City,
                State = request.State,
                IsActive = request.IsActive,
                Type = request.Type,
                CreatedBy = !string.IsNullOrWhiteSpace(request.CreatedBy) ?request.CreatedBy : userId,
                CreatedDate = DateTime.Now
            };

            IdentityResult result = await _repo.RegisterUser(user, request.Password, request.Role);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return Ok(errorResult);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LogInModel request)
        {
            logger.Info("Hell You have visited the Index view" + Environment.NewLine + DateTime.Now);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser result = await _repo.FindUser(request.Email, request.Password);

            return Ok(result);

        }

        /// <summary>
        /// Method used to get user details 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("GetUserAsync")]
        public async Task<IHttpActionResult> GetUserAsync(string email)
        {
            ResultModel<ApplicationUser> result = await _repo.GetUserAsync(email);
            ResultModel<RegisterViewModel> response = new ResultModel<RegisterViewModel>();
            if (result.success)
            {
                try
                {
                    RegisterViewModel data = new RegisterViewModel(result.Data);
                    response.success = true;
                    response.Data = data;
                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
           
            return Ok(response);
        }

        /// <summary>
        /// Method used to update user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("UpdateUserAsync")]
        public async Task<IHttpActionResult> UpdateUserAsync(RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.Role))
            {
                return BadRequest("ID or Role is empty");
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email.Substring(0, request.Email.IndexOf("@")),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ShopName = request.ShopName,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Pincode = request.Pincode,
                City = request.City,
                State = request.State,
                IsActive = request.IsActive,
                Type = request.Type,
                ModifiedBy = userId,
                ModifiedDate = DateTime.Now
            };


            IdentityResult result = await _repo.UpdateUserAsync(user, request.Role);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return Ok("");
            }

            return Ok(result);
        }

        /// <summary>
        /// Method used to get all distibuter
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("GetDistributor")]
        public IHttpActionResult GetDistributor()
        {
            ResultListModel<ApplicationUser> result =  _repo.GetAllDistributor();
            ResultListModel<RegisterViewModel> response = new ResultListModel<RegisterViewModel>();
            if (result.success)
            {
                try
                {
                    List<RegisterViewModel> data = new List<RegisterViewModel>();
                    data = (from item in result.Data
                            select new RegisterViewModel(item)).ToList();
                    response.Data = data;
                    response.success = true;
                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
         
            return Ok(response);
        }

        /// <summary>
        /// Methos used to get the Retailer
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("GetRetailerByDistributor")]
        public IHttpActionResult GetRetailerByDistributor()
        {
            ResultListModel<ApplicationUser> result = _repo.GetRetailerByDistributor(userId);
            ResultListModel<RegisterViewModel> response = new ResultListModel<RegisterViewModel>();
            if (result.success)
            {
                try
                {
                    List<RegisterViewModel> data = new List<RegisterViewModel>();
                    data = (from item in result.Data
                            select new RegisterViewModel(item)).ToList();
                    response.Data = data;
                    response.success = true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Methos used to get the type
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("GetTitchyCottageType")]
        public IHttpActionResult GetTitchyCottageType()
        {
            return Ok(_repo.GetTitchyCottageType());
        }

        /// <summary>
        /// Method used to get the roles
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("GetTitchyCottagRoles")]
        public IHttpActionResult GetTitchyCottagRoles()
        {
            return Ok(_repo.GetTitchyCottagRoles());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}