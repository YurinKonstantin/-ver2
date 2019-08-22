using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        public async void MainYpravlenia(int IntervalNewFile1, int kolTestRan, int intTestRan, int timeRanHors, int timeRanMin, CancellationToken token)
        {
            DateTime tmpStart = DateTime.UtcNow;
            DateTime alarmNewFile = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0, 0); 
            try
            {
                try
                {

                    if (_DataColecClassTestRan.Count != 0)
                        foreach (ClassTestRan f in _DataColecClassTestRan)
                        {

                            f.Alam = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, f.Alam.Hour, f.Alam.Minute, 0, 0);
                            if (DateTime.Compare(DateTime.UtcNow, f.Alam) > 0)
                            {
                                f.Alam = f.Alam.AddDays(1);
                            }
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("g3" + ex.ToString());
                }
                try
                {


                    alarmNewRun = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, 0);
                    if (DateTime.Compare(DateTime.UtcNow, alarmNewRun) > 0)
                    {
                        alarmNewRun = alarmNewRun.AddMonths(1);
                    }
                    temp = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0, 0);
                    if (setP.FlagMainRezim)
                    {
                        temp = temp.AddHours(IntervalTemp);
                    }
                    else
                    {
                        temp = temp.AddMinutes(IntervalTemp);
                    }


                    alarmNewFile = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0, 0);
                    if (DateTime.Compare(DateTime.UtcNow, alarmNewFile) > 0)
                    {
                        alarmNewFile = alarmNewFile.AddMinutes(IntervalNewFile1);
                    }

                }
                catch (Exception ex)
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

                       
                        DateTime tmp = DateTime.UtcNow;
                        var ff = tmp.Subtract(tmpStart);
                        await prodTime.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => { prodTime.Text = ff.Days.ToString("00")+"д"+ ff.Hours.ToString("00") + "ч" + ff.Minutes.ToString("00") + "м" + ff.Seconds.ToString("00") + "c"; }));

                        if (setP.FlagMainRezim)
                        {


                            TestRanAndNewFile(kolTestRan, intTestRan, 1);
                        }
                        else
                        {
                            if (DateTime.Compare(DateTime.UtcNow, alarmNewFile) >= 0)
                            {
                                BAAK12T.NewFileURANDelegate?.Invoke();
                                alarmNewFile = alarmNewFile.AddMinutes(IntervalNewFile1);
                            }
                        }
                     
                 

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("g1" + ex.ToString());
                }
            }
            catch(Exception )
            {

            }
     
        }
    }
}
