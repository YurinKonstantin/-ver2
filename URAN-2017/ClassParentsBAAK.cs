using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
   public class ClassParentsBAAK: INotifyPropertyChanged, IDisposable
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string Port = "3000";
        private string PortData = "3007";
        private string host;
       public Byte[] WRbuffer = new Byte[14];
        //Буффер для записи
      public  Byte[] Rbuffer;//Буффер для Чтения
     // public List<byte> DataBAAKList = new List<byte>();
        private static bool синхронизация = false;
        public static bool Синхронизация
        {
            get
            {
                return синхронизация;
            }
            set
            {
                синхронизация = (value);
               


            }
        }
     
        public NetworkStream ns;
       public NetworkStream nsData;
        private string статусБААК12 = "Ошибка";
        public string CтатусБААК12
        {
            get
            {
                return статусБААК12;
            }
            set
            {
                статусБААК12 = value;
                this.OnPropertyChanged(nameof(CтатусБААК12));
            }

        }
        public TcpClient clientBAAK12T;
       public TcpClient clientBAAK12TData;
        public bool Conect300Statys = true;
        public bool Conect307Statys = true;
        public string Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
                this.OnPropertyChanged(nameof(Host));
            }
        }
        public async Task Conect3000()
        {
            try
            {
               clientBAAK12T = new TcpClient();
               await clientBAAK12T.ConnectAsync(Host, Int32.Parse(Port));
               ns = clientBAAK12T.GetStream();
            }
            catch(Exception ex)
            {
                CтатусБААК12 = "Произошла ОШИБКА подключения порт 3000";
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { CтатусБААК12 = "Произошла ОШИБКА подключения порт 3000"; }));
                Conect300Statys = false;
            }
        }
        public async Task Conect3007()
        {
            try
            {
                clientBAAK12TData = new TcpClient();
               await clientBAAK12TData.ConnectAsync(Host, Int32.Parse(PortData));
                nsData = clientBAAK12TData.GetStream();

            }
            catch
            {
               // CтатусБААК12 = "Произошла ОШИБКА подключения порт 3007";
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { CтатусБААК12 = "Произошла ОШИБКА подключения порт 3007"; }));
                Conect307Statys = false;
            }

        }
        public async Task ConnectAll()
        {
            try
            {
               await Conect3000();
               await Conect3007();
                if (clientBAAK12T.Connected & clientBAAK12TData.Connected)
                {
                    
                    CтатусБААК12 = "Подключена, ожидает СТАРТ";
                     Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { CтатусБААК12 = "Подключена, ожидает СТАРТ"; }));
                    Conect300Statys = true;

                }
                else
                {
                    CтатусБААК12 = "Произошла ОШИБКА Connect";
                    Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { CтатусБААК12 = "Произошла ОШИБКА"; }));
                    Conect300Statys = false;
                }

            }
            catch (Exception )
            {
                CтатусБААК12 = "Произошла ОШИБКА подключения";
                Conect300Statys = false;
            }

        }
        public void DicsConect3000()
        {
            try
            {
                if (clientBAAK12T.Connected)
                {
                    clientBAAK12T.Close();
                    ns.Close();
                    Conect300Statys = false;
                    CтатусБААК12 = "Отключена";

                }

            }
            catch
            {
                MessageBox.Show("Ошибка");
                CтатусБААК12 = "Произошла ОШИБКА";
            }
        }
        public void DicsConect3007()
        {
            try
            {
                if (clientBAAK12TData.Connected)
                {
                    clientBAAK12TData.Close();
                    nsData.Close();
                    Conect307Statys = false;
                }

            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        public void DicsConectAll()
        {
            DicsConect3000();
            DicsConect3007();
            Conect300Statys = false;

        }
        public void WreadReg3000(uint AdRedF, uint DatRegF)//Пишем данные на плату
        {
           
            try
            {

                if (clientBAAK12T.Connected && ns!=null)
                {
                    Rbuffer = new Byte[14];//Создаем буффер для чтения
                    Write1(Preob1(WRbuffer, AdRedF, DatRegF), 0, 14);//Формируем и Отправляем созданный пакет
                    int r = Read13000(Rbuffer, 0, 10);//читаем ответ
                    if (r < 0)
                    {
                        Conect300Statys = false;
                        статусБААК12 = "Ошибка Read13000";
                    }
                    int i = Проверка(Rbuffer, 10);
                    if (i != 1)
                    {
                        //MessageBox.Show("Ошибка при записи в регистр"+i.ToString());
                        Conect300Statys = false;
                    }
                    else
                    {

                    }
                }
                else
                {
                    статусБААК12 = "Ошибка";
                }
            }
            catch(Exception)
            {
                статусБААК12 = "Ошибка";


            }
           
        }
        //TODO разобраться с проверкой ответа на запись в регистр
      /*  public int Проверка(byte[] WbufferF, int x)
        {
            if (WbufferF[9] == 0 && WbufferF[8] == 0 && WbufferF[7] == 0 && WbufferF[6] == 0 && WbufferF[5] == x)
            {

                return 1;
            }
            else
            {

                return 0;
            }
        }
        */
        public int Проверка(byte[] WbufferF, int x)
        {
            if (WbufferF[9] == 0 && WbufferF[8] == 0 )
            {

                return 1;
            }
            else
            {

                return 0;
            }
        }
        public void ReadReg3000(uint AdRedF)//читаем данные с плат
        {
            if (clientBAAK12T.Connected && ns != null)
            {

                Rbuffer = new Byte[14];//Создаем буффер для чтения
                Write1(Preob2(WRbuffer, AdRedF), 0, 12);//Формируем и Отправляем созданный пакет

                Read13000(Rbuffer, 0, 12);//читаем ответ

                if (Проверка(Rbuffer, 12) == 1)
                {

                    MessageBox.Show("Содержимое регистра=" + Convert.ToString((Rbuffer[11] << 8) + " " + Rbuffer[10]));

                }
                else
                {
                    MessageBox.Show("Ошибка при чтении из регистра");
                }
                Rbuffer = null;
            }
        }
        public byte[] Preob1(byte[] WRbufferF, uint AdRedF, uint DatRegF)//Создаем пакет для записи в регист
        {
            WRbufferF[0] = 0x0b;//индефикатор отправителя старший байт
            WRbufferF[1] = 0xb8; //индефикатор отправителя младший байт
            WRbufferF[2] = 0x0b; //индефикатор получателя старший байт
            WRbufferF[3] = 0xb8;  //индефикатор получателя младшитй байт байт
            WRbufferF[4] = 0;    //длинна данных в байтах старший байт
            WRbufferF[5] = 14;    //длинна данных в байтах младший байт
            WRbufferF[6] = 0;   //
            WRbufferF[7] = 0;   //
            WRbufferF[8] = Convert.ToByte(AdRedF & 0xff);//Адрес регистра записи старший байт
            WRbufferF[9] = Convert.ToByte((AdRedF >> 8) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[10] = Convert.ToByte((AdRedF >> 16) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[11] = Convert.ToByte((AdRedF >> 24) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[12] = Convert.ToByte(DatRegF & 0xff);//Данные для записи старший байт
            WRbufferF[13] = Convert.ToByte((DatRegF >> 8) & 0xff);//Данные для записи старший байт
            return WRbufferF;
        }
        public byte[] Preob2(byte[] WRbufferF, uint AdRedF1)//Создаем пакет для запроса чтения из регистра
        {
            WRbufferF[0] = 0x0b;//индефикатор отправителя старший байт
            WRbufferF[1] = 0xb9; //индефикатор отправителя младший байт
            WRbufferF[2] = 0x0b; //индефикатор получателя старший байт
            WRbufferF[3] = 0xb9;  //индефикатор получателя младшитй байт байт
            WRbufferF[4] = 0;    //длинна данных в байтах старший байт
            WRbufferF[5] = 12;    //длинна данных в байтах младший байт
            WRbufferF[6] = 0;   //
            WRbufferF[7] = 0;   //
            WRbufferF[8] = Convert.ToByte(AdRedF1 & 0xff);//Адрес регистра записи старший байт
            WRbufferF[9] = Convert.ToByte((AdRedF1 >> 8) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[10] = Convert.ToByte((AdRedF1 >> 16) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[11] = Convert.ToByte((AdRedF1 >> 24) & 0xff);//Адрес регистра записи младший байт

            return WRbufferF;
        }
        public byte[] Preob3(byte[] WRbufferF, uint AdRedF, uint DatRegF)//Создаем пакет для запроса чтения из регистра
        {
            WRbufferF[0] = 0x0b;//индефикатор отправителя старший байт
            WRbufferF[1] = 0xba; //индефикатор отправителя младший байт
            WRbufferF[2] = 0x0b; //индефикатор получателя старший байт
            WRbufferF[3] = 0xba;  //индефикатор получателя младшитй байт байт
            WRbufferF[4] = 0;    //длинна данных в байтах старший байт
            WRbufferF[5] = 14;    //длинна данных в байтах младший байт
            WRbufferF[6] = 0;   //
            WRbufferF[7] = 0;   //
            WRbufferF[8] = Convert.ToByte(AdRedF & 0xff);//Адрес регистра записи старший байт
            WRbufferF[9] = Convert.ToByte((AdRedF >> 8) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[10] = Convert.ToByte((AdRedF >> 16) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[11] = Convert.ToByte((AdRedF >> 24) & 0xff);//Адрес регистра записи младший байт
            WRbufferF[12] = Convert.ToByte(DatRegF & 0xff);//Данные для записи старший байт
            WRbufferF[13] = Convert.ToByte((DatRegF >> 8) & 0xff);//Данные для записи старший байт
            return WRbufferF;
        }

        public void Write1(Byte[] bufF, int offsF, int lenF)//
        {
            
                if (ns.CanWrite && clientBAAK12T.Connected && ns != null)
            {
                try
                {
                   ns.WriteTimeout = 100;
                  ns.Write(bufF, offsF, lenF);
                }
                catch
                {

                    статусБААК12 = "Ошибка";
                }
            }
            else
            {
                статусБААК12 = "Ошибка";
                MessageBox.Show("Ошибка записи в регистр");
                return;
            }
        }
        public int Read13000(Byte[] bufF, int offsF, int lenF)//Для чтения ответа при записи в регистр
        {

            if (ns.CanWrite && clientBAAK12T.Connected && ns != null)
            {
                try
                {
                    ns.ReadTimeout = 100;
                   ns.Read(bufF, offsF, lenF);

                }
                catch (Exception)
                {
                    статусБААК12 = "Ошибка";
                    Conect300Statys = false;
                    
                    return -2;
                }
            }
            else
            {
                статусБААК12 = "Ошибка";
                Conect300Statys = false;
                // MessageBox.Show("Ошибка чтения при записи в регистр");
                return -1;
               
            }
            return 0;


        }

        /// <summary>
        /// Для чтения данных плат БААК12
        /// </summary>
        /// <param name="buf">возвращает содержимое потока данных</param>
        /// <returns></returns>
        public int Read13007(out byte[] buf)//Для чтения данных
        {
            buf = new byte[4096];
            
            if (nsData.CanRead && nsData.DataAvailable && nsData != null)
            {
                
                    try
                    {

                    return nsData.Read(buf, 0, 4096);
                    
                    }
                    catch (Exception ex)
                    {
                  
                    return -2;
                    }
            }
            else
            {
                
            return -1;
            }
            
        }

        public void Dispose() => throw new NotImplementedException();

    }
}
