using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitchyCottage.Security.Model;
using TitchyCottage.Security.DbContext;
using NLog;
using Type = TitchyCottage.Utility.Enum.Type;

namespace TitchyCottage.Security.Repository
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AuthContext()));
        }

        public async Task<IdentityResult> RegisterUser(ApplicationUser userModel,string password,  string[] roles)
        {

            try
            {
                var result = await _userManager.CreateAsync(userModel, password);
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null && result.Succeeded)
                {
                    result = await _userManager.AddToRolesAsync(user.Id, roles);
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new IdentityResult(ex.Message);
            }
           
        }

        public async Task<ResultModel<ApplicationUser>> GetUserAsync(string email)
        {
            var result = new ResultModel<ApplicationUser>();
            try
            {
                result.Data = await _userManager.FindByEmailAsync(email);
                result.success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return result;
        }


        public ResultListModel<ApplicationUser> GetAllDistributor()
        {
            ResultListModel<ApplicationUser> result = new ResultListModel<ApplicationUser>();
            try
            {
                var data  = _ctx.Users.Where(_ => _.Type == (int)Type.Distributor).ToList(); 
                result.Data = data;
                result.success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return result;
        }

        public ResultListModel<ApplicationUser> GetRetailerByDistributor(string userId)
        {
            ResultListModel<ApplicationUser> result = new ResultListModel<ApplicationUser>();
            try
            {
                var data = _ctx.Users.Where(_ => _.Type == (int)Type.Retailer && _.CreatedBy == userId).ToList();
                result.Data = data;
                result.success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser userModel, string[] roles)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null)
                {
                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    user.ShopName = userModel.ShopName;
                    user.AddressLine1 = userModel.AddressLine1;
                    user.AddressLine2 = userModel.AddressLine2;
                    user.Latitude = userModel.Latitude;
                    user.Longitude = userModel.Longitude;
                    user.Pincode = userModel.Pincode;
                    user.City = userModel.City;
                    user.State = userModel.State;
                    user.IsActive = userModel.IsActive;
                    user.Type = userModel.Type;
                    user.ModifiedBy = userModel.ModifiedBy;
                    user.ModifiedDate = userModel.ModifiedDate;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && roles.Length > 0)
                {
                    var oldRoles = new List<string>();
                    foreach(var item in user.Roles)
                    {
                        oldRoles.Add(item.RoleId == Convert.ToString((int)Type.Admin) ? Convert.ToString(Type.Admin) : item.RoleId == Convert.ToString((int)Type.Distributor) ? Convert.ToString(Type.Distributor) : Convert.ToString(Type.Retailer));
                    }

                    if (oldRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user.Id, oldRoles.ToArray());
                    }

                    result = await _userManager.AddToRolesAsync(user.Id,roles);
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new IdentityResult(ex.Message);
            }


        }

        public IList<TitchyCottageType> GetTitchyCottageType()
        {
            IList<TitchyCottageType> result = new List<TitchyCottageType>
            {
                new TitchyCottageType
                {
                    Id = (int)Type.Distributor,
                    Value = Type.Distributor.ToString()
                },
                new TitchyCottageType
                {
                    Id = (int)Type.Retailer,
                    Value = Type.Retailer.ToString()
                }
            };

            return result;
        }

        public IList<TitchyCottageType> GetTitchyCottagRoles()
        {
            IList<TitchyCottageType> result = new List<TitchyCottageType>
            {
                new TitchyCottageType
                {
                    Id = (int)Type.Admin,
                    Value = Type.Admin.ToString()
                },
                new TitchyCottageType
                {
                    Id = (int)Type.Distributor,
                    Value = Type.Distributor.ToString()
                },
                new TitchyCottageType
                {
                    Id = (int)Type.Retailer,
                    Value = Type.Retailer.ToString()
                }
            };

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
