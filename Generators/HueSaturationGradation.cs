using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class HueSaturationGradation : BaseGenerator
    {
        public override String Filename { get { return "hueSaturationGradation"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 360; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                for (int y = 0; y < BitmapHeight; y++)
                {
                    Color color = Helpers.HSL2RGB((double)x / BitmapWidth, (double)y / BitmapHeight, 0.5);
                    SetPixelArgb(x, y, 255, color.R, color.G, color.B);
                }
            }
        }
    }
}
