using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class GammaTest : BaseGenerator
    {
        public override String Filename { get { return "gammaTest"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 360; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                double g1 = (double)x/(BitmapWidth-1);
                NormalizedColor color1 = NormalizedColor.FromArgb(1.0, g1, g1, g1);
                for (int y = 0; y < BitmapHeight/3; y++)
                {
                    SetPixelArgb(x, y, color1);
                }

                double g2 = Math.Pow(g1, 2.2);
                NormalizedColor color2 = NormalizedColor.FromArgb(1.0, g2, g2, g2);
                for (int y = BitmapHeight / 3; y < 2 * BitmapHeight/3; y++)
                {
                    SetPixelArgb(x, y, color2);
                }

                double g3 = Math.Pow(g1, 1.0/2.2);
                NormalizedColor color3 = NormalizedColor.FromArgb(1.0, g3, g3, g3);
                for (int y = 2 * BitmapHeight / 3; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color3);
                }                
            }
        }
    }
}
