using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using System.Runtime.Serialization;
using URAN_2017.FolderSetUp;

namespace URAN_2017
{
    [Serializable]
    public class UserSetting
    {
        public static void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
            using (StreamWriter wr = new StreamWriter(md + "\\UranSetUp\\" + "setting1.xml"))
            {
                xs.Serialize(wr, Bak._DataColec1);
                //  xs.Serialize(wr, Bak._DataColec1NoTail);
                wr.Close();
            }
          

        }
        public static void SerialAll()
        {
            UserSetting set = new UserSetting();
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
              
                fs.Close();

            }
            

        }
        public static void SerialBAA12_200()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
            using (StreamWriter wr = new StreamWriter(md + "\\UranSetUp\\" + "setting1.xml"))
            {
                xs.Serialize(wr, Bak._DataColec1);
                //  xs.Serialize(wr, Bak._DataColec1NoTail);
                wr.Close();
            }

        }
        public static void SerialBAA12_100()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            XmlSerializer xs1 = new XmlSerializer(typeof(ObservableCollection<Bak>));
            using (StreamWriter wr1 = new StreamWriter(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
            {
                xs1.Serialize(wr1, Bak._DataColecBAAK100);
                //  xs.Serialize(wr, Bak._DataColec1NoTail);
                wr1.Close();
            }

        }

        private bool _FlagPorog;
        public bool FlagPorog
        {
            get
            {
                return _FlagPorog;
            }
            set
            {
                _FlagPorog = value;
            }
        }

        private static bool _FlagOtbor = true;
        public static bool FlagOtbor
        {
            get
            {
                return _FlagOtbor;
            }
            set
            {
                _FlagOtbor = value;
            }
        }
        private  bool _FlagSaveBin = true;
        public  bool FlagSaveBin
        {
            get
            {
                return _FlagSaveBin;
            }
            set
            {
                _FlagSaveBin = value;
            }
        }

        private  bool _FlagSaveBD = true;
        public  bool FlagSaveBD
        {
            get
            {
                return _FlagSaveBD;
            }
            set
            {
                _FlagSaveBD = value;
            }
        }
        private bool _FlagTestRan;
        public bool FlagTestRan
        {
            get
            {
                return _FlagTestRan;
            }
            set
            {
                _FlagTestRan = value;
            }
        }
        private int колТригТест = 100;
        public int КолТригТест
        {
            get
            {
                return колТригТест;
            }
            set
            {
                колТригТест = value;
            }
        }
        private bool _FlagClok;
        public bool FlagClok
        {
            get
            {
                return _FlagClok;
            }
            set
            {
                _FlagClok = value;
            }
        }
        private int интервалТригТест = 250;
        public int ИнтервалТригТест
        {
            get
            {
                return интервалТригТест;
            }
            set
            {
                интервалТригТест = value;
            }
        }
        private string ipMGVS = "192.168.1.80";
        public string IpMGVS
        {
            get
            {
                return ipMGVS;
            }
            set
            {
                ipMGVS = value;
            }
        }
        private string portMGVS = "500";
        public string PortMGVS
        {
            get
            {
                return portMGVS;
            }
            set
            {
                portMGVS = value;
            }
        }
        private int delayClok = 250;
        public int DelayClok
        {
            get
            {
                return delayClok;
            }
            set
            {
                delayClok = value;
            }
        }
        private int lincClok = 512;
        public int LincClok
        {
            get
            {
                return lincClok;
            }
            set
            {
                lincClok = value;
            }
        }
        private int timeRanHors = 00;
        public int TimeRanHors
        {
            get
            {
                return timeRanHors;
            }
            set
            {
                timeRanHors = value;
            }
        }
        private int timeRanMin = 00;
        public int TimeRanMin
        {
            get
            {
                return timeRanMin;
            }
            set
            {
                timeRanMin = value;
            }
        }
        private string _WaySetup = @"D:\";
        public string WaySetup
        {
            get
            {
                return _WaySetup;
            }
            set
            {
                _WaySetup = value;
            }
        }
        private string _WaySetupData = @"D:\";//Пишем сюда данные бинарные
        public string WayDATA
        {
            get
            {
                return _WaySetupData;
            }
            set
            {
                _WaySetupData = value;
            }

        }
        private string _WaySetupDataBd = @"D:\";//Пишем сюда данные бинарные
        public string WayDATABd
        {
            get
            {
                return _WaySetupDataBd;
            }
            set
            {
                _WaySetupDataBd = value;
            }

        }
        private string _TestWaySetupDataBd = @"D:\";//Пишем сюда данные бинарные
        public string TestWayDATABd
        {
            get
            {
                return _TestWaySetupDataBd;
            }
            set
            {
                _TestWaySetupDataBd = value;
            }

        }
        private int _Porog = 10;
        public int Porog
        {
            get
            {
                return _Porog;
            }
            set
            {
                _Porog = CorectPorog(value);
            }
        }
        private int _PorogNO = 10;
        public int PorogNO
        {
            get
            {
                return _PorogNO;
            }
            set
            {
                _PorogNO = CorectPorog(value);
            }
        }
        private UInt32 _Discret = 1;
        public UInt32 Discret
        {
            get
            {
                return _Discret;
            }
            set
            {
                _Discret = Convert.ToUInt32(value);
            }
        }
        private int CorectPorog(int porog)
        {
            if (porog < 2048 & porog > 0)
            {
                return porog;
            }
            else
            {
                MessageBox.Show("введено не допустимое значение порога. Интервал значений от 1 до 2048. Будет установлен порог 100");
                return 100;
            }

        }
        private int _intervalFile = 5;
        public int IntervalFile
        {
            get
            {
                return _intervalFile;
            }
            set
            {
                _intervalFile = CorectInterval(value);
            }
        }
        private int CorectInterval(int interval)
        {
            if (interval < 60 & interval > 0)
            {
                return interval;
            }
            else
            {
                MessageBox.Show("Введено не допустимое значение интервала. Интервал значений от 1 до 60. Будет установлено значение 5 минут");
                return 5;
            }
        }
        private bool _FlagTrg = false;
        public bool FlagTrg
        {
            get
            {
                return _FlagTrg;
            }
            set
            {
                _FlagTrg = value;
            }
        }
        private bool _FlagOtob;
        public bool FlagOtob
        {
            get
            {
                return _FlagOtob;
            }
            set
            {
                _FlagOtob = value;
            }
        }
        private UInt32 _Trg = 10;
        public UInt32 Trg
        {
            get
            {
                return _Trg;
            }
            set
            {
                _Trg = CorectTrig(value);
            }
        }

        private UInt32 _TrgNO = 10;
        public UInt32 TrgNO
        {
            get
            {
                return _TrgNO;
            }
            set
            {
                _TrgNO = CorectTrig(value);
            }
        }
        private UInt32 CorectTrig(UInt32 trg)
        {
            if (trg < 13 & trg > 0)
            {
                return trg;
            }
            else
            {
                MessageBox.Show("Введено не допустимое значение триггера. Интервал значений от 1 до 12. Будет установлен триггер 12");
                return 12;
            }
        }
        private bool _FlagAuto;
        public bool FlagAuto
        {
            get
            {
                return _FlagAuto;
            }
            set
            {
                _FlagAuto = value;
            }
        }


        private bool _flagMS = false;
        public bool FlagMS
        {
            get
            {
                return _flagMS;
            }
            set
            {
                _flagMS = value;
            }
        }

        private string _MS = "192.168.2.190";
        public string MS
        {
            get
            {
                return _MS;
            }
            set
            {
                _MS = value;
            }
        }

        private string _MS1 = "192.168.2.191";
        public string MS1
        {
            get
            {
                return _MS1;
            }
            set
            {
                _MS1 = value;
            }
        }

        private int dataLenght = 1;
        public int DataLenght
        {
            get
            {
                return dataLenght;
            }
            set
            {
                dataLenght = CorectDataLenght(value);
            }
        }
        private int CorectDataLenght(int lenght)
        {
            if (lenght < 3 & lenght > 0)
            {
                return lenght;
            }
            else
            {
                MessageBox.Show("введено не допустимое значение порога. Интервал значений от 1 до 2. Будет установлена длина в 1");
                return 1;
            }

        }


    }

}
