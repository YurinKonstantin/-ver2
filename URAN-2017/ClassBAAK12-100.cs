using NeutronObrabotka;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using URAN_2017.FolderSetUp;

namespace URAN_2017
{
   public class ClassBAAK12_100 : BAAK12T, IDisposable
    {
    
 
      
    
        /// <summary>
        /// Вычитывает нужные файлы
        /// </summary>
       public void ВычитываемДанныеНужные1()
        {
            bool endd = false;
            int x = 0;
            while (!endd)
            {
                int res = Read13007(out byte[] buf);//читаем с платы
                if (res > 0)
                {

                    if (buf[0] == 0xFF)
                    {
                        CountFlagEnd++;
                    }
                    else
                    {
                        CountFlagEnd = 0;
                    }
                    DataBAAKList.Add(buf[0]);


                    if ((data_w != null) & (data_fs != null) & CountFlagEnd == 4)
                    {

                        WriteFileData(DataBAAKList);
                        DataBAAKList.Clear();
                        CountFlagEnd = 0;

                        КолПакетов++;
                        DataBAAKList = new List<byte>();
                    }

                    x = 0;

                }
                else
                {
                    x++;
                }
                if (x < 50)
                {
                    endd = true;
                }

            }


        }



   
   

        /// <summary>
        /// Расчет темпа и запись результата в БД
        /// </summary>
        public override void TempPacetov()
        {
         
            ТемпПакетов = Convert.ToInt32(КолПакетов) - Пакетов;
            Пакетов = Convert.ToInt32(КолПакетов);

            ТемпПакетовN = Convert.ToInt32(КолПакетовN) - ПакетовN;
            ПакетовN = Convert.ToInt32(КолПакетовN);
        }
        /// <summary>
        /// Создает новый файл
        /// </summary>
        public override void CreatFileData()
        {
            try
            {
                string path = NameFileWay;
                string subpath = @"V";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    MessageBox.Show("папки небыло" + "\n" + NameFile, "Сохранение данных");
                }
                dirInfo = new DirectoryInfo(path + @"\" + subpath);

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                string tipPl;
                tipPl = "V";
                String sd = Time();
                NameFile = NameFileWay + @"\" + subpath + @"\" + NamKl + "_" + sd + "_" + "V" + ".bin";
                data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                data_w = new BinaryWriter(data_fs);
               // BDReadFile(NamKl + "_" + sd + "_" + "V", NameBAAK12, sd, BAAK12T.NameRan);
                NameFileClose = NamKl + "_" + sd + "_" + tipPl;
          
            }
            catch (Exception ex)
            {
                InDe(false);
                Brushes = System.Windows.Media.Brushes.Red;
                CтатусБААК12 = "Ошибка при создании файла";
            }
            // }
        }
        public override void CloseFile()//закрытие файла
        {
            // if(Conect300Statys)
            //{
            if (data_w != null)
            {
                try
                {
                    data_w.Close();
                    data_w.Dispose();
                    BDReadCloseFile(NameFileClose, Time());
                }
                catch (Exception)
                {
                    InDe(false);
                    Brushes = System.Windows.Media.Brushes.Red;
                    CтатусБААК12 = "Ошибка при закрытии потока файла";
                }
                // }
                if (data_fs != null)
                {
                    try
                    {
                        data_fs.Close();
                        data_fs.Dispose();
                    }
                    catch (Exception)
                    {
                        Brushes = System.Windows.Media.Brushes.Red;
                        CтатусБААК12 = "Ошибка при закрытии файла";
                    }
                }
            }
        }

