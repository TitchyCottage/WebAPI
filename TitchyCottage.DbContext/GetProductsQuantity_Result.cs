//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TitchyCottage.DbContext
{
    using System;
    
    public partial class GetProductsQuantity_Result
    {
        public long ID { get; set; }
        public int ProductID { get; set; }
        public string Lot { get; set; }
        public string DateCode { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public System.DateTime ExpiredDate { get; set; }
        public Nullable<decimal> StockInQuantity { get; set; }
        public Nullable<decimal> ShopInQuantity { get; set; }
        public Nullable<decimal> SoldOutQuantity { get; set; }
        public Nullable<System.DateTime> ManufacturerDate { get; set; }
    }
}
