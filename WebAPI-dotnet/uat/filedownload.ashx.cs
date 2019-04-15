using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using WebAPI.Utility;

namespace WebAPI.uat
{
    /// <summary>
    /// WARNING: This handler is only for download test, 
    /// </summary>
    public class filedownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Request.ContentType = "application/json";
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string inputJson = reader.ReadToEnd();

            JsonParser parser = new JsonParser();
            Dictionary<string, string> input = parser.DeserializeStrDict(inputJson);
            var broker = input["broker"];
            var barcode = input["barcode"];

            string fileName = "file.zip";
            string filePath = context.Server.MapPath("./");

            context.Response.Clear();
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            string fileStr = filePath + fileName;
            context.Response.WriteFile(fileStr);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}