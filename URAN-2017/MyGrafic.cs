﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
namespace URAN_2017
{
   public class MyGrafic 
    {
       static public SeriesCollection SeriesCollection { get; set; }
      
     static public ObservableCollection<string> Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }
    
         public void NewCol()
        {
            SeriesCollection = new SeriesCollection(){};
            Labels = new ObservableCollection<string>();
        }
     static public void Add(string nameKl1)
        {
            SeriesCollection.Add(new LineSeries
            {
                Fill = Brushes.Transparent,
                Title = "Кластер №"+nameKl1,
                Values = new ChartValues<int> {}
               
            });
        }
       static  public void AddPoint(int NKl, int temp)
        {
            
            SeriesCollection[NKl].Values.Add(temp);

        }

    }
}
