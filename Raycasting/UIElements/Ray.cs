using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Raycasting
{
    public class Ray
    {
        private Vector _pos;
        const int rayLength = 5;
        public double addAngle = 13;
        public List<Line> _rays = new List<Line>();

        public Ray(Vector pos)
        {
            _pos = pos;
        }

        public void CreateRay(List<Boundary> _boundaries, Canvas _Playground, Vector mousePos)
        {
            _rays.Clear();
            _Playground.Children.Clear();

            for (double i = 0; i < 360; i += addAngle)
            {
                double radians = i * Math.PI / 180;
                Vector direction = new Vector(Math.Cos(radians), Math.Sin(radians));
                
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0);
                gradientBrush.EndPoint = new Point(1, 1);
                gradientBrush.GradientStops.Add(new GradientStop(Colors.SpringGreen, 1));

                Line ray = new Line()
                {
                    Stroke = gradientBrush,
                    StrokeThickness = 1,
                    X1 = _pos.X,
                    Y1 = _pos.Y,
                    X2 = _pos.X + direction.X * rayLength,
                    Y2 = _pos.Y + direction.Y * rayLength
                };

                _rays.Add(ray);

                Line updatedRay = GetClosestpoint(_boundaries, ray, mousePos);

                if (updatedRay != null)
                {
                    _Playground.Children.Add(updatedRay);
                }
                else
                {
                    _Playground.Children.Add(ray);
                }
            }
        }

        public void UpdateRay(List<Boundary> _boundaries, Canvas _Playground, Vector mousePos)
        {
            for (double i = 0; i < _rays.Count; i++)
            {
                double radians = (i * addAngle) * Math.PI / 180;
                Vector direction = new Vector(Math.Cos(radians), Math.Sin(radians));

                int a = Convert.ToInt32(i);
                _rays[a].X1 = mousePos.X;
                _rays[a].Y1 = mousePos.Y;
                _rays[a].X2 = mousePos.X + direction.X * rayLength;
                _rays[a].Y2 = mousePos.Y + direction.Y * rayLength;

                Line updatedRay = GetClosestpoint(_boundaries, _rays[a], mousePos);

                if (updatedRay != null)
                {
                    _Playground.Children.Add(updatedRay);
                }
                else
                {
                    _Playground.Children.Add(_rays[a]);
                }
            }
        }

        private Line GetClosestpoint(List<Boundary> _boundaries, Line ray, Vector pos)
        {
            Vector? closestpoint = null;
            double minDist = double.MaxValue;

            foreach (var boundary in _boundaries)
            {
                Vector? point = GetIntersection(ray, boundary.Line);

                if (point.HasValue)
                {
                    double distance = Distance(pos, point.Value);

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

                return ray;
            }
            return null;
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

        public void ChangeRayColor(string color)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            foreach (var ray in _rays)
            {
                switch (color)
                {
                    case "Red":
                        brush.GradientStops.Add(new GradientStop(Colors.Red, 1));
                        break;
                    case "Blue":
                        brush.GradientStops.Add(new GradientStop(Colors.Blue, 1));
                        break;
                    case "Yellow":
                        brush.GradientStops.Add(new GradientStop(Colors.Yellow, 1));
                        break;
                    case "Green":
                        brush.GradientStops.Add(new GradientStop(Colors.Green, 1));
                        break;
                    case "LightYellow":
                        brush.GradientStops.Add(new GradientStop(Colors.LightYellow, 1));
                        break;
                    case "LightCoral":
                        brush.GradientStops.Add(new GradientStop(Colors.LightCoral, 1));
                        break;
                    case "IndianRed":
                        brush.GradientStops.Add(new GradientStop(Colors.IndianRed, 1));
                        break;
                    default:
                        brush.GradientStops.Add(new GradientStop(Colors.SpringGreen, 1));
                        break;
                }
                ray.Stroke = brush;
            }
        }
    }
}