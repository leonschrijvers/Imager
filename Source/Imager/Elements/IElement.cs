using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Imager.Elements
{
    public interface IElement
    {
        void Render(Graphics g);
    }
}
