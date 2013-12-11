using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace pngGenerator.Generators
{
    public abstract class BaseGenerator
    {
        public abstract String Filename{get;}

        public abstract int BitmapWidth { get;}
        public abstract int BitmapHeight { get;}

        private byte[] rgbaValues;
        private int bitmapStride;
        private int bitmapComponents;

        public Bitmap CreateBitmap()
        {
            var bitmap = new Bitmap(BitmapWidth, BitmapHeight);

            BitmapData bitmapData = bitmap.LockBits(
                Rectangle.FromLTRB(0, 0, BitmapWidth, BitmapHeight),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            bitmapStride = bitmapData.Stride;
            bitmapComponents = GetComponentsNumber(bitmapData.PixelFormat);

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
            int pixelIndex = (bitmapStride * y) + (bitmapComponents * x);
            rgbaValues[pixelIndex + 0] = b;
            rgbaValues[pixelIndex + 1] = g;
            rgbaValues[pixelIndex + 2] = r;
            rgbaValues[pixelIndex + 3] = a;
        }

        static private int GetComponentsNumber(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return 3;

                case PixelFormat.Format32bppArgb:
                    return 4;

                default:
                    Debug.Assert(false);
                    return 0;
            }
        }
    }
}
