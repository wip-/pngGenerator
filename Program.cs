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
using pngGenerator.Generators;

namespace pngGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("pngGenerator");
            if (!Sub())
            {
                Console.WriteLine();
                Console.WriteLine("Press key to exit");
                Console.ReadKey();
            }
        }

        // return IsSuccess
        static private bool Sub()
        {
            // Just switch between generators HERE:
            //BaseGenerator generator = new All24bppColors();
            //BaseGenerator generator = new BlueRedGradation();
            BaseGenerator generator = new Blue80Red20();




            Bitmap bitmap = generator.CreateBitmap();
            
            var fileName = generator.Filename + ".png";
            int suffix = 0;
            while (File.Exists(fileName))
            {
                fileName = generator.Filename + suffix++ + ".png";
            }

            bitmap.Save(fileName, ImageFormat.Png);

            // select new file
            Process.Start("explorer.exe", @"/select,""" + AppDomain.CurrentDomain.BaseDirectory + fileName + "\"");
                        
            return true;
        }
    }
}
