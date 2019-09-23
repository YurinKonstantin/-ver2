using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using URAN_2017.FolderSetUp;

namespace URAN_2017
{
    public partial class MainWindow
    {
        bool diag;
        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            myGif.Position = new TimeSpan(0, 0, 1);
            myGif.Play();
        }
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
                PingReply reply = await pingSender.SendPingAsync(ip, 200);
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
        /// Из файла настроек загружаем настройки
        /// (нужно ли сразу производить конект, тест, ip адреса для кластеров и модуля синхронизации)
        /// </summary>
        public async Task НачальныеНастройки()//Из файла настроек загрузаем настройки(нужно ли сразу производить конект, тест, ip адреса для кластеров и модуля синхронизации)
        {
            setP = new ClassSetUpProgram();
            ClassSerilization.DeSerial(out setP);
            GridStartInfo.Visibility = Visibility.Visible;
            if(setP.FlagMainRezim)
            {
                await DeSerial200();
                BAAK12T.NameFileWay = set.WayDATA;
                BAAK12T.NameFileSetUp = set.WaySetup;
                BAAK12T.TrgAll = set.Trg;
                BAAK12T.PorogAll = Convert.ToUInt32(set.Porog);
                BAAK12T.DataLenght = Convert.ToUInt32(set.DataLenght);
                BAAK12T.wayDataBD = set.WayDATABd;
                BAAK12T.wayDataTestBD = set.TestWayDATABd;
                BAAK12T.PorogNutron = otbN.Porog;
                BAAK12T.DlNutron = otbN.Dlit;
             
                if (set.Discret != 0)
                {
                    BAAK12T.ДискретностьХвост = 100 / set.Discret;
                }

                ClassBAAK12NoTail.NameFileWay = set.WayDATA;
                ClassBAAK12NoTail.NameFileSetUp = set.WaySetup;
                ClassBAAK12NoTail.TrgAll = set.TrgNO;
                ClassBAAK12NoTail.PorogAll = Convert.ToUInt32(set.PorogNO);
                ClassBAAK12NoTail.DataLenght = Convert.ToUInt32(set.DataLenght);
                ClassBAAK12NoTail.wayDataBD = set.WayDATABd;
                ClassBAAK12NoTail.wayDataTestBD = set.TestWayDATABd;
            }
            else
            {
                await DeSerial100();

                ClassBAAK12_100.NameFileWay = set.WayDATA;
              
                ClassBAAK12_100.NameFileSetUp = set.WaySetup;
                ClassBAAK12_100.TrgAll = set.Trg;
                ClassBAAK12_100.PorogAll = Convert.ToUInt32(set.Porog);
                ClassBAAK12_100.DataLenght = Convert.ToUInt32(set.DataLenght);
                ClassBAAK12_100.wayDataBD = set.WayDATABd;
                ClassBAAK12_100.wayDataTestBD = set.TestWayDATABd;

            }
           
            diag = true;//При загрузке применять диагностику
            conectsr = true;//при загрузке конектится к платам
            IntervalNewFile = set.IntervalFile;
       
            
            ClassParentsBAAK.Синхронизация = set.FlagMS;
         
            
           // toggle.IsChecked = set.FlagMS;
            BuMC.Toggled1 = !set.FlagMS;
            if (BuMC.Toggled1 == true)
            {

              
                LabFlagMC.Content = "Вкл";
                LabFlagMC.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
               
                LabFlagMC.Content = "Выкл";
                LabFlagMC.Foreground = System.Windows.Media.Brushes.Red;

            }
        
            ChecRez.IsChecked = setP.FlagMainRezim;
        }
        ClassSetUpProgram setP;
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
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            DateTime dateTime1 = new DateTime();
            dateTime1 = DateTime.Now;

