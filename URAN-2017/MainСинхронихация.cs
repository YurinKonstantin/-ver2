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
        /// <summary>
        /// Определяет начальные значения в таймер платы в зависимости от типа запуска(от МГВС, локальные часы)
        /// </summary>
        /// <param name="t"></param>
        private void TimeTaimer(int t)
        {

            if (ClassParentsBAAK.Синхронизация)//старт будет с синхронизацие
            {

                if (set.FlagClok)//Подготовка к старту с МГВС
                {

                    TimeTaimerMGVS(t);

                }
                else//Подготовка к старту с МС без МГВС
                {
                    TimeTaimerMC(t);
                }


            }
            else//старт будет без синхронизацие
            {

                TimeTaimerNoCinx();
            }
        }
        /// <summary>
        /// Готовит Установку к старту с МГВС
        /// </summary>
        /// <param name="t"></param>
        private void TimeTaimerMGVS(int t)
        {
            DateTime taimer = DateTime.UtcNow;
            int ox12 = 0;
            int ox14 = 0;
            int ox16 = 0;
            try
            {
                
                //  MessageBox.Show("TimeTaimer");
                string v = "{" + quote + "name" + quote + ":" + quote + "sync_device" + quote + "," + quote + "link_mask" + quote + ":" + set.LincClok + "," + quote + "delay" + quote + ":" + t + "}";
                ОтправкаЧтение(v, set.IpMGVS, set.PortMGVS);
                taimer = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, MS.hours, MS.minutes, MS.seconds, 0);
                // MessageBox.Show("Размер ожидания ");
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Размер ожидания " + t.ToString() + "\n" + "Значение от таймера МГВС " + taimer.Hour.ToString("00") + ":" +
                    taimer.Minute.ToString("00") + ":" + taimer.Second.ToString("00") + ":" + "0" + "\n"); //допишет текст в конец файла
                                                                                                           //MessageBox.Show("полученно"+ MS.hours+" "+ MS.minutes+" "+ MS.seconds);
                taimer = taimer.AddSeconds(t + 2);
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Значение с смещением" + taimer.Hour.ToString("00") + ":" +
                    taimer.Minute.ToString("00") + ":" + taimer.Second.ToString("00") + ":" + "0" + "\n"); //допишет текст в конец файла
                ox12 = ((taimer.Second & 0x1f) << 11) | (0 << 1);
                ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | (taimer.Second >> 5);
                ox16 = ((taimer.Day >> 4) & 0x03);
                ClassBAAK12NoTail.Time0x12 = Convert.ToUInt32(ox12);
                ClassBAAK12NoTail.Time0x14 = Convert.ToUInt32(ox14);
                ClassBAAK12NoTail.Time0x16 = Convert.ToUInt32(ox16);
                BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
                BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
                BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Значение на запись в регистры" + ox12.ToString() + " " +
                   ox14.ToString() + " " + ox16.ToString() + ":" + "0" + "\n"); //допишет текст в конец файла
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Значение в регистрх" + BAAK12T.Time0x12.ToString() + " " +
                 BAAK12T.Time0x14.ToString() + " " + BAAK12T.Time0x16.ToString() + ":" + "0" + "\n"); //допишет текст в конец файла
                TimeTaimer1 = taimer.Day.ToString("00") + " " + taimer.Hour.ToString("00") + ":" + taimer.Minute.ToString("00") + ":" + taimer.Second.ToString("00") + ":" + "0";

                // BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(0);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка: " + e.ToString() + " Запуск с системным временм");


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
                TimeTaimer1 = taimer.Day.ToString("00") + " " + taimer.Hour.ToString("00") + ":" + taimer.Minute.ToString("00") + ":" + taimer.Second.ToString("00") + ":" + taimer.Millisecond.ToString("000");
                // BAAK12T.NameRan = Convert.ToString(taimer.Day) + "." + Convert.ToString(taimer.Month) + "." + Convert.ToString(taimer.Year) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
            }
        }
        /// <summary>
        /// Готовит Установку к старту с МС но без МГВС
        /// </summary>
        /// <param name="t"></param>
        private void TimeTaimerMC(int t)
        {
            
            
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Запуск с синхронизацией Старт с МС без МГВС"; }));//
            int ox12 = 0;
            int ox14 = 0;
            int ox16 = 0;
            DateTime taimer = DateTime.UtcNow;
           // taimer = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, MS.hours, MS.minutes, MS.seconds, 0);

            taimer = taimer.AddSeconds(t);
            ox12 = ((taimer.Second & 0x1f) << 11) | (0 << 1);
            ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | (taimer.Second >> 5);
            ox16 = ((taimer.Day >> 4) & 0x03);
            BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
            BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
            BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
            ClassBAAK12NoTail.Time0x12 = Convert.ToUInt32(ox12);
            ClassBAAK12NoTail.Time0x14 = Convert.ToUInt32(ox14);
            ClassBAAK12NoTail.Time0x16 = Convert.ToUInt32(ox16);
            TimeTaimer1 = Convert.ToString(taimer.Day) + " " + Convert.ToString(taimer.Hour) + ":" + Convert.ToString(taimer.Minute) + ":" + Convert.ToString(taimer.Second) + ":" + Convert.ToString(taimer.Millisecond);
            BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
        }
        /// <summary>
        /// Готовит Установку к старту без синхронизации
        /// </summary>
        /// <param name="t"></param>
        private void TimeTaimerNoCinx()
        {
            int ox12 = 0;
            int ox14 = 0;
            int ox16 = 0;
            DateTime taimer = DateTime.UtcNow;
            taimer = taimer.AddSeconds(5);
            ox12 = (((taimer.Second) & 0x1f) << 11) | (taimer.Millisecond << 1);
            ox14 = ((taimer.Day & 0x0f) << 12) | (taimer.Hour << 7) | (taimer.Minute << 1) | ((taimer.Second) >> 5);
            ox16 = ((taimer.Day >> 4) & 0x03);
            BAAK12T.Time0x12 = Convert.ToUInt32(ox12);
            BAAK12T.Time0x14 = Convert.ToUInt32(ox14);
            BAAK12T.Time0x16 = Convert.ToUInt32(ox16);
            ClassBAAK12NoTail.Time0x12 = Convert.ToUInt32(ox12);
            ClassBAAK12NoTail.Time0x14 = Convert.ToUInt32(ox14);
            ClassBAAK12NoTail.Time0x16 = Convert.ToUInt32(ox16);


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

        /// <summary>
        /// Записывет начальное значение часов в платы и разрешает клок МС
        /// </summary>
        /// <param name="ожидание"></param>
        public void РежимСинхИлиНет(int ожидание)
        {
            try
            {
                TimeTaimer(ожидание);
                if (ClassParentsBAAK.Синхронизация)
                {
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Запуск с синхронизацией"; }));//
                    Thread.Sleep(2000);
                    if (!set.FlagClok)
                    {
                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Подготовка МС"; }));
                        Thread.Sleep(2000);
                        //TimeTaimer(ожидание);
                        BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
                        ClassBAAK12NoTail.ЗаписьВремяРегистрDelegate?.Invoke();
                        BAAK12T.СтартЧасовDelegate?.Invoke();
                        //СтартЧасовDelegate();
                        // Thread.Sleep(ожидание * 1000);
                        int min = 0;
                        while (min < ожидание * 1000)
                        {
                            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Ожидание =" + ожидание + " сек." + " Осталось " + (((ожидание * 1000) - min) / 1000).ToString() + " сек."; }));
                            Thread.Sleep(1000);
                            min = min + 1000;
                        }
                        try
                        {
                            MS.АвтономныйКлокРазрешен(1);
                        }
                        catch
                        {

                        }
                        try
                        {
                           // MS1.АвтономныйКлокРазрешен(1);
                        }
                        catch
                        {

                        }
                    }
                    else//запуск с МГВМ
                    {

                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Подготовка МГВС"; }));
                        Thread.Sleep(1000);
                        Thread.Sleep(1000);
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
                        Thread.Sleep(1000);
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
                    Thread.Sleep(2000);
                    //TimeTaimer(5);
                    BAAK12T.ЗаписьВремяРегистрDelegate?.Invoke();
                    ClassBAAK12NoTail.ЗаписьВремяРегистрDelegate?.Invoke();
                    // СтартЧасовDelegate();
                }

              
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при работе с синхронизацией");
            }
        }
    }
}
