﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Raycasting
{
    public partial class MainWindow : Window
    {
        private Ray _ray;
        private Random _random = new Random();
        private List<Boundary> _boundaryList = new List<Boundary>();

        private Vector prevMousePos;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoundaries();

            //tbx_RayAngle.Text = 2.ToString();
        }

        private void InitializeBoundaries()
        {
            int amountBoundaries = _random.Next(3, 13);

            for (int i = 0; i < amountBoundaries; i++)
            {
                double randomX1 = _random.Next(1, 1000);
                double randomY1 = _random.Next(1, 1000);
                double randomX2 = _random.Next(1, 1000);
                double randomY2 = _random.Next(1, 1000);

                _boundaryList.Add(new Boundary(randomX1, randomY1, randomX2, randomY2));
            }

            if (cbxBoundaryLines.IsChecked == true)
            {
                AddBoundaryLines();
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
                if (mousePos != prevMousePos)
                {
                    Playground.Children.Clear();

                    foreach (var boundary in _boundaryList)
                    {
                        Playground.Children.Add(boundary.Line);
                    }

                    if (_ray != null && _ray.addAngle.ToString() != slr_RayAngle.Value.ToString())
                    {
                        _ray.CreateRay(_boundaryList, Playground, mousePos);
                    }
                    else if (_ray != null)
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
            prevMousePos = mousePos;
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
            _boundaryList.Add(new Boundary(0, -1, 1000, -1));      //Top line
            _boundaryList.Add(new Boundary(-3, 0, -3, 1000));      //Left line
            _boundaryList.Add(new Boundary(1000, 0, 1000, 1000));  //Right line
            _boundaryList.Add(new Boundary(0, 1000, 1000, 1000));  //Bottom line
        }

        private void RemoveBoundaryLines()
        {
            for (int i = 0; i < 4; i++)
            {
                Boundary removeBoundary = _boundaryList[_boundaryList.Count - 1];
                Playground.Children.Remove(removeBoundary.Line);
                _boundaryList.RemoveAt(_boundaryList.Count - 1);
            }
        }


        private void cmbx_RayColor_DropDownClosed(object sender, EventArgs e)
        {
            if (_ray != null)
                _ray.ChangeRayColor(cmbx_RayColor.Text);
        }

        private void slr_RayAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double customAngle = slr_RayAngle.Value;

            if (_ray != null)
            {
                if (customAngle >= 0.1 && customAngle <= 30)
                {
                    _ray.addAngle = customAngle;
                }
                else
                {
                    _ray.addAngle = 10;
                }
            }

            Vector mousePos = (Vector)Mouse.GetPosition(Playground);
            if (_ray != null)
                _ray.CreateRay(_boundaryList, Playground, mousePos);
            //tbx_RayAngle
            //<TextBox x:Name="tbx_RayAngle" Text="13" TextWrapping="Wrap" TextChanged="tbx_RayAngle_TextChanged" Margin="1081,119,16,846" ToolTip="The lower the ray angle the more rays will be created. (Min value: 0,1 | Max value: 30)"/>
        }
    }
}