using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Fractal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        List<Point> StartPoints = new ()
        {
            new Point(150, 150),
            new Point(200, 450),
            new Point(400, 250),

        };
        int Delay = 1000;
        readonly Random r = new ();
        List<MyPoint> myPoints = new ();

        public MainWindow()
        {
            InitializeComponent();

            Run();

        }

        void Run()
        {
            DrawTriangle();
        }

        void InitPoints(int n)
        {

            MyPoint firstPoint = new(2500, 2500, 2, 2, Brushes.Blue);
            MyPoint nextPoint = firstPoint;


            for (int i = 0; i < n; i++)
            {
                Point p = StartPoints[r.Next(3)];
                MyPoint prewPoint = nextPoint;
                myPoints.Add(prewPoint);
                double newX = prewPoint.X - (prewPoint.X - p.X) / 2;
                double newY = prewPoint.Y - (prewPoint.Y - p.Y) / 2;

                nextPoint = (new MyPoint(newX, newY, 2, 2, Brushes.Blue, prewPoint));
            }

        }

        async Task DrawPoints(List<MyPoint> points)
        {

            foreach (var point in points)
            {

                Ellipse ellipse = new Ellipse();
                ellipse.Fill = point.Color;
                ellipse.Width = point.Width;
                ellipse.Height = point.Height;
                Canvas.SetLeft(ellipse, point.X);
                Canvas.SetTop(ellipse, point.Y);

                canvas.Children.Add(ellipse);
                
                await Task.Delay(Delay);
            }
        }




        void DrawTriangle(){

            Polygon polygon = new() { 
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
        
        async private void DrawPoints(object sender, RoutedEventArgs e)
        {
            myPoints.Clear();
            canvas.Children.Clear();
            DrawTriangle();
            InitPoints(int.Parse(PointsTextBox.Text));
            await DrawPoints(myPoints);

        }

        private void DelayTextChanged(object sender, TextChangedEventArgs e)
        {
            Delay = int.Parse(DelayTextBox.Text);
        }
    }
}
