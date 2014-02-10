using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class AlphaGradation2d : BaseGenerator
    {
        public override String Filename { get { return "alphaGradation2d"; } }
        public override int BitmapWidth { get { return 1024; } }
        public override int BitmapHeight { get { return 1024; } }

        public override void FillPixelArray()
        {
            //┓+ background
            for (int x = 0; x < 1024; x++)
            {
                for (int y = 0; y < 1024; y++)
                {
                    NormalizedColor color = NormalizedColor.FromArgb(0, 0, 1, 0);
                    SetPixelArgb(x, y, color);
                }
            }


            //┏
            for (int y = 256; y < 512; y++)
            {
                double a = Helpers.Lerp(y, 256, 511, 0, 1);
                NormalizedColor color = NormalizedColor.FromArgb(a, 0, 1, 0);
                for (int x = 256; x < 512; x++)
                {
                    SetPixelArgb(x, y, color);
                }
            }



            //┗
            for (int x = 256; x < 512; x++)
            {
                for (int y = 512; y < 768; y++)
                {
                    NormalizedColor color = NormalizedColor.FromArgb(1, 0, 1, 0);
                    SetPixelArgb(x, y, color);
                }
            }

            //┛
            for (int x = 512; x < 768; x++)
            {
                double a = Helpers.Lerp(x, 512, 767, 1, 0);
                NormalizedColor color = NormalizedColor.FromArgb(a, 0, 1, 0);
                for (int y = 512; y < 768; y++)
                {
                    SetPixelArgb(x, y, color);
                }
            }



            // 
        }
    }
}
