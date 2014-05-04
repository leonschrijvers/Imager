using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Imager.Elements
{
    public class Dot : IElement
    {
        private int _x;
        private int _y;

        private int _radius;

        private Brush _brush;

        public Dot(int x, int y, int radius, int color) : this(x, y, radius, color, color, color)
        {
           
        }

        public Dot(int x, int y, int radius, int r, int g, int b)
        {
            _x = x;
            _y = y;
            _radius = radius;

            _brush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void Render(Graphics g)
        {
            try
            {
                g.FillEllipse(_brush, _x, _y, _radius, _radius);
            }
            catch (Exception)
            {
                // TODO
            }

        }
    }
}
