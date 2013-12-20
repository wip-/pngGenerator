using System;
using System.Drawing;

namespace pngGenerator.Generators
{
    public class Component : BaseGenerator
    {
        public override String Filename { get { return "components"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        

        // 1d-variation of 8x8x8 = 512 values of R, G, B components
        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                //NormalizedColor color = Helpers.HSL2RGB((double)x / BitmapWidth, 1.0, 0.5);

                int indexG = x / (8*8);
                int indexR = (x / 8) % 8;
                int indexB = x % 8;

                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, 255, 
                        LookUpTable8Values[indexR],
                        LookUpTable8Values[indexG],
                        LookUpTable8Values[indexB]);
                }
            }
        }

        /// <summary>
        /// Table mapping values in the range [0,255] to a set of 8 values distributed nicely
        /// </summary>
        private readonly byte[] LookUpTable8Values = { 0, 37, 73, 109, 145, 181, 218, 255 };

        // dumb quantization: artificially replace 255 values precision by 8 values precision 
        private static byte Range256toRange8(int b)
        {
            return (byte)(32 * (b / 32));
        }

    }
}

