﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using TitchyCottage.Service;
using TitchyCottage.Service.Products;

namespace TitchyCottage.Api.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private ProductService _product;
        private string userId = string.Empty;
        public ProductController()
        {
            _product = new ProductService();
            userId = ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId") != null ? ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst("UserId").Value : string.Empty;
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

        [Route("AddOrUpdateProductQuantity")]
        public IHttpActionResult AddOrUpdateProductQuantity(ProductQuantityModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.CreatedBy = userId;
            return Ok(_product.AddOrUpdateProductQuantity(request));

        }

        [Route("GetProductQuantityByProductId")]
        public IHttpActionResult GetProductQuantityByProductId(int ID)
        {
            return Ok(_product.GetProductQuantityByProductId(ID));

        }

        [Route("GetProductQuantityList")]
        public IHttpActionResult GetProductQuantityList()
        {
            return Ok(_product.GetProductQuantityList());

        }

    }
}
