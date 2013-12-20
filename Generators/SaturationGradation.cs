using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class SaturationGradation : BaseGenerator
    {
        public override String Filename { get { return "saturationGradation"; } }
        public override int BitmapWidth { get { return 256; } }
        public override int BitmapHeight { get { return 256; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                NormalizedColor color = Helpers.HSL2RGB(0.0, x/255.0, 0.5);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }
            }
        }
    }
}
