using System;
using System.Diagnostics;
using System.Drawing;
using ColorMine.ColorSpaces;

namespace pngGenerator.Generators
{
    public class CieLuvUgradation : BaseGenerator
    {
        public override String Filename { get { return "CieLuv-Ugradation"; } }
        public override int BitmapWidth { get { return 32; } }
        public override int BitmapHeight { get { return 1024; } }

        public override void FillPixelArray()
        {
            for (int x = 0; x < 1024; ++x)
            {
                double L = 100;
                double U = Helpers.Lerp(x, 0, 1023, -134, 224);
                double V = 0;

                var luv = new Luv { L = L, U = U, V = V };
                var rgb = luv.To<Rgb>();

                Color color = Color.FromArgb(255, 0, 0, 0);
                if (!double.IsNaN(rgb.R) && !double.IsNaN(rgb.G) && !double.IsNaN(rgb.B))
                {
                    color = Color.FromArgb(
                        255, 
                        Convert.ToByte(rgb.R.Clamp0_255()),
                        Convert.ToByte(rgb.G.Clamp0_255()),
                        Convert.ToByte(rgb.B.Clamp0_255()));
                }

                for (int y = 0; y < 32; ++y)
                {
                    SetPixelArgb(x, y, 
                        color.A, 
                        color.R, 
                        color.G, 
                        color.B);
                }

            }
        }
    }
}
