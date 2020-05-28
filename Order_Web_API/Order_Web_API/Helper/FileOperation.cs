using Order_Web_API.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Order_Web_API.Helper
{
    public class FileOperation
    {
        #region "Public"
        /// <summary>
        /// Get All File paths from CSV directory
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileFromCSVDirectory()
        {
            //Refering to the CSV folder wher all csv files are present
            //if in future Order csv needed to add in just drop those in to the CSV folder
            List<string> filePaths = Directory.GetFiles(HostingEnvironment.MapPath(Constants.CSVFolderPath),
                Constants.CSVExtention).ToList();

            return filePaths;
        }

        /// <summary>
        /// Get all lines from passd file path
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns></returns>
        public List<string> GetAllLinesFromFilePath(string filePath)
        {
           return File.ReadLines(filePath).ToList();
        }

        #endregion
    }
}