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
        int timeSlepCol = 1;
        /// <summary>
        /// бескоечный цикл Читаем данные с платы и пишем в файл
        /// </summary>
        /// <param name="token1"></param>
        private void ZapicDataTasc(CancellationToken token1)
        {
            //Task myRe = Task.Run(() => Anime2());
            int tt = 20;
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { tt = tt / timeSlepCol; }));
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
                Thread.Sleep(tt);
                BAAK12T.ЗаписьвФайлDelegate?.Invoke();//Читаем данные с платы и пишем в файл

            }
        }


        /// <summary>
        /// Читаем данные из очереди и пишем в БД
        /// </summary>
        /// <param name="token2"></param>
        private void ZapicDataBDTasc(CancellationToken token2)
        {
            //Task myRe = Task.Run(() => Anime2());
            int tt = 20;
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { tt = tt / timeSlepCol; }));
            while (true)
            {
                //  if (myRe.IsCompleted)
                // {
                //     myRe = Task.Run(() => Anime2());

                // }
                Thread.Sleep(tt);
                if (token2.IsCancellationRequested)
                {
                    return;
                }
                
                BAAK12T.ЗаписьвФайлБДDelegate?.Invoke();//Читаем данные из очереди и пишем в БД

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
                   
                   // int tt =10;
                    //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { tt = tt / timeSlepCol; }));
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                            
                            return;
                        }
                      
                      
                        TempPacetov(temp, IntervalTemp);
                        Thread.Sleep(1);
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
                try
                {


                    DateTime taimer2 = DateTime.UtcNow;
                    lock (MyGrafic.SeriesCollection)
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
                    try
                    {
                        DateTime taimer = DateTime.UtcNow;
                        string str = String.Empty;
                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { str = rezimYst.Content.ToString(); }));
                        if (_DataColecViev.Count != 0)
                        {
                            str += " " + (_DataColecViev.Count + _DataColecVievList2.Count) + "\n\t";
                            foreach (BAAK12T bak in _DataColecViev)
                            {
                                str += bak.NamKl + "\t" + bak.CтатусБААК12 + "\t" + bak.КолПакетов + "\t" + bak.ТемпПакетов + "\n\t";
                            }
                            foreach (ClassBAAK12NoTail bak in _DataColecVievList2)
                            {
                                str += bak.NamKl + "\t" + bak.CтатусБААК12 + "\t" + bak.КолПакетов + "\t" + bak.ТемпПакетов + "\n\t";
                            }
                        }
                        File.WriteAllText(@"C:\\Users\yurin\OneDrive\Monitoring.txt", "Time" + taimer.ToLongTimeString() + "\n" + str); //допишет текст в конец файла

                    }
                    catch (Exception ex)
                    {

                    }
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
                catch (Exception ex)
                {
                   // MessageBox.Show("gffeeeeegfg" + ex.ToString());
                }

            }

        }
       
       
    }
}
