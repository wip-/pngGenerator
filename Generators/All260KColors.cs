using System;

namespace pngGenerator.Generators
{
    public class All260KColors : BaseGenerator
    {
        public override String Filename { get { return "260K"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }


        // nicely remaps between [0, 63] to [0, 255], with rather uniform intervals (diffuse error)
        private int SmoothRemap(int input)
        {
            return 4 * input + (int)Math.Floor((double)input / 16);
        }

        public override void FillPixelArray()
        {
            for (int g = 0; g <= 63; ++g)
            for (int r = 0; r <= 63; ++r)
            for (int b = 0; b <= 63; ++b)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int gY = g / 8;
                int gX = g - 8 * gY;
                int x = 64 * gX + r;
                int y = 64 * gY + b;

                SetPixelArgb(x, y, 255, 
                    (byte)SmoothRemap(r),
                    (byte)SmoothRemap(g),
                    (byte)SmoothRemap(b));
            }
        }
    }
}
