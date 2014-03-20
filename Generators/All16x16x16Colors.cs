using System;

namespace pngGenerator.Generators
{
    public class All16x16x16Colors : BaseGenerator
    {
        public override String Filename { get { return "All16x16x16Colors"; } }
        public override int BitmapWidth { get { return 64; } }
        public override int BitmapHeight { get { return 64; } }

        public override void FillPixelArray()
        {
            for (int g = 0; g <= 15; ++g)
            for (int r = 0; r <= 15; ++r)
            for (int b = 0; b <= 15; ++b)
            {
                // x and y indices of a block of 16x16 pixels with same G value
                // We place 16 such blocks, only R and G vary inside
                int gY = g / 4;
                int gX = g - 4 * gY;
                int x = 16 * gX + r;
                int y = 16 * gY + b;

                SetPixelArgb(x, y, 255, 
                    (byte)(17*r),
                    (byte)(17*g),
                    (byte)(17*b));
            }
        }
    }
}
