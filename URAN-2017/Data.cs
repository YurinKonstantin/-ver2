using System;
using System.Net.Sockets;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

namespace URAN_2017
{
   public partial class MainWindow
    {

        const string quote = "\"";

        /// <summary>
        /// бескоечный цикл Читаем данные с платы и пишем в файл
        /// </summary>
        /// <param name="token1"></param>
        private void ZapicDataTasc(CancellationToken token1)
        {
            //Task myRe = Task.Run(() => Anime2());
           
            while (true)
            {
              //  if (myRe.IsCompleted)
               // {
               //     myRe = Task.Run(() => Anime2());
                    
               // }
                if (token1.IsCancellationRequested)
                {
                    
                    return;
                }

                BAAK12T.ЗаписьвФайлDelegate?.Invoke();//Читаем данные с платы и пишем в файл

            }
        }


        private void ZapicDataBDTasc(CancellationToken token2)
        {
            //Task myRe = Task.Run(() => Anime2());

            while (true)
            {
                //  if (myRe.IsCompleted)
                // {
                //     myRe = Task.Run(() => Anime2());

                // }
                if (token2.IsCancellationRequested)
                {
                    return;
                }

                BAAK12T.ЗаписьвФайлБДDelegate?.Invoke();//Читаем данные с платы и пишем в файл

            }
        }

        /// <summary>
        /// чтение дынных с плат, запуск тестовых наборов, расчет темпа счета и новый ран раз в месяц
        /// </summary>
        /// <param name="IntervalNewFile1"></param>
        /// <param name="kolTestRan"></param>
        /// <param name="intTestRan"></param>
        /// <param name="timeRanHors"></param>
        /// <param name="timeRanMin"></param>
        /// <param name="token"></param>
        private void ReadDataTask(CancellationToken token)
        {
           
            try
            {
               
                try
                { 
                    temp = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0, 0);
                    if(setP.FlagMainRezim)
                    {
                     temp = temp.AddHours(IntervalTemp);
                    }
                    else
                    {
                     temp = temp.AddMinutes(IntervalTemp);
                    }


                    

                }
                catch(Exception ex)
                {
                    MessageBox.Show("g2" + ex.ToString());
                }
                try
                {


                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                            
                            return;
                        }
                    
                        try
                        {
                            TempPacetov(temp, IntervalTemp);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("gffeeeeegfg" + ex.ToString());
                        }
                        BAAK12T.ReadDataURANDelegate?.Invoke();//Читаем данные с платы и пишем в файл

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("g1" + ex.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("gffgfg"+ex.ToString());
            }
        }
      
        /// <summary>
        /// расчет темпа пакетов с плат
        /// </summary>
        /// <param name="alarm2">время для расчета темпа счета</param>
        /// <param name="inter">интервал темпа счета</param>
        private  void TempPacetov(DateTime alarm2, int inter)
        {

            if (DateTime.Compare(DateTime.UtcNow, alarm2) > 0)
            {
                DateTime taimer2 = DateTime.UtcNow;
                lock(MyGrafic.SeriesCollection)
                {
                    if (MyGrafic.SeriesCollection.Count > 72)
                    {
                        // Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.SeriesCollection.RemoveAt(0); }));
                        MyGrafic.SeriesCollection.RemoveAt(0);

                        // Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Labels.RemoveAt(0); }));
                        MyGrafic.Labels.RemoveAt(0);

                    }
                }
                lock (MyGrafic.Labels)
                {
                   // Dispatcher.Invoke(DispatcherPriority.Render, new Action(() => { MyGrafic.Labels.Add(taimer2.Hour.ToString("00") + ":" + taimer2.Minute.ToString("00") + " " + taimer2.Day.ToString("00") + "." + taimer2.Month.ToString("00")); }));
                    MyGrafic.Labels.Add(taimer2.Hour.ToString("00") + ":" + taimer2.Minute.ToString("00") + " " + taimer2.Day.ToString("00") + "." + taimer2.Month.ToString("00"));

                }
                lock (MyGrafic.LabelsN)
                {
                    // Dispatcher.Invoke(DispatcherPriority.Render, new Action(() => { MyGrafic.LabelsN.Add(taimer2.Hour.ToString("00") + ":" + taimer2.Minute.ToString("00") + " " + taimer2.Day.ToString("00") + "." + taimer2.Month.ToString("00")); }));
                    MyGrafic.LabelsN.Add(taimer2.Hour.ToString("00") + ":" + taimer2.Minute.ToString("00") + " " + taimer2.Day.ToString("00") + "." + taimer2.Month.ToString("00"));

                }

                BAAK12T.TempURANDelegate?.Invoke();
                //temp = temp.AddMinutes(inter);
                if (setP.FlagMainRezim)
                {
                    temp = temp.AddHours(IntervalTemp);
                }
                else
                {
                    temp = temp.AddMinutes(IntervalTemp);
                }

            }

        }
       
       
    }
}
