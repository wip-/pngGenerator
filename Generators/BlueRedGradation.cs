using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pngGenerator.Generators
{
    public class BlueRedGradation : BaseGenerator
    {
        public override String Filename { get { return "blue"; } }
        public override int BitmapWidth { get { return 640; } }
        public override int BitmapHeight { get { return 800; } }

        public override void FillPixelArray()
        {
            for (int y = 0; y < BitmapHeight; y++)
            for (int x = 0; x < BitmapWidth; x++)
            {
                SetPixelArgb(x, y, 255, 
                    (byte)(255 * (float)x / BitmapWidth), 
                    0, 
                    (byte)(255 * (float)y / BitmapHeight));
            }
        }
    }
}
