using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Imager.Elements
{
    public class Polygon : IElement
    {
        private Point[] _points;

        private Brush _brush;

        public Polygon(int x, int y, int size, int color) : this(x, y, size, color, color, color)
        {
        }

        public Polygon(int x, int y, int size, int r, int g, int b)
        {
            _brush = new SolidBrush(Color.FromArgb(r, g, b));

            Random random = new Random();
            int c = random.Next(10);

            _points = new Point[c];

            for (int i = 0; i < c; i++)
            {
                Point point = new Point();

                point.X = x + random.Next(-size, size);
                point.Y = y + random.Next(-size, size);

                _points[i] = point;
            }

        }

        public void Render(Graphics g)
        {
            try
            {
                g.FillPolygon(_brush, _points);
            }
            catch (Exception)
            { 

            }
        }
    }
}
