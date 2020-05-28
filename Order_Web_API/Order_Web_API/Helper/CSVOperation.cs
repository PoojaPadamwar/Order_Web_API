using Order_Web_API.IServices;
using Order_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order_Web_API.Helper
{
    /// <summary>
    /// All operation related to the CSV file conversion
    /// </summary>
    public class CSVOperation
    {
        public FileOperation _fileOperation;

        #region Constructor
        public CSVOperation()
        {
            _fileOperation = new FileOperation();
        }

        #endregion
                
        #region Public

        /// <summary>
        /// Get list of OrderModel from CSV files
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrdersFromCSVFiles()
        {
            List<string> filePaths = _fileOperation.GetAllFileFromCSVDirectory();

            List<OrderModel> orderModels = new List<OrderModel>();

            foreach (string fileName in filePaths)
            {
                List<string> allLines = _fileOperation.GetAllLinesFromFilePath(fileName);
                orderModels.Add(FillModelFromCSVLines(allLines));
            }

            return orderModels;

        }
       
        #endregion

        #region Private

        /// <summary>
        /// Fill Order Model from CSV file all lines 
        /// </summary>
        /// <param name="allLines">All lines of CSV file</param>
        /// <returns></returns>
        private OrderModel FillModelFromCSVLines(List<string> allLines)
        {
            OrderModel orderModel = new OrderModel();
            allLines.RemoveAt(0);

            orderModel.OrderLines = new List<OrderLineModel>();
            foreach (var line in allLines)
            {
                List<string> lineItem = line.Split('|').ToList();
                lineItem.RemoveAt(12);
                lineItem.RemoveAt(0);

                orderModel.OrderNumber = int.Parse(lineItem[0]);
                orderModel.OrderDate = DateTime.Parse(lineItem[8]);

                orderModel.Product = new ProductModel();
                orderModel.Product.ProductGroup = lineItem[7];
                orderModel.Product.ProductNumber = lineItem[2];

                OrderLineModel orderLine = new OrderLineModel();
                orderLine.OrderLineNumber = int.Parse(lineItem[1]);
                orderLine.Quantity = int.Parse(lineItem[3]);
                orderLine.Name = lineItem[4];
                orderLine.Description = lineItem[5];
                orderLine.Price = Double.Parse(lineItem[6]);
                
                orderLine.Customer = new CustomerModel();
                orderLine.Customer.CustomerName = lineItem[9];
                orderLine.Customer.CustomerName = lineItem[10];
                orderModel.OrderLines.Add(orderLine);
            }
            return orderModel;
        }

        #endregion
    }
}