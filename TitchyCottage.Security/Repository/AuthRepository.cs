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

        public async Task<IdentityResult> RegisterUser(ApplicationUser userModel,string password,  string role)
        {

            try
            {
                userModel.Type = role == Convert.ToString(Type.Admin) ? (int)Type.Admin:
                    role == Convert.ToString(Type.Distributor) ? (int)Type.Distributor :
                    role == Convert.ToString(Type.Manufacturer) ? (int)Type.Manufacturer : (int)Type.Retailer;

                var result = await _userManager.CreateAsync(userModel, password);
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null && result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user.Id, role);
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

        public ResultListModel<ApplicationUser> GetRetailerByDistributor()
        {
            ResultListModel<ApplicationUser> result = new ResultListModel<ApplicationUser>();
            try
            {
                var data = _ctx.Users.Where(_ => _.Type == (int)Type.Retailer && _.IsActive).ToList();
                result.Data = data;
                result.success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser userModel, string role)
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

                    user.Type = role == Convert.ToString(Type.Admin) ? (int)Type.Admin :
                    role == Convert.ToString(Type.Distributor) ? (int)Type.Distributor :
                    role == Convert.ToString(Type.Manufacturer) ? (int)Type.Manufacturer : (int)Type.Retailer;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrWhiteSpace(role))
                {
                    //var oldRoles = new List<string>();
                    //foreach(var item in user.Roles)
                    //{
                    //    oldRoles.Add(item.RoleId == Convert.ToString((int)Type.Admin) ? Convert.ToString(Type.Admin) : item.RoleId == Convert.ToString((int)Type.Distributor) ? Convert.ToString(Type.Distributor) : Convert.ToString(Type.Retailer));
                    //}

                    if (user.Roles.Any())
                    {
                        var roleString = user.Roles.First().RoleId;
                        await _userManager.RemoveFromRoleAsync(user.Id, roleString == Convert.ToString((int)Type.Admin) ? Convert.ToString(Type.Admin) : roleString == Convert.ToString((int)Type.Distributor) ? Convert.ToString(Type.Distributor): roleString == Convert.ToString((int)Type.Manufacturer) ? Convert.ToString(Type.Manufacturer) : Convert.ToString(Type.Retailer));
                    }

                    result = await _userManager.AddToRoleAsync(user.Id,role);
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
                    Id = (int)Type.Manufacturer,
                    Value = Type.Manufacturer.ToString()
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
