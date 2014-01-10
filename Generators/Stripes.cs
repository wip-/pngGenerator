using System;
using System.Diagnostics;

namespace pngGenerator.Generators
{
    public class Stripes : BaseGenerator
    {
        public override String Filename { get { return "stripes"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        private int StripeWidth = 16;

        public override void FillPixelArray()
        {
            NormalizedColor bkgColor0 = NormalizedColor.FromArgb(1, 1, 1, 1);
            NormalizedColor bkgColor1 = NormalizedColor.FromArgb(1, 0, 1, 1);
            for (int y = 0; y < BitmapHeight; y++)
            {
                NormalizedColor bkgColor;
                if ((y % (2 * StripeWidth)) > StripeWidth)
                    bkgColor = bkgColor0;
                else
                    bkgColor = bkgColor1;

                for (int x = 0; x < BitmapWidth; x++)
                {
                    SetPixelArgb(x, y, bkgColor);
                }
            }
        }
    }
}