        /// <summary>
        /// записываем данных о событии из очереди в в бд
        /// </summary>
        public override void WriteInFileIzOcherediBD()
        {
            ClassZapicBD100 BDData = new ClassZapicBD100();
            try
            {

                OcherediNaZapicBD.TryDequeue(out BDData);
                if (BDData != null)
                {
                  
                     //TODO   BDReadСобытие(BDData.nameFileBD, BDData.nameBAAKBD, BDData.timeBD, BDData.nameRanBD, BDData.AmpBD, BDData.nameklasterBD, BDData.NlBD, BDData.sigBDnew);//пишем в бд
               

                    КолПакетовОчер2++;
                    
              


                }


            }
            catch (InvalidOperationException)
            {

            }
            catch (NullReferenceException ee)
            {
                Debug.WriteLine("Error 590");
            }
            catch (Exception e)
            {

                CтатусБААК12 = "Ошибка. Отключена " + e;
                InDe(false);
            }
        }
      
        
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public override void WriteInFileIzOcheredi()//работа с данными из очереди
        {
            try
            {

                bool? ed = OcherediNaZapic.TryDequeue(out dataYu);
                if (ed == true)
                {


                    if (dataYu.ListData != null)
                    {
                        byte[] d = new byte[dataYu.ListData.Count];
                        int x = 0;
                        foreach (Byte b in dataYu.ListData)
                        {
                            d[x] = b;
                            x++;
                        }
                        if (data_w != null)
                        {
                            data_w.Write(d);
                        }


                        Double[] sigm = new double[12];

                        Obrabotka(dataYu.ListData, out int[] Ampl, out string time1, out double[] NL, out sigm, (int)DataLenght, out int dN, out bool neu);//парсинг данных

                        if (neu)
                        {
                            OcherediNaZapicBD.Enqueue(new ClassZapicBD100() { nameFileBD = NameFileClose, nameBAAKBD = NameBAAK12, timeBD = time1, nameRanBD = BAAK12T.NameRan, AmpBD = (int)Ampl[dN - 1], nameklasterBD = NamKl, NlBD = (int)NL[dN - 1], sigBDnew = sigm[dN - 1] });

                        }

                        КолПакетовОчер++;
                        // DataBAAKList1 = null;
                        d = null;
                    }
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (Exception e)
            {

                CтатусБААК12 = "Ошибка. Отключена " + e;
                InDe(false);
            }
        }
        WindowChart windowChart;
        public WindowChart openWindowsChart()
        {
            windowChart = new WindowChart(NamKl);

            windowChart.Show();
            return windowChart;
        }
        private void Obrabotka(List<Byte> buf00, out int[] Amp, out string time, out double[] Nul, out double[] sig, int dl, out int dn, out bool neutron)
        {
            int[,] data = new int[12, 1024*dl];
   
            sig = new double[12];
            Nul = new double[12];
            time = "0";
            dn = 0;
            neutron = false;
            bool bad = false;

            Amp = new int[12];

      



        
            try
            {
               
                ParserBAAK12.ParseBinFileBAAK12.ParseBinFileBAAK200(buf00.ToArray(), dl,  out data, out time);
                if (grafOtob)
                {
                    //  Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    //  {
                    Application.Current.Dispatcher.Invoke((Action)delegate { MyGrafic.AddPointRaz(data, "Кластер" + namKl, masnul); });
                    //  }));
                }
                if (windowChart != null)
                {
                    // Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    // {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        windowChart.AddPointRaz(data);
                    });
                    //  }));
                }
                // To Do
                ParserBAAK12.ParseBinFileBAAK12.MaxAmpAndNul(data, ref sig, ref Amp, ref Nul, ref bad, true, 1, 1);
                if(!bad)
                {
                    Obrabotca.AmpAndTime(data, masnul, out int[] maxTime, out int[] maxAmp);
                
                    if (Obrabotca.Dneutron(maxAmp, PorogNutron, out dn))
                    {
                     Obrabotca.FirstTme(maxTime[dn-1], maxAmp[dn-1], PorogNutron, data, masnul, dn);
                    }

                 
                }



            }
            catch (Exception)
            {

            }

        }



  
        private int CountFlagEnd = 0;
        private int CountFlagEndErroy = 0;


