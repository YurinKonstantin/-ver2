using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
namespace URAN_2017
{
   public class MyGrafic : UserControl, INotifyPropertyChanged
    {
       public double _toRaz=100;
       public double _fromRaz=0;
        public static double _to = 48;
        public static double  _from = 0;
       public static MainWindow MainWindow;
        static public SeriesCollection SeriesCollection { get; set; }
        static public SeriesCollection SeriesCollectionN { get; set; }
        static public SeriesCollection SeriesCollectionRaz { get; set; }
        static public ObservableCollection<string> Labels { get; set; }
        static public ObservableCollection<int> LabelsRaz { get; set; }
        static public ObservableCollection<string> LabelsN { get; set; }
        public Func<int, string> YFormatter { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public double FromRaz
        {
            get { return _fromRaz; }
            set
            {

                _fromRaz = value;
                OnPropertyChanged("FromRaz");

            }
        }

     public double ToRaz
        {
            get { return _toRaz; }
            set
            {
                _toRaz = value;
               
            }
        }
        public double From
        {
            get { return _from; }
            set
            {

                _from = value;
                OnPropertyChanged("From");

            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;

            }
        }
        public void NewCol()
        {
           
            SeriesCollection = new SeriesCollection(){};
            Labels = new ObservableCollection<string>();
            SeriesCollectionN = new SeriesCollection() { };
            LabelsN = new ObservableCollection<string>();
            SeriesCollectionRaz = new SeriesCollection() { };
            LabelsRaz = new ObservableCollection<int>();



        }
       
        static public void Add(string nameKl1)
        {
            
         
            SeriesCollection.Add(new LineSeries
            {
                Fill = Brushes.Transparent,
                Title = "Кластер №"+nameKl1,
                Values = new ChartValues<int> {},
                LineSmoothness = 0

            });

           
            SeriesCollectionN.Add(new LineSeries
            {
                Fill = Brushes.Transparent,
                Title = "Кластер №" + nameKl1,
                Values = new ChartValues<int> { },
                LineSmoothness = 0

            });
           
         

               // infoZaprocBD(nameKl1, kl, connectionString);

        }
      static  public void infoZaprocBD( string kl, int d, string connectionString)
        {

            connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + connectionString;
            var podg = new OleDbConnection(connectionString);
            podg.Open();
            DateTime dateTime =new DateTime();
            dateTime = DateTime.UtcNow;
            DateTime dateTime1 = new DateTime();
            dateTime1 = DateTime.UtcNow;

            dateTime1= dateTime1.AddHours(-71);
          
            while(dateTime1.Subtract(dateTime).TotalHours != 0)
            {
                   int x = 0;
                    var camand = new OleDbCommand
                    {
                        Connection = podg,
                         CommandText = "select темп from [Темп] where ( Кластер№ = '" + kl.ToString() + "' and год = " + dateTime1.Year.ToString() + " and месяц = " + dateTime1.Month.ToString() + " and день = " + dateTime1.Day.ToString() + " and час = " + dateTime1.Hour.ToString() + "  ) order by Код"

                    };
                try
                {
                    var chit = camand.ExecuteReader();
                    while (chit.Read() == true)
                    {
                        x = Convert.ToInt32(chit.GetValue(0));
                    }
                }
                catch(Exception ex)
                {

                }
               SeriesCollection[d].Values.Add(x);
               SeriesCollectionN[d].Values.Add(0);
               dateTime1 = dateTime1.AddHours(1);
            }
            podg.Close();
        }
        static public void AddRaz()
        {
            for (int i = 0; i < 12; i++)
            {
                SeriesCollectionRaz.Add(new LineSeries
                {
                    Fill = Brushes.Transparent,
                    Title = "Детектор №" + i.ToString(),
                    Values = new ChartValues<int> { }
                   

                });
            }
        }
        static  public void AddPoint(int NKl, int temp, int tempN)
        {
          if(SeriesCollection[NKl].Values.Count> _to)
            {
                SeriesCollection[NKl].Values.RemoveAt(0);
                
            }
            if (SeriesCollectionN[NKl].Values.Count > _to)
            {
                SeriesCollectionN[NKl].Values.RemoveAt(0);
               
            }
            if(Labels.Count> _to)
            {
                Labels.RemoveAt(0);
            }
            if (LabelsN.Count > _to)
            {
                LabelsN.RemoveAt(0);
            }
            if (temp>=0)
            {
                SeriesCollection[NKl].Values.Add(temp);
            }
           else
            {
                SeriesCollection[NKl].Values.Add(0);
            }
           if(tempN>=0)
            {
                SeriesCollectionN[NKl].Values.Add(tempN);
            }
           else
            {
                SeriesCollectionN[NKl].Values.Add(0);
            }

        }
        static public async void AddPointRaz(int[,] det, string nameKl)
        {
            // Start_time = DateTime.Now;
            //ClassTextFile.CreatFileData(PathText.Text + Start_time.Year.ToString() + "_" + Start_time.Month.ToString() + "_" + Start_time.Day.ToString() + "_" + Start_time.Hour.ToString() + "_" + Start_time.Minute.ToString());
            await MainWindow.linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { MainWindow.linegraph.Children.Clear(); }));
            await MainWindow.ChatMain.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { MainWindow.ChatMain.Title = nameKl; }));
            var x = new int[1024];
            // var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v)/v).ToArray();
            var y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
            }
          
            for(int i = 0; i < 12; i++)
            {
                var lg = new InteractiveDataDisplay.WPF.LineGraph();
            


                // lg.Stroke = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(xx), Convert.ToByte(255 -xx), Convert.ToByte(0 +xx)));
                switch(i)
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
                        lg.Stroke = new SolidColorBrush(Colors.Azure);
                        break;
                    case 9:
                        lg.Stroke = new SolidColorBrush(Colors.DarkOliveGreen);
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
         
                
                lg.Description = String.Format("Детектор №"+(i+1).ToString());
                lg.StrokeThickness = 2;
                for(int j=0; j<1024; j++)
                {
                    y[j] = det[i, j];
                }
                lg.Plot(x, y);
                MainWindow.linegraph.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { MainWindow.linegraph.Children.Add(lg); }));

            }
           

         
            //barChart.PlotBars(y);

        }
        static public void AddTecPoint(int NKl, int temp)
        {
            if (SeriesCollection[NKl].Values.Count != 0)
            {
                SeriesCollection[NKl].Values.RemoveAt(SeriesCollection[NKl].Values.Count - 1);
            }

            SeriesCollection[NKl].Values.Add(temp);
           

        }
        static public void AddTecPointN(int NKl, int temp)
        {
            if (SeriesCollectionN[NKl].Values.Count != 0)
            {
                SeriesCollectionN[NKl].Values.RemoveAt(SeriesCollectionN[NKl].Values.Count - 1);
            }

           
            SeriesCollectionN[NKl].Values.Add(temp);


        }
        static int step = 1;
        static public int Step
        {
            get
            {
                return step;
            }
            set
            {
                step = value;
            }
        }
        
    }
}
