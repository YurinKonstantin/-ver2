using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace URAN_2017
{

    public partial class BAAK12T:IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        //Общее 
        public void НастройкаКлок()
        {
             if(clientBAAK12T.Connected && ns!=null)
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
            if(Синхронизация)
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
                if(x<50)
                {
                    endd = true;
                }

            }
            

        }
        private void СохраняемДанныеНужные()
        { if (UserSetting.FlagSaveBin)
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
            catch (Exception )
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
            try
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
            catch(Exception)
            {
                InDe(false);
            }
            
            

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

           s = sDay + "." + sMonth + "." + tmp.Year.ToString() + " " + shour + "." + sMinute+"."+sSec;
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
            try
            {
              BDReadTemP(NameBAAK12, ТемпПакетов);
            }
                catch
            {

            }
            try
            {
               // Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => { MyGrafic.AddPoint(Nkl, ТемпПакетов); }));
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, new Action(() => { MyGrafic.AddPoint(Nkl, ТемпПакетов); }));
               
            }
            catch
            {

            }
                
            
            
          
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
                string tipPl;
                tipPl = "T";

                    String sd = Time();
                    NameFile = NameFileWay + @"\" + NamKl + "_" + sd + "_"+tipPl  + ".bin";
                    data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                    data_w = new BinaryWriter(data_fs);
                    BDReadFile(NamKl + "_" + sd + "_" + tipPl, NameBAAK12, sd, BAAK12T.NameRan);
                    NameFileClose = NamKl + "_" + sd+ "_"+tipPl;
                }
                catch (Exception ex)
                {
                InDe(false);
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
                    data_w.Dispose();
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
                catch (Exception )
                {
                        CтатусБААК12 = "Ошибка при закрытии файла";
                    }
            }
            }
        }
        public void WriteFileData(List<byte> DataBAAKList)//пишет в файл
        {

            
                
                    try
                    {foreach(Byte b in DataBAAKList)
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
                    if(BDData.tipDataSob)
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
                    if (UserSetting.FlagSaveBin)
                    {
                        if (data_w != null)
                        {
                            data_w.Write(d);
                        }
                    }
                    if (UserSetting.FlagOtbor)
                    {
                        int[] coutN = new int[12];

                        Double[] sigm = new double[12];
                        if (BAAKTAIL)
                        {
                            Obrabotka(dataYu.ListData, out int[] Ampl, out string time1, out coutN, out int[] NL, out sigm, dataYu.tipDataTest);//парсинг данных
                            OcherediNaZapicBD.Enqueue(new ClassZapicBD() { tipDataTest = dataYu.tipDataTest, tipDataSob = true, nameFileBD = NameFileClose, nameBAAKBD = NameBAAK12, timeBD = time1, nameRanBD = BAAK12T.NameRan, AmpBD = Ampl, nameklasterBD = NamKl, NnutBD = coutN, NlBD = NL, sigBDnew = sigm });
                        }
                    }
                        КолПакетовОчер++;
                   DataBAAKList1=null;
                    d = null;
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch(NullReferenceException )
            {

            }
            catch(Exception e)
            {
                
                CтатусБААК12 = "Ошибка. Отключена "+e;
                InDe(false);
            }
        }
        private void Obrabotka(List<Byte> buf00, out int[] Amp, out string time, out int[] nn1, out int[] Nul, out double[] sig, bool testT)
        {
            int[,] data = new int[12, 1024];
            int[,] dataTail = new int[12, 20000];
            sig = new Double[12];
            Nul= new int[12];
            time = "0";
            nn1 = new int[12];
            Amp =new int[12];
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
                Neutron( dataTail, BAAK12T.PorogNutron, BAAK12T.DlNutron, out nn1, time, testT);
            }
            catch(Exception)
            {

            }
           
       }
        private void Neutron(int [,] n, int AmpOtbora, int dlitOtb, out int [] nn, string timeSob, Boolean test)
        {
            
            nn = new int[12];
            for (int i = 0; i < 12; i++)
            {
                int countnutron = 0;
               
                int Nu = Convert.ToInt32(masnul[i]);
              int  AmpOtbora1 = AmpOtbora + Nu;
                for (int j = 100; j < 20000; j++)
                {
                    int countmaxtime = 0;
                    int countfirsttime = 0;
                    int countendtime = 0;
                    int countfirsttime3 = 0;
                    int countendtime3 = 0;
                    int Amp= n[i,j];
                    if (Amp >= AmpOtbora1)//ищем претендента на нейтрон по порогу
                    {
                        //ищем граници претиндента нейтрона
                        countendtime = j;
                        int v = Amp;
                        while (v > Nu)
                        {
                            countendtime++;
                            v = n[i, countendtime];
                        }
                        v = Amp;
                        countfirsttime = j;
                        while (v > Nu)
                        {
                            countfirsttime--;
                            v = n[i, countfirsttime];
                        }
                      if (countendtime - countfirsttime >= dlitOtb)
                       { 
                         countendtime3 = j;
                        v = Amp;
                          while (v > Nu + 3)
                          {
                            countendtime3++;
                            v = n[i, countendtime3];
                          }

                        v = Amp;
                        countfirsttime3 = j;
                          while (v > Nu + 3)
                          {
                            countfirsttime3--;
                            v = n[i, countfirsttime3];
                          }
                          for (int v1 = countfirsttime; v1 <= countendtime; v1++)//точка максимум и значение максимум
                          {
                            if (Amp < n[i, v1])
                            {
                                Amp = n[i, v1];
                                countmaxtime = v1;
                            }
                          }
                         if (countendtime3 - countfirsttime3 >= dlitOtb)
                          {
                                 Amp = Amp - Nu;
                                OcherediNaZapicBD.Enqueue(new ClassZapicBD() {tipDataTest=test, tipDataSob = false,nameFileBD=NameFileClose, DBD=i, AmpSobBD=Amp, TimeFirstBD= countfirsttime, TimeEndBD= countendtime, timeBD= timeSob, TimeAmpBD= countmaxtime, TimeFirst3BD= countfirsttime3, TimeEnd3BD= countendtime3 });
                                countnutron++;
                          
                          }
                       }
                        j = countendtime + 1;
                    }
                    
                   
                }
                nn[i] = countnutron;
            }

        }


        /// <summary>
        ///Определение амплитуды сигнала от заряженной компаненты
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="Amp1"></param>
        private void MaxAmpAndNul(int [,] data1, out int[] Amp1, out int[] maxNul, out Double[] sigma)
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
                Amp1[z] = max- Nu;
            }

           
            int Nsob = 150;//число точек от начала для поиска нулевой линнии
            for(int i=0; i<12; i++)
            {
                int [] sumNul= new int[Nsob];// точки нулевой линии для "a" - го канала
                for (int a = 0; a < Nsob; a++)
                {

                    maxNul[i] = (maxNul[i] + data1[i, a]);
                    sumNul[a]=  data1[i, a];// точки нулевой линии для "a"-го канала

                }
                maxNul[i] = maxNul[i] / Nsob;
                sigma[i] = Math.Sqrt(Sum(sumNul, maxNul[i]) / Nsob);
            }
        }
        private Double Sum(int[] n, int x)
        {
            Double res = 0;
            foreach(int i in n)
            {
                res =res + Math.Pow((i - x), 2) ;
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
                            OcherediNaZapic.Enqueue(new DataYu {ListData= DataBAAKList, tipDataTest= Flagtest });
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
            catch (Exception)
            {
                CтатусБААК12 = "Ошибка 2 чтения с платы. Отключена";
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
            if(f)
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
                ConnnectURANDelegate += ConnectAll; //подписка на конект
                НастройкаURANDelegate += Настройка; //подписка на запуск(загрузка регистров начало, создание файла и тд )
                ПускURANDelegate += Пуск;//запускает тамер и разрешает триггер
                ReadDataURANDelegate += ReadData;//подписка на чтение данных с платы
                NewFileURANDelegate += NewFileData;//подписка на создание нового файла
                StopURANDelegate += Stop;//подписка на остоновку набора кластера 1
                DiscConnnectURANDelegate +=DicsConectAll;
                TempURANDelegate += TempPacetov;
                ДеИнсталяцияDelegate += DeInitializeKlaster1;

                TestRanSetUpDelegate += TestRanПодготовка;
                TestRanStartDelegate += TriggerStartProgram;
                TestRanTheEndDelegate += TestRanTheEnd;
                //_DataColecViev.Add(inz);//Добавляем кластер для отображения
                ЗаписьВремяРегистрDelegate += FirsTime;
                СтартЧасовDelegate += ПускСтартТайм;
                ЗаписьвФайлDelegate += WriteInFileIzOcheredi;
                СтопТриггерDelegate += TriggerStopОго;
                ЗаписьвФайлБДDelegate += WriteInFileIzOcherediBD;


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
                ConnnectURANDelegate -= ConnectAll;//подписка на конект
                НастройкаURANDelegate -= Настройка;//подписка на запуск(загрузка регистров начало, создание файла и тд )
                ReadDataURANDelegate -= ReadData;
                NewFileURANDelegate -= NewFileData;
                StopURANDelegate -= Stop;
                DiscConnnectURANDelegate -= DicsConectAll;
              
                ПускURANDelegate -= Пуск;
                TempURANDelegate -= TempPacetov;
                TestRanSetUpDelegate -= TestRanПодготовка;
                TestRanStartDelegate -= TriggerStartProgram;
                TestRanTheEndDelegate -= TestRanTheEnd;
                ЗаписьВремяРегистрDelegate -= FirsTime;
                СтартЧасовDelegate -= ПускСтартТайм;
                ЗаписьвФайлDelegate -= WriteInFileIzOcheredi;
                СтопТриггерDelegate -= TriggerStop;
                ЗаписьвФайлБДDelegate -= WriteInFileIzOcherediBD;
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
                ДеИнсталяцияDelegate -= DeInitializeKlaster1;

            }
            catch(Exception)
            {
               // MessageBox.Show("Ошибка");
            }
        }
        
    }

}