            dateTime1 = dateTime1.AddHours(-71);
           MyGrafic.Labels.Clear();
            MyGrafic.LabelsN.Clear();
            while (dateTime1.Subtract(dateTime).TotalHours != 0)
            {
                MyGrafic.Labels.Add(dateTime1.ToString());
                MyGrafic.LabelsN.Add(dateTime1.ToString());
                dateTime1 = dateTime1.AddHours(1);
            }
            foreach(BAAK12T bAAK12T in _DataColecViev)
            {
                MyGrafic.infoZaprocBD(bAAK12T.NamKl, bAAK12T.Nkl, BAAK12T.wayDataBD);
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
                 await   rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка готова к старту Плат БААК12-200 "+"\t"+ _DataColecViev.Count.ToString(); }));

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
            if (setP.FlagMainRezim)
            {
                if (await LocalPing(set.MS))
                {
                    InitializeMS(set.MS);
                    //toggle.IsEnabled = true;
                    BuMC.IsEnabled = true;
                    MS1View.Visibility = Visibility.Visible;
                    MS.text = "Ip " + set.MS.ToString();
                }
                else
                {
                    ListEr.Add(new ClassErrorStartAndIspravlenie() { Name = "MC1", Error = "Не обнаружен МС1", ErrorIsprav = "Перегрузите питание МС1 и повторно произведите старт устанвоки или нажмите 'продолжить' и затем 'Обновить'", ArduinoIP = "0" });
                    await stMS.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен"; }));


                    ClassParentsBAAK.Синхронизация = false;
                    //toggle.IsChecked = false;
                   // toggle.IsEnabled = false;
                    BuMC.IsEnabled = false;
                    BuMC.Toggled1 = true;
                    if (BuMC.Toggled1 == true)
                    {


                        LabFlagMC.Content = "Вкл";
                        LabFlagMC.Foreground = System.Windows.Media.Brushes.Green;



                    }
                    else
                    {

                        LabFlagMC.Content = "Выкл";
                        LabFlagMC.Foreground = System.Windows.Media.Brushes.Red;

                    }

                }
                if (await LocalPing(set.MS1))
                {
                    InitializeMS1(set.MS1);
                   // toggle.IsEnabled = true;
                    BuMC.IsEnabled = false;
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
                if (await LocalPing(set.MS))
                {
                    InitializeMS(set.MS);
                    //toggle.IsEnabled = true;
                    BuMC.IsEnabled = true;
                    MS1View.Visibility = Visibility.Visible;
                    MS.text = "Ip " + set.MS.ToString();
                }
                else
                {
                    ListEr.Add(new ClassErrorStartAndIspravlenie() { Name = "MC1", Error = "Не обнаружен МС1", ErrorIsprav = "Перегрузите питание МС1 и повторно произведите старт устанвоки или нажмите 'продолжить' и затем 'Обновить'", ArduinoIP = "0" });
                    await stMS.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен"; }));


                    ClassParentsBAAK.Синхронизация = false;
                    //toggle.IsChecked = false;
                    // toggle.IsEnabled = false;
                    BuMC.IsEnabled = false;
                    BuMC.Toggled1 = true;
                    if (BuMC.Toggled1 == true)
                    {


                        LabFlagMC.Content = "Вкл";
                        LabFlagMC.Foreground = System.Windows.Media.Brushes.Green;



                    }
                    else
                    {

                        LabFlagMC.Content = "Выкл";
                        LabFlagMC.Foreground = System.Windows.Media.Brushes.Red;

                    }

                }
                if (await LocalPing(set.MS1))
                {
                    InitializeMS1(set.MS1);
                    // toggle.IsEnabled = true;
                    BuMC.IsEnabled = false;
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
            int h = 0;
            int k = 0;
            if (setP.FlagMainRezim)
            {
                if (_DataColec1.Count != 0)
                {
                    timeSlepCol = _DataColec1.Count;
                    var ListBAAK12T = from BAAK_T in _DataColec1 where BAAK_T.BAAK12NoT ==false select BAAK_T;
                    foreach (Bak bak in ListBAAK12T)
                    {

                       


                        k++;
                        if (bak.FkagNameBAAK)
                        {

                            if (await LocalPing(bak.KLIP))
                            {


                               // if (bak.BAAK12NoT)
                                {
                                //    Кластер1_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", brushes = Brushes.Black, ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK };
                                 //   Кластер1_2.Inciliz = true;
                                  //  _DataColecVievList2.Add(Кластер1_2);
                                 //   await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                  //  await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                  //  await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                    //ViewSet(true, true);
                                }
                               // else
                                {
                                    Debug.WriteLine(" MyGrasssfi");
                                    await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                    await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                    await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                    Кластер1 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", Brushes = Brushes.Black, ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK, FlagSaveBD=set.FlagSaveBD, FlagSaveBin=set.FlagSaveBin };
                                    Кластер1.Inciliz = true;
                                    _DataColecViev.Add(Кластер1);
                                    //ViewSet(true, false);

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
                             //   if (bak.BAAK12NoT)
                                {
                                //    ss = "БААК12-200";
                                }
                             //   else
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

                    }
                    var ListBAAK12 = from BAAK_NT in _DataColec1 where BAAK_NT.BAAK12NoT == true select BAAK_NT;
                    foreach (Bak bak in ListBAAK12)
                    {




                        k++;
                        if (bak.FkagNameBAAK)
                        {

                            if (await LocalPing(bak.KLIP))
                            {


                                
                                        Кластер1_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", Brushes = Brushes.Black, ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT, trigOtBAAK = bak.TrigOtBAAK, FlagSaveBD=set.FlagSaveBD, FlagSaveBin=set.FlagSaveBin };
                                       Кластер1_2.Inciliz = true;
                                      _DataColecVievList2.Add(Кластер1_2);
                                       await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                      await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                     await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));
                                    //ViewSet(true, true);
                                
                             


                           
                            }
                            else
                            {
                                string ss = "БААК12-200";
                             
                                        
                                
                              
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
            else
            {
                ViewSet(false, true);
                if (_DataColecBAAK12100.Count != 0)
                {
                    timeSlepCol = _DataColecBAAK12100.Count;
                    foreach (Bak bak in _DataColecBAAK12100)
                    {
                        k++;
                      

                         
                                if (bak.FkagNameBAAK)
                                {
                                    if (await LocalPing(bak.KLIP))
                                    {

                                
                                            Кластер1_3 = new ClassBAAK12_100 { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ",
                                                Brushes = Brushes.Black,
                                                ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT,
                                                trigOtBAAK = bak.TrigOtBAAK, FlagSaveBin=set.FlagSaveBin, FlagSaveBD=set.FlagSaveBD };
                                            Кластер1_3.Inciliz = true;
                  
                                            _DataColecVievList3.Add(Кластер1_3);
                                        
                                ViewSet(false, true);
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
}
