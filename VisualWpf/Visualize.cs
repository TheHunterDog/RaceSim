using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using WPF;
using Model;

namespace WPF
{
    public static class Visualize
    {
        public static BitmapSource DrawTrack(Track b)
        {
            return Images.CreateBitmapSourceFromGdiBitmap(Images.GetEmptyBitmap(Images.DEFAULT_WIDTH,Images.DEFAULT_HEIGHT));
        }
    }
}
