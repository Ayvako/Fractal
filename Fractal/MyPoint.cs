using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fractal
{
    class MyPoint
    {

        public double X { get; set; }
        public double Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public MyPoint Parent { get; set; }
        public SolidColorBrush Color { get; set; }


        public MyPoint(double x, double y, int h, int w, SolidColorBrush color)
        {
            X = x;
            Y = y;
            Height = h;
            Width = w;
            Color = color;

            Parent = null;
        }

        public MyPoint(double x, double y, int h, int w, SolidColorBrush color, MyPoint parent)
        {
            X = x;
            Y = y;
            Height = h;
            Width = w;
            Color = color;

            Parent = parent;


        }
    }
}
