using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WPF;

internal static class Images
{
    private static Dictionary<string, Bitmap> _cache = new();

    public static Bitmap LoadImage(string imageUri)
    {
        if (_cache.ContainsKey(imageUri)) return _cache[imageUri];

        _cache[imageUri] = new Bitmap(imageUri);
        return _cache[imageUri];
    }

    public static void Clear()
    {
        _cache.Clear();
    }

    public static void Init()
    {
        _cache = new Dictionary<string, Bitmap>();
    }

    public static Bitmap GetEmptyBitmap(int width, int height)
    {
        const string empty = "EMPTY";
        if (_cache.ContainsKey(empty)) return (Bitmap) _cache[empty].Clone();
        _cache.Add(empty, new Bitmap(width, height));
        var g = Graphics.FromImage(_cache[empty]);
        g.Clear(Color.DarkGreen);
        return (Bitmap) _cache[empty].Clone();
    }

    public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
    {
        if (bitmap == null)
            throw new ArgumentNullException(nameof(bitmap));

        var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

        var bitmapData = bitmap.LockBits(
            rect,
            ImageLockMode.ReadWrite,
            PixelFormat.Format32bppArgb);

        try
        {
            var size = rect.Width * rect.Height * 4;

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