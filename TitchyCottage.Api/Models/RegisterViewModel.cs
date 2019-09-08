using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TitchyCottage.Security.Model;
using RoleEnum = TitchyCottage.Utility.Enum.Type;

namespace TitchyCottage.Api.Models
{
    public class RegisterViewModel
    {
        public string Id { get; set; }
        //public string UserName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string CurrentPassword { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string ShopName { get; set; }
        [MaxLength(250)]
        public string AddressLine1 { get; set; }
        [MaxLength(250)]
        public string AddressLine2 { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Pincode { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public RegisterViewModel(ApplicationUser poco)
        {
            Id = poco.Id;
            //UserName = poco.UserName;
            FirstName = poco.FirstName;
            LastName = poco.LastName;
            Email = poco.Email;
            PhoneNumber = poco.PhoneNumber;
            ShopName = poco.ShopName;
            AddressLine1 = poco.AddressLine1;
            AddressLine2 = poco.AddressLine2;
            Latitude = poco.Latitude;
            Longitude = poco.Longitude;
            Pincode = poco.Pincode;
            City = poco.City;
            State = poco.State;
            IsActive = poco.IsActive;
            Type = poco.Type;
            CreatedBy = poco.CreatedBy;
            CreatedDate = poco.CreatedDate;
            ModifiedBy = poco.ModifiedBy;
            ModifiedDate = poco.ModifiedDate;

            //var tempRole = new List<string>();
            //foreach(var role in poco.Roles)
            //{
            //    tempRole.Add(role.RoleId);
            //}
            Role = poco.Roles.Any() ? (poco.Roles.First().RoleId == Convert.ToString((int)RoleEnum.Admin) ? "Admin" :
                               poco.Roles.First().RoleId == Convert.ToString((int)RoleEnum.Distributor) ? "Distributor" :
                               poco.Roles.First().RoleId == Convert.ToString((int)RoleEnum.Retailer) ? "Retailer" : "Manufacturer") : "";

        }


        public RegisterViewModel()
        {

        }
    }

}