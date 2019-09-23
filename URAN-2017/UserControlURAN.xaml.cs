using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для UserControlURAN.xaml
    /// </summary>
    public partial class UserControlURAN : UserControl, INotifyPropertyChanged
    {
        public UserControlURAN()
        {
            InitializeComponent();
            vizualDetectors();
        }
        List<Detector> detectors { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowDeteClea()
        {
            canvas.Children.Clear();

            if (detectors.Count>0)
            {
                foreach(var d in detectors)
                {
                    d.ColorSFil = new SolidColorBrush(Colors.White);
                    d.ColorSStroce = new SolidColorBrush(Colors.Gray);
                }
            }


            k1d1.Fill = new SolidColorBrush(Colors.White);

            k1d2.Fill = new SolidColorBrush(Colors.White);
            k1d3.Fill = new SolidColorBrush(Colors.White);
            k1d4.Fill = new SolidColorBrush(Colors.White);
            k1d5.Fill = new SolidColorBrush(Colors.White);
            k1d6.Fill = new SolidColorBrush(Colors.White);
            k1d7.Fill = new SolidColorBrush(Colors.White);
            k1d8.Fill = new SolidColorBrush(Colors.White);
            k1d9.Fill = new SolidColorBrush(Colors.White);
            k1d10.Fill = new SolidColorBrush(Colors.White);
            k1d11.Fill = new SolidColorBrush(Colors.White);
            k1d12.Fill = new SolidColorBrush(Colors.White);


            k2d1.Stroke = new SolidColorBrush(Colors.Gray);
            k2d2.Stroke = new SolidColorBrush(Colors.Gray);
            k2d3.Stroke = new SolidColorBrush(Colors.Gray);
            k2d4.Stroke = new SolidColorBrush(Colors.Gray);
            k2d5.Stroke = new SolidColorBrush(Colors.Gray);
            k2d6.Stroke = new SolidColorBrush(Colors.Gray);
            k2d7.Stroke = new SolidColorBrush(Colors.Gray);
            k2d8.Stroke = new SolidColorBrush(Colors.Gray);
            k2d9.Stroke = new SolidColorBrush(Colors.Gray);
            k2d10.Stroke = new SolidColorBrush(Colors.Gray);
            k2d11.Stroke = new SolidColorBrush(Colors.Gray);
            k2d12.Stroke = new SolidColorBrush(Colors.Gray);

            k2d1.Fill = new SolidColorBrush(Colors.White);
            k2d2.Fill = new SolidColorBrush(Colors.White);
            k2d3.Fill = new SolidColorBrush(Colors.White);
            k2d4.Fill = new SolidColorBrush(Colors.White);
            k2d5.Fill = new SolidColorBrush(Colors.White);
            k2d6.Fill = new SolidColorBrush(Colors.White);
            k2d7.Fill = new SolidColorBrush(Colors.White);
            k2d8.Fill = new SolidColorBrush(Colors.White);
            k2d9.Fill = new SolidColorBrush(Colors.White);
            k2d10.Fill = new SolidColorBrush(Colors.White);
            k2d11.Fill = new SolidColorBrush(Colors.White);
            k2d12.Fill = new SolidColorBrush(Colors.White);

            k3d1.Stroke = new SolidColorBrush(Colors.Gray);
            k3d2.Stroke = new SolidColorBrush(Colors.Gray);
            k3d3.Stroke = new SolidColorBrush(Colors.Gray);
            k3d4.Stroke = new SolidColorBrush(Colors.Gray);
            k3d5.Stroke = new SolidColorBrush(Colors.Gray);
            k3d6.Stroke = new SolidColorBrush(Colors.Gray);
            k3d7.Stroke = new SolidColorBrush(Colors.Gray);
            k3d8.Stroke = new SolidColorBrush(Colors.Gray);
            k3d9.Stroke = new SolidColorBrush(Colors.Gray);
            k3d10.Stroke = new SolidColorBrush(Colors.Gray);
            k3d11.Stroke = new SolidColorBrush(Colors.Gray);
            k3d12.Stroke = new SolidColorBrush(Colors.Gray);

            k3d1.Fill = new SolidColorBrush(Colors.White);
            k3d2.Fill = new SolidColorBrush(Colors.White);
            k3d3.Fill = new SolidColorBrush(Colors.White);
            k3d4.Fill = new SolidColorBrush(Colors.White);
            k3d5.Fill = new SolidColorBrush(Colors.White);
            k3d6.Fill = new SolidColorBrush(Colors.White);
            k3d7.Fill = new SolidColorBrush(Colors.White);
            k3d8.Fill = new SolidColorBrush(Colors.White);
            k3d9.Fill = new SolidColorBrush(Colors.White);
            k3d10.Fill = new SolidColorBrush(Colors.White);
            k3d11.Fill = new SolidColorBrush(Colors.White);
            k3d12.Fill = new SolidColorBrush(Colors.White);

            k4d1.Stroke = new SolidColorBrush(Colors.Gray);
            k4d2.Stroke = new SolidColorBrush(Colors.Gray);
            k4d3.Stroke = new SolidColorBrush(Colors.Gray);
            k4d4.Stroke = new SolidColorBrush(Colors.Gray);
            k4d5.Stroke = new SolidColorBrush(Colors.Gray);
            k4d6.Stroke = new SolidColorBrush(Colors.Gray);
            k4d7.Stroke = new SolidColorBrush(Colors.Gray);
            k4d8.Stroke = new SolidColorBrush(Colors.Gray);
            k4d9.Stroke = new SolidColorBrush(Colors.Gray);
            k4d10.Stroke = new SolidColorBrush(Colors.Gray);
            k4d11.Stroke = new SolidColorBrush(Colors.Gray);
            k4d12.Stroke = new SolidColorBrush(Colors.Gray);

            k4d1.Fill = new SolidColorBrush(Colors.White);
            k4d2.Fill = new SolidColorBrush(Colors.White);
            k4d3.Fill = new SolidColorBrush(Colors.White);
            k4d4.Fill = new SolidColorBrush(Colors.White);
            k4d5.Fill = new SolidColorBrush(Colors.White);
            k4d6.Fill = new SolidColorBrush(Colors.White);
            k4d7.Fill = new SolidColorBrush(Colors.White);
            k4d8.Fill = new SolidColorBrush(Colors.White);
            k4d9.Fill = new SolidColorBrush(Colors.White);
            k4d10.Fill = new SolidColorBrush(Colors.White);
            k4d11.Fill = new SolidColorBrush(Colors.White);
            k4d12.Fill = new SolidColorBrush(Colors.White);

            k5d1.Stroke = new SolidColorBrush(Colors.Gray);
            k5d2.Stroke = new SolidColorBrush(Colors.Gray);
            k5d3.Stroke = new SolidColorBrush(Colors.Gray);
            k5d4.Stroke = new SolidColorBrush(Colors.Gray);
            k5d5.Stroke = new SolidColorBrush(Colors.Gray);
            k5d6.Stroke = new SolidColorBrush(Colors.Gray);
            k5d7.Stroke = new SolidColorBrush(Colors.Gray);
            k5d8.Stroke = new SolidColorBrush(Colors.Gray);
            k5d9.Stroke = new SolidColorBrush(Colors.Gray);
            k5d10.Stroke = new SolidColorBrush(Colors.Gray);
            k5d11.Stroke = new SolidColorBrush(Colors.Gray);
            k5d12.Stroke = new SolidColorBrush(Colors.Gray);

            k5d1.Fill = new SolidColorBrush(Colors.White);
            k5d2.Fill = new SolidColorBrush(Colors.White);
            k5d3.Fill = new SolidColorBrush(Colors.White);
            k5d4.Fill = new SolidColorBrush(Colors.White);
            k5d5.Fill = new SolidColorBrush(Colors.White);
            k5d6.Fill = new SolidColorBrush(Colors.White);
            k5d7.Fill = new SolidColorBrush(Colors.White);
            k5d8.Fill = new SolidColorBrush(Colors.White);
            k5d9.Fill = new SolidColorBrush(Colors.White);
            k5d10.Fill = new SolidColorBrush(Colors.White);
            k5d11.Fill = new SolidColorBrush(Colors.White);
            k5d12.Fill = new SolidColorBrush(Colors.White);

            k6d1.Stroke = new SolidColorBrush(Colors.Gray);
            k6d2.Stroke = new SolidColorBrush(Colors.Gray);
            k6d3.Stroke = new SolidColorBrush(Colors.Gray);
            k6d4.Stroke = new SolidColorBrush(Colors.Gray);
            k6d5.Stroke = new SolidColorBrush(Colors.Gray);
            k6d6.Stroke = new SolidColorBrush(Colors.Gray);
            k6d7.Stroke = new SolidColorBrush(Colors.Gray);
            k6d8.Stroke = new SolidColorBrush(Colors.Gray);
            k6d9.Stroke = new SolidColorBrush(Colors.Gray);
            k6d10.Stroke = new SolidColorBrush(Colors.Gray);
            k6d11.Stroke = new SolidColorBrush(Colors.Gray);
            k6d12.Stroke = new SolidColorBrush(Colors.Gray);

            k6d1.Fill = new SolidColorBrush(Colors.White);
            k6d2.Fill = new SolidColorBrush(Colors.White);
            k6d3.Fill = new SolidColorBrush(Colors.White);
            k6d4.Fill = new SolidColorBrush(Colors.White);
            k6d5.Fill = new SolidColorBrush(Colors.White);
            k6d6.Fill = new SolidColorBrush(Colors.White);
            k6d7.Fill = new SolidColorBrush(Colors.White);
            k6d8.Fill = new SolidColorBrush(Colors.White);
            k6d9.Fill = new SolidColorBrush(Colors.White);
            k6d10.Fill = new SolidColorBrush(Colors.White);
            k6d11.Fill = new SolidColorBrush(Colors.White);
            k6d12.Fill = new SolidColorBrush(Colors.White);

        }
        public void vizualDetectors()
        {
            Ellipse ellipse = new Ellipse() { Width = 20, Height=20, StrokeThickness=3, Fill=new SolidColorBrush(Colors.Red) };
            
            canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, 100);
            Canvas.SetTop(ellipse, 100);

            ellipse = new Ellipse() { Width = 20, Height = 20, StrokeThickness = 3, Fill = new SolidColorBrush(Colors.Red) };

            canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, 50);
            Canvas.SetTop(ellipse, 50);
        }
        private async Task<Color> GetColorByOffset(GradientStopCollection collection, double offset)
        {
            GradientStop[] stops = collection.OrderBy(x => x.Offset).ToArray();
            if (offset <= 0)
            { return stops[0].Color; }

            if (offset >= 1)
            { return stops[stops.Length - 1].Color; }
            GradientStop left = stops[0], right = null;
            foreach (GradientStop stop in stops)
            {
                if (stop.Offset >= offset)
                {
                    right = stop;
                    break;
                }
                left = stop;
            }

            offset = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 2);

            byte a = (byte)((right.Color.A - left.Color.A) * offset + left.Color.A);
            byte r = (byte)((right.Color.R - left.Color.R) * offset + left.Color.R);
            byte g = (byte)((right.Color.G - left.Color.G) * offset + left.Color.G);
            byte b = (byte)((right.Color.B - left.Color.B) * offset + left.Color.B);
            return Color.FromArgb(a, r, g, b);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  Debug.WriteLine(text1d1.Background.ToString());
          
          
        }
        /*  public async Task ShowDetecAsync(List<int> classSob, int Kl)
 {
    // ShowDeteClea();
     //int[] intmMax = new int[classSobL.Count];
     //int[] intmMin = new int[classSobL.Count];


     int i = 0;
  //   foreach (ClassSob classSob in classSobL)
     {
     //    intmMax[i] = classSob.mAmp.Max();
        // intmMin[i] = classSob.mAmp.Min();

         i++;


     }
     foreach (int x in classSob)
     {


         int max = intmMax.Max();
         int min = intmMin.Min();



         double step = max / 5;


         Text3.Text = step.ToString();
         Text2.Text = (2 * step).ToString();
         Text1.Text = (3 * step).ToString();
         Text0.Text = (4 * step).ToString();
         TextMax.Text = max.ToString();


         if (classSob.nameklaster == "1")
         {

             k1d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k1d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);





             k1d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k1d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k1d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k1d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k1d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k1d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k1d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k1d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k1d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k1d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k1d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k1d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));





         }
         if (classSob.nameklaster == "2")
         {
             k2d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k2d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);


             k2d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k2d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k2d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k2d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k2d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k2d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k2d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k2d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k2d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k2d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k2d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k2d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));

         }
         if (classSob.nameklaster == "3")
         {
             k3d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k3d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);


             k3d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k3d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k3d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k3d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k3d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k3d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k3d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k3d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k3d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k3d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k3d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k3d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));
         }

         if (classSob.nameklaster == "4")
         {
             k4d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k4d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

             k4d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k4d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k4d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k4d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k4d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k4d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k4d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k4d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k4d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k4d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k4d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k4d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));
         }
         if (classSob.nameklaster == "5")
         {
             k5d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k5d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k5d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k5d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k5d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k5d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k5d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k5d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k5d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k5d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k5d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k5d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k5d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));

         }
         if (classSob.nameklaster == "6")
         {
             k6d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
             k6d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

             k6d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[0])) / (double)(max - min))));
             k6d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[1])) / (double)(max - min))));
             k6d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[2])) / (double)(max - min))));
             k6d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[3])) / (double)(max - min))));
             k6d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[4])) / (double)(max - min))));
             k6d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[5])) / (double)(max - min))));
             k6d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[6])) / (double)(max - min))));
             k6d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[7])) / (double)(max - min))));
             k6d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[8])) / (double)(max - min))));
             k6d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[9])) / (double)(max - min))));
             k6d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[10])) / (double)(max - min))));
             k6d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(max - classSob.mAmp[11])) / (double)(max - min))));



         }
     }
 }
 public async Task ShowDetecТAsync(List<ClassSob> classSobL)
 {
     ShowDeteClea();



     int[] intmMaxn = new int[classSobL.Count];
     int[] intmMinn = new int[classSobL.Count];
     int i = 0;
     foreach (ClassSob classSob in classSobL)
     {

         intmMaxn[i] = classSob.mCountN.Max();
         intmMinn[i] = classSob.mCountN.Min();
         i++;


     }
     foreach (ClassSob classSob in classSobL)
     {

         if (intmMaxn.Sum() != 0)
         {


             int maxn = intmMaxn.Max();
             int minn = intmMinn.Min();



             double stepn = maxn / 5;

             Text3.Text = stepn.ToString();
             Text2.Text = (2 * stepn).ToString();
             Text1.Text = (3 * stepn).ToString();
             Text0.Text = (4 * stepn).ToString();
             TextMax.Text = maxn.ToString();


             if (classSob.nameklaster == "1")
             {

                 k1d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k1d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);







                 k1d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k1d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k1d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k1d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k1d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k1d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k1d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k1d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k1d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k1d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k1d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k1d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));




             }
             if (classSob.nameklaster == "2")
             {
                 k2d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k2d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k2d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k2d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k2d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k2d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k2d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k2d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k2d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k2d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k2d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k2d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k2d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));




             }
             if (classSob.nameklaster == "3")
             {
                 k3d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k3d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);


                 k3d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k3d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k3d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k3d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k3d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k3d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k3d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k3d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k3d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k3d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k3d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k3d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));
             }

             if (classSob.nameklaster == "4")
             {
                 k4d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k4d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

                 k4d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k4d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k4d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k4d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k4d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k4d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k4d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k4d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k4d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k4d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k4d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k4d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));
             }
             if (classSob.nameklaster == "5")
             {
                 k5d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k5d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k5d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k5d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k5d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k5d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k5d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k5d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k5d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k5d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k5d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k5d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k5d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));

             }
             if (classSob.nameklaster == "6")
             {
                 k6d1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d2.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d3.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d4.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d5.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d6.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d7.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d8.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d9.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d10.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d11.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                 k6d12.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);

                 k6d1.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[0])) / (double)(maxn - minn))));
                 k6d2.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[1])) / (double)(maxn - minn))));
                 k6d3.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[2])) / (double)(maxn - minn))));
                 k6d4.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[3])) / (double)(maxn - minn))));
                 k6d5.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[4])) / (double)(maxn - minn))));
                 k6d6.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[5])) / (double)(maxn - minn))));
                 k6d7.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[6])) / (double)(maxn - minn))));
                 k6d8.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[7])) / (double)(maxn - minn))));
                 k6d9.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[8])) / (double)(maxn - minn))));
                 k6d10.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[9])) / (double)(maxn - minn))));
                 k6d11.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[10])) / (double)(maxn - minn))));
                 k6d12.Fill = new SolidColorBrush(await GetColorByOffset(GrCol.GradientStops, (((double)(maxn - classSob.mCountN[11])) / (double)(maxn - minn))));



             }
         }*/
        //}
        // }
    }
    public class Detector
    {
       public int Klaster { get; set; }
        public int nomerDetectora { get; set; }
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public SolidColorBrush ColorSFil { get; set; }
        public SolidColorBrush ColorSStroce { get; set; }
        public SolidColorBrush ColorNFil { get; set; }
        public SolidColorBrush ColorNStroce { get; set; }
        public int ColS { get; set; }
        public int ColN { get; set; }

    }
}
