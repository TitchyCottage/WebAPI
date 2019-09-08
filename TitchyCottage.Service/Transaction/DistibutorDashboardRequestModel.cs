using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service.Transaction
{
    public class DistibutorDashboardRequestModel
    {
        public string ProductName { get; set; }
        public string Lot { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string DistibutorId { get; set; }
    }

    public class DistibutorDashboardResponseModel
    {
        public string ShopName { get; set; }
        public string ProductName { get; set; }
        public string Lot { get; set; }
        public DateTime? ManufacturerDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal StockInQuantity { get; set; }
        public decimal? SoldOutQuantity { get; set; }

    }
}
