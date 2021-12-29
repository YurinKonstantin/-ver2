using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Windows;

namespace URAN_2017
{
    public partial class BAAK12T : ClassParentsBAAK, INotifyPropertyChanged
    {
        public  List<byte> DataBAAKList = new List<byte>();
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
        public delegate void НастройкаUranOldDelegate();
        /// <summary>
        /// загрузка регистров начало, создание файла и тд
        /// </summary>
        public static НастройкаUranOldDelegate НастройкаURANOldDelegate;
        public static bool grafOtob = false;
        public static string otobKl = "0";
        
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
       
        public  delegate void СтопТриггер();
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
        private static string nameRan="0";
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
        private static int porogNutron;
        public static int PorogNutron
        {
            get
            {
                return porogNutron;
            }
            set
            {
                porogNutron = value;
            }
        }
        private static int dlNutron;
        public static int DlNutron
        {
            get
            {
                return dlNutron;
            }
            set
            {
                dlNutron = value;
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

        private int темпПакетов=0;
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
        private int темпПакетовN = 0;
        /// <summary>
        /// Темп счета принятых пакетов
        /// </summary>
        public int ТемпПакетовN
        {
            get
            {
                return темпПакетовN;
            }
            set
            {
                темпПакетовN = value;
                this.OnPropertyChanged(nameof(ТемпПакетовN));
            }

        }
        private int пакетовN = 0;
        public int ПакетовN
        {
            get
            {
                return пакетовN;
            }
            set
            {
                пакетовN = value;
                this.OnPropertyChanged(nameof(ПакетовN));
            }

        }
        private int колПакетовN = 0;
        public int КолПакетовN
        {
            get
            {
                return колПакетовN;
            }
            set
            {
                колПакетовN = value;
                this.OnPropertyChanged(nameof(КолПакетовN));
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
















    }
}
