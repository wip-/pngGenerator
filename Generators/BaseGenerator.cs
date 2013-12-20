using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace pngGenerator.Generators
{
    public abstract class BaseGenerator
    {
        public abstract String Filename{get;}

        public abstract int BitmapWidth { get;}
        public abstract int BitmapHeight { get;}
        public virtual PixelFormat PixelFormat { get { return PixelFormat.Format32bppArgb; } }

        private byte[] rgbaValues;
        private int bitmapStride;
        private int bytesPerPixel;
        private int bytesPerComponent;

        public Bitmap CreateBitmap()
        {
            var bitmap = new Bitmap(BitmapWidth, BitmapHeight, PixelFormat);

            BitmapData bitmapData = bitmap.LockBits(
                Rectangle.FromLTRB(0, 0, BitmapWidth, BitmapHeight),
                ImageLockMode.WriteOnly,
                PixelFormat);

            bitmapStride = bitmapData.Stride;
            //bytesPerPixel = GetComponentsNumber(bitmapData.PixelFormat);
            bytesPerPixel = Image.GetPixelFormatSize(PixelFormat) / 8;
            bytesPerComponent = bytesPerPixel / GetComponentsNumber(PixelFormat);

            int dataBytesSize = bitmapStride * BitmapHeight;
            rgbaValues = new byte[dataBytesSize];

            FillPixelArray();

            Marshal.Copy(rgbaValues, 0, bitmapData.Scan0, dataBytesSize);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        public abstract void FillPixelArray();

        protected void SetPixelArgb(int x, int y, byte a, byte r, byte g, byte b)
        {
            int pixelIndex = GetPixelInternalIndex(x, y);
            rgbaValues[pixelIndex + 0 * bytesPerComponent] = b;
            rgbaValues[pixelIndex + 1 * bytesPerComponent] = g;
            rgbaValues[pixelIndex + 2 * bytesPerComponent] = r;
            rgbaValues[pixelIndex + 3 * bytesPerComponent] = a;
        }

        protected void SetPixelArgb(int x, int y, NormalizedColor color)
        {
            int pixelIndex = GetPixelInternalIndex(x, y);
            if (PixelFormat == PixelFormat.Format32bppArgb)
            {
                Color color32 = color.ToColor32();
                rgbaValues[pixelIndex + 0] = color32.B;
                rgbaValues[pixelIndex + 1] = color32.G;
                rgbaValues[pixelIndex + 2] = color32.R;
                rgbaValues[pixelIndex + 3] = color32.A;
            }
            else
            {
                Color64 color64 = color.ToColor64();
                Array.Copy(BitConverter.GetBytes(color64.B), 0, rgbaValues, pixelIndex + 0, 2);
                Array.Copy(BitConverter.GetBytes(color64.G), 0, rgbaValues, pixelIndex + 2, 2);
                Array.Copy(BitConverter.GetBytes(color64.R), 0, rgbaValues, pixelIndex + 4, 2);
                Array.Copy(BitConverter.GetBytes(color64.A), 0, rgbaValues, pixelIndex + 6, 2);
            }
        }

        // Get index of pixel in Data array from its coordinates
        private Int32 GetPixelInternalIndex(int x, int y)
        {
            return (bitmapStride * y) + (bytesPerPixel * x);
        }

        static private int GetComponentsNumber(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return 3;

                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format64bppArgb:
                    return 4;

                default:
                    Debug.Assert(false);
                    return 0;
            }
        }
    }
}
