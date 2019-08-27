using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TitchyCottage.Service.Products;

namespace TitchyCottage.Api.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private ProductService _product;
        public ProductController()
        {
            _product = new ProductService();
        }

        [Route("AddOrUpdateProduct")]
        public IHttpActionResult AddOrUpdateProduct(ProductModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_product.AddOrUpdateProduct(request));

        }

        [Route("GetProductByProductId")]
        public IHttpActionResult GetProductByProductId(int productID)
        {
            return Ok(_product.GetProductByProductId(productID));

        }

        [Route("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            return Ok(_product.GetProducts());

        }

    }
}
