using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace BHao.Web.Util
{
    public class UIHelper
    {
        #region Slike
        public static Image ResizeImage(Image imgToResize, Size size)
        {
          
            double imageWidth = imgToResize.Width;
            double imageHeight = imgToResize.Height;

            double maxHeight = size.Height;
            double maxWidth = size.Width;

            double aspectRatio = imageWidth / imageHeight;
            double boxRatio = maxWidth / maxHeight;
            double scaleFactor = 0;

            if ( boxRatio > aspectRatio )
                scaleFactor = maxHeight / imageHeight;
            else
                scaleFactor = maxWidth / imageWidth;

            int newWidth = (int)(imageWidth * scaleFactor);
            int newHeight = (int)(imageHeight * scaleFactor);

            Bitmap b = new Bitmap(newWidth,newHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, newWidth, newHeight);
            g.Dispose();

            return (Image)b;            
        }

        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }
        #endregion

    }
}
