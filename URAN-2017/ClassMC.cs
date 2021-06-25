using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    class ClassMC:ClassParentsBAAK, INotifyPropertyChanged
    {
        private int microseconds=0;
        public int Microseconds
        {
            get
            {
                return microseconds;
            }
            set
            {
                microseconds = (value);
            }
        }
        public string TimeTaimer1 = "0 0:0:0:0";
        public int link_mask;               //int
        public int millisecond=0;            //int
        public int minutes=0;             //int
        public string text;     //string
        public int hours=0;               //int
        public int delay;           //int
        public int seconds=0;
        public int data_transfer;                   //int
        public int synchronized_from_GPS;               //int
        public string synchronization_time;     //string
        public int synchronized;                    //int
        public int data_socket;                 //int
        public int control_socket;                  //int
        public int status; //int
        public int corectdelay;      //int
        public int corectlink_mask;     //int
        public bool flagMGVS = false;
        public void ОбщаяНастройка()
        {
            АвтономныйКлокРазрешен(0);
            RegistersInit_DRS4();
            DACLoad();

        }
        private string Port = "3000";
        public new void Conect3000()
        {
            try
            {
                clientBAAK12T = new TcpClient();
                clientBAAK12T.Connect(Host, Int32.Parse(Port));
                ns = clientBAAK12T.GetStream();
            }
            catch
            {
                CтатусБААК12 = "Произошла ОШИБКА подключения порт 3000";
                Conect300Statys = false;
            }
            if (clientBAAK12T.Connected)
            {
                CтатусБААК12 = "Подключена, ожидает СТАРТ";
                Conect300Statys = true;

            }
            else
            {
                CтатусБААК12 = "Произошла ОШИБКА";
                Conect300Statys = false;
            }
        }
        public void АвтономныйКлокРазрешен(uint x)//Разрешение синхроимпульсов в режиме автономной работы (= 1 – синхроимпульсы разрешены)
        {
            WreadReg3000(0x60, x);
        }

      
        private void RegistersInit_DRS4()
        {
            WreadReg3000(0x100, 0);
            // WreadReg3000(0x60, 0);
            // WreadReg3000(0, 0);	// 1 = Program Trigger, 0 = Ext. Auto, 0x10 = DRS Enable, 0x20 = DENABLE Always High
            //WreadReg3000(0, 0x31);	// 
            // WreadReg3000(34, 4);	// 4 = Stop without Data Останавливает DRS без передачи данных
            // WreadReg3000(0x21, 0xDFF);  // Program DRS4
            //WreadReg3000(0x21, 0xBFF);  // Program DRS4
            // WreadReg3000(0x21, 0xCFF);	// Program DRS4
            // WreadReg3000(16, 1);    // DMA Enable
            // WreadReg3000(0x10, 1); //MC start
            // WreadReg3000(0x21, 1); //MC Event Number
            // WreadReg3000(0x3, 10);    // Delay DRS Stop (50 = 500 ns)
            WreadReg3000(0x30, 0);    // Offset 0 ch
            WreadReg3000(0x40, 1024);  // Length 0 ch
            WreadReg3000(0x31, 0);    // Offset 1 ch
            WreadReg3000(0x41, 1024);  // Length 1 ch
            WreadReg3000(0x32, 0);    // Offset 2 ch
            WreadReg3000(0x42, 1024);  // Length 2 ch
            WreadReg3000(0x33, 0);    // Offset 3 ch
            WreadReg3000(0x43, 1024);  // Length 3 ch
            WreadReg3000(0x34, 0);    // Offset 4 ch
            WreadReg3000(0x44, 1024);  // Length 4 ch
            WreadReg3000(0x35, 0);    // Offset 5 ch
            WreadReg3000(0x45, 1024);  // Length 5 ch
            WreadReg3000(0x36, 0);    // Offset 6 ch
            WreadReg3000(0x46, 1024);  // Length 6 ch
            WreadReg3000(0x37, 0);    // Offset 7 ch
            WreadReg3000(0x47, 1024);  // Length 7 ch
            WreadReg3000(0x38, 0);    // Offset 8 ch
            WreadReg3000(0x48, 1024);  // Length 8 ch
                                       //num = XilWriteReg(0, 0x31);	// 1 = Program Trigger, 0 = Ext. Auto
                                       // 1 = Program Trigger, 0 = Ext. Auto
        //    WreadReg3000(0, 0x30);
            if (flagMGVS)
            {
                WreadReg3000(0x2000026, 0x0);
               // WreadReg3000(26, 0x0);
                WreadReg3000(0, 0x10);
                WreadReg3000(0x100, 1);

            }
            else
            {


               // WreadReg3000(0, 0x31);
                WreadReg3000(0x2000026, 0x1);
                WreadReg3000(0x60, 0);
                //   WreadReg3000(0x100, 0); // Trigger Enable 1- Разрешение внешнего триггера 
                Debug.WriteLine("RegistersInit_DRS4");
                WreadReg3000(0, 0x10);
                WreadReg3000(0x100, 1);

            }
            

        }
        private void DACLoad()
        {

             WreadReg3000(0x23, 20);	// Set Refclk (5 G)
            //			num = XilWriteReg(0x23, 102);	// Set Refclk (1 G)

          //  num = XilWriteReg32(0x20, 0x08000001);

          //  num = WriteDAC(0, 9175); // Bias
            
            //            num = WriteDAC(1, 3932); // TLEVEL
           // num = WriteDAC(1, 500); // TLEVEL
           
            //			num = WriteDAC(2, 8500); // O-OFS
            //num = WriteDAC(2, 17039); // O-OFS
            
           // num = WriteDAC(4, 20316); // ROFS
            
           // num = WriteDAC(5, 9830); // CAL-
            //			num = WriteDAC(5, 7209); // CAL-
            
            //num = WriteDAC(7, 9830); // CAL+
            //			num = WriteDAC(7, 12452); // CAL+

        }

    }
}
