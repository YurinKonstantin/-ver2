using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        bool diag;
        bool conectsr;
        private int intervalNewFile = 5;
        private int intervalTemp = 1;
        public int IntervalNewFile { get => intervalNewFile; set => intervalNewFile = value; }
        public int IntervalTemp { get => intervalTemp; set => intervalTemp = value; }
        /// <summary>
        /// Сканер адресов IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private async Task<bool> LocalPing(string ip)//Сканер адресов IP
        {
            try
            {


                Ping pingSender = new Ping();
                PingReply reply = await pingSender.SendPingAsync(ip, 500);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
  
        /// <summary>
        /// Из файла настроек загрузаем настройки
        /// (нужно ли сразу производить конект, тест, ip адреса для кластеров и модуля синхронизации)
        /// </summary>
        public async Task НачальныеНастройки()//Из файла настроек загрузаем настройки(нужно ли сразу производить конект, тест, ip адреса для кластеров и модуля синхронизации)
        {

            GridStartInfo.Visibility = Visibility.Visible;
          await  DeSerial();
            diag = true;//При загрузке применять диагностику
            conectsr = true;//при загрузке конектится к платам
            IntervalNewFile = set.IntervalFile;
            BAAK12T.NameFileWay = set.WayDATA;
            BAAK12T.NameFileSetUp = set.WaySetup;
            BAAK12T.TrgAll = set.Trg;
            BAAK12T.PorogAll = Convert.ToUInt32(set.Porog);
            BAAK12T.DataLenght = Convert.ToUInt32(set.DataLenght);
            BAAK12T.wayDataBD = set.WayDATABd;
            BAAK12T.wayDataTestBD = set.TestWayDATABd;
            BAAK12T.PorogNutron = otbN.Porog;
            BAAK12T.DlNutron = otbN.Dlit;

            ClassBAAK12NoTail.NameFileWay = set.WayDATA;
            ClassBAAK12NoTail.NameFileSetUp = set.WaySetup;
            ClassBAAK12NoTail.TrgAll = set.TrgNO;
            ClassBAAK12NoTail.PorogAll = Convert.ToUInt32(set.PorogNO);
            ClassBAAK12NoTail.DataLenght = Convert.ToUInt32(set.DataLenght);
            ClassBAAK12NoTail.wayDataBD = set.WayDATABd;
            ClassBAAK12NoTail.wayDataTestBD = set.TestWayDATABd;
            
            ClassParentsBAAK.Синхронизация = set.FlagMS;
            if(set.Discret!=0)
            {
                BAAK12T.ДискретностьХвост = 100 / set.Discret;
            }
            
            toggle.IsChecked = set.FlagMS;
            ChecRez.IsChecked = UserSetting.FlagMainRezim;
        }
       
        /// <summary>
        /// Производит процедуру подготовки установки к пуску.
        /// Определяем к каким кластерам можно подключиться и модулю синхронизации.
        /// Конектимся к доступным кластерам.
        /// </summary>
        public async Task Запуск()//Производит процедуру подготовки установки к пуску
        {


            GridStartInfo.Visibility = Visibility.Visible;

          
           
       await  rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Foreground = System.Windows.Media.Brushes.Red; }));
            
         await rezimYst1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => { rezimYst.Content = "Начальные настройки из файла"; }));
          
           
          await  НачальныеНастройки();//загрузаем настройки
            if (diag)
            {
                //Thread.Sleep(1000);
                await FirstDiagnosticaSistem();//Определяем к каким кластерам можно подключиться и модулю синхронизации
            }
            if (conectsr)
            {
              //  Thread.Sleep(2000);
            
           await rezimYst1.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => { rezimYst1.Content = "Конект"; }));
               
               // Thread.Sleep(4000);
               await Conect();//Конектимся к доступным кластерам
            }
            GridStartInfo.Visibility = Visibility.Hidden;
            if(ListEr.Count!=0)
            {
                GridStartInfoError.Visibility = Visibility.Visible;
            }
            else
            {
                GridStartInfoError.Visibility = Visibility.Hidden;
            }
            

        }
        /// <summary>
        /// Подключаемся к доступным платам
        /// </summary>
        public async Task Conect()
        {
            try
            {
               // Thread.Sleep(1000);
               if(ConnnectURANDelegate != null)
                {
                    ConnnectURANDelegate?.Invoke();
                }
                if (BAAK12T.ConnnectURANDelegate!=null)
                {
                    await BAAK12T.ConnnectURANDelegate?.Invoke();
                 await   rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка готова к старту"; }));

                }
                else
                {
             
               await rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Плат для подключения не обнаружено"; }));
                }
               
          
               
            }
            catch (NullReferenceException e)
            {
               
                MessageBox.Show(e.ToString()+ "Conect1()");
                
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Conect2()");
            }

        }

        public async void ViewSet(bool rez, bool noT)
        {
            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
            if (rez)
            {
               await Tab1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { Tab1.Visibility = Visibility.Visible; }));
                await klP3.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP3.Visibility = Visibility.Collapsed; }));
                await List3.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => List3.Visibility = Visibility.Collapsed));
                if(noT)
                {
                   
                    await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                    await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                }
                if (!noT)
                {
                    await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                    await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                }
            }
            else
            {
                await Tab1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { Tab1.Visibility = Visibility.Collapsed; }));
                await Chart.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { Chart.Visibility = Visibility.Collapsed; }));
                await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Collapsed; }));
                await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Collapsed));

                await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Collapsed; }));
                await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Collapsed));
                await klP3.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP3.Visibility = Visibility.Visible; }));
                await List3.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List3.Visibility = Visibility.Visible));

            }
        }
        public async void ViewClose(bool rez, bool noT)
        {
            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Collapsed; }));
            
                await klP3.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP3.Visibility = Visibility.Collapsed; }));
                await List3.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List3.Visibility = Visibility.Collapsed));
               

                    await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Collapsed; }));
                    await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Collapsed));
               
                    await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Collapsed; }));
                    await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Collapsed));
                
          
        }
        /// <summary>
        /// Определяем к каким кластерам можно и нужно подключиться и модулю синхронизации
        /// </summary>
        public async Task FirstDiagnosticaSistem()//Определяем к каким кластерам можно подключиться и модулю синхронизации
        {
            ListEr.Clear();
            if (UserSetting.FlagMainRezim)
            {
                if (await LocalPing(set.MS))
                {
                    InitializeMS(set.MS);
                    toggle.IsEnabled = true;
                    MS1View.Visibility = Visibility.Visible;
                    MS.text = "Ip " + set.MS.ToString();
                }
                else
                {
                    ListEr.Add(new ClassErrorStartAndIspravlenie() { Name = "MC1", Error = "Не обнаружен МС1", ErrorIsprav = "Перегрузите питание МС1 и повторно произведите старт устанвоки или нажмите 'продолжить' и затем 'Обновить'", ArduinoIP = "0" });
                    await stMS.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен"; }));


                    ClassParentsBAAK.Синхронизация = false;
                    toggle.IsChecked = false;
                    toggle.IsEnabled = false;

                }
                if (await LocalPing(set.MS1))
                {
                    InitializeMS1(set.MS1);
                    toggle.IsEnabled = true;
                    MS2View.Visibility = Visibility.Visible;
                    MS1.text = "Ip " + set.MS1.ToString();

                }
                else
                {
                    ListEr.Add(new ClassErrorStartAndIspravlenie() { Name = "MC2", Error = "Не обнаружен МС2", ErrorIsprav = "Перегрузите питание МС2 и повторно произведите старт устанвоки или нажмите 'продолжить' и затем 'Обновить'", ArduinoIP = "0" });

                    //Thread.Sleep(1000);
                    //   stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен";
                    //  Thread.Sleep(500);
                    //  ClassParentsBAAK.Синхронизация = false;
                    // toggle.IsChecked = false;
                    //  toggle.IsEnabled = false;

                }
            }
            else
            {

            }
            int h = 0;
            int k = 0;
            if (UserSetting.FlagMainRezim)
            {
               

                if (_DataColec1.Count != 0)
                {
                    foreach (Bak bak in _DataColec1)
                    {
                        k++;
                        switch (k)
                        {

                            case 1:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {


                                        if (bak.BAAK12NoT)
                                        {
                                            Кластер1_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер1_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер1_2);
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер1 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер1.Inciliz = true;
                                            _DataColecViev.Add(Кластер1);
                                            ViewSet(true, false);


                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));


                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }


                                        //InitializeKlaster1(Кластер1);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                            "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                            "3. Установите все галочки Relay и нажмите 'Set'" +
                                            "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;

                            case 2:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            Кластер2_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер2_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер2_2);
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер2 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер2.Inciliz = true;
                                            _DataColecViev.Add(Кластер2);
                                            ViewSet(true, false);


                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }


                                        //InitializeKlaster2(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                            "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                            "3. Установите все галочки Relay и нажмите 'Set'" +
                                            "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 3:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер3_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер3_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер3_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер3 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер3.Inciliz = true;
                                            _DataColecViev.Add(Кластер3);

                                            ViewSet(true,false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster3(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                             "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                             "3. Установите все галочки Relay и нажмите 'Set'" +
                                             "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 4:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер4_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер4_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер4_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);

                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер4 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер4.Inciliz = true;
                                            _DataColecViev.Add(Кластер4);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;

                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster4(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                              "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                              "3. Установите все галочки Relay и нажмите 'Set'" +
                                              "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;

                            case 5:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер5_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер5_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер5_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер5 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер5.Inciliz = true;
                                            _DataColecViev.Add(Кластер5);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster5(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                                  "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                                  "3. Установите все галочки Relay и нажмите 'Set'" +
                                                  "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 6:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер6_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер6_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер6_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер6 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер6.Inciliz = true;
                                            _DataColecViev.Add(Кластер6);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster6(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                              "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                              "3. Установите все галочки Relay и нажмите 'Set'" +
                                              "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 7:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер7_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = true };
                                            Кластер7_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер7_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер7 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                            Кластер7.Inciliz = true;
                                            _DataColecViev.Add(Кластер7);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster6(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                          "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                          "3. Установите все галочки Relay и нажмите 'Set'" +
                                          "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 8:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер8_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер8_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер8_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);
                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер8 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер8.Inciliz = true;
                                            _DataColecViev.Add(Кластер8);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster6(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                              "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                              "3. Установите все галочки Relay и нажмите 'Set'" +
                                              "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                            case 9:
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {
                                        if (bak.BAAK12NoT)
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            Кластер9_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер9_2.Inciliz = true;
                                            _DataColecVievList2.Add(Кластер9_2);
                                            await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                            await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                            ViewSet(true, true);

                                        }
                                        else
                                        {
                                            await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                            await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                            await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                            Кластер9 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер9.Inciliz = true;
                                            _DataColecViev.Add(Кластер9);
                                            ViewSet(true, false);
                                            try
                                            {
                                                Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { MyGrafic.Add(bak.Klname); }));
                                                h++;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.ToString());
                                            }
                                        }

                                        //InitializeKlaster6(bak.KLIP, bak.Klname, bak.nameBAAK);
                                    }
                                    else
                                    {
                                        string ss = null;
                                        if (bak.BAAK12NoT)
                                        {
                                            ss = "БААК12-200";
                                        }
                                        else
                                        {
                                            ss = "БААК12-200N";
                                        }
                                        ListEr.Add(new ClassErrorStartAndIspravlenie()
                                        {
                                            Name = "Кластер " + bak.Klname.ToString(),
                                            ArduinoIP = "1",
                                            Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                            ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                               "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                               "3. Установите все галочки Relay и нажмите 'Set'" +
                                               "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                                        });


                                    }
                                }
                                break;
                        }

                    }
                }
            }
            else
            {
                ViewSet(false, true);
                if (_DataColecBAAK12100.Count != 0)
                {
                    foreach (Bak bak in _DataColecBAAK12100)
                    {
                        k++;
                      

                         
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {

                                
                                            Кластер1_3 = new ClassBAAK12_100 { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                            Кластер1_3.Inciliz = true;
                                            _DataColecVievList3.Add(Кластер1_3);
                                        
                                ViewSet(false, true);
                                    }

                                }
                        else
                        {
                            string ss = null;
                          
                            
                                ss = "БААК12-100";
                            
                            ListEr.Add(new ClassErrorStartAndIspravlenie()
                            {
                                Name = "Кластер " + bak.Klname.ToString(),
                                ArduinoIP = "1",
                                Error = "Не обнаружена плата " + ss + " кластера" + bak.Klname.ToString(),
                                ErrorIsprav = "1. Откройте программу Relya Control и выберите вкладку " + bak.Klname.ToString() +
                                   "\n" + "2. В программе Relay Control нажмите кнопку 'Set'" + "\n" +
                                   "3. Установите все галочки Relay и нажмите 'Set'" +
                                   "\n" + "4. Закройте программу Relay control, ожидайте 2- 3 минуты завершения перезапуска платы, после нажмите «Обновить»."
                            });


                        }

                    }
                }


            }


        }
    }
}
