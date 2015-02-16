using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace pngGenerator
{

    public struct Color64
    {
        public UInt16 A;
        public UInt16 R;
        public UInt16 G;
        public UInt16 B;

        private Color64(UInt16 a, UInt16 r, UInt16 g, UInt16 b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static Color64 FromArgb(UInt16 a, UInt16 r, UInt16 g, UInt16 b)
        {
            return new Color64(a, r, g, b);
        }
    }



    // color class for which each component is stored in a double belonging to [0, 1]
    public class NormalizedColor
    {
        public double A;
        public double R;
        public double G;
        public double B;

        public NormalizedColor(double a, double r, double g, double b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static NormalizedColor FromArgb(double a, double r, double g, double b)
        {
            return new NormalizedColor(a, r, g, b);
        }

        /// <summary>
        /// Convert to 32bpp Color
        /// </summary>
        /// <returns></returns>
        public Color ToColor32()
        {
            return Color.FromArgb(
                Convert.ToByte((255 * A).Clamp0_255()),
                Convert.ToByte((255 * R).Clamp0_255()),
                Convert.ToByte((255 * G).Clamp0_255()),
                Convert.ToByte((255 * B).Clamp0_255()));
        }

        /// <summary>
        /// Convert to 64bpp Color
        /// </summary>
        /// <returns></returns>
        public Color64 ToColor64()
        {
            return Color64.FromArgb(
                Convert.ToUInt16((65535 * A).Clamp0_65535()),
                Convert.ToUInt16((65535 * R).Clamp0_65535()),
                Convert.ToUInt16((65535 * G).Clamp0_65535()),
                Convert.ToUInt16((65535 * B).Clamp0_65535()));
        }
    }


    public static class Helpers
    {
        // http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static NormalizedColor HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0: r = v; g = mid1; b = m; break;
                    case 1: r = mid2; g = v; b = m; break;
                    case 2: r = m; g = v; b = mid1; break;
                    case 3: r = m; g = mid2; b = v; break;
                    case 4: r = mid1; g = m; b = v; break;
                    case 5: r = v; g = m; b = mid2; break;
                }
            }
            //Color rgb;
            //rgb.R = Convert.ToByte(r * 255.0f);
            //rgb.G = Convert.ToByte(g * 255.0f);
            //rgb.B = Convert.ToByte(b * 255.0f);
            return NormalizedColor.FromArgb(1, r, g, b);
        }

        public static double Lerp(
            double oldVal,
            double oldMin, double oldMax,
            double newMin, double newMax)
        {
            double newVal = newMin + (oldVal - oldMin) / (oldMax - oldMin) * (newMax - newMin);
            return newVal;
        }


        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ILCreateFromPathW(string pszPath);

        [DllImport("shell32.dll")]
        private static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, int cild, IntPtr apidl, int dwFlags);

        [DllImport("shell32.dll")]
        private static extern void ILFree(IntPtr pidl);

        // http://stackoverflow.com/a/14601675/758666
        public static void OpenFolderAndSelectFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            IntPtr pidl = ILCreateFromPathW(filePath);
            SHOpenFolderAndSelectItems(pidl, 0, IntPtr.Zero, 0);
            ILFree(pidl);
        }
    }

    public static class MyExtensions
    {
        public static double Clamp0_255(this double value)
        {
            return value.Clamp(0, 255);
        }

        public static double Clamp0_65535(this double value)
        {
            return value.Clamp(0, 65535);
        }

        //http://stackoverflow.com/a/2683487/758666
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }  
    
}
