using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service
{
    public class ProductQuantityModel
    {
        public long ID { get; set; }
        public int ProductID { get; set; }
        public string Lot { get; set; }
        public string DateCode { get; set; }
        public decimal? TotalQuantity { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime? ManufacturerDate { get; set; }
        public decimal? StockInQuantity { get; set; }
        public decimal? ShopInQuantity { get; set; }
        public decimal? SoldOutQuantity { get; set; }
        public string CreatedBy { get; set; }
    }
}
