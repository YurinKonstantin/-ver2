using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public static double _to = 72;
        public static double  _from = 0;
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
                Values = new ChartValues<int> {}
               
            });
            SeriesCollectionN.Add(new LineSeries
            {
                Fill = Brushes.Transparent,
                Title = "Кластер №" + nameKl1,
                Values = new ChartValues<int> { }

            });
        
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
          
           if(temp>=0)
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
        static public void AddPointRaz(int[,] det)
        {
            LabelsRaz.Clear();
            SeriesCollectionRaz.Clear();


            int[] dd = new int[1024];
          
            for (int i = 0; i < 12; i++)
                {

                var cv = new ChartValues<int>();
                var temporalCv = new int[1024];
                for (int j = 0; j < 1024; j += 1)
                    {

                    // SeriesCollectionRaz[i].Values.Add(det[i, j]);

                    temporalCv[j] = det[i, j];
                    if (i==0)
                        {
                           
                                LabelsRaz.Add(j);
                            
                                                      
                        }
                       
                    }
                cv.AddRange(temporalCv);
                SeriesCollectionRaz.Add(new LineSeries
                {
                    Fill = Brushes.Transparent,
                    Title = "Детектор №" + i.ToString(),
                    Values = cv


                });


            }

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
