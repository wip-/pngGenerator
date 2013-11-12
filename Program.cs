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
            if (!Sub(new string[] { "blue" }))
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

            int bitmapWidth = 640;
            int bitmapHeight = 800;

            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);


            BitmapData bitmapData = bitmap.LockBits(
                Rectangle.FromLTRB(0, 0, bitmapWidth, bitmapHeight),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            
            int bitmapStride = bitmapData.Stride;
            int bitmapComponents = GetComponentsNumber(bitmapData.PixelFormat);

            int dataBytesSize = bitmapStride * bitmapHeight;
            byte[] rgbaValues = new byte[dataBytesSize];

            for (int y = 0; y < bitmapHeight; y++)
            {
                for (int x = 0; x < bitmapWidth; x++)
                {
                    int index = (bitmapStride * y) + (bitmapComponents * x);

                    rgbaValues[index + 0] = (byte)(255 * (float)y / bitmapHeight);      // B
                    rgbaValues[index + 1] = 0;                                          // G
                    rgbaValues[index + 2] = (byte)(255 * (float)x / bitmapWidth);       // R
                    rgbaValues[index + 3] = 255;                                        // A
                }
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
