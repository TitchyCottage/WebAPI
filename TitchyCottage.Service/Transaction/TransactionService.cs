﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitchyCottage.DbContext;

namespace TitchyCottage.Service.Transaction
{
    public class TransactionService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ResultModel<ShopTransactionModel> CheckInByShop(ShopTransactionModel Qty)
        {
            var result = new ResultModel<ShopTransactionModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {

                    context.AddOrUpdateShopTransaction(Qty.ID, Qty.ShopID, Qty.ProductID, Qty.ProductQuantityID, Qty.Quantity, Qty.CreatedBy,null, Qty.CreatedBy,null);
                    result.Message = string.Concat(Qty.Quantity.ToString(), " ", "Quantity has been Checked in successfully.");
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

        public ResultModel<CheckOutRequestModel> CheckOutFromShop(CheckOutRequestModel Qty)
        {
            var result = new ResultModel<CheckOutRequestModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {

                    context.CheckOutTransaction(Qty.ShopId, Qty.ProductID, Qty.ProductQuantityID, Qty.Quantity, Qty.Isreturn, Qty.CreatedBy, null);
                    result.Message = Qty.Isreturn ?string.Concat(Qty.Quantity.ToString(), " ", "Quantity has been Sold Out successfully.") :
                        string.Concat(Qty.Quantity.ToString(), " ", "Quantity has been Returned successfully.");

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

        public ResultListModel<DistibutorDashboardResponseModel> GetShopInformationForDistibutor(DistibutorDashboardRequestModel req)
        {
            var result = new ResultListModel<DistibutorDashboardResponseModel>();
            try
            {
                using (var context = new TitchyCottageEntities())
                {

                    var data = context.GetShopInformationForDistibutor(req.DistibutorId, req.ProductName, req.Lot, req.ExpiredDate).ToList();
                    if (data !=null && data.Any())
                    {
                        result.Data = (from item in data
                                       select new DistibutorDashboardResponseModel
                                       {
                                           ExpiredDate = item.ExpiredDate,
                                           Lot = item.Lot,
                                           ManufacturerDate = item.ManufacturerDate,
                                           ProductName = item.ProductName,
                                           ShopName = item.ShopName,
                                           SoldOutQuantity = item.SoldOutQuantity,
                                           StockInQuantity = item.StockInQuantity,
                                           TotalQuantity = item.TotalQuantity
                                       }
                                        ).ToList();
                    }
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
