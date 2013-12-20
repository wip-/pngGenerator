using System;

namespace pngGenerator.Generators
{
    public class Blue80Red20 : BaseGenerator
    {
        public override String Filename { get { return "blue20red80"; } }
        public override int BitmapWidth { get { return 100; } }
        public override int BitmapHeight { get { return 100; } }

        public override void FillPixelArray()
        {
            for (int y = 0; y < BitmapHeight; y++)
            for (int x = 0; x < BitmapWidth; x++)
            {
                if( x<80)
                    SetPixelArgb(x, y, 255, 255, 0, 0);
                else
                    SetPixelArgb(x, y, 255, 0, 0, 255);
            }
        }
    }
}
