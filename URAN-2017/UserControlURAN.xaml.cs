using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using URAN_2017.WorkBD;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для UserControlURAN.xaml
    /// </summary>
    public partial class UserControlURAN : UserControl, INotifyPropertyChanged
    {
        public string pathBD = String.Empty;
        public UserControlURAN()
        {
            InitializeComponent();
            vizualDetectors();
           
           // vizualDetectorA(1, Colors.Red);
        }
        Point capturePoint { get; set; }

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.CaptureMouse();
            capturePoint = e.MouseDevice.GetPosition(scrollViewer);
        }

        private void ScrollViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!scrollViewer.IsMouseCaptured) return;
            Point currentPoint = e.MouseDevice.GetPosition(scrollViewer);
            var deltaX = capturePoint.X - currentPoint.X;
            var deltaY = capturePoint.Y - currentPoint.Y;
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + deltaX);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + deltaY);
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
           
            for(int i=0; i<72; i++)
            {
                vizualDetectorA(i+1, new SolidColorBrush(Colors.White));
                vizualDetectorN(i+1, new SolidColorBrush(Colors.White), false);
            }
       
    
        }
        public void vizualDetectors()
        {
            int count = 0;
            for(int i=0; i<9;i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    UserControlDetector userControlDetector = new UserControlDetector() { Dnet= new SolidColorBrush(Colors.White), Dsig= new SolidColorBrush(Colors.White) };
                   // Ellipse ellipse = new Ellipse() { Width = 20, Height = 20, StrokeThickness = 3, Fill = new SolidColorBrush(Colors.White), Stroke = new SolidColorBrush(Colors.White) };
                    //ellipse.Tag = count + 1;
                    userControlDetector.Tag = count + 1;
                    userControlDetector.Dneutron.Tag = count + 1;
                    userControlDetector.DSig.Tag = count + 1;
                    userControlDetector.DSig.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    userControlDetector.Dneutron.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    //ellipse.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    userControlDetector.DSig.MouseEnter += Ellipse_MouseEnter;
                    userControlDetector.Dneutron.MouseEnter += Ellipse_MouseEnter;
                   // ellipse.MouseEnter += Ellipse_MouseEnter;
                    canvas.Children.Add(userControlDetector);
                    Canvas.SetLeft(userControlDetector, (120*1.5)+30+(40*2*i));
                    Canvas.SetTop(userControlDetector, 30+(40*2*j));
                   detectors.Add(new Detector() { nomerDetectora=count+1, coordinate=new Coordinate()
                   {
                       x = (int)(120 +30+ (40 * 2 * i)),
                       y = (int)(30 + (40 * 2 * j))
                   },
                       ColN =0,
                       ColS =0,
                       ColorNFil = new SolidColorBrush(Colors.White),
                       ColorSStroce = new SolidColorBrush(Colors.White)
                   });
               
                    count++;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    UserControlDetector userControlDetector = new UserControlDetector() { Dnet = new SolidColorBrush(Colors.White), Dsig = new SolidColorBrush(Colors.White) };
                    //Ellipse ellipse = new Ellipse() { Width = 20, Height = 20, StrokeThickness = 3, Fill = new SolidColorBrush(Colors.White), Stroke = new SolidColorBrush(Colors.White) };
                    canvas.Children.Add(userControlDetector);
                    userControlDetector.Tag = count + 1;
                    userControlDetector.Dneutron.Tag = count + 1;
                    userControlDetector.DSig.Tag = count + 1;
                    //ellipse.Tag = count + 1;
                    userControlDetector.DSig.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    userControlDetector.Dneutron.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;

                    //  ellipse.MouseLeftButtonUp += Ellipse_MouseRightButtonDown;
                    userControlDetector.DSig.MouseEnter += Ellipse_MouseEnter;
                    userControlDetector.Dneutron.MouseEnter += Ellipse_MouseEnter;

                   // ellipse.MouseEnter += Ellipse_MouseEnter;
                    Canvas.SetLeft(userControlDetector, 25 + (40*2 * i));
                    Canvas.SetTop(userControlDetector, (290*1.5)+25 + (40*2 * j));
                    detectors.Add(new Detector()
                    {
                        nomerDetectora = count + 1,
                        coordinate = new Coordinate() { x = (int)(25 + (40 * 2 * i)), y = (int)((290 * 1.5)+25 + (40 * 2 * j)) },
                        ColN = 0,
                        ColS = 0,
                        ColorNFil = new SolidColorBrush(Colors.White),
                        ColorSStroce = new SolidColorBrush(Colors.White)
                    });
               
                    count++;
                }
            }

        }


      public  ObservableCollection<BAAK12T> _DataColecViev = new ObservableCollection<BAAK12T>();
       

      
        public void vizualDetectorA(int nomerDetector, SolidColorBrush color)
        {

            //canvas.Children.RemoveAt(10+nomerDetector-1);
            int x = 0;
           foreach(var d in canvas.Children)
            {
                if(x==22+ nomerDetector - 1)
                {
                    UserControlDetector detector = (UserControlDetector)d;
                    detector.Dsig = color;
                }
                x++;
            }
           foreach(var d in detectors)
            {
                if(d.nomerDetectora==nomerDetector)
                {
                    d.ColorSStroce = color;
                }
            }
        }
        public void TempBD()
        {
            sobs.Clear();
            if (pathBD.Split('.')[1] == "db" || pathBD.Split('.')[1] == "db3")
            {
                DataAccesBDData.Path = pathBD;
                int timenaz = Convert.ToInt32(textH.Text);

                DateTime dateTime = new DateTime();
                dateTime = DateTime.UtcNow;
                DateTime dateTime1 = new DateTime();
                dateTime1 = DateTime.UtcNow;

                dateTime1 = dateTime1.AddHours(-timenaz);
                string uslovietime = String.Empty;
                DateTime dateTime1Naz = new DateTime();

                int x3 = 0;
                for (int i = timenaz; i > 0; i--)
                {
                    dateTime1Naz = DateTime.UtcNow;
                    dateTime1Naz = dateTime1Naz.AddHours(-i);
                    if (x3 > 0)
                    {
                        uslovietime += "or Время Like '" + dateTime1Naz.Day.ToString("00") + "." + dateTime1Naz.Hour.ToString("00") + "%' ";

                    }
                    else
                    {


                        uslovietime += "Время Like '" + dateTime1Naz.Day.ToString("00") + "." + dateTime1Naz.Hour.ToString("00") + "%' ";
                        // "(Время Like '" + dateTime1.Day.ToString("00") + "." + dateTime1.Hour.ToString("00") + "%' or Время Like '" + dateTime.Day.ToString("00") + "." + dateTime.Hour.ToString("00") + "%')";
                    }
                    x3++;
                }





                string sz = " (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."
                  + dateTime.Month.ToString("00") + "." + dateTime.Year.ToString() + "%') and (" + uslovietime + "or Время Like '" + dateTime.Day.ToString("00") +
                  "." + dateTime.Hour.ToString("00") + "%') order by Primary_Key";
                    // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"

                var ListSob = DataAccesBDData.GetDataSob(sz);

                try
                {

                    

                  
                        if (chekNoise.IsChecked == true)
                        {

                          foreach(var d in (from ssob in ListSob where ssob.bad == false select ssob))
                          {
                            sobs.Add(new Sob() { namePSB = d.Плата, kl =Convert.ToInt32(d.Кластер), masA = d.АмпCh, masN = d.NCh, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(d.Time.ToString().Split('.')[1]), Convert.ToInt32(d.Time.ToString().Split('.')[2]), 0, 0) });

                          }

                        }
                        else
                        {
                             foreach (var d in  ListSob)
                             {
                                 sobs.Add(new Sob() { namePSB = d.Плата, kl = Convert.ToInt32(d.Кластер), masA = d.АмпCh, masN = d.NCh, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(d.Time.ToString().Split('.')[1]), Convert.ToInt32(d.Time.ToString().Split('.')[2]), 0, 0) });

                             }
                        }
                    List<WorkBD.ViewTaiblBDData.ClassTablSob> ff = (from s in ListSob where s.Кластер == "1" select s).ToList();
                    if(ff.Count>0)
                    TKl1.Text = ff.ElementAt(ff.Count-1).Time.ToString();

                    ff = (from s in ListSob where s.Кластер == "2" select s).ToList();
                    if (ff.Count > 0)
                        TKl2.Text = ff.ElementAt(ff.Count - 1).Time.ToString();
                  
                    ff = (from s in ListSob where s.Кластер == "3" select s).ToList();
                    if (ff.Count > 0)
                        TKl3.Text = ff.ElementAt(ff.Count - 1).Time.ToString();

                    ff = (from s in ListSob where s.Кластер == "4" select s).ToList();
                    if (ff.Count > 0)
                        TKl4.Text = ff.ElementAt(ff.Count - 1).Time.ToString();

                    ff = (from s in ListSob where s.Кластер == "5" select s).ToList();
                    if (ff.Count > 0)
                        TKl5.Text = ff.ElementAt(ff.Count - 1).Time.ToString();

                    ff = (from s in ListSob where s.Кластер == "6" select s).ToList();
                    if (ff.Count > 0)
                        TKl6.Text = ff.ElementAt(ff.Count - 1).Time.ToString();

                    sobs = (from s in sobs where s.dataTime.CompareTo(dateTime1) >= 0 select s).ToList<Sob>();
                    int colS = (from s in sobs where s.kl == 1 select s).Count();
                    int colN = (from s in sobs where s.kl == 1 select s.masN.Sum()).Sum();
                    textSAllKl1.Text = " " + colS.ToString();
                    textnAllKl1.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 2 select s).Count();
                    colN = (from s in sobs where s.kl == 2 select s.masN.Sum()).Sum();
                    textSAllKl2.Text = " " + colS.ToString();
                    textnAllKl2.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 3 select s).Count();
                    colN = (from s in sobs where s.kl == 3 select s.masN.Sum()).Sum();
                    textSAllKl3.Text = " " + colS.ToString();
                    textnAllKl3.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 4 select s).Count();
                    colN = (from s in sobs where s.kl == 4 select s.masN.Sum()).Sum();
                    textSAllKl4.Text = " " + colS.ToString();
                    textnAllKl4.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 5 select s).Count();
                    colN = (from s in sobs where s.kl == 5 select s.masN.Sum()).Sum();
                    textSAllKl5.Text = " " + colS.ToString();
                    textnAllKl5.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 6 select s).Count();
                    colN = (from s in sobs where s.kl == 6 select s.masN.Sum()).Sum();
                    textSAllKl6.Text = " " + colS.ToString();
                    textnAllKl6.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";
     

                    ShowDeteClea();
              
                    try
                    {
                        
                        //ToDo
                        foreach (var d in detectors)
                        {
                            Debug.WriteLine( d.Klaster().ToString());
                            bool activ = false;
                            foreach (var sd in _DataColecViev)
                            {
                                
                                if(sd.NamKl== d.Klaster().ToString())
                                {

                                    activ = true;
                                }
                               
                            }
                            d.ColS = (from x in sobs where x.kl == d.Klaster() && x.masA[d.nomerDetectora - ((x.kl - 1) * 12) - 1] > 5 select x).Count();
                            d.SumSobAll = (from s in sobs where s.kl == d.Klaster() select s).Count();
                            vizualDetectorA(d.nomerDetectora, d.ColorTextSob);
                            d.ColN = (from x in sobs where x.kl == d.Klaster() && x.masN[d.nomerDetectora - ((x.kl - 1) * 12) - 1] > 0 select x.masN[d.nomerDetectora - ((x.kl - 1) * 12) - 1]).Sum();
                            vizualDetectorN(d.nomerDetectora, d.ColorTextNeutron, activ);
                        }
                        // MessageBox.Show(chit.GetValue(2).ToString()+"\n"+ chit.GetValue(1).ToString()+"\n"+ dateTime1.ToString()+"\t"+s1.dataTime.ToString() +"\n"+ s1.dataTime.CompareTo(dateTime1).ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + "\n" + ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error1" + ex.ToString());
                }
            }
            else
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + pathBD;
                var podg = new OleDbConnection(connectionString);
                podg.Open();
                int timenaz = Convert.ToInt32(textH.Text);
                DateTime dateTime = new DateTime();
                dateTime = DateTime.UtcNow;
                DateTime dateTime1 = new DateTime();
                dateTime1 = DateTime.UtcNow;
                dateTime1 = dateTime1.AddHours(-timenaz);
                string uslovietime = String.Empty;
                DateTime dateTime1Naz = new DateTime();

                int x3 = 0;
                for (int i = timenaz; i > 0; i--)
                {
                    dateTime1Naz = DateTime.UtcNow;
                    dateTime1Naz = dateTime1Naz.AddHours(-i);
                    if (x3 > 0)
                    {
                        uslovietime += "or Время Like '" + dateTime1Naz.Day.ToString("00") + "." + dateTime1Naz.Hour.ToString("00") + "%' ";

                    }
                    else
                    {


                        uslovietime += "Время Like '" + dateTime1Naz.Day.ToString("00") + "." + dateTime1Naz.Hour.ToString("00") + "%' ";
                        // "(Время Like '" + dateTime1.Day.ToString("00") + "." + dateTime1.Hour.ToString("00") + "%' or Время Like '" + dateTime.Day.ToString("00") + "." + dateTime.Hour.ToString("00") + "%')";
                    }
                    x3++;
                }



                var camand = new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "select * from [Событие] where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."
                     + dateTime.Month.ToString("00") + "." + dateTime.Year.ToString() + "%') and (" + uslovietime + "or Время Like '" + dateTime.Day.ToString("00") +
                     "." + dateTime.Hour.ToString("00") + "%') order by Код"
                    // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"


                };


                try
                {

                    var chit = camand.ExecuteReader();

                    while (chit.Read() == true)
                    {
                        if (chekNoise.IsChecked == true)
                        {
                            if (!String.IsNullOrEmpty(chit.GetValue(55).ToString()) && Convert.ToBoolean(chit.GetValue(55)))
                            {

                            }
                            else
                            {

                                int[] masA = new int[12];
                                int[] masN = new int[12];
                                // MessageBox.Show((chit.GetValue(3)).ToString() + "\n" + (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());

                                masA[0] = Convert.ToInt32(chit.GetValue(5));
                                masA[1] = Convert.ToInt32(chit.GetValue(6));
                                masA[2] = Convert.ToInt32(chit.GetValue(7));
                                masA[3] = Convert.ToInt32(chit.GetValue(8));
                                masA[4] = Convert.ToInt32(chit.GetValue(9));
                                masA[5] = Convert.ToInt32(chit.GetValue(10));
                                masA[6] = Convert.ToInt32(chit.GetValue(11));
                                masA[7] = Convert.ToInt32(chit.GetValue(12));
                                masA[8] = Convert.ToInt32(chit.GetValue(13));
                                masA[9] = Convert.ToInt32(chit.GetValue(14));
                                masA[10] = Convert.ToInt32(chit.GetValue(15));
                                masA[11] = Convert.ToInt32(chit.GetValue(16));

                                masN[0] = Convert.ToInt32(chit.GetValue(18));
                                masN[1] = Convert.ToInt32(chit.GetValue(19));
                                masN[2] = Convert.ToInt32(chit.GetValue(20));
                                masN[3] = Convert.ToInt32(chit.GetValue(21));
                                masN[4] = Convert.ToInt32(chit.GetValue(22));
                                masN[5] = Convert.ToInt32(chit.GetValue(23));
                                masN[6] = Convert.ToInt32(chit.GetValue(24));
                                masN[7] = Convert.ToInt32(chit.GetValue(25));
                                masN[8] = Convert.ToInt32(chit.GetValue(26));
                                masN[9] = Convert.ToInt32(chit.GetValue(27));
                                masN[10] = Convert.ToInt32(chit.GetValue(28));
                                masN[11] = Convert.ToInt32(chit.GetValue(29));

                                // MessageBox.Show((chit.GetValue(3)).ToString()+"\n"+ (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());
                                // sobs.Add(new Sob() { namePSB= chit.GetValue(3).ToString(), kl=Convert.ToInt32(chit.GetValue(2)), masA=masA, masN=masN, dataTime=new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                                // x = Convert.ToInt32(chit.GetValue(1));
                                sobs.Add(new Sob() { namePSB = chit.GetValue(3).ToString(), kl = Convert.ToInt32(chit.GetValue(4)), masA = masA, masN = masN, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                            }

                        }
                        else
                        {

                            //Debug.WriteLine("dfgd"+chit.FieldCount.ToString());

                            int[] masA = new int[12];
                            int[] masN = new int[12];
                            // MessageBox.Show((chit.GetValue(3)).ToString() + "\n" + (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());

                            masA[0] = Convert.ToInt32(chit.GetValue(5));
                            masA[1] = Convert.ToInt32(chit.GetValue(6));
                            masA[2] = Convert.ToInt32(chit.GetValue(7));
                            masA[3] = Convert.ToInt32(chit.GetValue(8));
                            masA[4] = Convert.ToInt32(chit.GetValue(9));
                            masA[5] = Convert.ToInt32(chit.GetValue(10));
                            masA[6] = Convert.ToInt32(chit.GetValue(11));
                            masA[7] = Convert.ToInt32(chit.GetValue(12));
                            masA[8] = Convert.ToInt32(chit.GetValue(13));
                            masA[9] = Convert.ToInt32(chit.GetValue(14));
                            masA[10] = Convert.ToInt32(chit.GetValue(15));
                            masA[11] = Convert.ToInt32(chit.GetValue(16));

                            masN[0] = Convert.ToInt32(chit.GetValue(18));
                            masN[1] = Convert.ToInt32(chit.GetValue(19));
                            masN[2] = Convert.ToInt32(chit.GetValue(20));
                            masN[3] = Convert.ToInt32(chit.GetValue(21));
                            masN[4] = Convert.ToInt32(chit.GetValue(22));
                            masN[5] = Convert.ToInt32(chit.GetValue(23));
                            masN[6] = Convert.ToInt32(chit.GetValue(24));
                            masN[7] = Convert.ToInt32(chit.GetValue(25));
                            masN[8] = Convert.ToInt32(chit.GetValue(26));
                            masN[9] = Convert.ToInt32(chit.GetValue(27));
                            masN[10] = Convert.ToInt32(chit.GetValue(28));
                            masN[11] = Convert.ToInt32(chit.GetValue(29));

                            // MessageBox.Show((chit.GetValue(3)).ToString()+"\n"+ (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());
                            // sobs.Add(new Sob() { namePSB= chit.GetValue(3).ToString(), kl=Convert.ToInt32(chit.GetValue(2)), masA=masA, masN=masN, dataTime=new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                            // x = Convert.ToInt32(chit.GetValue(1));
                            sobs.Add(new Sob() { namePSB = chit.GetValue(3).ToString(), kl = Convert.ToInt32(chit.GetValue(4)), masA = masA, masN = masN, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                        }

                    }


                    sobs = (from s in sobs where s.dataTime.CompareTo(dateTime1) >= 0 select s).ToList<Sob>();
                    int colS = (from s in sobs where s.kl == 1 select s).Count();
                    int colN = (from s in sobs where s.kl == 1 select s.masN.Sum()).Sum();
                    textSAllKl1.Text = " " + colS.ToString();
                    textnAllKl1.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 2 select s).Count();
                    colN = (from s in sobs where s.kl == 2 select s.masN.Sum()).Sum();
                    textSAllKl2.Text = " " + colS.ToString();
                    textnAllKl2.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 3 select s).Count();
                    colN = (from s in sobs where s.kl == 3 select s.masN.Sum()).Sum();
                    textSAllKl3.Text = " " + colS.ToString();
                    textnAllKl3.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 4 select s).Count();
                    colN = (from s in sobs where s.kl == 4 select s.masN.Sum()).Sum();
                    textSAllKl4.Text = " " + colS.ToString();
                    textnAllKl4.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 5 select s).Count();
                    colN = (from s in sobs where s.kl == 5 select s.masN.Sum()).Sum();
                    textSAllKl5.Text = " " + colS.ToString();
                    textnAllKl5.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";

                    colS = (from s in sobs where s.kl == 6 select s).Count();
                    colN = (from s in sobs where s.kl == 6 select s.masN.Sum()).Sum();
                    textSAllKl6.Text = " " + colS.ToString();
                    textnAllKl6.Text = " " + colN.ToString() + "(" + ((double)colN / (double)colS).ToString("0.00") + ")";
                    //MessageBox.Show("Read" + "\n" + sobs.Count.ToString());

                    ShowDeteClea();
                    // MessageBox.Show("Clea" + "\n" + sobs.Count.ToString());
                    try
                    {

                        bool activ = false;
                        foreach (var d in detectors)
                        {
                            foreach (var sd in _DataColecViev)
                            {
                                if (sd.NamKl == d.Klaster().ToString())
                                {

                                    activ = true;
                                }
                            }
                            d.ColS = (from x in sobs where x.kl == d.Klaster() && x.masA[d.nomerDetectora - ((x.kl - 1) * 12) - 1] > 5 select x).Count();
                            d.SumSobAll = (from s in sobs where s.kl == d.Klaster() select s).Count();

                            vizualDetectorA(d.nomerDetectora, d.ColorTextSob);


                            d.ColN = (from x in sobs where x.kl == d.Klaster() && x.masN[d.nomerDetectora - ((x.kl - 1) * 12) - 1] > 0 select x.masN[d.nomerDetectora - ((x.kl - 1) * 12) - 1]).Sum();





                            vizualDetectorN(d.nomerDetectora, d.ColorTextNeutron, activ);


                        }
                        // MessageBox.Show(chit.GetValue(2).ToString()+"\n"+ chit.GetValue(1).ToString()+"\n"+ dateTime1.ToString()+"\t"+s1.dataTime.ToString() +"\n"+ s1.dataTime.CompareTo(dateTime1).ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + "\n" + ex.ToString());

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error1" + ex.ToString());
                }



                podg.Close();
            }
            try
            {


                if (Convert.ToBoolean(checList.IsChecked))
                {
                    SobListBD();
                }
            }
            catch(Exception ex)
            {

            }
           

        }
        public void SobListBD()
        {

            if (pathBD.Split('.')[1] == "db" || pathBD.Split('.')[1] == "db3")
            {
                DataAccesBDData.Path = pathBD;

                string uslovietime = String.Empty;

                string sz = "  order by Primary_Key";

                // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"

                var ListSob = DataAccesBDData.GetDataSobTop10(textColSobList.Text);
                list.ItemsSource = ListSob;
                try
                {




                    if (chekNoise.IsChecked == true)
                    {



                    }
                    else
                    {

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error1" + ex.ToString());
                }
            }
            else
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + pathBD;
                var podg = new OleDbConnection(connectionString);
                podg.Open();
                int timenaz = Convert.ToInt32(textH.Text);
                DateTime dateTime = new DateTime();
                dateTime = DateTime.UtcNow;
                DateTime dateTime1 = new DateTime();
                dateTime1 = DateTime.UtcNow;
                dateTime1 = dateTime1.AddHours(-timenaz);
                string uslovietime = String.Empty;






                var camand = new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "select TOP 10 from [Событие] order by Код"
                    // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"


                };


                try
                {

                    var chit = camand.ExecuteReader();

                    while (chit.Read() == true)
                    {
                        if (chekNoise.IsChecked == true)
                        {
                            if (!String.IsNullOrEmpty(chit.GetValue(55).ToString()) && Convert.ToBoolean(chit.GetValue(55)))
                            {

                            }
                            else
                            {

                                int[] masA = new int[12];
                                int[] masN = new int[12];
                                // MessageBox.Show((chit.GetValue(3)).ToString() + "\n" + (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());

                                masA[0] = Convert.ToInt32(chit.GetValue(5));
                                masA[1] = Convert.ToInt32(chit.GetValue(6));
                                masA[2] = Convert.ToInt32(chit.GetValue(7));
                                masA[3] = Convert.ToInt32(chit.GetValue(8));
                                masA[4] = Convert.ToInt32(chit.GetValue(9));
                                masA[5] = Convert.ToInt32(chit.GetValue(10));
                                masA[6] = Convert.ToInt32(chit.GetValue(11));
                                masA[7] = Convert.ToInt32(chit.GetValue(12));
                                masA[8] = Convert.ToInt32(chit.GetValue(13));
                                masA[9] = Convert.ToInt32(chit.GetValue(14));
                                masA[10] = Convert.ToInt32(chit.GetValue(15));
                                masA[11] = Convert.ToInt32(chit.GetValue(16));

                                masN[0] = Convert.ToInt32(chit.GetValue(18));
                                masN[1] = Convert.ToInt32(chit.GetValue(19));
                                masN[2] = Convert.ToInt32(chit.GetValue(20));
                                masN[3] = Convert.ToInt32(chit.GetValue(21));
                                masN[4] = Convert.ToInt32(chit.GetValue(22));
                                masN[5] = Convert.ToInt32(chit.GetValue(23));
                                masN[6] = Convert.ToInt32(chit.GetValue(24));
                                masN[7] = Convert.ToInt32(chit.GetValue(25));
                                masN[8] = Convert.ToInt32(chit.GetValue(26));
                                masN[9] = Convert.ToInt32(chit.GetValue(27));
                                masN[10] = Convert.ToInt32(chit.GetValue(28));
                                masN[11] = Convert.ToInt32(chit.GetValue(29));

                                // MessageBox.Show((chit.GetValue(3)).ToString()+"\n"+ (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());
                                // sobs.Add(new Sob() { namePSB= chit.GetValue(3).ToString(), kl=Convert.ToInt32(chit.GetValue(2)), masA=masA, masN=masN, dataTime=new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                                // x = Convert.ToInt32(chit.GetValue(1));
                                sobs.Add(new Sob() { namePSB = chit.GetValue(3).ToString(), kl = Convert.ToInt32(chit.GetValue(4)), masA = masA, masN = masN, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                            }

                        }
                        else
                        {

                            //Debug.WriteLine("dfgd"+chit.FieldCount.ToString());

                            int[] masA = new int[12];
                            int[] masN = new int[12];
                            // MessageBox.Show((chit.GetValue(3)).ToString() + "\n" + (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());

                            masA[0] = Convert.ToInt32(chit.GetValue(5));
                            masA[1] = Convert.ToInt32(chit.GetValue(6));
                            masA[2] = Convert.ToInt32(chit.GetValue(7));
                            masA[3] = Convert.ToInt32(chit.GetValue(8));
                            masA[4] = Convert.ToInt32(chit.GetValue(9));
                            masA[5] = Convert.ToInt32(chit.GetValue(10));
                            masA[6] = Convert.ToInt32(chit.GetValue(11));
                            masA[7] = Convert.ToInt32(chit.GetValue(12));
                            masA[8] = Convert.ToInt32(chit.GetValue(13));
                            masA[9] = Convert.ToInt32(chit.GetValue(14));
                            masA[10] = Convert.ToInt32(chit.GetValue(15));
                            masA[11] = Convert.ToInt32(chit.GetValue(16));

                            masN[0] = Convert.ToInt32(chit.GetValue(18));
                            masN[1] = Convert.ToInt32(chit.GetValue(19));
                            masN[2] = Convert.ToInt32(chit.GetValue(20));
                            masN[3] = Convert.ToInt32(chit.GetValue(21));
                            masN[4] = Convert.ToInt32(chit.GetValue(22));
                            masN[5] = Convert.ToInt32(chit.GetValue(23));
                            masN[6] = Convert.ToInt32(chit.GetValue(24));
                            masN[7] = Convert.ToInt32(chit.GetValue(25));
                            masN[8] = Convert.ToInt32(chit.GetValue(26));
                            masN[9] = Convert.ToInt32(chit.GetValue(27));
                            masN[10] = Convert.ToInt32(chit.GetValue(28));
                            masN[11] = Convert.ToInt32(chit.GetValue(29));

                            // MessageBox.Show((chit.GetValue(3)).ToString()+"\n"+ (chit.GetValue(4)).ToString() + "\n" + (chit.GetValue(5)).ToString());
                            // sobs.Add(new Sob() { namePSB= chit.GetValue(3).ToString(), kl=Convert.ToInt32(chit.GetValue(2)), masA=masA, masN=masN, dataTime=new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                            // x = Convert.ToInt32(chit.GetValue(1));
                            // sobs.Add(new Sob() { namePSB = chit.GetValue(3).ToString(), kl = Convert.ToInt32(chit.GetValue(4)), masA = masA, masN = masN, dataTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[1]), Convert.ToInt32(chit.GetValue(1).ToString().Split('.')[2]), 0, 0) });
                        }

                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error1" + ex.ToString());
                }



                podg.Close();
            }

        }
        List<Sob> sobs = new List<Sob>();
        public class Sob
        {
            public string namePSB { get; set; }
            public int kl { get; set; }
            public int[] masA = new int[12];
            public int[] masN=new int[12];
            public DateTime dataTime = new DateTime();
            public int SobAll{ get; set; }
            public int NeutronAll
            {
                get
                {
                    return masN.Sum();
                }
                set
                {

                }
            }
        }
        public void vizualDetectorN(int nomerDetector, SolidColorBrush color, bool activ)
        {

            //canvas.Children.RemoveAt(10+nomerDetector-1);
            int x = 0;
            foreach (var d in canvas.Children)
            {
                if (x == 22 + nomerDetector - 1)
                {
                    UserControlDetector detector = (UserControlDetector)d;
                    detector.Dnet = color;
                    detector.Activ = activ;
                }
                x++;
            }
            foreach (var d in detectors)
            {
                if (d.nomerDetectora == nomerDetector)
                {
                    d.ColorNFil = color;
                    d.activ = activ;
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

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            int x = (int)((Ellipse)sender).Tag;
            int tempS = 0;
            int tempN = 0;
            int nomerKl = 0;
            SolidColorBrush solidColorBrushN = new SolidColorBrush();
            SolidColorBrush solidColorBrushS = new SolidColorBrush();
            foreach (var d in detectors)
            {
                if (d.nomerDetectora == x)
                {
                    tempN = d.ColN;
                    tempS = d.ColS;
                    nomerKl = d.Klaster();
                    solidColorBrushN = d.ColorTextNeutron;
                    solidColorBrushS = d.ColorTextSob;


                }
            }
            textKl.Text = "Кластер № " + nomerKl.ToString();
            textDetec.Text = "Детектор№ " + x.ToString() + "(" + (x - (12 * (nomerKl - 1))).ToString() + ")";
            textSob.Foreground = solidColorBrushS;
            textSob.Text = " " + tempS.ToString();
            try
            {

                textN.Foreground = solidColorBrushN;
                textN.Text = " " + tempN.ToString() + "(" + ((double)((double)tempN / (double)(from s in sobs where s.kl == nomerKl select s).Count())).ToString("0.0000") + ")";
            }
            catch(Exception ex)
            {
                textN.Text = " " + tempN.ToString() + "(" + (0).ToString()+")";
            }
            SobAll.Text =" "+ ((from s in sobs where s.kl == nomerKl select s).Count()).ToString();
            NeutronovAll.Text =" "+ ((from s in sobs where s.kl == nomerKl select s.NeutronAll).Sum()).ToString();
        }
        public class Detector
        {
            bool activKl = false;
            public int Klaster()
            {
                int nomerKl = 0;
                if (nomerDetectora > 0 && nomerDetectora < 13)
                {
                    return 1;
                }

                if (nomerDetectora > 12 && nomerDetectora < 25)
                {
                    return 2;
                }
                if (nomerDetectora > 24 && nomerDetectora < 37)
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
            public int SumSobAll { get; set; }
            public int SumNeutronAll { get; set; }
            public SolidColorBrush ColorSStroce { get; set; }
            public SolidColorBrush ColorNFil { get; set; }

            public int ColS { get; set; }
            public int ColN { get; set; }
            public SolidColorBrush ColorTextSob
            {
                get
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.White);
                    if (ColS < (double)(SumSobAll * 0.1))
                    {
                        solidColorBrush = new SolidColorBrush(Colors.Yellow);
                    }
                    if (ColS >= (double)(SumSobAll * 0.1))
                    {
                        solidColorBrush = new SolidColorBrush(Colors.Green);
                    }
                    if (ColS >= (double)(SumSobAll * 0.7))
                    {
                        solidColorBrush = new SolidColorBrush(Colors.Red);
                    }
                    return solidColorBrush;
                }
            }
            public SolidColorBrush ColorTextNeutron
            {
                get
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.White);
                   

                    double t = 0.0000;
                    if (SumSobAll == 0)
                    {
                        t = 0.0000;
                    }
                    else
                    {
                        t = ((double)((double)ColN / (double)SumSobAll));
                    }
                    if (t == 0.000)
                    {
                        solidColorBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x04, 0xC5, 0x04));

                    }
                    if (t >= 0.00001)
                    {
                        solidColorBrush = new SolidColorBrush(Colors.Green);

                    }
                    if (t >= 0.2)
                    {

                        solidColorBrush = new SolidColorBrush(Colors.Orange);
                    }
                    if (t >= 0.5)
                    {

                        solidColorBrush = new SolidColorBrush(Colors.Red);
                    }

                    return solidColorBrush;
                }
            }
            public bool activ
            {
                get; set;
            }
           
        }
        public class Coordinate
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            scaleCanvas.ScaleX = scaleCanvas.ScaleX - .1;
            scaleCanvas.ScaleY = scaleCanvas.ScaleY - .1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            scaleCanvas.ScaleX = scaleCanvas.ScaleX + .1;
            scaleCanvas.ScaleY = scaleCanvas.ScaleY  +.1;

        }

        private void chekNoise_Checked(object sender, RoutedEventArgs e)
        {
            TempBD();
        }

        private void chekNoise_Unchecked(object sender, RoutedEventArgs e)
        {
            TempBD();

        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            var re = (Rectangle)(sender);
         
                re.StrokeThickness = 2;
            
          
            
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            var re = (Rectangle)(sender);
            if( re.IsMouseOver)
            {

            }
            else
            {
                re.StrokeThickness = 1;
            }
                
           


        }

        private void checList_Checked(object sender, RoutedEventArgs e)
        {
            list.Visibility = Visibility.Visible;
        }

        private void checList_Unchecked(object sender, RoutedEventArgs e)
        {
            list.Visibility = Visibility.Collapsed;
        }
    }
   
 
}
