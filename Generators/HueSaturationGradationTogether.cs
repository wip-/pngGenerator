using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class HueSaturationGradationTogether : BaseGenerator
    {
        public override String Filename { get { return "hueSaturationGradationTogether"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 360; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                NormalizedColor color = Helpers.HSL2RGB((double)x / BitmapWidth, (double)x / BitmapHeight, 0.5);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }
            }
        }
    }
}
