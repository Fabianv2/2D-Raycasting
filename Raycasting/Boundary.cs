using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace Raycasting
{
    public class Boundary
    {
        public Line Line { get; set; }

        public Boundary(double x1, double y1, double x2, double y2)
        {
            Line = new Line()
            {
                Stroke = Brushes.White,
                StrokeThickness = 1,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
        }
    }
}