        /// <summary>
        /// Читает данные с платы и пишет их в очередь, считаем количество пакетов
        /// </summary>
        public override void ReadData()//Читает данные с платы и пишет их в очередь
        {
            try
            {
                
                if (clientBAAK12TData.Connected && nsData != null)
                {
                   
                    int res = Read13007(out byte[] buf);//читаем с платы
                    if (res > 0)
                    {
                       
                        for (int i = 0; i < res; i++)
                        {
                            if (buf[i] == 0xFF)
                            {
                                CountFlagEnd++;
                            }
                            else
                            {
                                CountFlagEnd = 0;
                            }

                            DataBAAKList.Add(buf[i]);
                            if (CountFlagEndErroy == 4)
                            {
                                //MessageBox.Show(DataBAAKList.Count.ToString());

                                КолПакетовEr++;
                                DataBAAKList.Clear();

                                DataBAAKList = new List<byte>();
                                CountFlagEnd = 0;
                                CountFlagEndErroy = 0;
                              
                            }
                            if ((data_w != null) & (data_fs != null) & CountFlagEnd == 4)
                            {
                                //MessageBox.Show(DataBAAKList.Count.ToString());
                                OcherediNaZapic.Enqueue(new DataYu { ListData = DataBAAKList, tipDataTest = Flagtest });
                                КолПакетов++;
                                DataBAAKList = new List<byte>();
                                CountFlagEnd = 0;
                                
                                CountFlagEndErroy = 0;
                            }
                        }

                    }
                }
                else
                {
                    Brushes = System.Windows.Media.Brushes.Red;
                    CтатусБААК12 = "Ошибка 1 чтения с платы. Отключена";
                    InDe(false);
                }

            }
            catch (Exception ex)
            {
                Brushes = System.Windows.Media.Brushes.Red;
                CтатусБААК12 = "Ошибка 2 чтения с платы. Отключена" + ex.ToString();
                InDe(false);
                MessageBox.Show("Не Работает", "Ошибка");
            }

        }
    



    
        public override void SettingAll()
        {
            if (signalPozitif)
            {
                BlocAndPolarnost(9252);
            }
            else
           {
                BlocAndPolarnost(9220);
            }
            TriggerStop();
            WreadReg3000(0x200004 + 8, 0xfff);//маска каналов  
            WreadReg3000(0x200202, 0xfa);
            WreadReg3000(0x200204, 0xfa);
            ADCSetUp();
            AllSetPorogAll(PorogAll);
            OffSetData();
            DataLengt();
            WreadReg3000(0x200004 + 8, 0xfff);
            for (uint j = 0; j < 12; j++)
            {

                WreadReg3000(BaseA_M + AM_FThrBase + j * 2, 2400); // Filter Threshold
            }






            if (!trigOtBAAK)
            {
                Trigger(0x200006, TrgAll);
            }
            else
            {
                Trigger(0x200006, 256);
            }
            Winduws(0x20000a, 10);
          //  WreadReg3000(0x200208, ДискретностьХвост);//дискретность хвоста
            AllStopDelay(650);
            for (uint j = 0; j < 12; j++)
            {
                WreadReg3000(0x200050 + j * 2, 0xfff); // матрица совпадений
            }
            WreadReg3000(0x200020, 0x1);
        }






        public void newPorog(int porog)
        {
            TriggerStop();
           
            AllSetPorogAll((uint)porog);
          
            TriggerStart();

        }

        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        /// <param name="porog">порог срабатывания</param>
        /// <param name="trig">триггер</param>
        /// <param name="trigProg">если =true, то по количеству</param>
        public override void TestRanПодготовка(int porog, int trig, Boolean trigProg)
        {
            try
            {


                CтатусБААК12 = "Подготовка к тестовому набору";
                Thread.Sleep(500);

                TriggerStopОго();
                CтатусБААК12 = "Вычитываем данные";
                Thread.Sleep(500);
                // ВычитываемДанныеНужные();
                ВычитываемДанныеНенужные();

                //TriggerStop();
                CтатусБААК12 = "вычитываем очередь";

                Thread.Sleep(500);
                int koloch = 0;
                while (OcherediNaZapic.Count != 0 | koloch < 50)
                {
                    koloch++;
                    //Thread.Sleep(500);
                    CтатусБААК12 = "вычитываем очередь" + " =" + OcherediNaZapic.Count;
                }
                CтатусБААК12 = "Закрытие файла";
                Thread.Sleep(1000);
                CloseFile();
                Flagtest = true;
                CтатусБААК12 = "Открытие тестового файла";
                Thread.Sleep(500);
                //if (Conect300Statys)
                // {
                string tipPl = "V";

                try
                {
                    string path = NameFileWay;
                    string subpath = @"TestV";
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    dirInfo = new DirectoryInfo(path + @"\" + subpath);

                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    String sd = Time();
                    NameFile = NameFileWay + @"\" + subpath + @"\" + NamKl + "_" + "Test" + "_" + sd + "_" + tipPl + ".bin";
                    data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                    data_w = new BinaryWriter(data_fs);
                    BDReadFile(NamKl + "_" + "Test" + "_" + sd, NameBAAK12, sd, BAAK12T.NameRan);
                    NameFileClose = NamKl + "_" + "Test" + "_" + sd + "_" + tipPl;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка открытия файла" + ex.ToString(), "Ошибка");
                }
                // }
                КолПакетов = 0;
                КолПакетовEr = 0;
                КолПакетовОчер = 0;
                КолПакетовОчер2 = 0;
                КолПакетовN = 0;
                if (!trigProg)//по длительности
                {
                    try
                    {


                        CтатусБААК12 = "Тестовый набор по длительности";
                        Thread.Sleep(500);
                        AllSetPorogAll(Convert.ToUInt32(porog));
                        Trigger(0x200006, Convert.ToUInt32(trig));
                        //TriggerStart();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    CтатусБААК12 = "Тестовый набор по количеству";
                    TriggerProgramSetap();
                    Thread.Sleep(500);
                    TriggerStart();
                    //TriggerProgramSetap();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("TestRanПодготовка");
            }
        }
        /// <summary>
        /// Завершение тестовго набора и возрат настроек обычного набора
        /// </summary>

     
        /// <summary>
        /// Очередь с данными для записи
        /// </summary>
        ConcurrentQueue<DataYu> OcherediNaZapic = new ConcurrentQueue<DataYu>();
        ConcurrentQueue<ClassZapicBD100> OcherediNaZapicBD = new ConcurrentQueue<ClassZapicBD100>();
        private string namKl;
        private long колПакетов = 0;
        private long колПакетовОчер = 0;
        private long колПакетовОчер2 = 0;
        private long колПакетовEr = 0;

        private static uint _PorogAll = 10;
        private static uint time0x10 = 0;
        private static uint time0x12 = 0;
        private static uint time0x14 = 0;
        private static uint time0x16 = 0;


        private bool inciliz = false;
   


        private static uint _TrgAll = 1;
        private static string nameRan = "0";
        private string NameFileClose = "9";
   
  
        FileStream data_fs;
        BinaryWriter data_w;
        private const uint BaseA_M = 0x200000;
        private const uint AM_FThrBase = 0x80;

        private static string _nameFileWay = @"D:\";



        public override  void BDReadFile(string nameFile, string nameBAAK, string timeFile, string nameRan)
        {
            if (false)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;

                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();
                    // MessageBox.Show("Подключение открыто");
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Dispose();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "BDReadFile");
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();


                }
            }
        }
    

