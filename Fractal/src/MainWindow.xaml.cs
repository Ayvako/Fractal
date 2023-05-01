using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractal
{

    public partial class MainWindow : Window
    {
        List<Point> StartPoints = new ();
        List<MyPoint> MyPoints = new ();
        Random rnd = new();
        int Delay;

        public MainWindow()
        {
            InitializeComponent();

        }

        void InitPoints(int n)
        {
            MyPoint firstPoint = new(rnd.NextDouble() * canvas.ActualWidth, rnd.NextDouble() * canvas.ActualHeight, 2, 2, Brushes.Red);
            MyPoint nextPoint = firstPoint;

            for (int i = 0; i < n; i++)
            {
                Point p = StartPoints[rnd.Next(3)];
                MyPoint prewPoint = nextPoint;
                MyPoints.Add(prewPoint);
                double newX = prewPoint.X - (prewPoint.X - p.X) / 2;
                double newY = prewPoint.Y - (prewPoint.Y - p.Y) / 2;

                nextPoint = (new MyPoint(newX, newY, 2, 2, Brushes.Blue, prewPoint));
            }

        }

        async Task DrawPoints(List<MyPoint> points)
        {

            foreach (var point in points)
            {
                Ellipse ellipse = new()
                {
                    Fill = point.Color,
                    Width = point.Width,
                    Height = point.Height
                };

                Canvas.SetLeft(ellipse, point.X);
                Canvas.SetTop(ellipse, point.Y);

                canvas.Children.Add(ellipse);
                
                await Task.Delay(Delay);
            }
        }

        
        async private void DrawPoints(object sender, RoutedEventArgs e)
        {
            MyPoints.Clear();
            ClearCanvas<Ellipse>();
            InitPoints(int.Parse(PointsTextBox.Text));
            await DrawPoints(MyPoints);

        }

        private void ClearCanvas<T>() where T:UIElement
        {
            foreach (var child in canvas.Children.OfType<T>().ToList())
            {
                canvas.Children.Remove(child);
            }

        }

        private void DelayTextChanged(object sender, TextChangedEventArgs e)
        {
            Delay = int.Parse(DelayTextBox.Text);
        }

        private void DrawTriangle(object sender, RoutedEventArgs e)
        {
            ClearCanvas<Polygon>();
            ClearCanvas<Ellipse>();



            do
            {
                StartPoints = new List<Point>() {
                    new Point(rnd.NextDouble() * canvas.ActualWidth, rnd.NextDouble() * canvas.ActualHeight),
                    new Point(rnd.NextDouble() * canvas.ActualWidth, rnd.NextDouble() * canvas.ActualHeight),
                    new Point(rnd.NextDouble() * canvas.ActualWidth, rnd.NextDouble() * canvas.ActualHeight)
                };

            } while (!TriangleIsValid(StartPoints));

            

            Polygon polygon = new()
            {
                Stroke = Brushes.Black,
                Fill = Brushes.LightBlue,
                StrokeThickness = 2
            };

            foreach (var p in StartPoints)
            {
                polygon.Points.Add(p);
            }

            canvas.Children.Add(polygon);
        }

        private static bool TriangleIsValid(List<Point> points)
        {

            Point p1 = points[0];
            Point p2 = points[1];
            Point p3 = points[2];
            double a = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            double b = Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
            double c = Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));



            if (CalculateTriangleAngles(a, b, c).Min() < 20)
                return false;

            if (CalculateTriangleArea(a, b, c) < 40000)
                return false;


            return true;
        }

        
        private static List<double> CalculateTriangleAngles(double a, double b, double c)
        {
            double alpha = Math.Acos((b * b + c * c - a * a) / (2 * b * c));
            double beta = Math.Acos((c * c + a * a - b * b) / (2 * c * a));
            double gamma = Math.Acos((a * a + b * b - c * c) / (2 * a * b));

            alpha = alpha * 180 / Math.PI;
            beta = beta * 180 / Math.PI;
            gamma = gamma * 180 / Math.PI;

            return new() { alpha, beta, gamma };
        }
        private static double CalculateTriangleArea(double a, double b, double c)
        {
            double p = (a + b + c) / 2.0;
            double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            Debug.WriteLine(area);
            return area;
        }

    }
}
