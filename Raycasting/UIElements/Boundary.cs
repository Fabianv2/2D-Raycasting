using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Raycasting
{
    public class Boundary
    {
        public Line Line;

        public Boundary(double x1, double y1, double x2, double y2)
        {
            Line = new Line()
            {
                Stroke = Brushes.White,
                StrokeThickness = 2,
                Effect = new BlurEffect(),
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
        }
    }
}
