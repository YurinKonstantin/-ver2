using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        private void TimeTaimer(int t)
        {

            int ox12 = 0;
            int ox14 = 0;
            int ox16 = 0;
            DateTime taimer = DateTime.UtcNow;
           // MessageBox.Show(ClassParentsBAAK.Синхронизация.ToString());
            if (ClassParentsBAAK.Синхронизация)
            {
               // MessageBox.Show(set.FlagClok.ToString());
                if (set.FlagClok)
                {

                    try
                    {
                        //MessageBox.Show("Отправляем зарпос на сих");
                        string v = "{" + quote + "name" + quote + ":" + quote + "sync_device" + quote + "," + quote + "link_mask" + quote + ":" + set.LincClok + "," + quote + "delay" + quote + ":" + t + "}";
                        ОтправкаЧтение(v, set.IpMGVS, set.PortMGVS);
                        taimer = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, MS.hours, MS.minutes, MS.seconds, 0);
                      //  MessageBox.Show("полученно"+ MS.hours+" "+ MS.minutes+" "+ MS.seconds);
                        taimer = taimer.AddSeconds(t + 2);
                        ox12 = ((taimer.Second & 0x1f) << 11) | (0 << 1);
                        ox14 = ((taimer.Day & 0x0f) << 12) | (MS.hours << 7) | (MS.minutes << 1) | (taimer.Second >> 5);
                        ox16 = ((taimer.Day >> 4) & 0x03);
                        ClassBAAK12NoTail.Time0x12 = Convert.ToUInt32(ox12);
                        ClassBAAK12NoTail.Time0x14 = Convert.ToUInt32(ox14);
                        ClassBAAK12NoTail.Time0x16 = Convert.ToUInt32(ox16);
                        BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
                        BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
                        BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
                        TimeTaimer1 = taimer.Day.ToString()+" "+ MS.hours.ToString()+":"+ MS.minutes.ToString()+":"+ MS.seconds.ToString()+":"+"0";

                        // BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(0);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ошибка: "+e.ToString()+" Запуск с системным временм");
                        
                       
                        taimer = taimer.AddSeconds(t);
                        ox12 = (((taimer.Second) & 0x1f) << 11) | (taimer.Millisecond << 1);
                        ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | ((taimer.Second) >> 5);
                        ox16 = ((taimer.Day >> 4) & 0x03);
                        ClassBAAK12NoTail.Time0x12 = Convert.ToUInt32(ox12);
                        ClassBAAK12NoTail.Time0x14 = Convert.ToUInt32(ox14);
                        ClassBAAK12NoTail.Time0x16 = Convert.ToUInt32(ox16);
                        BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
                        BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
                        BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
                        TimeTaimer1= Convert.ToString(taimer.Day) + " " +  Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
                        // BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
                    }

                }
                else
                {
                    taimer = taimer.AddSeconds(t);
                    ox12 = (((taimer.Second) & 0x1f) << 11) | (taimer.Millisecond << 1);
                    ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | ((taimer.Second) >> 5);
                    ox16 = ((taimer.Day >> 4) & 0x03);
                    BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
                    BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
                    BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
                    TimeTaimer1 = Convert.ToString(taimer.Day) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
                    //BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
                }


            }
            else
            {

                taimer = taimer.AddSeconds(5);
                ox12 = (((taimer.Second) & 0x1f) << 11) | (taimer.Millisecond << 1);
                ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | ((taimer.Second) >> 5);
                ox16 = ((taimer.Day >> 4) & 0x03);
                BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
                BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
                BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
                //BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);

            }
        }

        private void ОтправкаЧтение(string text, string iP, string port)
        {
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + iP + ":" + port + "/api/v1/MGTSServer/URAN/" + text) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";

                // httpWebRequest.BeginGetResponse((ar) =>
                // {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        Thread.Sleep(50);
                        MS = JsonConvert.DeserializeObject<ClassMC>(output.ToString());
                        //Dispatcher.BeginInvoke(new Action<string>(s => { Otvet.Items.Add(data.Microseconds.ToString()); }), data.Microseconds.ToString());
                        //MessageBox.Show(data.text.ToString());
                        response.Close();

                    }
                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    MessageBox.Show(ez.ToString());
                }

                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
                //labekSystemMessage.Items.Add("Ошибка: " + ex);
                MessageBox.Show(ex.ToString());
            }
        }

        public void РежимСинхИлиНет(int ожидание)
        {
            TimeTaimer(ожидание);
            if (ClassParentsBAAK.Синхронизация)
            {
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Запуск с синхронизацией"; }));
                if (!set.FlagClok)
                {
                  rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Подготовка МС"; }));
                    //TimeTaimer(ожидание);
                    BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
                    //СтартЧасовDelegate();
                   // Thread.Sleep(ожидание * 1000);
                    int min = 0;
                    while (min < ожидание * 1000)
                    {
                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Ожидание =" + ожидание  + " сек." + " Осталось " + (((ожидание * 1000) - min) / 1000).ToString() +  "сек."; }));
                        Thread.Sleep(1000);
                        min = min + 1000;
                    }
                    MS.АвтономныйКлокРазрешен(1);
                }
                else//запуск с МГВМ
                {
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Подготовка МГВС"; }));
                    BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
                    BAAK12T.СтартЧасовDelegate?.Invoke();
                    int min = 0;
                    while (min < ожидание * 1000)
                    {
                       rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Ожидание =" + ожидание + "сек." + " Осталось " + (((ожидание * 1000) - min) / 1000).ToString() + "сек."; }));
                        Thread.Sleep(1000);
                        min = min + 1000;
                    }
                    int flagStart = 1;
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Ожидается ответ от МГВС"; }));
                    while (flagStart == 1)
                    {

                        Thread.Sleep(500);
                        string v = "{" + quote + "name" + quote + ":" + quote + "check_sync_progress" + quote + "," + quote + "link_mask" + quote + ":" + set.LincClok + "," + quote + "delay" + quote + ":" + set.DelayClok + "}";
                        ОтправкаЧтение(v, set.IpMGVS, set.PortMGVS);
                        flagStart = MS.status;
                    }
                   rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Далее"; }));

                }

            }
            else
            {
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Запуск без синхронизации"; }));
                //TimeTaimer(5);
                BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
                // СтартЧасовDelegate();
            }

           /* Thread t;
            ThreadStart thr;
            int x = 5;
           while(x>1)
            {

                thr = () => { WindowОзапуске Window11 = new WindowОзапуске(); Window11.lab.Content = x; Window11.Closed += (s, a) => Window11.Dispatcher.InvokeShutdown(); Window11.Show(); System.Windows.Threading.Dispatcher.Run();  };
                 t = new Thread(thr);
                t.SetApartmentState(ApartmentState.STA);
                x--;
                t.Start();
                Thread.Sleep(1000);
                t.Abort();
                t.Join();
              
            }
            thr = () => { WindowОзапуске Window11 = new WindowОзапуске(); Window11.lab.Content = "ПУСК"; Window11.Closed += (s, a) => Window11.Dispatcher.InvokeShutdown(); Window11.Show(); System.Windows.Threading.Dispatcher.Run();  };
            t = new Thread(thr);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            Thread.Sleep(1000);
            t.Abort();
            t.Join();
            */

        }
    }
}
