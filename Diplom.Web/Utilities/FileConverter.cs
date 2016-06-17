using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Diplom.Web.Utilities
{
    public static class FileConvertor
    {
        public static byte[] ImageFileToByte(HttpPostedFileBase file)
        {
            byte[] bytes = null;
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var image = file;
                    image.InputStream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }
            }
            return bytes;
        }
        public static Image ResizeOrigImg(Image image, int nWidth)
        {
            int newWidth, newHeight;
            var coefH = ((double)image.Width / (double)image.Height) * nWidth;
            var coefW = (double)nWidth / (double)image.Width;
            if (coefW >= coefH)
            {
                newHeight = (int)(image.Height * coefH);
                newWidth = (int)(image.Width * coefH);
            }
            else
            {
                newHeight = (int)(image.Height * coefW);
                newWidth = (int)(image.Width * coefW);
            }

            Image result = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.Dispose();
            }
            return result;
        }
        public static Image StreamToImage(HttpPostedFileBase file)
        {
            return Image.FromStream(file.InputStream, true, true);
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}