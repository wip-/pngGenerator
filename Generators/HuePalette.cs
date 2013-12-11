using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pngGenerator.Generators
{
    public class HuePalette : BaseGenerator
    {
        public override String Filename { get { return "hue"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 60; } }

        public override void FillPixelArray()
        {
            // quantize to 12 colors
            int colorsCount = 12;
            int colorBandWidth = BitmapWidth / colorsCount;

            for (int c = 0; c < colorsCount; ++c)
            {
                Color color = Helpers.HSL2RGB((double)c / colorsCount, 1.0, 0.5);

                for (int x = 0; x < colorBandWidth; x++)
                {
                    for (int y = 0; y < BitmapHeight; y++)
                    {
                        SetPixelArgb(colorBandWidth * c + x, y, 255, color.R, color.G, color.B);
                    }
                }
            }
        }
    }
}
