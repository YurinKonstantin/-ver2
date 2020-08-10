using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using URAN_2017.WorkBD;

namespace URAN_2017
{
    public partial class MainWindow
    {
        public string pathBD = String.Empty;
        private async void dispatcherTimerMGVS_Tick(object sender, EventArgs e)
        {
            string rez = String.Empty;

            DateTime tmp = DateTime.UtcNow;
            await LabelTestMGVS.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => { LabelTestMGVS.Content = "  " + tmp.Hour.ToString("00") + ":" + tmp.Minute.ToString("00") + ":" + tmp.Second.ToString("00") + "  " + tmp.Day.ToString("00") + "." + tmp.Month.ToString("00") + "." + tmp.Year.ToString(); }));

        }
        public List<Sob> SobListBD()
        {
            List<Sob> sobs2 = new List<Sob>();
            if (pathBD.Split('.')[1] == "db" || pathBD.Split('.')[1] == "db3")
            {
                DataAccesBDData.Path = pathBD;

                string uslovietime = String.Empty;

                string sz = "  order by Primary_Key";

                // CommandText = "select * from [Событие] Where (ИмяФайла Like '%_" + dateTime.Day.ToString("00") + "."+ dateTime.Month.ToString("00")+".2019 %' ) order by Код desc"

                var ListSob = DataAccesBDData.GetDataSobTop10("10");
                
                foreach(var d in ListSob)
                {
                  
                    
                    sobs2.Add(new Sob() { dataTime=DateTime.Now }); ;

                }


            }
            return sobs2;

        }
        List<Sob> sobs1 = new List<Sob>();
        public class Sob
        {
            public string namePSB { get; set; }
            public int kl { get; set; }
            public int[] masA = new int[12];
            public int[] masN = new int[12];
            public DateTime dataTime = new DateTime();
            public int SobAll { get; set; }
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
    }
}
