using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
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
           // vizualDetectorA(1, Colors.Red);
        }

        public List<Detector> detectors = new List<Detector>();
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
                    d.ColorNFil = new SolidColorBrush(Colors.White);
                    d.ColorSStroce = new SolidColorBrush(Colors.Gray);
                }
            }
        }
        public void vizualDetectors()
        {
            int count = 0;
            for(int i=0; i<9;i++)
            {
                for (int j = 0; j < 4; j++)
                {


                    Ellipse ellipse = new Ellipse() { Width = 20, Height = 20, StrokeThickness = 3, Fill = new SolidColorBrush(Colors.White), Stroke = new SolidColorBrush(Colors.White) };
                    ellipse.Tag = count + 1;
                    ellipse.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    canvas.Children.Add(ellipse);
                    Canvas.SetLeft(ellipse, 110+(40*i));
                    Canvas.SetTop(ellipse, 25+(40*j));
                   detectors.Add(new Detector() { nomerDetectora=count+1, coordinate=new Coordinate() { x= 110 + (40 * i), y= 25 + (40 * j)},
                       ColN =0, ColS=0, ColorNFil= new SolidColorBrush(Colors.White), ColorSStroce = new SolidColorBrush(Colors.White)});
               
                    count++;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {


                    Ellipse ellipse = new Ellipse() { Width = 20, Height = 20, StrokeThickness = 3, Fill = new SolidColorBrush(Colors.White), Stroke = new SolidColorBrush(Colors.White) };
                    canvas.Children.Add(ellipse);
                    ellipse.Tag = count + 1;
                    ellipse.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    Canvas.SetLeft(ellipse, 20 + (40 * i));
                    Canvas.SetTop(ellipse, 255 + (40 * j));
                    detectors.Add(new Detector()
                    {
                        nomerDetectora = count + 1,
                        coordinate = new Coordinate() { x = 110 + (40 * i), y = 25 + (40 * j) },
                        ColN = 0,
                        ColS = 0,
                        ColorNFil = new SolidColorBrush(Colors.White),
                        ColorSStroce = new SolidColorBrush(Colors.White)
                    });
               
                    count++;
                }
            }

        }

      

       

      
        public void vizualDetectorA(int nomerDetector, Color color)
        {

            //canvas.Children.RemoveAt(10+nomerDetector-1);
            int x = 0;
           foreach(var d in canvas.Children)
            {
                if(x==10+ nomerDetector - 1)
                {
                    Ellipse ellipse1 = (Ellipse)d;
                    ellipse1.Stroke = new SolidColorBrush(color);
                }
                x++;
            }
           foreach(var d in detectors)
            {
                if(d.nomerDetectora==nomerDetector)
                {
                    d.ColorSStroce = new SolidColorBrush(color);
                }
            }
        }
        public void TempBD(string connectionString)
        {
            connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + connectionString;
            var podg = new OleDbConnection(connectionString);
            podg.Open();
            DateTime dateTime = new DateTime(2019, 09, 12);
           // dateTime = DateTime.UtcNow;
            DateTime dateTime1 = new DateTime();
            dateTime1 = DateTime.UtcNow;

            dateTime1 = dateTime1.AddHours(-1);

                int x = 0;
           
            
                var camand = new OleDbCommand
                {
                    Connection = podg,
                     CommandText = "select * from [Событие] where ИмяФайла Like '%_" + dateTime.Day.ToString("00")+"." 
                     + dateTime.Month.ToString("00") +"."+ dateTime.Year.ToString() + "%' order by Код"
                    // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"
                     

                };
                try
                {
                    var chit = camand.ExecuteReader();
                    while (chit.Read() == true)
                    {
                    if (x < 5)
                    {


                        // x = Convert.ToInt32(chit.GetValue(1));
                        MessageBox.Show(chit.GetValue(2).ToString());
                        x++;
                    }
                    }
                }
                catch (Exception ex)
                {
                MessageBox.Show("Error");
                }
               
               
            
            podg.Close();
            MessageBox.Show("Конец");
        }
        public void vizualDetectorN(int nomerDetector, Color color)
        {

            //canvas.Children.RemoveAt(10+nomerDetector-1);
            int x = 0;
            foreach (var d in canvas.Children)
            {
                if (x == 10 + nomerDetector - 1)
                {
                    Ellipse ellipse1 = (Ellipse)d;
                    ellipse1.Fill = new SolidColorBrush(color);
                }
                x++;
            }
            foreach (var d in detectors)
            {
                if (d.nomerDetectora == nomerDetector)
                {
                    d.ColorNFil = new SolidColorBrush(color);
                }
            }

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

        private void Ellipse_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int x = (int)((Ellipse)sender).Tag;
            int tempS = 0;
            int tempN = 0;
            int nomerKl = 0;
            foreach(var d in detectors)
            {
                if (d.nomerDetectora == x)
                {
                    tempN = d.ColN;
                    tempS = d.ColS;
                    nomerKl = d.Klaster();
                }
            }
            MessageBox.Show("Кластер № "+nomerKl.ToString()+"\n"+"Детектор "+x.ToString()+"\n"+"Темп счета событий:"+tempS.ToString()+"\n"+"Темп счета нейтронов:"+tempN.ToString());

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
        bool activKl = false;
        public int Klaster()
        {
            int nomerKl = 0;
            if(nomerDetectora>0&&nomerDetectora<13)
            {
                return 1;
            }
         
            if (nomerDetectora > 12 && nomerDetectora < 25)
            {
                return 2;
            }
            if (nomerDetectora >24 && nomerDetectora < 37)
            {
                return 3;
            }
            if (nomerDetectora > 36 && nomerDetectora < 49)
            {
                return 4;
            }
            if (nomerDetectora > 48 && nomerDetectora < 61)
            {
                return 5;
            }
            if (nomerDetectora > 60 && nomerDetectora < 73)
            {
                return 6;
            }
            return 0;
        }
        public int nomerDetectora { get; set; }
        public Coordinate coordinate = new Coordinate();
        
        public SolidColorBrush ColorSStroce { get; set; }
        public SolidColorBrush ColorNFil { get; set; }
    
        public int ColS { get; set; }
        public int ColN { get; set; }
   

}
    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
