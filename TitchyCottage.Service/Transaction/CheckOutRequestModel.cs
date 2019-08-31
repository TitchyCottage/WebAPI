using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service.Transaction
{
    public class CheckOutRequestModel
    {
        public string ShopId { get; set; }
        public int ProductID { get; set; }
        public long ProductQuantityID { get; set; }
        public decimal Quantity { get; set; }
        public bool Isreturn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
