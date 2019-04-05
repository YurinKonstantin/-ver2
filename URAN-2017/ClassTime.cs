using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace URAN_2017
{
    public partial class MainWindow
    {
        private void RanName()
        {
            DateTime taimer = DateTime.UtcNow;
            BAAK12T.NameRan = taimer.Day.ToString("00") + "." + taimer.Month.ToString("00") + "." + Convert.ToString(taimer.Year) + " " + taimer.Hour.ToString("00") + ":" + taimer.Minute.ToString("00") + ":" +taimer.Second.ToString("00") + ":" + taimer.Millisecond.ToString("000");

        }
        public string TimeTaimer1 = "0 0:0:0:0";
        private string TimeПуск()
        {
            DateTime taimer = DateTime.UtcNow;
            return taimer.Day.ToString("00") + "." + taimer.Month.ToString("00") + "." + Convert.ToString(taimer.Year) + " " + taimer.Hour.ToString("00") + ":" + taimer.Minute.ToString("00") + ":" + taimer.Second.ToString("00") + ":" +taimer.Millisecond.ToString("000");
        }
        private void TimeTask()
        {
            while (true)
            {


                Thread.Sleep(1000);
               
                DateTime tmp = DateTime.UtcNow;

              

                customer.CustomerTime = "  " + tmp.Hour.ToString("00") + ":" + tmp.Minute.ToString("00") + ":" + tmp.Second.ToString("00") + "  " + tmp.Day.ToString("00") + "." + tmp.Month.ToString("00") + "." + tmp.Year.ToString();

                //return s;
            }
        }
    }
}
