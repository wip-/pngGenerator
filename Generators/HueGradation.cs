﻿using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class HueGradation : BaseGenerator
    {
        public override String Filename { get { return "hueGradation"; } }
        public override int BitmapWidth { get { return 360; } }
        public override int BitmapHeight { get { return 360; } }

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
