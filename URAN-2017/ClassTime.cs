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
            BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);

        }
        public string TimeTaimer1 = "0 0:0:0:0";
        private string TimeПуск()
        {
            DateTime taimer = DateTime.UtcNow;
            return Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
        }
        private void TimeTask()
        {
            while (true)
            {


                Thread.Sleep(1000);
                String shour, sMinute, sDay, sMonth, sSec;
                DateTime tmp = DateTime.UtcNow;

                if (Convert.ToUInt32(tmp.Hour.ToString()) < 10)
                {
                    shour = Convert.ToString("0" + tmp.Hour.ToString());
                }
                else
                {
                    shour = Convert.ToString(tmp.Hour.ToString());
                }
                if (Convert.ToUInt32(tmp.Minute.ToString()) < 10)
                {
                    sMinute = Convert.ToString("0" + tmp.Minute.ToString());
                }
                else
                {
                    sMinute = Convert.ToString(tmp.Minute.ToString());
                }
                if (Convert.ToUInt32(tmp.Day.ToString()) < 10)
                {
                    sDay = Convert.ToString("0" + tmp.Day.ToString());
                }
                else
                {
                    sDay = Convert.ToString(tmp.Day.ToString());
                }
                if (Convert.ToUInt32(tmp.Month.ToString()) < 10)
                {
                    sMonth = Convert.ToString("0" + tmp.Month.ToString());
                }
                else
                {
                    sMonth = Convert.ToString(tmp.Month.ToString());
                }

                if (Convert.ToUInt32(tmp.Second.ToString()) < 10)
                {
                    sSec = Convert.ToString("0" + tmp.Second.ToString());
                }
                else
                {
                    sSec = Convert.ToString(tmp.Second.ToString());
                }

                customer.CustomerTime = "  " + shour + ":" + sMinute + ":" + sSec + "  " + sDay + "." + sMonth + "." + tmp.Year.ToString();

                //return s;
            }
        }
    }
}
