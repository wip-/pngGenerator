using System;

namespace pngGenerator.Generators
{
    public class All24bppColors : BaseGenerator
    {
        public override String Filename { get { return "16Mcolors"; } }
        public override int BitmapWidth { get { return 4096; } }
        public override int BitmapHeight { get { return 4096; } }

        public override void FillPixelArray()
        {
            for (int g = 0; g <= 255; ++g)
            for (int r = 0; r <= 255; ++r)
            for (int b = 0; b <= 255; ++b)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int gY = g / 16;
                int gX = g - 16 * gY;
                int x = 256 * gX + r;
                int y = 256 * gY + b;
                        
                SetPixelArgb(x, y, 255, (byte)r, (byte)g, (byte)b);
            }
        }
    }
}
