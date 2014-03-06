using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class GrayscaleIcon : BaseGenerator
    {
        public override String Filename { get { return "grayscaleIcon"; } }
        public override int BitmapWidth { get { return 255; } }
        public override int BitmapHeight { get { return 9; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                double val = (double)x/255;
                NormalizedColor color = new NormalizedColor(1, val, val, val);
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, color);
                }                
            }
        }
    }
}
