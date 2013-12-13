using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class LightnessGradation : BaseGenerator
    {
        public override String Filename { get { return "lightnessGradation"; } }
        public override int BitmapWidth { get { return 256; } }
        public override int BitmapHeight { get { return 256; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                Color color = Helpers.HSL2RGB(0.0, 1.0, x / 255.0);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, 255, color.R, color.G, color.B);
                }
            }
        }
    }
}
