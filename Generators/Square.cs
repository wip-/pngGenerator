using System;
using System.Diagnostics;

namespace pngGenerator.Generators
{
    public class Square : BaseGenerator
    {
        public override String Filename { get { return "square"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        private int SquareCenterX = 256;
        private int SquareCenterY = 250;
        private int SquareWidth = 128;
        private int SquareHardAlphaWidth = 64;  // pixel within this distance of the square center will have alpha=1

        public override void FillPixelArray()
        {
            // we don't care about overdraw

            // background (stripes)
            NormalizedColor bkgColor0 = NormalizedColor.FromArgb(0, 1, 1, 1);
            for (int y = 0; y < BitmapHeight; y++)
            for (int x = 0; x < BitmapWidth; x++)
            {
                SetPixelArgb(x, y, bkgColor0);
            }


            // foreground (square)
            NormalizedColor squareColor0 = NormalizedColor.FromArgb(1, 0, 1, 0);
            for (int x = 0; x < BitmapWidth; x++)
            for (int y = 0; y < BitmapHeight; y++)
            {
                if (IsInSquare(x, y))
                {
                    if (y > SquareCenterY)
                    {
                        // hard alpha
                        SetPixelArgb(x, y, squareColor0);
                    }
                    else
                    {
                        // alpha gradation
                        int dist = GetSquareRingNumber(x, y);   // distance to center
                        if( dist < SquareHardAlphaWidth )
                        {
                            SetPixelArgb(x, y, squareColor0);
                        }
                        else
                        {
                            double alpha = Helpers.Lerp(
                                dist, 
                                SquareHardAlphaWidth, SquareWidth,
                                1, 0);
                            NormalizedColor squareColor = NormalizedColor.FromArgb(alpha, 0, 1, 0);
                            SetPixelArgb(x, y, squareColor);
                        }
                    }                   
                }
            }
        }

        private bool IsInSquare(int x, int y)
        {
            if (x < SquareCenterX - SquareWidth) return false;
            if (x > SquareCenterX + SquareWidth) return false;
            if (y < SquareCenterY - SquareWidth) return false;
            if (y > SquareCenterY + SquareWidth) return false;

            return true;
        }

        // which "square ring" is the pixel in
        // ring 0 is the square center (pixel)
        // ring 1 is the 3x3 pixels strip around ring 0
        // ring 2 is the 5x5 pixels strip around ring 1
        // and so on
        private int GetSquareRingNumber(int x, int y)
        {
            //// find if pixel is in horizontal quadrant (▹,◁) or vertical quadrant (▿,▵)
            //bool inVerticalQuadrant = ( Math.Abs(SquareCenterY-y) > Math.Abs(SquareCenterX-x) );
            //if( inVerticalQuadrant )
            //    return Math.Abs(SquareCenterY-y);
            //else
            //    return Math.Abs(SquareCenterX-x);

            return Math.Max(Math.Abs(SquareCenterY - y) , Math.Abs(SquareCenterX - x));
        }

    }
}
