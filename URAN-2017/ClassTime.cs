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
        /// <summary>
        /// Определяет имя Рана и записывает его в BAAK.NameRan
        /// </summary>
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
      
        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime tmp = DateTime.UtcNow;
            await NowTime.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => { NowTime.Text = "  " + tmp.Hour.ToString("00") + ":" + tmp.Minute.ToString("00") + ":" + tmp.Second.ToString("00") + "  " + tmp.Day.ToString("00") + "." + tmp.Month.ToString("00") + "." + tmp.Year.ToString(); }));

        }
    }
}
