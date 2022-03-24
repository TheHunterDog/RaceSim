 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF
{
    static class Images
    {
        public static int DEFAULT_WIDTH = 0;
        public static int DEFAULT_HEIGHT = 0;
        private static Dictionary<String, System.Drawing.Bitmap> cache;
        public static System.Drawing.Bitmap LoadImage(String ImageURI)
        {
            if (cache.ContainsKey(ImageURI))
            {
                return cache[ImageURI];
            }
            else
            {
                cache[ImageURI] = new System.Drawing.Bitmap(ImageURI);
                return (System.Drawing.Bitmap)cache[ImageURI].Clone();
            }
        }
        public static void Clear()
        {
            cache.Clear();
        }
        public static System.Drawing.Bitmap GetEmptyBitmap(int width, int height)
        {
            const String Empty = "EMPTY";
            if (!cache.ContainsKey(Empty))
            {
                cache.Add(Empty, new System.Drawing.Bitmap(width,height));
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(cache[Empty]);
                g.Clear(System.Drawing.Color.DarkGreen);

            }
            return (System.Drawing.Bitmap)cache[Empty].Clone();

        }
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }

}
