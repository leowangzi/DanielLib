using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DanielLib.Utilities.ImageHandler
{
    public class LoadImageFromDisk
    {
        #region LoadImageFromDisk 从硬盘上读取图片

        public static BitmapSource GetImage(String filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.EndInit();

                    return bitmap;
                }
            }

            return null;
        }

        #endregion
    }
}
