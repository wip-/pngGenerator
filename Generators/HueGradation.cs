using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pngGenerator.Generators
{
    public class HueGradation : BaseGenerator
    {
        public override String Filename { get { return "hue"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 60; } }

        public override void FillPixelArray()
        {
            for (int y = 0; y < BitmapHeight; y++)
            for (int x = 0; x < BitmapWidth; x++)
            {
                Color color = Helpers.HSL2RGB((double)x/BitmapWidth, 1.0, 0.5);
                SetPixelArgb(x, y, 255, color.R, color.G, color.B);
            }
        }
    }
}
