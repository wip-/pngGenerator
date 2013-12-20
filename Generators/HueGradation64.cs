using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace pngGenerator.Generators
{
    // hue gradation, but with color depth == 64
    public class HueGradation64 : BaseGenerator
    {
        public override String Filename { get { return "hueGradation64"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }
        public override PixelFormat PixelFormat { get { return PixelFormat.Format64bppArgb; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                NormalizedColor color = Helpers.HSL2RGB((double)x/BitmapWidth, 1.0, 0.5);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }                
            }
        }
    }
}
