using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class RedGradation : BaseGenerator
    {
        public override String Filename { get { return "redGradation"; } }
        public override int BitmapWidth { get { return 256; } }
        public override int BitmapHeight { get { return 256; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, 255, (byte)x, 0, 0);
                }
            }
        }
    }
}
