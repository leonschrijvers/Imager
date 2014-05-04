using Imager.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Imager
{
    public class GenerationImage
    {
        private List<IElement> _elements;

        private Random _random = new Random();

        private int _width;
        private int _height;

        public GenerationImage(int width, int height)
        {
            _width = width;
            _height = height;

            _elements = new List<IElement>();
        }

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(_width, _height);

            return bitmap;
        }

        public void Render(Graphics g)
        {
            g.Clear(Color.White);

            foreach (IElement element in _elements)
            {
                element.Render(g);
            }
        }

        public GenerationImage Clone()
        {
            GenerationImage image = new GenerationImage(_width, _height);

            foreach (IElement element in _elements)
            {
                image._elements.Add(element);
            }

            return image;
        }

        public void Mutate<T>(ColorMode colorMode) where T : IElement
        {
            _elements.Add(ElementFactory.GetElement<T>(_width, _height, colorMode));
        }
    }
}
