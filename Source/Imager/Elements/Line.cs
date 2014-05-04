using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Imager.Elements
{
    public class Line : IElement
    {
        private int _x;
        private int _y;
        private int _length;
        private int _angle;

        private Random _random = new Random();

        private Pen _pen;

        public Line(int x, int y, int angle, int length, int color) : this(x, y, angle, length, color, color, color)
        {

        }

        public Line(int x, int y, int angle, int length, int r, int g, int b)
        {
            _x = x;
            _y = y;
            _angle = angle;
            _length = length;

            _pen = new Pen(Color.FromArgb(r, g, b));
        }

        public void Render(Graphics g)
        {
           double rad = _angle * (Math.PI / 180);

           int x2 = _x + (int)(_length * Math.Sin(rad));
           int y2 = _y + (int)(_length * Math.Cos(rad));

           try
           {
               g.DrawLine(_pen, _x, _y, x2, y2);
           } 
           catch(Exception) 
           {
               // TODO
           }
        }
    }
}
