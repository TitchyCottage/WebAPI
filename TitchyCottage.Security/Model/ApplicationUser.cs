using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Security.Model
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
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
    }
}
