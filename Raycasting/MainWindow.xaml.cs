using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Raycasting
{
    public partial class MainWindow : Window
    {
        private Ray _ray;
        private Random _random = new Random();
        private List<Boundary> _boundaryList = new List<Boundary>();
        private List<Boundary> _boundaryLines = new List<Boundary>();
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoundaries();
        }

        private void InitializeBoundaries()
        {
            int amountBoundaries = _random.Next(4, 15);

            for (int i = 0; i < amountBoundaries; i++)
            {
                double randomX1 = _random.Next(1, 1000);
                double randomY1 = _random.Next(1, 1000);
                double randomX2 = _random.Next(1, 1000);
                double randomY2 = _random.Next(1, 1000);

                _boundaryList.Add(new Boundary(randomX1, randomY1, randomX2, randomY2));
            }

            foreach (var boundary in _boundaryList)
            {
                Playground.Children.Add(boundary.Line);
            }
        }

        private void Playground_MouseMove(object sender, MouseEventArgs e)
        {
            Vector mousePos = (Vector)e.GetPosition(Playground);

            if (mousePos.X >= 0 && mousePos.X <= 1000 && mousePos.Y >= 0 && mousePos.Y <= 1000)
            {
                Playground.Children.Clear();

                if (_boundaryLines != null && _boundaryLines.Count != 0)
                {
                    foreach (var boundary in _boundaryLines)
                    {
                        Playground.Children.Add(boundary.Line);
                    }
                }

                foreach (var boundary in _boundaryList)
                {
                    Playground.Children.Add(boundary.Line);
                }

                if (_ray != null)
                {
                    _ray.UpdateRay(_boundaryList, Playground, mousePos);
                }
                else
                {
                    _ray = new Ray(mousePos);
                    _ray.CreateRay(_boundaryList, Playground, mousePos);
                }
            }
        }

        private void btnCreateBoundaries_Click(object sender, RoutedEventArgs e)
        {
            CreateBoundaries();
        }

        private void CreateBoundaries()
        {
            _boundaryList.Clear();
            Playground.Children.Clear();
            InitializeBoundaries();

            if (cbxBorderlines.IsChecked == true)
            {
                AddBoundaryLines();
            }
        }

        private void BoundaryLines_Checked(object sender, RoutedEventArgs e)
        {
            AddBoundaryLines();
        }

        private void BoundaryLines_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveBoundaryLines();
        }

        private void AddBoundaryLines()
        {
            _boundaryLines.Add(new Boundary(0, -1, 1000, -1));      //Top line
            _boundaryLines.Add(new Boundary(-3, 0, -3, 1000));      //Left line
            _boundaryLines.Add(new Boundary(1000, 0, 1000, 1000));  //Right line
            _boundaryLines.Add(new Boundary(0, 1000, 1000, 1000));  //Bottom line
            //_boundaryList.Add(new Boundary(0, -1, 1000, -1));      //Top line
            //_boundaryList.Add(new Boundary(-3, 0, -3, 1000));      //Left line
            //_boundaryList.Add(new Boundary(1000, 0, 1000, 1000));  //Right line
            //_boundaryList.Add(new Boundary(0, 1000, 1000, 1000));  //Bottom line

            //foreach (var boundary in _boundaryLines)
            //{
            //    Playground.Children.Add(boundary.Line);
            //}
        }

        private void RemoveBoundaryLines()
        {
            foreach (var boundary in _boundaryLines)
            {
                Playground.Children.Remove(boundary.Line);
            }
            _boundaryLines.Clear();
        }
    }
}