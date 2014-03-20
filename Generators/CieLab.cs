using System;
using System.Diagnostics;
using System.Drawing;
using ColorMine.ColorSpaces;

namespace pngGenerator.Generators
{
    public class CieLab : BaseGenerator
    {
        public override String Filename { get { return "CieLab"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        public override void FillPixelArray()
        {
            //CheckCieLuvRanges();

            for (int l = 0; l <= 63; ++l)
            for (int a = 0; a <= 63; ++a)
            for (int b = 0; b <= 63; ++b)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int gY = l / 8;
                int gX = l - 8 * gY;
                int x = 64 * gX + a;
                int y = 64 * gY + b;

                double L, A, B;

                L = Helpers.Lerp(l, 0, 63, 0, 100);
                A = Helpers.Lerp(a, 0, 63, -128, 128);
                B = Helpers.Lerp(b, 0, 63, -128, 128);
                           
                var lab = new Lab { L = L, A = A, B = B };
                var rgb = lab.To<Rgb>();

                if (double.IsNaN(rgb.R) || double.IsNaN(rgb.G) || double.IsNaN(rgb.B))
                {
                    SetPixelArgb(x, y, 255, 0, 0, 0);
                }

                SetPixelArgb(x, y, 255,
                    Convert.ToByte(rgb.R.Clamp0_255()),
                    Convert.ToByte(rgb.G.Clamp0_255()),
                    Convert.ToByte(rgb.B.Clamp0_255()));
            }
        }
    }
}
