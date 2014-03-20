using System;
using System.Diagnostics;
using System.Drawing;
using ColorMine.ColorSpaces;

namespace pngGenerator.Generators
{
    public class CieLuv : BaseGenerator
    {
        public override String Filename { get { return "CieLuv"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }


        // according to http://colormine.org/color-converter, u,v belong to:
        // u:[-134, 224]
        // v:[-140, 122]
        //
        // in practice, L, u, v belong to the following ranges:
        // L:[0.0, 100.0]
        // u:[-83.079751931, 175.053035736]
        // v:[-134.11607, 107.401364]
        private void CheckCieLuvRanges()
        {
            double maxL = double.MinValue;
            double maxU = double.MinValue;
            double maxV = double.MinValue;
            double minL = double.MaxValue;
            double minU = double.MaxValue;
            double minV = double.MaxValue;

            for (int r = 0; r < 256; ++r)
            for (int g = 0; g < 256; ++g)
            for (int b = 0; b < 256; ++b)
            {
                var rgb = new Rgb { R = r, G = g, B = b };
                var luv = rgb.To<Luv>();

                maxL = Math.Max(maxL, luv.L);
                maxU = Math.Max(maxU, luv.U);
                maxV = Math.Max(maxV, luv.V);
                minL = Math.Min(minL, luv.L);
                minU = Math.Min(minU, luv.U);
                minV = Math.Min(minV, luv.V);
            }
            Debugger.Break();
        }


        public override void FillPixelArray()
        {
            //CheckCieLuvRanges();

            for (int l = 0; l <= 63; ++l)
            for (int u = 0; u <= 63; ++u)
            for (int v = 0; v <= 63; ++v)
            {
                // x and y indices of a block of 256x256 pixels with same G value
                // We place 256 such blocks, only R and G vary inside
                int gY = l / 8;
                int gX = l - 8 * gY;
                int x = 64 * gX + u;
                int y = 64 * gY + v;

                double L, U, V;

                L = Helpers.Lerp(l, 0, 63, 0, 100);
                U = Helpers.Lerp(u, 0, 63, -134, 224);
                V = Helpers.Lerp(v, 0, 63, -140, 122);
               
                var luv = new Luv { L = L, U = U, V = V };
                var rgb = luv.To<Rgb>();

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
