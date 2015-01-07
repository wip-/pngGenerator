using System;
using System.Diagnostics;

namespace pngGenerator.Generators
{
    public class VerticalColorStripes : BaseGenerator
    {
        public override String Filename { get { return "VerticalColorStripes"; } }
        public override int BitmapWidth { get { return 512; } }
        public override int BitmapHeight { get { return 512; } }

        private struct Color
        {
            public byte R;
            public byte G;
            public byte B;

            public Color(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }
        }

        private static readonly Color _black       = new Color(  0,   0,   0);
        private static readonly Color _brown       = new Color(128, 115, 100);
        private static readonly Color _lightGreen  = new Color(185, 220, 145);
        private static readonly Color _darkGreen   = new Color(115, 145,  95);
        private static readonly Color _lightBlue   = new Color(165, 225, 205);
        private static readonly Color _darkBlue    = new Color(115, 180, 160);
        private static readonly Color _lightRed    = new Color(255, 200, 175);
        private static readonly Color _darkRed     = new Color(230, 165, 140);

        private struct TriColor
        {
            public Color OutsideColor;
            public Color InsideColor;

            public TriColor(Color outside, Color inside)
            {
                OutsideColor = outside;
                InsideColor = inside;
            }
        }

        private static readonly TriColor _blackTriColor = new TriColor(_black, _brown);
        private static readonly TriColor _greenTriColor = new TriColor(_lightGreen, _darkGreen);
        private static readonly TriColor _blueTriColor = new TriColor(_lightBlue, _darkBlue);
        private static readonly TriColor _redTriColor = new TriColor(_lightRed, _darkRed);

        private static readonly TriColor[] _tricolors =
        {
            _blackTriColor,
            _greenTriColor,

            _blackTriColor,
            _blueTriColor,

            _blackTriColor,
            _redTriColor,

            _blackTriColor,
            _blueTriColor,
        };

        public override void FillPixelArray()
        {
            for (int x = 0; x < BitmapWidth; x++)
            {
                int triColorIndex    = (x%256)/32;
                int triColorSubIndex = (x%256)%32;

                TriColor tricolor = _tricolors[triColorIndex];
                Color color = (triColorSubIndex >= 11 && triColorSubIndex <= 20)
                    ? tricolor.InsideColor
                    : tricolor.OutsideColor;

                for (int y = 0; y < BitmapHeight; y++)
                {
                    SetPixelArgb(x, y, 255, color.R, color.G, color.B);
                }
            }
        }
    }
}
