using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raycasting
{
    public partial class MainWindow : Window
    {
        private Ray _ray;
        private Random _random = new Random();
        private List<Ray> _rayList = new List<Ray>();
        private List<Boundary> _boundaryList = new List<Boundary>();
        

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoundaries();

            MouseMove += Playground_MouseMove;
        }

        private void InitializeBoundaries()
        {
            int amountBoundaries = 5;

            for (int i = 0; i < amountBoundaries; i++)
            {
                double randomX1 = _random.Next(1, 1000);
                double randomY1 = _random.Next(1, 1000);
                double randomX2 = _random.Next(1, 1000);
                double randomY2 = _random.Next(1, 1000);

                _boundaryList.Add(new Boundary(randomX1, randomY1, randomX2, randomY2));
            }

            //Borderlines
            //_boundaryList.Add(new Boundary(0, -1, 1000, -1)); //Top line
            //_boundaryList.Add(new Boundary(-3, 0, -3, 1000)); //Left line
            //_boundaryList.Add(new Boundary(1000, 0, 1000, 1000)); //Right line
            //_boundaryList.Add(new Boundary(0, 1000, 1000, 1000)); //Bottom line

            foreach (var boundary in _boundaryList)
            {
                Playground.Children.Add(boundary.Line);
            }
        }

        private void Playground_MouseMove(object sender, MouseEventArgs e)
        {
            Vector mousePos = (Vector)e.GetPosition(this);

            Playground.Children.Clear();

            foreach (var boundary in _boundaryList)
            {
                Playground.Children.Add(boundary.Line);
            }

            _rayList.Clear();
            _ray = new Ray(mousePos);
            _ray.CreateRay(_boundaryList, Playground);
        }
    }
}