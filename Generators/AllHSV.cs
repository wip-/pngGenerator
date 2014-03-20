using ColorMine.ColorSpaces;
using System;

namespace pngGenerator.Generators
{
    public class AllHSV : BaseGenerator
    {
        public override String Filename { get { return "AllHSV"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        public override void FillPixelArray()
        {

            //var rgbTest = new Rgb { R = 255, G = 255, B = 255 };
            //var hslTest = rgbTest.To<Hsl>();


            for (int l = 0; l <= 63; ++l)
            for (int h = 0; h <= 63; ++h)
            for (int s = 0; s <= 63; ++s)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int lY = l / 8;
                int lX = l - 8 * lY;
                int x = 64 * lX + h;
                int y = 64 * lY + s;

                var H = Helpers.Lerp(h, 0, 64, 0, 360);
                var S = Helpers.Lerp(s, 0, 63, 0, 100);
                var L = Helpers.Lerp(l, 0, 63, 0, 100);

                if (h == 63)
                    L = 0;

                var hsl = new Hsl{H=H, S=S, L=L};
                var rgb = hsl.To<Rgb>();
                
                SetPixelArgb(x, y, 255, 
                    (byte)rgb.R,
                    (byte)rgb.G,
                    (byte)rgb.B);
            }
        }
    }
}
