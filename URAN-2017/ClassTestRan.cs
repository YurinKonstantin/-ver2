using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace URAN_2017
{
   public class ClassTestRan
    {
       DateTime alarm;
        public static ObservableCollection<ClassTestRan> _DataColec2;
        public static void InstCol()
        {
            _DataColec2 = new ObservableCollection<ClassTestRan>();
            // _DataColec2.Add(new ClassTestRan { Hors = "07", Mins ="00", TipTest = "По порогу" });
            // _DataColec2.Add(new ClassTestRan { Hors = "10", Mins = "00", TipTest = "По числу событий" });
            
        }
        public static void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ClassTestRan>));
            using (StreamWriter wr = new StreamWriter(md + "\\UranSetUp\\" + "ClassTestRanSetting1.xml"))
            {
                xs.Serialize(wr, ClassTestRan._DataColec2);
            }
        }
        public static void AddTestRan(DateTime alarm1, String timeHors, String timeMin, string tip, int dlitel, int por, int trig, int kkol, int intt, Boolean progTrig)
        {
            _DataColec2.Add(new ClassTestRan { Hors = timeHors, Mins = timeMin, TipTest = tip, alarm=alarm1, Dlit=dlitel, Porog=por, Trig=trig, Kolsob=kkol, Interval=intt, ProgramTrigTest=progTrig});
        }
        public static void DelTestRan(int iy)
        {

            if (iy > -1)
            {
                _DataColec2.RemoveAt(iy);
            }



        }
        public DateTime Alam
        {
            get
            {
                return alarm;
            }
            set
            {
                alarm = value;
            }
        }
        public void IncAlam()
        {
            alarm= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, Convert.ToInt32(Hors), Convert.ToInt32(Mins), 0, 0);
        }
       
        private string tipTest = "По порогу";
        public string TipTest
        {
            get
            {
                return tipTest;
            }
            set
            {
                tipTest = value;
            }
        }
        private String hors = "00";
        public String Hors
        {
            get
            {
                return hors;
            }
            set
            {
                hors = value;
            }
        }
        private String mins = "00";
        public String Mins
        {
            get
            {
                return mins;
            }
            set
            {
                mins = value;
            }
        }
        private int kolsob;
        public int Kolsob
        {
            get
            {
                return kolsob;
            }
            set
            {
                kolsob = value;
            }
        }
        private int interval;
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }
        private Boolean programTrigTest=false;
        public Boolean ProgramTrigTest
        {
            get
            {
                return programTrigTest;
            }
            set
            {
                programTrigTest = value;
            }
        }
        private int dlit;
        public int Dlit
        {
            get
            {
                return dlit;
            }
            set
            {
                dlit = value;
            }
        }
        private int porog;
        public int Porog
        {
            get
            {
                return porog;
            }
            set
            {
                porog = value;
            }
        }
        private int trig;
        public int Trig
        {
            get
            {
                return trig;
            }
            set
            {
                trig = value;
            }
        }
        public string uslovie
        {
            get
            {
                if(TipTest== "По длительности")
                {
                    return " Длительность: "+Dlit.ToString()+" Порог: "+Porog.ToString()+" Триггер: "+Trig.ToString();
                }
                if(TipTest== "По количеству")
                {
                    return "Количество событий: " + Kolsob.ToString() + " Интервал: " + Interval.ToString();
                }
                return TipTest;
            }
        }

    }
}
