using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class HueGradationIcon : BaseGenerator
    {
        public override String Filename { get { return "hueGradationIcon"; } }
        public override int BitmapWidth { get { return 255; } }
        public override int BitmapHeight { get { return 9; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                NormalizedColor color;
                if (x < BitmapWidth / 2)
                    color = Helpers.HSL2RGB(0.5 + (double)x / BitmapWidth, 1.0, 0.5);
                else
                    color = Helpers.HSL2RGB(-0.5 + (double)x / BitmapWidth, 1.0, 0.5);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }                
            }
        }
    }
}
