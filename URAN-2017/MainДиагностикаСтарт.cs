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
        /// <summary>
        /// Определяем к каким кластерам можно и нужно подключиться и модулю синхронизации
        /// </summary>
        public async Task FirstDiagnosticaSistem()//Определяем к каким кластерам можно подключиться и модулю синхронизации
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
               
              await stMS.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {  stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен";}));
               
              
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
                //Thread.Sleep(1000);
             //   stMS.Content = "МС не обнаружен, запуск с синхронизации не возможен";
              //  Thread.Sleep(500);
              //  ClassParentsBAAK.Синхронизация = false;
               // toggle.IsChecked = false;
              //  toggle.IsEnabled = false;

            }
            int h = 0;
            int k = 0;
           if(_DataColec1.Count!=0)
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
                                        Кластер1_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер1_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер1_2);
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible;}));
                                     await  klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                      await  List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(()=>List2.Visibility=Visibility.Visible));
                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер1 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер1.Inciliz = true;
                                        _DataColecViev.Add(Кластер1);
                                     

                                      
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
                            }
                            break;

                        case 2:
                            if (bak.FkagNameBAAK)
                            {
                                if (await LocalPing(bak.KLIP))
                                {
                                    if (bak.BAAK12NoT)
                                    {
                                        Кластер2_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер2_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер2_2);
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер2 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер2.Inciliz = true;
                                        _DataColecViev.Add(Кластер2);
                                       
                                     

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
                                        Кластер3_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер3_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер3_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер3 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер3.Inciliz = true;
                                        _DataColecViev.Add(Кластер3);
                                       
                                       
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
                                        Кластер4_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер4_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер4_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));


                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер4 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер4.Inciliz = true;
                                        _DataColecViev.Add(Кластер4);
                                    
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
                                        Кластер5_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp,  Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер5_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер5_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер5 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер5.Inciliz = true;
                                        _DataColecViev.Add(Кластер5);
                                      
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
                                        Кластер6_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер6_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер6_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер6 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер6.Inciliz = true;
                                        _DataColecViev.Add(Кластер6);
                                 
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
                                        Кластер7_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер7_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер7_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер7 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер7.Inciliz = true;
                                        _DataColecViev.Add(Кластер7);
                                 
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
                                        Кластер8_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер8_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер8_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));

                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер8 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер8.Inciliz = true;
                                        _DataColecViev.Add(Кластер8);
                                   
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
                                        Кластер9_2 = new ClassBAAK12NoTail() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp,  Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер9_2.Inciliz = true;
                                        _DataColecVievList2.Add(Кластер9_2);
                                        await klP2.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP2.Visibility = Visibility.Visible; }));
                                        await List2.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List2.Visibility = Visibility.Visible));


                                    }
                                    else
                                    {
                                        await BorderT.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { BorderT.Visibility = Visibility.Visible; }));
                                        await klP1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { klP1.Visibility = Visibility.Visible; }));
                                        await List1.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => List1.Visibility = Visibility.Visible));
                                        Кластер9 = new BAAK12T() { Host = bak.KLIP, NamKl = bak.Klname, NameBAAK12 = bak.NameBAAK, CтатусБААК12 = "Ожидает СТАРТ", ИнтервалТемпаСчета = IntervalTemp, Nkl = h, BAAKTAIL = !bak.BAAK12NoT };
                                        Кластер9.Inciliz = true;
                                        _DataColecViev.Add(Кластер9);
                                    
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
                            }
                            break;
                    }

                }
            }
          

        }
    }
}
