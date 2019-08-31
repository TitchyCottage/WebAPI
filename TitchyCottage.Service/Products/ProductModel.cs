using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service.Products
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Required,MaxLength(50)]
        public string ProductName { get; set; }
        [MaxLength(250)]
        public string ProductDescription { get; set; }
        public decimal? Cost { get; set; }
    }

}
