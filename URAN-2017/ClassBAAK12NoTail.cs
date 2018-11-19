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

namespace URAN_2017
{
   public class ClassBAAK12NoTail: ClassParentsBAAK, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void НастройкаКлок()
        {
            if (clientBAAK12T.Connected && ns != null)
            {
                //TODO настройка регистров с синхронизацией
                CтатусБААК12 = "Запись настроик с клок";
                SettingCloc();
                Thread.Sleep(50);
                CтатусБААК12 = "Запись общих настроик";
                SettingAll();
                Thread.Sleep(50);
                CтатусБААК12 = "Создаем файл";
                CreatFileData();
                Thread.Sleep(50);
                CтатусБААК12 = "Разрешаем передачу данных";
                StartdataReg();//Разрешает передачу данных
                Thread.Sleep(50);
                CтатусБААК12 = "Вычитываем ненужные файлы";
                ВычитываемДанныеНенужные();
                Thread.Sleep(50);
                CтатусБААК12 = "Работает";
            }
            else
            {
                CтатусБААК12 = "НЕТ подключения";
                InDe(false);
            }


        }
        public void Настройка()
        {
            if (Синхронизация)
            {
                //if (Conect300Statys)
                // {
                TriggerStop();
                НастройкаКлок();

                // }
                // else
                // {
                //    CтатусБААК12 = "НЕТ подключения";
                //}
            }
            else
            {
                // if (Conect300Statys)
                //{
                TriggerStop();
                //MessageBox.Show("Настройка без клок");
                НастройкаБезКлок();

                // }
                //  else
                //  {
                //    CтатусБААК12 = "НЕТ подключения";
                // }
            }
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
        private void СохраняемДанныеНужные()
        {
            while (true)
            {
                try
                {
                    DataYu clasdata = new DataYu();
                    OcherediNaZapic.TryDequeue(out clasdata);
                    if (clasdata.ListData != null)
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
        public void НастройкаБезКлок()
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
        public void Пуск()
        {
            if (Синхронизация)
            {
                // if (Conect300Statys)
                // {
                ПускКлок();
                //  }
                // else
                //{
                //    CтатусБААК12 = "НЕТ подключения";
                //}
            }
            else
            {
                // if (Conect300Statys)
                // {
                ПускБезКлок();
                // }
                // else
                // {
                //     CтатусБААК12 = "НЕТ подключения";
                // }
            }
        }
        public void ПускБезКлок()
        {
            StartTime(2);
            TriggerStart();
        }
        public void ПускСтартТайм()
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
        public void СтартТаймКлок()
        {
            StartTime(1);
        }
        public void СтартТаймНоКлок()
        {
            StartTime(2);
        }
        public void ПускКлок()
        {

            TriggerStart();
        }
        public void Stop()
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
        public void TriggerStart()//Разрешение выроботки триггерного сигнала
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
        public void TriggerStop()//Запрет выроботки триггерного сигнала
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
        public void StartdataReg()//запускаем передачу данных
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
        public void StopdataReg()//останавливаем передачу данных
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
        private string Time()
        {
            String s, shour, sMinute, sDay, sMonth, sSec;
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

            s = sDay + "." + sMonth + "." + tmp.Year.ToString() + " " + shour + "." + sMinute + "." + sSec;
            // s = sDay + "." + sMonth + "." + "" + shour + ":" + sMinute;
            return s;
        }
        /// <summary>
        /// Расчет темпа и запись результата в БД
        /// </summary>
        public void TempPacetov()
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
        public void CreatFileData()
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
                string tipPl;
                tipPl = "N";
                String sd = Time();
                NameFile = NameFileWay +@"\"+ subpath+ @"\" + NamKl + "_" + sd + "_" +"N" + ".bin";
                data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                data_w = new BinaryWriter(data_fs);
                BDReadFile(NamKl + "_" + sd+"_"+"N", NameBAAK12, sd, BAAK12T.NameRan);
                NameFileClose = NamKl + "_" + sd + "_" + tipPl;
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
        public void CloseFile()//закрытие файла
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
        public void WriteFileData(List<byte> DataBAAKList)//пишет в файл
        {



            try
            {
                foreach (Byte b in DataBAAKList)
                {
                    data_w.Write(b);
                }
                //data_w.Write(buf);

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка записи");
            }

        }

        /// <summary>
        /// записываем данных о событии из очереди в в бд
        /// </summary>
        public void WriteInFileIzOcherediBD()
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
        DataYu dataYu;
        public Boolean Flagtest = false;
        public Boolean BAAKTAIL = true;
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public void WriteInFileIzOcheredi()//работа с данными из очереди
        {
            try
            {
                dataYu = new DataYu();
                OcherediNaZapic.TryDequeue(out dataYu);
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
        public void ReadData()//Читает данные с платы и пишет их в очередь
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
        public void NewFileData()
        {

            // if (Conect300Statys)
            // {
            TriggerStop();
            CloseFile();
            CreatFileData();
            TriggerStart();
            //  }

        }
        /// <summary>
        /// программный сигнал триггер
        /// </summary>
        public void TriggerStartProgram()//программный сигнал триггер
        {
            WreadReg3000(0x200034, 1);
        }

        private void InDe(bool f)
        {
            if (f)
            {
                InitializeKlaster1();
            }
            else
            {
                DeInitializeKlaster1();
            }
        }

        private void TriggerStopОго()
        {
            Trigger(0x200006, 11);
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

        public void StartTime(uint t)
        {
            WreadReg3000(0x200002, t);
        }
        public void SettingNoCloc()
        {

            WreadReg3000(0x20001c, 0);

            WreadReg3000(0x200026, 0); // Generator Clock

            WreadReg3000(0x200024, 1);
            WreadReg3000(0x200024, 0);//Сброс регистров
            Thread.Sleep(500);
            WreadReg3000(0x20001c, 0);
            WreadReg3000(0x200026, 0); // Generator Clock

        }
        public void SettingCloc()
        {
            WreadReg3000(0x20001c, 1);
            WreadReg3000(0x200026, 1); // optc Clock
            WreadReg3000(0x200002, 1);// Запуск по клоку
            WreadReg3000(0x20001c, 1);//синхронизация таймера
            WreadReg3000(0x200024, 1); //Сброс регистров
            Thread.Sleep(600);
            WreadReg3000(0x200024, 0);// нормальная работа регистров
            WreadReg3000(0x20001c, 1);//синхронизация таймера
            WreadReg3000(0x200026, 1);

        }
        public void SettingAll()
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






            Trigger(0x200006, TrgAll);
            Winduws(0x20000a, 10);
            WreadReg3000(0x200208, ДискретностьХвост);//дискретность хвоста
            AllStopDelay(650);
            for (uint j = 0; j < 12; j++)
            {
                WreadReg3000(0x200050 + j * 2, 0xfff); // матрица совпадений
            }
            WreadReg3000(0x200020, 0x1);
        }

        public void FirsTime()//Время внутреннего таймера
        {

            WreadReg3000(0x200010, Time0x10);

            WreadReg3000(0x200012, Time0x12);

            WreadReg3000(0x200014, Time0x14);

            WreadReg3000(0x200016, Time0x16);

        }
        public void BlocAndPolarnost(uint x)//Полярность сигнала 8224 - запрос разрешен и положительная полярность, 9252 - запрос запрешен, и положительная полярность
        {
            WreadReg3000(0x90000, x);

            WreadReg3000(0x98000, x);

            WreadReg3000(0xb0000, x);

            WreadReg3000(0xb8000, x);

            WreadReg3000(0xd0000, x);

            WreadReg3000(0xd8000, x);

        }
        public void AllStopDelay(uint wind)//задержка остановки записи после прихода триггера
        {
            WreadReg3000(0x9000e, wind);
            WreadReg3000(0x9800e, wind);
            WreadReg3000(0xb000e, wind);
            WreadReg3000(0xb800e, wind);
            WreadReg3000(0xd000e, wind);
            WreadReg3000(0xb800e, wind);
        }
        public void StopDelay(uint Reg, uint wind)//задержка остановки записи после прихода триггера в отсчетах
        {
            WreadReg3000(Reg, wind);
        }
        public void Trigger(uint Reg, uint tr)//Кратность совпадений и подверждение
        {
            WreadReg3000(Reg, tr);
        }
        public void Winduws(uint Reg, uint wind)//окно совпадений дискретность 10 нс от 0 до 254
        {
            WreadReg3000(Reg, wind);
        }
        public void SetPorog(uint Reg, uint por)
        {
            WreadReg3000(Reg, por);
        }
        public uint[] masnul = new uint[12];
        public void AllSetPorogAll(uint PorogAll1)
        {
            BDselect(out masnul);
            SetPorog(0x90010, masnul[0] + PorogAll1);//низкий порог для канала 0(первый)
            SetPorog(0x90020, masnul[0] + 1 + PorogAll1);//высокий порог для канала 0(первый)

            SetPorog(0x90014, masnul[1] + PorogAll1);//низкий порог для канала 1
            SetPorog(0x90024, masnul[1] + 1 + PorogAll1);//высокий порог для канала 1

            SetPorog(0x98010, masnul[2] + PorogAll1);//низкий порог для канала 2
            SetPorog(0x98020, masnul[2] + 1 + PorogAll1);//высокий порог для канала 2

            SetPorog(0x98014, masnul[3] + PorogAll1);//низкий порог для канала 3
            SetPorog(0x98024, masnul[3] + 1 + PorogAll1);//высокий порог для канала 3

            SetPorog(0xb0010, masnul[4] + PorogAll1);//низкий порог для канала 4
            SetPorog(0xb0020, masnul[4] + 1 + PorogAll1);//высокий порог для канала 4

            SetPorog(0xb0014, masnul[5] + PorogAll1);//низкий порог для канала 5
            SetPorog(0xb0024, masnul[5] + 1 + PorogAll1);//высокий порог для канала 5

            SetPorog(0xb8010, masnul[6] + PorogAll1);//низкий порог для канала 6
            SetPorog(0xb8020, masnul[6] + 1 + PorogAll1);//высокий порог для канала 6

            SetPorog(0xb8014, masnul[7] + PorogAll1);//низкий порог для канала 7
            SetPorog(0xb8024, masnul[7] + 1 + PorogAll1);//высокий порог для канала 7

            SetPorog(0xd0010, masnul[8] + PorogAll1);//низкий порог для канала 8
            SetPorog(0xd0020, masnul[8] + 1 + PorogAll1);//высокий порог для канала 8

            SetPorog(0xd0014, masnul[9] + PorogAll1);//низкий порог для канала 9
            SetPorog(0xd0024, masnul[9] + 1 + PorogAll1);//высокий порог для канала 9

            SetPorog(0xd8010, masnul[10] + PorogAll1);//низкий порог для канала 10
            SetPorog(0xd8020, masnul[10] + 1 + PorogAll1);//высокий порог для канала 10

            SetPorog(0xd8014, masnul[11] + PorogAll1);//низкий порог для канала 11
            SetPorog(0xd8024, masnul[11] + 1 + PorogAll1);//высокий порог для канала 11

        }
        //   +++ Настройка АЦП+++
        public void ADCSetUp()
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
        public void OffSetData()
        {
            WreadReg3000(0x90040, 10300);//смешение данных канала 0
            WreadReg3000(0x90042, 10300);//смешение данных канала 1
            WreadReg3000(0x98040, 10300);//смешение данных канала 2
            WreadReg3000(0x98042, 10300);//смешение данных канала 3
            WreadReg3000(0xb0040, 10300);//смешение данных канала 4
            WreadReg3000(0xb0042, 10300);//смешение данных канала 5
            WreadReg3000(0xb8040, 10300);//смешение данных канала 6
            WreadReg3000(0xb8042, 10300);//смешение данных канала 7
            WreadReg3000(0xd0040, 10300);//смешение данных канала 8
            WreadReg3000(0xd0042, 10300);//смешение данных канала 9
            WreadReg3000(0xd8040, 10300);//смешение данных канала 10
            WreadReg3000(0xd8042, 10300);//смешение данных канала 11
        }
        public void DataLengt()
        {
            WreadReg3000(0x90044, 2048 * DataLenght);//длинна данных канала 0
            WreadReg3000(0x90046, 2048 * DataLenght);//длинна данных канала 1
            WreadReg3000(0x98044, 2048 * DataLenght);//длинна данных канала 2
            WreadReg3000(0x98046, 2048 * DataLenght);//длинна данных канала 3
            WreadReg3000(0xb0044, 2048 * DataLenght);//длинна данных канала 4
            WreadReg3000(0xb0046, 2048 * DataLenght);//длинна данных канала 5
            WreadReg3000(0xb8044, 2048 * DataLenght);//длинна данных канала 6
            WreadReg3000(0xb8046, 2048 * DataLenght);//длинна данных канала 7
            WreadReg3000(0xd0044, 2048 * DataLenght);//длинна данных канала 8
            WreadReg3000(0xd0046, 2048 * DataLenght);//длинна данных канала 9
            WreadReg3000(0xd8044, 2048 * DataLenght);//длинна данных канала 10
            WreadReg3000(0xd8046, 2048 * DataLenght);//длинна данных канала 11
        }
        public void TriggerProgramSetap()//внешний триггер
        {
            WreadReg3000(0x200006, 0x101);
        }


        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        /// <param name="porog">порог срабатывания</param>
        /// <param name="trig">триггер</param>
        /// <param name="trigProg">если =true, то по количеству</param>
        public void TestRanПодготовка(int porog, int trig, Boolean trigProg)
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
            string tipPl;
            if (!BAAKTAIL)
            {
                tipPl = "N";
            }
            else
            {
                tipPl = "T";
            }
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
                NameFile = NameFileWay + @"\" + subpath + @"\" + NamKl + "_" + "Test" + "_" + sd + "_" + tipPl + ".bin";
                data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                data_w = new BinaryWriter(data_fs);
                BDReadFile(NamKl + "_" + "Test" + "_" + sd, NameBAAK12, sd, BAAK12T.NameRan);
                NameFileClose = NamKl + "_" + "Test" + "_" + sd + "_" + tipPl;
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
                Trigger(0x200006, Convert.ToUInt32(trig));
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
        public void TestRanTheEnd(Boolean trigProg)
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
                //Thread.Sleep(500);
                CтатусБААК12 = "вычитываем очередь" + " =" + OcherediNaZapic.Count;
            }
            Thread.Sleep(500);
            CтатусБААК12 = "Работает";
            //TriggerStop();
            NewFileData();
            if (!trigProg)
            {
                Trigger(0x200006, TrgAll);
                AllSetPorogAll(PorogAll);
            }
            else
            {
                Trigger(0x200006, TrgAll);
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
        public List<byte> DataBAAKList = new List<byte>();
        public List<byte> DataBAAKList1 = new List<byte>();
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
        public int Nkl = 0;
        public delegate Task ConnectDelegate();       // Тип делегата   
        /// <summary>
        /// конектимся к плате
        /// </summary>
        public static ConnectDelegate ConnnectURANDelegate;

        public delegate void DiscConnectDelegate();       // Тип делегата   
        /// <summary>
        /// отключается от платы
        /// </summary>
        public static DiscConnectDelegate DiscConnnectURANDelegate;

        public delegate void НастройкаUranDelegate();
        /// <summary>
        /// загрузка регистров начало, создание файла и тд
        /// </summary>
        public static НастройкаUranDelegate НастройкаURANDelegate;

        public delegate void StopUranDelegate();
        /// <summary>
        /// остоновку набора кластера
        /// </summary>
        public static StopUranDelegate StopURANDelegate;

        public delegate void RedDataDelegate();
        /// <summary>
        /// чтение данных с платы
        /// </summary>
        public static RedDataDelegate ReadDataURANDelegate;

        public delegate void NewFileDelegate();
        /// <summary>
        /// создание нового файла
        /// </summary>
        public static NewFileDelegate NewFileURANDelegate;

        public delegate void ПускDelegate();
        /// <summary>
        /// запускает тамер и разрешает триггер
        /// </summary>
        public static ПускDelegate ПускURANDelegate;

        public delegate void TempDelegate();
        /// <summary>
        /// Расчет темпа и запись результата в БД
        /// </summary>
        public static TempDelegate TempURANDelegate;

        public delegate void CountPac();
        public static CountPac CountPacDelegate;

        public delegate void ДеИнсталяция();
        /// <summary>
        /// убирает все подписки делегата
        /// </summary>
        public static ДеИнсталяция ДеИнсталяцияDelegate;

        public delegate void TestRanSetUp(int x, int e, Boolean t);//
        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        public static TestRanSetUp TestRanSetUpDelegate;

        public delegate void TestRanStart();
        /// <summary>
        /// программный сигнал триггер
        /// </summary>
        public static TestRanStart TestRanStartDelegate;

        public delegate void TestRanTheEndD(Boolean z);
        /// <summary>
        /// Завершение тестовго набора и возрат настроек обычного набора
        /// </summary>
        public static TestRanTheEndD TestRanTheEndDelegate;

        public delegate void ЗаписьВремяРегистр();
        public static ЗаписьВремяРегистр ЗаписьВремяРегистрDelegate;


        public delegate void СтартЧасов();
        public static СтартЧасов СтартЧасовDelegate;

        public delegate void ЗаписьвФайл();
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public static ЗаписьвФайл ЗаписьвФайлDelegate;

        public delegate void ЗаписьвФайлБД();
        /// <summary>
        /// записываем данные из очереди в файл и в бд
        /// </summary>
        public static ЗаписьвФайл ЗаписьвФайлБДDelegate;

        public delegate void СтопТриггер();
        /// <summary>
        /// Запрещает триггер
        /// </summary>
        public static СтопТриггер СтопТриггерDelegate;


        private bool inciliz = false;
        public bool Inciliz
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
        public static uint PorogAll
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
        public static uint ДискретностьХвост
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
        public static uint TrgAll
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
        public static string wayDataBD;
        public static string wayDataTestBD;
        FileStream data_fs;
        BinaryWriter data_w;
        private const uint BaseA_M = 0x200000;
        private const uint AM_FThrBase = 0x80;
        public string NameFile = "";
        private static string _nameFileWay = @"D:\";
        public static string NameFileWay
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
        public long КолПакетов
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
        public long КолПакетовEr
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
        public long КолПакетовОчер
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
        public long КолПакетовОчер2
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
        public int ТемпПакетов
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
        public int Пакетов
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
        public int ИнтервалТемпаСчета
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
        public static string NameFileSetUp
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
        public static uint DataLenght
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
        public string NameBAAK12
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
        public string NamKl
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
        public static uint Time0x10 { get => time0x10; set => time0x10 = value; }
        public static uint Time0x12 { get => time0x12; set => time0x12 = value; }
        public static uint Time0x14 { get => time0x14; set => time0x14 = value; }
        public static uint Time0x16 { get => time0x16; set => time0x16 = value; }
        /// <summary>
        /// Имя рана
        /// </summary>
        public static string NameRan { get => nameRan; set => nameRan = value; }

        private void BDReadFile(string nameFile, string nameBAAK, string timeFile, string nameRan)
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
        private void BDReadNeutron(string nameFile, int D, int Amp, int TimeFirst, int TimeEnd, string time, int TimeAmp, int TimeFirst3, int TimeEnd3, bool test)
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


        private void BDReadСобытие(string nameFile, string nameBAAK, string time, string nameRan, int[] Amp, string nameklaster, int[] Nnut, int[] Nl, Double[] sig, bool test)
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
        private void BDReadCloseFile(string nameFile, string time)
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
        public void BDReadTemP(string nameBAAK, int temp)
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }

    }
}
