using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imager.Elements
{
    public static class ElementFactory
    {
        public static IElement GetElement<T>(int width, int height, ColorMode colorMode)
        {
            Random r = new Random();

            if (typeof(T) == typeof(Dot))
            {
                if (colorMode == ColorMode.Color)
                {
                    return new Dot(r.Next(width), r.Next(height), r.Next(width / 4), r.Next(255), r.Next(255), r.Next(255));
                }
                else
                {
                    return new Dot(r.Next(width), r.Next(height), r.Next(width / 4), r.Next(255));
                }
            }
            else if (typeof(T) == typeof(Line))
            {
                if (colorMode == ColorMode.Color)
                {
                    return new Line(r.Next(width), r.Next(height), r.Next(360), r.Next(width / 4), r.Next(255), r.Next(255), r.Next(255));
                }
                else
                {
                    return new Line(r.Next(width), r.Next(height), r.Next(360), r.Next(width / 4), r.Next(255));
                }
            }
            else if (typeof(T) == typeof(Polygon))
            {
                if (colorMode == ColorMode.Color)
                {
                    return new Polygon(r.Next(width), r.Next(height), r.Next(width / 4), r.Next(255), r.Next(255), r.Next(255));
                }
                else
                {
                    return new Polygon(r.Next(width), r.Next(height), r.Next(width / 4), r.Next(255));
                }
            }
            else
            {
                return null;
            }
        }
    }
}
