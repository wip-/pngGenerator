using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            BaseGenerator generator = new Compare16x16x16Colors();
            //BaseGenerator generator = new BlueRedGradation();
            //BaseGenerator generator = new All16x16x16Colors();




            Bitmap bitmap = generator.CreateBitmap();
            
            var fileName = generator.Filename + ".png";
            int suffix = 0;
            while (File.Exists(fileName))
            {
                fileName = generator.Filename + suffix++ + ".png";
            }

            bitmap.Save(fileName, ImageFormat.Png);

            // select new file
          //Process.Start("explorer.exe", @"/select,""" + AppDomain.CurrentDomain.BaseDirectory + fileName + "\"");
            //Helpers.OpenFolderAndSelectFile(AppDomain.CurrentDomain.BaseDirectory + fileName);

            // call viewer
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + fileName);
   
            return true;
        }
    }
}
