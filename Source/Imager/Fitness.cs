using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Imager
{
    public class Fitness
    {
        private Color[,] _source;

        public Fitness(Color[,] source)
        {
            _source = source;
        }

        public double Calculate(GenerationImage image)
        {
            double error = 0;

            using (Bitmap bitmap = image.GetBitmap())
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    image.Render(g);

                    BitmapData bmd = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,  PixelFormat.Format24bppRgb);

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Color c1 = GetPixel(bmd, x, y);
                            Color c2 = _source[x, y];

                            double pixelError = GetColorFitness(c1, c2);
                            error += pixelError;
                        }
                    }

                    bitmap.UnlockBits(bmd);
                }
            }

            return error;
        }

        private unsafe Color GetPixel(BitmapData bmd, int x, int y)
        {
            byte* p = (byte*)bmd.Scan0 + y * bmd.Stride + 3 * x;
            return Color.FromArgb(p[2], p[1], p[0]);
        }

        private double GetColorFitness(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            return r * r + g * g + b * b;
        }
    }
}
