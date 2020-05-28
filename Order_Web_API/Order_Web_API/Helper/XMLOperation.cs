using Order_Web_API.IServices;
using Order_Web_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Order_Web_API.Helper
{
    public class XMLOperation
    {
        public CSVOperation _csvOperation;

        #region Constructor
        public XMLOperation()
        {
            _csvOperation = new CSVOperation();
            this.ProcessXML();
        }
        #endregion

        #region Public

        public OrderModel GetOrderModelFromXML(XElement matchElement, OrderModel order)
        {
            order = new OrderModel();
            XElement element = matchElement.Parent;
            order.OrderNumber = int.Parse(element.Element("OrderNumber").Value);
            order.OrderDate = DateTime.Parse(element.Element("OrderDate").Value);

            order.Product = new ProductModel();
            foreach (XElement eleProduct in element.Descendants("Product"))
            {
                order.Product.ProductNumber = eleProduct.Element("ProductNumber").Value;
                order.Product.ProductGroup = eleProduct.Element("ProductGroup").Value;
            }
            order.OrderLines = new List<OrderLineModel>();

            foreach (XElement eleOrderLine in element.Descendants("OrderLineModel"))
            {
                OrderLineModel orderLine = new OrderLineModel();
                orderLine.OrderLineNumber = int.Parse(eleOrderLine.Element("OrderLineNumber").Value);

                orderLine.Name = eleOrderLine.Element("Name").Value;
                orderLine.Description = eleOrderLine.Element("Description").Value;
                orderLine.Price = Double.Parse(eleOrderLine.Element("Price").Value);

                orderLine.Quantity = int.Parse(eleOrderLine.Element("Quantity").Value);

                orderLine.Customer = new CustomerModel();
                foreach (XElement eleCustomer in element.Descendants("Customer"))
                {
                    orderLine.Customer.CustomerName = eleCustomer.Element("CustomerName").Value;
                    orderLine.Customer.CustomerNumber = int.Parse(eleCustomer.Element("CustomerNumber").Value);
                }

                order.OrderLines.Add(orderLine);
            }
            return order;
        }

        public XDocument GetLodedXMLDocument()
        {
            return XDocument.Load(HostingEnvironment.MapPath(Constants.XMLFilePath));
        }
        
        #endregion

        #region Private

        private void SaveXMLFile(string xml)
        {
            XmlDocument xd = new XmlDocument();

            xd.LoadXml(xml);

            xd.Save(HostingEnvironment.MapPath(Constants.XMLFilePath));
        }

        private string CreateXML(List<OrderModel> orderModels)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlSerializer xmlSerializer = new XmlSerializer(orderModels.GetType());

            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, orderModels);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        private void ProcessXML()
        {
            List<OrderModel> orderModels = _csvOperation.GetOrdersFromCSVFiles();

            string xml = CreateXML(orderModels);
            SaveXMLFile(xml);
        }

        #endregion
    }
}