using InteractiveDataDisplay.WPF;
using LiveCharts;
using LiveCharts.Events;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using ZedGraph;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для WindowChart.xaml
    /// </summary>
    public partial class WindowChart : Window
    {
       public class VLine
        {
            public int[] listX { get; set; }
            public int[] listY { get; set; }
        }
        public WindowChart(string nameKl)
        {
            InitializeComponent();
           // seriesCollectionm = new SeriesCollection();
            
            Title = "График темпа счета кластера" + nameKl;
            //InizChart();

        }
        public void ShowViewModel()
        {

            Chart1.Title = "RK";
        }

        List<VLine> listY = new List<VLine>();
        public async void AddPointRaz(int[,] det)
        {
            // Start_time = DateTime.Now;
           
            //ClassTextFile.CreatFileData(PathText.Text + Start_time.Year.ToString() + "_" + Start_time.Month.ToString() + "_" + Start_time.Day.ToString() + "_" + Start_time.Hour.ToString() + "_" + Start_time.Minute.ToString());
            await linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { linegraph.Children.Clear(); }));

            var x = new int[det.Length/12];
            // var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v)/v).ToArray();
            var y = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
            }
           
            for (int i = 0; i < 12; i++)
            {
                LineGraph lg = new InteractiveDataDisplay.WPF.LineGraph();
               


                // lg.Stroke = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(xx), Convert.ToByte(255 -xx), Convert.ToByte(0 +xx)));
                switch (i)
                {
                    case 1:
                        lg.Stroke = new SolidColorBrush(Colors.Red);
                        break;
                    case 2:
                        lg.Stroke = new SolidColorBrush(Colors.Green);
                        break;
                    case 3:
                        lg.Stroke = new SolidColorBrush(Colors.Black);
                        break;
                    case 4:
                        lg.Stroke = new SolidColorBrush(Colors.Blue);
                        break;
                    case 5:
                        lg.Stroke = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 6:
                        lg.Stroke = new SolidColorBrush(Colors.Aqua);
                        break;
                    case 7:
                        lg.Stroke = new SolidColorBrush(Colors.Thistle);
                        break;
                    case 8:
                        lg.Stroke = new SolidColorBrush(Colors.AliceBlue);
                        break;
                    case 9:
                        lg.Stroke = new SolidColorBrush(Colors.Aquamarine);
                        break;
                    case 10:
                        lg.Stroke = new SolidColorBrush(Colors.DarkBlue);
                        break;
                    case 11:
                        lg.Stroke = new SolidColorBrush(Colors.Gray);
                        break;
                    case 12:
                        lg.Stroke = new SolidColorBrush(Colors.DarkGoldenrod);
                        break;
                    default:

                        break;
                }

          
                lg.Description = String.Format("Детектор №" + (i+1).ToString());
                lg.StrokeThickness = 2;
                for (int j = 0; j < det.Length/12; j++)
                {
                    y[j] = det[i, j];
                }
                lg.Plot(x, y);
                listY.Add(new VLine() { listX = x, listY=y });
               linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { linegraph.Children.Add(lg); }));
            }
           

           

            //barChart.PlotBars(y);

        }
        public async void AddPoint()
        {
            // Start_time = DateTime.Now;
            //ClassTextFile.CreatFileData(PathText.Text + Start_time.Year.ToString() + "_" + Start_time.Month.ToString() + "_" + Start_time.Day.ToString() + "_" + Start_time.Hour.ToString() + "_" + Start_time.Minute.ToString());
            await linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { linegraph.Children.Clear(); }));

         
            int xx = 25;
            listY.ElementAt(1).listX[6]=6;
            //listY.ElementAt(1).listY.Add(3);
            for (int i = 0; i < 12; i++)
            {
                var x = new int[listY.ElementAt(i).listX.Count()];
                // var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v)/v).ToArray();
                var y = new int[listY.ElementAt(i).listX.Count()];

                x = listY.ElementAt(i).listX.ToArray();
                y = listY.ElementAt(i).listY.ToArray();
                LineGraph lg = new InteractiveDataDisplay.WPF.LineGraph();
                await linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { linegraph.Children.Add(lg); }));
           


                // lg.Stroke = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(xx), Convert.ToByte(255 -xx), Convert.ToByte(0 +xx)));
                switch (i)
                {
                    case 1:
                        lg.Stroke = new SolidColorBrush(Colors.Red);
                        break;
                    case 2:
                        lg.Stroke = new SolidColorBrush(Colors.Green);
                        break;
                    case 3:
                        lg.Stroke = new SolidColorBrush(Colors.Black);
                        break;
                    case 4:
                        lg.Stroke = new SolidColorBrush(Colors.Blue);
                        break;
                    case 5:
                        lg.Stroke = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 6:
                        lg.Stroke = new SolidColorBrush(Colors.Aqua);
                        break;
                    case 7:
                        lg.Stroke = new SolidColorBrush(Colors.Thistle);
                        break;
                    case 8:
                        lg.Stroke = new SolidColorBrush(Colors.AliceBlue);
                        break;
                    case 9:
                        lg.Stroke = new SolidColorBrush(Colors.Aquamarine);
                        break;
                    case 10:
                        lg.Stroke = new SolidColorBrush(Colors.DarkBlue);
                        break;
                    case 11:
                        lg.Stroke = new SolidColorBrush(Colors.Gray);
                        break;
                    case 12:
                        lg.Stroke = new SolidColorBrush(Colors.DarkGoldenrod);
                        break;
                    default:

                        break;
                }

                xx += 25;
                lg.Description = String.Format("Детектор №" + i.ToString());
                lg.StrokeThickness = 2;
               
                lg.Plot(x, y);
               // listY.Add(new VLine() { listX = x.ToList(), listY = y.ToList() });
            }



            //barChart.PlotBars(y);

        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

            if (PanSet.Visibility == Visibility.Collapsed)
            {
                PanSet.Visibility = Visibility.Visible;
            }
            else
            {
                PanSet.Visibility = Visibility.Collapsed;
            }

        }
        int XOt = 0;
        int YOt = 0;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            Chart1.PlotOriginX = v;
            XOt = v;
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            Chart1.PlotWidth = v - XOt;
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            Chart1.PlotOriginY = v;
            YOt = v;
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            Chart1.PlotHeight = v - YOt;
        }



  

        private void ToggleButton_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {


            TextFX.IsEnabled = !ToggleAutoX.On1;
            TextEX.IsEnabled = !ToggleAutoX.On1;

            if (ToggleAutoX.On1)
            {
                Chart1.IsAutoFitEnabled = true;
                if (!ToggleAutoY.On1)
                {
                    int v = Convert.ToInt32(TextFY.Text);
                    Chart1.PlotOriginY = v;
                    YOt = v;
                    v = Convert.ToInt32(TextEY.Text);
                    Chart1.PlotHeight = v - YOt;


                }
            }
            else
            {
                int v = Convert.ToInt32(TextFX.Text);
                Chart1.PlotOriginX = v;
                XOt = v;
                v = Convert.ToInt32(TextEX.Text);
                Chart1.PlotWidth = v - XOt;
            }



        }

        private void ToggleButton_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            TextFY.IsEnabled = !ToggleAutoY.On1;
            TextEY.IsEnabled = !ToggleAutoY.On1;
            if (ToggleAutoY.On1)
            {
                Chart1.IsAutoFitEnabled = true;
                if (!ToggleAutoX.On1)
                {
                    int v = Convert.ToInt32(TextFX.Text);
                    Chart1.PlotOriginX = v;
                    XOt = v;
                    v = Convert.ToInt32(TextEX.Text);
                    Chart1.PlotWidth = v - XOt;




                }
            }
            else
            {
                int v = Convert.ToInt32(TextFY.Text);
                Chart1.PlotOriginY = v;
                YOt = v;
                v = Convert.ToInt32(TextEY.Text);
                Chart1.PlotHeight = v - YOt;
            }
        }


    }
}
