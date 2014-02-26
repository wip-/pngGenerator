using System;
using System.Collections.Generic;

namespace pngGenerator.Generators
{
    public class All32x32Colors : BaseGenerator
    {
        public override String Filename { get { return "32x32colors"; } }
        public override int BitmapWidth { get { return 1024; } }
        public override int BitmapHeight { get { return 32; } }


        // nicely remaps between [0, 31] to [0, 255], with rather uniform intervals (diffuse error)
        private int SmoothRemap(int input)
        {
            return 8 * input + (int)Math.Floor((double)input / 4);
        }

        public override void FillPixelArray()
        {
            // test remap
            List<int> remaps = new List<int>();
            List<int> steps = new List<int>();
            for (int i = 0; i <= 31; ++i )
            {
                remaps.Add(SmoothRemap(i));
                if (i > 0)
                    steps.Add(remaps[i] - remaps[i - 1]);
            }

            for (int b = 0; b <= 31; ++b)
            {
                int xOffset = 32 * b;

                for (int r = 0; r <= 31; ++r)
                {
                    for (int g = 0; g <= 31; ++g)
                    {
                        int x = xOffset + r;
                        int y = g;

                        SetPixelArgb(x, y, 255,
                            (byte)SmoothRemap(r),
                            (byte)SmoothRemap(g),
                            (byte)SmoothRemap(b));
                    }
                }
            }
        }
    }
}
