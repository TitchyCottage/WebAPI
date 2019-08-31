using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service.Transaction
{
    public class ShopTransactionModel
    {
        public long ID { get; set; }
        public string ShopID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantityID { get; set; }
        public decimal Quantity { get; set; }
        public string CreatedBy { get; set; }
    }
}