        public void BDReadСобытие(string nameFile, string nameBAAK, string time, string nameRan, int Amp, string nameklaster, int Nl, Double sig, int nD, int TimeFirst, int TimeAmp)
        {
            if (FlagSaveBD)
            {
                string connectionString;
            
               
                
                    connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                


                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();
                    // MessageBox.Show("Подключение открыто");
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Событие](" + "Время, НомерRan, Кластер, Плата, МетодикаYuKO, Методика2, Методика3, nD, Amp, firstTime, firstTime, sig, Null) VALUES (" +
                                        "'"+time + "'" + "," + "'" + NameRan + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + true + "'" + ")"
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        // ToDo
                        /*  CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                          "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                          "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                          + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                          + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                          + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                          + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                          + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                          + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                          */
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();
                 
                        new OleDbCommand
                    {
                        Connection = podg,
                        // ToDo
                           /* CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                        "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                        "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                        + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                        + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                        + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                        + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                        + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                        + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                        */
                                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Dispose();

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message+ "BDReadСобытие");
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();
                }
            }
        }
       public override void BDReadCloseFile(string nameFile, string time)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;

                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();
                    // MessageBox.Show("Подключение открыто");
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Dispose();



                }
                catch
                {

                }
                finally
                {

                    podg.Close();

                }
            }
        }
        public override void BDReadTemP(string nameBAAK, int temp)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;

                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();
                    // MessageBox.Show("Подключение открыто");
                    DateTime taimer2 = DateTime.UtcNow;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Темп](" + "Кластер№, Плата, час, минута, год, месяц, день, темп ) VALUES (" + "'" + NamKl + "'" + "," + "'" + nameBAAK + "'" + "," + "'" + taimer2.Hour + "'" + ", " + "'" + taimer2.Minute + "'" + ", " + "'" + taimer2.Year + "'" + "," + "'" + taimer2.Month + "'" + "," + "'" + taimer2.Day + "'" + "," + "'" + temp + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Темп](" + "Кластер№, Плата, час, минута, год, месяц, день, темп ) VALUES (" + "'" + NamKl + "'" + "," + "'" + nameBAAK + "'" + "," + "'" + taimer2.Hour + "'" + ", " + "'" + taimer2.Minute + "'" + ", " + "'" + taimer2.Year + "'" + "," + "'" + taimer2.Month + "'" + "," + "'" + taimer2.Day + "'" + "," + "'" + temp + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("BDReadTemP"+ex.Message);
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();

                }
            }
        }

    }
}
