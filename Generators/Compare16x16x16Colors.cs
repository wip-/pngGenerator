using System;

namespace pngGenerator.Generators
{
    public class Compare16x16x16Colors : BaseGenerator
    {
        public override String Filename { get { return "Compare16x16x16Colors"; } }
#if false
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
#else
        public override int BitmapWidth { get { return 64*3; } }
        public override int BitmapHeight { get { return 64*3; } }

        private NormalizedColor GetAltColor(NormalizedColor colorIn)
        {
            NormalizedColor colorOut = NormalizedColor.FromArgb(colorIn.A, colorIn.R, colorIn.G, colorIn.B);
            colorOut.R = 0;
            return colorOut;
        }


        public override void FillPixelArray()
        {
            for (int g = 0; g <= 15; ++g)
            for (int r = 0; r <= 15; ++r)
            for (int b = 0; b <= 15; ++b)
            {
                // same as above, but we replace each pixel by a 3x3 pixel grid
                // colors and the two first pixels of the two first lines
                // the rest stays white

                // gX and gY indices of a block of (16x3)x(16x3) pixels with same G value
                // We place 16 such blocks, only R and G vary inside
                int gY = g / 4;
                int gX = g - 4 * gY;
                int X = 16 * gX + r;
                int Y = 16 * gY + b;
                // X and Y are indices of a block of 3x3 pixels with same G value

                NormalizedColor inputColor = new NormalizedColor(1, (double)r / 15, (double)g / 15, (double)b / 15);
                NormalizedColor outputColor = GetAltColor(inputColor);


                SetPixelArgb(3 * X + 0, 3 * Y + 0, inputColor);
                SetPixelArgb(3 * X + 1, 3 * Y + 0, inputColor);
                SetPixelArgb(3 * X + 0, 3 * Y + 1, outputColor);
                SetPixelArgb(3 * X + 1, 3 * Y + 1, outputColor);
            }
        }
#endif
    }
}
    