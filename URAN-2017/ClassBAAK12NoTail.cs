using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using URAN_2017.WorkBD;

namespace URAN_2017
{
   public class ClassBAAK12NoTail: BAAK12T, IDisposable
    {
        public new void Dispose()
        {
            throw new NotImplementedException();
        }
    
        /// <summary>
        /// Вычитывает ненужные файлы
        /// </summary>
        private void ВычитываемДанныеНенужные()
        {
            bool endd = false;
            int x = 0;
            while (!endd)
            {
                //MessageBox.Show("ghghdddd");
                byte[] buf = new byte[2048];
                int res = Read13007(out buf);//читаем с платы
                CтатусБААК12 = res.ToString();
                if (res > 0)
                {
                    CтатусБААК12 = res.ToString();
                    x = 0;
                }
                else
                {
                    x++;
                    CтатусБААК12 = x.ToString();
                }
                if (x < 2)
                {
                    endd = true;
                }

            }

        }
        /// <summary>
        /// Вычитывает нужные файлы
        /// </summary>
        private void ВычитываемДанныеНужные()
        {
            bool endd = false;
            int x = 0;
            while (!endd)
            {

                lock (OcherediNaZapic)
                {


                    if (OcherediNaZapic.Count() == 0)
                    {
                        x++;
                    }
                    else
                    {
                        x = 0;
                    }
                    if (x > 50)
                    {
                        endd = true;
                    }
                }
             

            }
               
        }
        private void СохраняемДанныеНужные()
        {
            while (true)
            {
                try
                {
                    DataYu clasdata = new DataYu();
                    OcherediNaZapic.TryDequeue(out clasdata);
                    if (clasdata !=null && clasdata.ListData != null)
                    {
                        int f = clasdata.ListData.Count;
                        byte[] d = new byte[f];
                        int x = 0;
                        foreach (Byte b in clasdata.ListData)
                        {
                            d[x] = b;
                            x++;
                        }
                        data_w.Write(d);
                        clasdata.ListData.Clear();
                        Array.Clear(d, 0, f);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                catch (NullReferenceException)
                {
                    break;
                }
            }


        }
        public override void НастройкаБезКлок()
        {
            try
            {
                CтатусБААК12 = "Запись настроик без клок";
                SettingNoCloc();
                CтатусБААК12 = "Запись общих настроик";
                SettingAll();
                CтатусБААК12 = "Создаем файл";
                CreatFileData();
                CтатусБААК12 = "Разрешаем передачу данных";
                StartdataReg();//Разрешает передачу данных
                CтатусБААК12 = "Вычитываем ненужные файлы";
                ВычитываемДанныеНенужные();
                CтатусБААК12 = "Работает";
            }
            catch (Exception)
            {
                InDe(false);
                MessageBox.Show("Ошибка при старте");
            }


        }

    
        public override void ПускСтартТайм()
        {
            if (Синхронизация)
            {
                // if (Conect300Statys)
                //{
                СтартТаймКлок();
                // }
                // else
                // {
                //    CтатусБААК12 = "НЕТ подключения";
                //  }
            }
            else
            {
                // if (Conect300Statys)
                // {
                СтартТаймНоКлок();
                //}
                // else
                //{
                CтатусБААК12 = "НЕТ подключения";
                //}
            }
        }
 
  
        public override void Stop()
        {
            Thread.Sleep(50);
            CтатусБААК12 = "Триггер СТОП";
            TriggerStop();
            Thread.Sleep(50);
            CтатусБААК12 = "Вычитываем данные";
            ВычитываемДанныеНужные();
            Thread.Sleep(50);
            WreadReg3000(0x200020, 0x0);
            Thread.Sleep(50);
            StopdataReg();
            Thread.Sleep(50);
            CтатусБААК12 = "Сохраняем";
            СохраняемДанныеНужные();
            Thread.Sleep(50);
            CтатусБААК12 = "Закрытие файла";
            CloseFile();
            Thread.Sleep(50);
            CтатусБААК12 = "Отключена";



        }
        public override void TriggerStart()//Разрешение выроботки триггерного сигнала
        {
            if (clientBAAK12T.Connected && ns != null)
            {
                BlocAndPolarnost(8224);
                WreadReg3000(0x200200, 1);
            }
            else
            {
                CтатусБААК12 = "НЕТ подключения";
                InDe(false);
            }
        }
        public override void TriggerStop()//Запрет выроботки триггерного сигнала
        {
            if (clientBAAK12T.Connected && ns != null)
            {
                BlocAndPolarnost(9252);
                Thread.Sleep(10);
                WreadReg3000(0x200200, 0);
            }
            else
            {
                CтатусБААК12 = "НЕТ подключения";
                InDe(false);
            }

        }
        /// <summary>
        /// запускаем передачу данных
        /// </summary>
        public override void StartdataReg()//запускаем передачу данных
        {
            // WreadReg3000(0x10, 0x0);
            //WRbuffer = new Byte[14];//Создаем буффер для записи
            if (clientBAAK12T.Connected && ns != null)
            {
                Rbuffer = new Byte[14];//Создаем буффер для чтения
                Write1(Preob3(WRbuffer, 0x10, 0x0), 0, 14);//Формируем и Отправляем созданный пакет
            }
            else
            {
                CтатусБААК12 = "НЕТ подключения";
                InDe(false);
            }


        }
        /// <summary>
        /// останавливаем передачу данных
        /// </summary>
        public override void StopdataReg()//останавливаем передачу данных
        {
            if (clientBAAK12T.Connected && ns != null)
            {
                Rbuffer = new Byte[14];//Создаем буффер для чтения
                Write1(Preob3(WRbuffer, 0x12, 0x0), 0, 14);//Формируем и Отправляем созданный пакет
            }
            else
            {
                CтатусБААК12 = "НЕТ подключения";
                InDe(false);
            }

        }

        /// <summary>
        /// Расчет темпа и запись результата в БД
        /// </summary>
        public override void TempPacetov()
        {
            //if (Conect300Statys)
            // {
            ТемпПакетов = Convert.ToInt32(КолПакетов) - Пакетов;
            Пакетов = Convert.ToInt32(КолПакетов);
           // BDReadTemP(NameBAAK12, ТемпПакетов);
           // MyGrafic.AddPoint(Nkl, ТемпПакетов);


            // }
        }
        /// <summary>
        /// Создает новый файл
        /// </summary>
        public override void CreatFileData()
        {//if (Conect300Statys)
         // {
            try
            {
                string path = NameFileWay;
                
                string subpath = @"7d";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo = new DirectoryInfo(path +@"\"+ subpath);

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
             
          
                String sd = Time();
                NameFile = NameFileWay +@"\"+ subpath+ @"\" + NamKl + "_" + sd + "_" +"N" + ".bin";
                data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                data_w = new BinaryWriter(data_fs);
                BDReadFile(NamKl + "_" + sd+"_"+"N", NameBAAK12, sd, BAAK12T.NameRan);
                NameFileClose = NamKl + "_" + sd + "_" + "N";
            }
            catch (Exception ex)
            {
                InDe(false);
                MessageBox.Show(ex.ToString()+"\n" + NameFile);
                CтатусБААК12 = "Ошибка при создании файла";
            }
            // }
        }
        /// <summary>
        /// закрытие файла
        /// </summary>
        public override void CloseFile()//закрытие файла
        {
            // if(Conect300Statys)
            //{
            if (data_w != null)
            {
                try
                {
                    data_w.Close();
                    data_w = null;
                    BDReadCloseFile(NameFileClose, Time());
                }
                catch (Exception)
                {
                    InDe(false);
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
            ClassZapicBD BDData = new ClassZapicBD();
            try
            {

                OcherediNaZapicBD.TryDequeue(out BDData);
                if (BDData != null)
                {
                    // OcherediNaZapicBD.Enqueue(new ClassZapicBD() { tipDataSob = true, nameFileBD = NameFileClose, nameBAAKBD = NameBAAK12, timeBD = time1, nameRanBD = BAAK12T.NameRan, AmpBD = Ampl, nameklasterBD = NamKl, NnutBD = coutN, NlBD = NL, sigBDnew = sigm });
                    if (BDData.tipDataSob)
                    {
                        BDReadСобытие(BDData.nameFileBD, BDData.nameBAAKBD, BDData.timeBD, BDData.nameRanBD, BDData.AmpBD, BDData.nameklasterBD, BDData.NnutBD, BDData.NlBD, BDData.sigBDnew, BDData.tipDataTest);//пишем в бд
                        КолПакетовОчер2++;
                    }
                    else
                    {
                        BDReadNeutron(BDData.nameFileBD, BDData.DBD, BDData.AmpSobBD, BDData.TimeFirstBD, BDData.TimeEndBD, BDData.timeBD, BDData.TimeAmpBD, BDData.TimeFirst3BD, BDData.TimeEnd3BD, BDData.tipDataTest);
                    }


                }


            }
            catch (InvalidOperationException)
            {

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
               // dataYu = new DataYu();
                OcherediNaZapic.TryDequeue(out DataYu dataYu);
                if (dataYu !=null && dataYu.ListData != null)
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
                  //  int[] coutN = new int[12];

                   // Double[] sigm = new double[12];
                  //  if (BAAKTAIL)
                  //  {
                    //    Obrabotka(dataYu.ListData, out int[] Ampl, out string time1, out coutN, out int[] NL, out sigm, dataYu.tipDataTest);//парсинг данных
                    //    OcherediNaZapicBD.Enqueue(new ClassZapicBD() { tipDataTest = dataYu.tipDataTest, tipDataSob = true, nameFileBD = NameFileClose, nameBAAKBD = NameBAAK12, timeBD = time1, nameRanBD = BAAK12T.NameRan, AmpBD = Ampl, nameklasterBD = NamKl, NnutBD = coutN, NlBD = NL, sigBDnew = sigm });
                   // }
                    КолПакетовОчер++;
                    DataBAAKList1 = null;
                    d = null;
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
        private void Obrabotka(List<Byte> buf00, out int[] Amp, out string time, out int[] nn1, out int[] Nul, out double[] sig, bool testT)
        {
            int[,] data = new int[12, 1024];
            int[,] dataTail = new int[12, 20000];
            sig = new Double[12];
            Nul = new int[12];
            time = "0";
            nn1 = new int[12];
            Amp = new int[12];
            try
            {
                // byte[] bb = new byte[buf00.Count];
                //bb = buf00.ToArray();
                ParserBAAK12.ParseBinFileBAAK12.ParseBinFileBAAK200H(buf00.ToArray(), out data, out time, out dataTail);
                //  Amp = new int[12];
                // Nul = new int[12];
                //   sig = new Double[12];
                MaxAmpAndNul(data, out Amp, out Nul, out sig);
                // MessageBox.Show(Nul.ToString()+" "+ dataTail[3, 100]+" " + dataTail[3, 101] + " " + dataTail[3, 102] + " " + dataTail[3, 103] + " " + dataTail[3, 104] + " " + dataTail[3, 105] + " " + dataTail[3, 106] + " ");
                // nn1 = new int[12];
               
            }
            catch (Exception)
            {

            }

        }



        /// <summary>
        ///Определение амплитуды сигнала от заряженной компаненты
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="Amp1"></param>
        private void MaxAmpAndNul(int[,] data1, out int[] Amp1, out int[] maxNul, out Double[] sigma)
        {
            Amp1 = new int[12];
            sigma = new Double[12];
            maxNul = new int[12];//сумма всех точек
            for (int z = 0; z < 12; z++)
            {
                int Nu = Convert.ToInt32(masnul[z]);
                int max = 0;

                for (int a = 0; a < 1024; a++)
                {
                    if (max < data1[z, a])
                    {
                        max = data1[z, a];
                    }
                }
                Amp1[z] = max - Nu;
            }


            int Nsob = 150;//число точек от начала для поиска нулевой линнии
            for (int i = 0; i < 12; i++)
            {
                int[] sumNul = new int[Nsob];// точки нулевой линии для "a" - го канала
                for (int a = 0; a < Nsob; a++)
                {

                    maxNul[i] = (maxNul[i] + data1[i, a]);
                    sumNul[a] = data1[i, a];// точки нулевой линии для "a"-го канала

                }
                maxNul[i] = maxNul[i] / Nsob;
                sigma[i] = Math.Sqrt(Sum(sumNul, maxNul[i]) / Nsob);
            }
        }
        private Double Sum(int[] n, int x)
        {
            Double res = 0;
            foreach (int i in n)
            {
                res = res + Math.Pow((i - x), 2);
            }
            return res;
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

                            if (buf[i] == 0xFE)
                            {
                                CountFlagEndErroy++;
                            }
                            else
                            {
                                CountFlagEndErroy = 0;
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
                    CтатусБААК12 = "Ошибка 1 чтения с платы. Отключена";
                    InDe(false);
                }

            }
            catch (Exception ex)
            {
                CтатусБААК12 = "Ошибка 2 чтения с платы. Отключена"+ex.ToString();
                InDe(false);
            }

        }
   
 

   

  
        private void InitializeKlaster1()//Функция производит подписку на все необходимые действия для работы
        {
            try
            {
                BAAK12T.ConnnectURANDelegate += ConnectAll; //подписка на конект
                BAAK12T.НастройкаURANDelegate += Настройка; //подписка на запуск(загрузка регистров начало, создание файла и тд )
                BAAK12T.ПускURANDelegate += Пуск;//запускает тамер и разрешает триггер
                BAAK12T.ReadDataURANDelegate += ReadData;//подписка на чтение данных с платы
                BAAK12T.NewFileURANDelegate += NewFileData;//подписка на создание нового файла
                BAAK12T.StopURANDelegate += Stop;//подписка на остоновку набора кластера 1
                BAAK12T.DiscConnnectURANDelegate += DicsConectAll;
                BAAK12T.TempURANDelegate += TempPacetov;
                BAAK12T.ДеИнсталяцияDelegate += DeInitializeKlaster1;

                BAAK12T.TestRanSetUpDelegate += TestRanПодготовка;
                BAAK12T.TestRanStartDelegate += TriggerStartProgram;
                BAAK12T.TestRanTheEndDelegate += TestRanTheEnd;
                //_DataColecViev.Add(inz);//Добавляем кластер для отображения
                BAAK12T.ЗаписьВремяРегистрDelegate += FirsTime;
                BAAK12T.СтартЧасовDelegate += ПускСтартТайм;
                BAAK12T.ЗаписьвФайлDelegate += WriteInFileIzOcheredi;
                BAAK12T.СтопТриггерDelegate += TriggerStopОго;
                BAAK12T.ЗаписьвФайлБДDelegate += WriteInFileIzOcherediBD;


            }
            catch (Exception)
            {
                DeInitializeKlaster1();
                CтатусБААК12 = "Ошибка инициализации. Отключена";

            }
        }
        public void DeInitializeKlaster1()//Функция производит отписку от всех  действия для работы
        {
            try
            {
                BAAK12T.ConnnectURANDelegate -= ConnectAll;//подписка на конект
                BAAK12T.НастройкаURANDelegate -= Настройка;//подписка на запуск(загрузка регистров начало, создание файла и тд )
                BAAK12T.ReadDataURANDelegate -= ReadData;
                BAAK12T.NewFileURANDelegate -= NewFileData;
                BAAK12T.StopURANDelegate -= Stop;
                BAAK12T.DiscConnnectURANDelegate -= DicsConectAll;
         
                BAAK12T.ПускURANDelegate -= Пуск;
                BAAK12T.TempURANDelegate -= TempPacetov;
                BAAK12T.TestRanSetUpDelegate -= TestRanПодготовка;
                BAAK12T.TestRanStartDelegate -= TriggerStartProgram;
                BAAK12T.TestRanTheEndDelegate -= TestRanTheEnd;
                BAAK12T.ЗаписьВремяРегистрDelegate -= FirsTime;
                BAAK12T.СтартЧасовDelegate -= ПускСтартТайм;
                BAAK12T.ЗаписьвФайлDelegate -= WriteInFileIzOcheredi;
                BAAK12T.СтопТриггерDelegate -= TriggerStop;
                BAAK12T.ЗаписьвФайлБДDelegate -= WriteInFileIzOcherediBD;
                while (!OcherediNaZapicBD.IsEmpty)
                {
                    OcherediNaZapicBD.TryDequeue(out ClassZapicBD classZapicBD);
                }
                while (!OcherediNaZapic.IsEmpty)
                {
                    DataYu dataYu = new DataYu();
                    OcherediNaZapic.TryDequeue(out dataYu);
                }
                //OcherediNaZapic.
                //OcherediNaZapicBD
                if (clientBAAK12T.Connected)
                {
                    clientBAAK12T.Close();
                    ns.Close();
                    ns = null;

                }

                if (clientBAAK12TData.Connected)
                {
                    clientBAAK12TData.Close();
                    nsData.Close();
                    nsData = null;
                }
                CloseFile();
                BAAK12T.ДеИнсталяцияDelegate -= DeInitializeKlaster1;

            }
            catch
            {
                // MessageBox.Show("Ошибка");
            }
        }


        public override void SettingAll()
        {
            BlocAndPolarnost(9252);
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
     
            AllStopDelay(650);
            for (uint j = 0; j < 12; j++)
            {
                WreadReg3000(0x200050 + j * 2, 0xfff); // матрица совпадений
            }
            WreadReg3000(0x200020, 0x1);
        }
        public new bool trigOtBAAK = false;
  


        public new uint[] masnul = new uint[12];

        //   +++ Настройка АЦП+++
        public new void ADCSetUp()
        {
            WreadReg3000(0x200004 + 8, 0x3ff);// пишем во все АЦП одновременно
            WreadReg3000(0x200000 + 12 * 2, 0x10);// пишем во все АЦП одновременно
            WreadReg3000(0x200000 + 13 * 2, 0x0503); //данные для обоих каналов
            WreadReg3000(0x200000 + 13 * 2, 0xff01); //Update Register
            WreadReg3000(0x200000 + 13 * 2, 0x1404); //Offset Binary
            WreadReg3000(0x200000 + 13 * 2, 0x1792); // DCO Delay  -- (2,5-0,4-0,2) ns = 1,9 ns = (18+1)= 0x12 (+ invert)
            WreadReg3000(0x200000 + 13 * 2, 0x181d);// Full Scale
            WreadReg3000(0x200000 + 13 * 2, 0xff01);// Update Register
        }





        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        /// <param name="porog">порог срабатывания</param>
        /// <param name="trig">триггер</param>
        /// <param name="trigProg">если =true, то по количеству</param>
        public override void TestRanПодготовка(int porog, int trig, Boolean trigProg)
        {

            CтатусБААК12 = "Подготовка к тестовому набору";
            Thread.Sleep(500);

            TriggerStopОго();
            CтатусБААК12 = "Вычитываем данные";
            Thread.Sleep(500);
            ВычитываемДанныеНужные();
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
          
               
            
        
            try
            {
                string path = NameFileWay;
                string subpath = @"Test7d";
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
                NameFile = NameFileWay + @"\" + subpath + @"\" + NamKl + "_" + "Test" + "_" + sd + "_" + "N" + ".bin";
                data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                data_w = new BinaryWriter(data_fs);
                BDReadFile(NamKl + "_" + "Test" + "_" + sd, NameBAAK12, sd, BAAK12T.NameRan);
                NameFileClose = NamKl + "_" + "Test" + "_" + sd + "_" + "N";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла" + ex.ToString());
            }
            // }
            КолПакетов = 0;
            КолПакетовEr = 0;
            КолПакетовОчер = 0;
            КолПакетовОчер2 = 0;
            if (!trigProg)//по длительности
            {
                CтатусБААК12 = "Тестовый набор по длительности";
                Thread.Sleep(500);
                AllSetPorogAll(Convert.ToUInt32(porog));
            
                if (!trigOtBAAK)
                {
                    Trigger(0x200006, TrgAll);
                }
                else
                {
                    Trigger(0x200006, 256);
                }
                //TriggerStart();

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
        /// <summary>
        /// Завершение тестовго набора и возрат настроек обычного набора
        /// </summary>
        public override void TestRanTheEnd(Boolean trigProg)
        {
            TriggerStopОго();


            CтатусБААК12 = "Вычитываем данные";
            Thread.Sleep(500);
            ВычитываемДанныеНужные();
            CтатусБААК12 = "вычитываем очередь";
            Thread.Sleep(500);
            int koloch = 0;
            while (OcherediNaZapic.Count != 0 | koloch < 50)
            {
                koloch++;
              
                CтатусБААК12 = "вычитываем очередь" + " =" + OcherediNaZapic.Count;
            }
            Thread.Sleep(500);
            CтатусБААК12 = "Работает";
            //TriggerStop();
            NewFileData();
            if (!trigProg)
            {
              
                if (!trigOtBAAK)
                {
                    Trigger(0x200006, TrgAll);
                }
                else
                {
                    Trigger(0x200006, 256);
                }
                AllSetPorogAll(PorogAll);
            }
            else
            {
              
                if (!trigOtBAAK)
                {
                    Trigger(0x200006, TrgAll);
                }
                else
                {
                    Trigger(0x200006, 256);
                }
                //ToDo подготовить к запуску отвнешнего триггера
            }


            //TriggerStart();
            Thread.Sleep(500);
            КолПакетовОчер = 0;
            КолПакетовОчер2 = 0;
            КолПакетовEr = 0;
            КолПакетов = 0;
          
            Пакетов = 0;
            ТемпПакетов = 0;
            Flagtest = false;

        }
        public new List<byte> DataBAAKList = new List<byte>();
        public new List<byte> DataBAAKList1 = new List<byte>();
        /// <summary>
        /// Очередь с данными для записи
        /// </summary>
        ConcurrentQueue<DataYu> OcherediNaZapic = new ConcurrentQueue<DataYu>();
        ConcurrentQueue<ClassZapicBD> OcherediNaZapicBD = new ConcurrentQueue<ClassZapicBD>();
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
        public new int Nkl = 0;
        public new delegate Task ConnectDelegate();       // Тип делегата   
        /// <summary>
        /// конектимся к плате
        /// </summary>
        public new static ConnectDelegate ConnnectURANDelegate;

        public new delegate void DiscConnectDelegate();       // Тип делегата   
        /// <summary>
        /// отключается от платы
        /// </summary>
        public new static DiscConnectDelegate DiscConnnectURANDelegate;

        public new delegate void НастройкаUranDelegate();
        /// <summary>
        /// загрузка регистров начало, создание файла и тд
        /// </summary>
        public new static НастройкаUranDelegate НастройкаURANDelegate;

        public new delegate void StopUranDelegate();
        /// <summary>
        /// остоновку набора кластера
        /// </summary>
        public new static StopUranDelegate StopURANDelegate;

        public new delegate void RedDataDelegate();
        /// <summary>
        /// чтение данных с платы
        /// </summary>
        public new static RedDataDelegate ReadDataURANDelegate;

        public new delegate void NewFileDelegate();
        /// <summary>
        /// создание нового файла
        /// </summary>
        public new static NewFileDelegate NewFileURANDelegate;

        public new delegate void ПускDelegate();
        /// <summary>
        /// запускает тамер и разрешает триггер
        /// </summary>
        public new static ПускDelegate ПускURANDelegate;

        public new delegate void TempDelegate();
        /// <summary>
        /// Расчет темпа и запись результата в БД
        /// </summary>
        public new static TempDelegate TempURANDelegate;

        public new delegate void CountPac();
        public new static CountPac CountPacDelegate;

        public new delegate void ДеИнсталяция();
        /// <summary>
        /// убирает все подписки делегата
        /// </summary>
        public new static ДеИнсталяция ДеИнсталяцияDelegate;

        public new delegate void TestRanSetUp(int x, int e, Boolean t);//
        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        public new static TestRanSetUp TestRanSetUpDelegate;

        public new delegate void TestRanStart();
        /// <summary>
        /// программный сигнал триггер
        /// </summary>
        public new static TestRanStart TestRanStartDelegate;

        public new delegate void TestRanTheEndD(Boolean z);
        /// <summary>
        /// Завершение тестовго набора и возрат настроек обычного набора
        /// </summary>
        public new static TestRanTheEndD TestRanTheEndDelegate;

        public new delegate void ЗаписьВремяРегистр();
        public new static ЗаписьВремяРегистр ЗаписьВремяРегистрDelegate;


        public new delegate void СтартЧасов();
        public new static СтартЧасов СтартЧасовDelegate;

        public new delegate void ЗаписьвФайл();
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public new static ЗаписьвФайл ЗаписьвФайлDelegate;

        public new delegate void ЗаписьвФайлБД();
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public new static ЗаписьвФайл ЗаписьвФайлБДDelegate;

        public new delegate void СтопТриггер();
        /// <summary>
        /// Запрещает триггер
        /// </summary>
        public new static СтопТриггер СтопТриггерDelegate;


        private bool inciliz = false;
        public new bool Inciliz
        {
            get
            {
                return inciliz;
            }
            set
            {
                inciliz = value;
            
                InDe(value);
                this.OnPropertyChanged(nameof(Inciliz));
            }
        }
        /// <summary>
        /// значение общего порога срабатывания
        /// </summary>
        public new static uint PorogAll
        {
            get
            {
                return _PorogAll;
            }
            set
            {
                _PorogAll = Convert.ToUInt32(value);
            }
        }
        private static uint дискретностьХвост = 100;
        public new static uint ДискретностьХвост
        {
            get
            {
                return дискретностьХвост;
            }
            set
            {
                дискретностьХвост = Convert.ToUInt32(value);
            }
        }
        private static uint _TrgAll = 1;
        private static string nameRan = "0";
        private string NameFileClose = "9";
        /// <summary>
        /// Занчение общего триггера
        /// </summary>
        public new static uint TrgAll
        {
            get
            {
                return _TrgAll;
            }
            set
            {
                _TrgAll = value;
            }
        }
        
        FileStream data_fs;
        BinaryWriter data_w;
        private const uint BaseA_M = 0x200000;
        private const uint AM_FThrBase = 0x80;
        public new string NameFile = "";
        private static string _nameFileWay = @"D:\";
        public new static string NameFileWay
        {
            get
            {
                return _nameFileWay;
            }
            set
            {
                _nameFileWay = value;
            }

        }
       
    

        /// <summary>
        /// Количество принятых пакетов
        /// </summary>
        public new long КолПакетов
        {
            get
            {
                return колПакетов;
            }
            set
            {
                колПакетов = value;

                this.OnPropertyChanged(nameof(КолПакетов));
            }
        }
        public new long КолПакетовEr
        {
            get
            {
                return колПакетовEr;
            }
            set
            {
                колПакетовEr = value;

                this.OnPropertyChanged(nameof(КолПакетовEr));
            }
        }
        public new long КолПакетовОчер
        {
            get
            {
                return колПакетовОчер;
            }
            set
            {
                колПакетовОчер = value;

                this.OnPropertyChanged(nameof(КолПакетовОчер));
            }
        }
        public new long КолПакетовОчер2
        {
            get
            {
                return колПакетовОчер2;
            }
            set
            {
                колПакетовОчер2 = value;

                this.OnPropertyChanged(nameof(КолПакетовОчер2));
            }
        }

        private int темпПакетов = 0;
        /// <summary>
        /// Темп счета принятых пакетов
        /// </summary>
        public new int ТемпПакетов
        {
            get
            {
                return темпПакетов;
            }
            set
            {
                темпПакетов = value;
                this.OnPropertyChanged(nameof(ТемпПакетов));
            }

        }
        private int пакетов = 0;
        public new int Пакетов
        {
            get
            {
                return пакетов;
            }
            set
            {
                пакетов = value;
                this.OnPropertyChanged(nameof(Пакетов));
            }

        }
        private int интервалТемпаСчета = 0;
        public new int ИнтервалТемпаСчета
        {
            get
            {
                return интервалТемпаСчета;
            }
            set
            {
                интервалТемпаСчета = value;
                this.OnPropertyChanged(nameof(ИнтервалТемпаСчета));
            }

        }
        private static string _nameFileSetUp = @"D:\";
        public new static string NameFileSetUp
        {
            get
            {
                return _nameFileSetUp;
            }
            set
            {
                _nameFileSetUp = value;
            }
        }
        private static uint dataLenght = 1;
        public new static uint DataLenght
        {
            get
            {
                return dataLenght;
            }
            set
            {
                dataLenght = Convert.ToUInt32(value);
            }
        }
        private string nameBAAK12 = "У1";
        /// <summary>
        /// Имя платы БААК12
        /// </summary>
        public new string NameBAAK12
        {
            get
            {
                return nameBAAK12;
            }
            set
            {
                nameBAAK12 = value;
            }
        }
        /// <summary>
        /// Имя кластера
        /// </summary>
        public new string NamKl
        {
            get
            {
                return namKl;
            }
            set
            {
                namKl = value;
                this.OnPropertyChanged(nameof(NamKl));

            }

        }
        public new static uint Time0x10 { get => time0x10; set => time0x10 = value; }
        public new static uint Time0x12 { get => time0x12; set => time0x12 = value; }
        public new static uint Time0x14 { get => time0x14; set => time0x14 = value; }
        public new static uint Time0x16 { get => time0x16; set => time0x16 = value; }
        /// <summary>
        /// Имя рана
        /// </summary>
        public new static string NameRan { get => nameRan; set => nameRan = value; }

      public new void BDReadFile(string nameFile, string nameBAAK, string timeFile, string nameRan)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    DataAccesBDData.Path = wayDataBD;
                    DataAccesBDData.AddDataTablФайлы(nameFile, nameBAAK, timeFile, nameRan);
                }
                else
                {


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
        }
        private void BDReadNeutron(string nameFile, int D, int Amp, int TimeFirst, int TimeEnd, string time, int TimeAmp, int TimeFirst3, int TimeEnd3, bool test)
        {
            if (FlagSaveBD)
            {
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    if (!test)
                    {


                        DataAccesBDData.Path = wayDataBD;
                        int x = 0;
                       
                        DataAccesBDData.AddDataTablSobNeutron(nameFile, D, Amp, TimeFirst, TimeEnd, time, TimeAmp, TimeFirst3, TimeEnd3, x);
                    }
                }
                else
                {


                    string connectionString;
                    if (test)
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataTestBD;
                    }
                    else
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                    }

                    // Создание подключения
                    var podg = new OleDbConnection(connectionString);
                    try
                    {

                        // Открываем подключение
                        podg.Open();
                        // MessageBox.Show("Подключение открыто");
                        OleDbCommand oleDbCommand = new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        };
                        oleDbCommand.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Dispose();

                    }
                    catch
                    {

                    }
                    finally
                    {
                        // закрываем подключение
                        podg.Close();

                    }
                }
            }
        }


        private void BDReadСобытие(string nameFile, string nameBAAK, string time, string nameRan, int[] Amp, string nameklaster, int[] Nnut, double[] Nl, Double[] sig, bool test)
        {
            if (FlagSaveBD)
            {
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    if (!test)
                    {


                        DataAccesBDData.Path = wayDataBD;
                        int x = 0;
                      
                        int[] nll = new int[12];
                        for (int i = 0; i < 12; i++)
                        {
                            nll[i] = Convert.ToInt32(Nl[i]);
                        }
                        DataAccesBDData.AddDataTablSob(nameFile, nameBAAK, time, Amp, nameklaster, Nnut, nll, sig, x);
                    }
                }
                else
                {


                    string connectionString;
                    if (test)
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataTestBD;
                    }
                    else
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                    }


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
                            CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                            "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                            "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                            "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
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
        }
        private void BDReadCloseFile(string nameFile, string time)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                DataAccesBDData.Path = wayDataBD;
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    DataAccesBDData.updateTimeStopDataTablФайл(time, nameFile);
                }
                else
                {


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
        }
        public new void BDReadTemP(string nameBAAK, int temp)
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
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();

                }
            }
        }
        private void BDselect(out uint[] masNul)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + NameFileSetUp;
            masNul = new uint[12];
            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {

                // Открываем подключение
                podg.Open();

                var chit = new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "select * from [Нулевая линия] where ИмяПлаты ='" + NameBAAK12 + "'"
                }.ExecuteReader(CommandBehavior.CloseConnection);
                while (chit.Read() == true)
                {

                    for (int i = 2; i < chit.FieldCount; i++)
                    {
                        masNul[i - 2] = Convert.ToUInt32(chit.GetValue(i));


                    }
                }
                new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "select * from [Нулевая линия] where ИмяПлаты ='" + NameBAAK12 + "'"
                }.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("BDselect" + ex.Message);
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }

    }
}
