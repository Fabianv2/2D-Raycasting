using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Raycasting
{
    public class Ray
    {
        private Vector _pos {  get; set; }
        const int rayLength = 10;
        
        public Ray(Vector pos)
        {
            _pos = pos;
        }

        public void CreateRay(List<Boundary> _boundaries, Canvas _Playground)
        {
            for (int i = 0; i < 360; i += 10)
            {
                double radians = i * Math.PI / 180;
                Vector direction = new Vector(Math.Cos(radians), Math.Sin(radians));

                Line ray = new Line()
                {
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    X1 = _pos.X,
                    Y1 = _pos.Y,
                    X2 = _pos.X + direction.X * rayLength,
                    Y2 = _pos.Y + direction.Y * rayLength
                };

                Vector? closestpoint = null;
                double minDist = double.MaxValue;

                foreach (var boundary in _boundaries)
                {
                    Vector? point = GetIntersection(ray, boundary.Line);
                    
                    if (point.HasValue)
                    {
                        double distance = Distance(_pos, point.Value);

                        if (distance < minDist)
                        {
                            minDist = distance;
                            closestpoint = point;
                        }
                    }
                }

                if (closestpoint.HasValue)
                {
                    ray.X2 = closestpoint.Value.X;
                    ray.Y2 = closestpoint.Value.Y;
                }

                _Playground.Children.Add(ray);
            }
        }


        private Vector? GetIntersection(Line ray, Line boundary)
        {
            //Boundary
            double x1 = boundary.X1;
            double y1 = boundary.Y1;
            double x2 = boundary.X2;
            double y2 = boundary.Y2;

            //Ray
            double x3 = ray.X1;
            double y3 = ray.Y1;
            double x4 = ray.X2;
            double y4 = ray.Y2;

            double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (denominator == 0)
            {
                return null;
            }

            double t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
            double u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;

            if (t > 0 && t < 1 && u > 0)
            {
                Vector point = new Vector();
                point.X = x1 + t * (x2 - x1);
                point.Y = y1 + t * (y2 - y1);

                return point;
            }
            else
            {
                return null;
            }
        }

        private double Distance(Vector pos1, Vector pos2)
        {
            return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }
    }
}
