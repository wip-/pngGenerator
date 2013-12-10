using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace pngGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("pngGenerator");
            //Sub(args);
            if (!Sub(new string[] { "16Mcolors" }))
            {
                Console.WriteLine();
                Console.WriteLine("Press key to exit");
                Console.ReadKey();
            }
        }

        // return IsSuccess
        static private bool Sub(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("usage: pngGenerator <output image name>");
                return false;
            }

            int bitmapWidth = 4096;     // 4k!
            int bitmapHeight = 4096;    // 4k!

            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);


            BitmapData bitmapData = bitmap.LockBits(
                Rectangle.FromLTRB(0, 0, bitmapWidth, bitmapHeight),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            
            int bitmapStride = bitmapData.Stride;
            int bitmapComponents = GetComponentsNumber(bitmapData.PixelFormat);

            int dataBytesSize = bitmapStride * bitmapHeight;
            byte[] rgbaValues = new byte[dataBytesSize];


            for (int g = 0; g <= 255; ++g )
                for (int r = 0; r <= 255; ++r)
                    for (int b = 0; b <= 255; ++b)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int gY = g / 16;
                int gX = g - 16*gY;
                int x = 256 * gX + r;
                int y = 256 * gY + b;
                int pixelIndex = (bitmapStride * y) + (bitmapComponents * x);

                rgbaValues[pixelIndex + 0] = (byte)b;        // B
                rgbaValues[pixelIndex + 1] = (byte)g;    // G
                rgbaValues[pixelIndex + 2] = (byte)r;    // R
                rgbaValues[pixelIndex + 3] = 255;        // A
            }
        
            Marshal.Copy(rgbaValues, 0, bitmapData.Scan0, dataBytesSize);
            bitmap.UnlockBits(bitmapData);

            var fileName = args[0] + ".png";
            int suffix = 0;
            while (File.Exists(fileName))
            {
                fileName = args[0] + suffix++ + ".png";
            }

            bitmap.Save(fileName, ImageFormat.Png);

            // select new file
            Process.Start("explorer.exe", @"/select,""" + AppDomain.CurrentDomain.BaseDirectory + fileName + "\"");
                        
            return true;
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
