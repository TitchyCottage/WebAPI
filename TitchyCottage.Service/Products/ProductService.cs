using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitchyCottage.DbContext;

namespace TitchyCottage.Service.Products
{
    public class ProductService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

    }
}
