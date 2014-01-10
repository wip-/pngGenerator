using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class AlphaGradation : BaseGenerator
    {
        public override String Filename { get { return "alphaGradation"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                double a = Helpers.Lerp(x, 0, BitmapWidth - 1, 0, 1);
                NormalizedColor color = NormalizedColor.FromArgb(a, 0, 1, 0);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }
            }
        }
    }
}
