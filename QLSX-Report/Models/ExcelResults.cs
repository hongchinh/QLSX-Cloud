using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
 
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ReportAPINet.Models
{
        public class ExcelResult : ActionResult
        {
            public string FileName { get; set; }
            public string Path { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.Buffer = true;
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
                context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
            }
        }
        public class ImageResult : ActionResult
        {
            public ImageResult() { }
            public int? Quality { get; set; }
            public Image Image { get; set; }
            public ImageFormat ImageFormat { get; set; }
            public byte[] EncodedImageBytes { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                // verify properties 
                if (EncodedImageBytes == null)
                {
                    if (Image == null)
                    {
                        throw new ArgumentNullException("Image");
                    }
                }
                if (ImageFormat == null)
                {
                    throw new ArgumentNullException("ImageFormat");
                }
                // output 
                context.HttpContext.Response.Clear();

                if (ImageFormat.Equals(ImageFormat.Bmp)) context.HttpContext.Response.ContentType = "image/bmp";
                if (ImageFormat.Equals(ImageFormat.Gif)) context.HttpContext.Response.ContentType = "image/gif";
                if (ImageFormat.Equals(ImageFormat.Icon)) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
                if (ImageFormat.Equals(ImageFormat.Jpeg)) context.HttpContext.Response.ContentType = "image/jpeg";
                if (ImageFormat.Equals(ImageFormat.Png)) context.HttpContext.Response.ContentType = "image/png";
                if (ImageFormat.Equals(ImageFormat.Tiff)) context.HttpContext.Response.ContentType = "image/tiff";
                if (ImageFormat.Equals(ImageFormat.Wmf)) context.HttpContext.Response.ContentType = "image/wmf";

                // output stream
                Stream outputStream = context.HttpContext.Response.OutputStream;
                if (EncodedImageBytes != null)
                {
                    outputStream.Write(EncodedImageBytes, 0, EncodedImageBytes.Length);
                }
                else
                {
                  //  ImageUtil.SaveImageToStream(outputStream, Image, ImageFormat, Quality);
                }
            }

        }

}
