using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PR5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int countDon = 30;
        List<double[]> dataList = new List<double[]>();
        DrawingGroup dg = new DrawingGroup();

        public MainWindow()
        {
            InitializeComponent();
            DataFill();
            Execute();
            image1.Source = new DrawingImage(dg);
        }
        //генерация точек
        void DataFill()
        {
            double[] sin = new double[countDon + 1];
            double[] cos = new double[countDon + 1];
            for (int i = 0; i < sin.Length; i++)
            {
                double angle = Math.PI * 2 / countDon * i;
                sin[i] = Math.Sin(angle);
                cos[i] = Math.Cos(angle);
            }
            dataList.Add(sin);
            dataList.Add(cos);
        }
        //Фон
        private void BackgroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing(); // об для описания геом фигуры
            RectangleGeometry rectangleGeometry = new RectangleGeometry(); // геометрия квадрата
            rectangleGeometry.Rect = new Rect(0, 0, 1, 1);
            geometryDrawing.Geometry = rectangleGeometry;
            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005); //перо рамки
            geometryDrawing.Brush = Brushes.Beige;//кисть закраски
            dg.Children.Add(geometryDrawing); // добавляем слой 
        }
        //горизонтальная сетка 
        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < 10; i++) // добавляем параллельные линии
            {
                LineGeometry line = new LineGeometry(new Point(1.0, i * 0.1), new Point(-0.1, i * 0.1));
                geometryGroup.Children.Add(line);
            }
            GeometryDrawing geometryDr = new GeometryDrawing();
            geometryDr.Geometry = geometryGroup;

            //настройка пера
            geometryDr.Pen = new Pen(Brushes.Gray, 0.003);
            double[] ds = { 1, 1, 1, 1, 1 };//штрих
            geometryDr.Pen.DashStyle = new DashStyle(ds, -1);
            geometryDr.Brush = Brushes.Beige;
            dg.Children.Add(geometryDr);
        }
        //синус линией
        private void SinFun()
        {
            //описание синусоиды
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < dataList[0].Length - 1; i++)
            {
                LineGeometry line = new LineGeometry(
                new Point((double)i / (double)countDon,
                0.5 - (dataList[0][i] / 2.0)),
                new Point((double)(i + 1) / (double)countDon,
                0.5 - (dataList[0][i + 1] / 2.0)));
                geometryGroup.Children.Add(line);
            }
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);
            dg.Children.Add(geometryDrawing);
        }
        private void CosFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < dataList[1].Length; i++)
            {
                EllipseGeometry ellips = new EllipseGeometry(
                new Point((double)i / (double)countDon,
                .5 - (dataList[1][i] / 2.0)), 0.01, 0.01);
                geometryGroup.Children.Add(ellips);
            }
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Green, 0.005);
            dg.Children.Add(geometryDrawing);
        }
        //Надписи
        private void MarkerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i <= 10; i++)
            {
                FormattedText formattedText = new FormattedText(
                String.Format("{0,7:F}", 1 - i * 0.2),
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                0.05,
                Brushes.Black);

                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Brush = Brushes.LightGray;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);

            dg.Children.Add(geometryDrawing);
        }

        void Execute()
        {
            DataFill();
            BackgroundFun();
            GridFun();
            SinFun();
            CosFun();
            MarkerFun();

        }
    }
}



