﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using TitchyCottage.DbContext;

namespace TitchyCottage.Service.Products
{
    public class ProductService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Product
        public ResultModel<ProductModel> AddOrUpdateProduct(ProductModel product)
        {
            var result = new ResultModel<ProductModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {

                    context.AddOrUpdateProduct(product.ProductID, product.ProductName, product.ProductDescription, product.Cost);
                    //context.Products.Add(student);
                    ////will execute sp_InsertStudentInfo 
                    //context.SaveChanges();

                    //student.StudentName = "Edit student using SP";
                    ////will execute sp_UpdateStudent
                    //context.SaveChanges();

                    //context.Students.Remove(student);
                    //will execute sp_DeleteStudentInfo 
                    //context.SaveChanges();
                    result.Message = "Product has been saved successfully.";
                    result.success = true;
                }
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.success = false;
                logger.Error(ex.Message);
            }

            return result;
        }

        public  ResultModel<ProductModel> GetProductByProductId(int productID)
        {

            var result = new ResultModel<ProductModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {
                    Product product = context.GetProductByID(productID).FirstOrDefault();

                    result.Data = new ProductModel
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Cost =product.Cost
                    };
                    result.success = true;
                }

            }
            catch(Exception ex)
            {
                result.success = false;
                logger.Error(ex.Message);
            }
            return result;
        }

        public ResultListModel<ProductModel> GetProducts()
        {

            var result = new ResultListModel<ProductModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {
                    List<Product> product = context.GetProducts().ToList();

                    result.Data = (from item in product
                                   select new ProductModel
                                   {
                                       ProductID = item.ProductID,
                                       ProductName =item.ProductName,
                                       ProductDescription = item.ProductDescription
                                   }).ToList();
                    result.success = true;
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                logger.Error(ex.Message);
            }
            return result;
        }
        #endregion

        #region Product Quantity
        public ResultModel<ProductModel> AddOrUpdateProductQuantity(ProductQuantityModel Qty)
        {
            var result = new ResultModel<ProductModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {

                    context.AddOrUpdateProductQuantity(Qty.ID, Qty.ProductID, Qty.Lot, Qty.DateCode, Qty.TotalQuantity, Qty.ExpiredDate, Qty.ManufacturerDate,Qty.CreatedBy);
                    result.Message = string.Concat(Qty.TotalQuantity.ToString(), " ", "Quantity has been added successfully.");
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.success = false;
                logger.Error(ex.Message);
            }

            return result;
        }

        public ResultModel<ProductQuantityModel> GetProductQuantityByProductId(int ID)
        {

            var result = new ResultModel<ProductQuantityModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {
                    ProductQuantity qty = context.GetProductQuantityByID(ID).FirstOrDefault();

                    result.Data = new ProductQuantityModel
                    {
                       ID = qty.ID,
                       DateCode = qty.DateCode,
                       ExpiredDate = qty.ExpiredDate,
                       Lot = qty.Lot,
                       ManufacturerDate = qty.ManufacturerDate,
                       ProductID = qty.ProductID,
                       ShopInQuantity = qty.ShopInQuantity,
                       SoldOutQuantity = qty.SoldOutQuantity,
                       StockInQuantity = qty.ShopInQuantity,
                       TotalQuantity = qty.TotalQuantity
                    };
                    result.success = true;
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                logger.Error(ex.Message);
            }
            return result;
        }

        public ResultListModel<ProductQuantityModel> GetProductQuantityList()
        {

            var result = new ResultListModel<ProductQuantityModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {
                    List<ProductQuantity> product = context.GetProductsQuantity().ToList();

                    result.Data = (from qty in product
                                   select new ProductQuantityModel
                                   {
                                       ID = qty.ID,
                                       DateCode = qty.DateCode,
                                       ExpiredDate = qty.ExpiredDate,
                                       Lot = qty.Lot,
                                       ManufacturerDate = qty.ManufacturerDate,
                                       ProductID = qty.ProductID,
                                       ShopInQuantity = qty.ShopInQuantity,
                                       SoldOutQuantity = qty.SoldOutQuantity,
                                       StockInQuantity = qty.ShopInQuantity,
                                       TotalQuantity = qty.TotalQuantity
                                   }).ToList();
                    result.success = true;
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                logger.Error(ex.Message);
            }
            return result;
        }

        #endregion

    }
}