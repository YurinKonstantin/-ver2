using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017
{
   
    //Клас для настроек баак
    public class Bak
    {
        public static ObservableCollection<Bak> _DataColec1;
      
        public string Klname { get; set; }
        public string KLIP { get; set; }
        public string NameBAAK { get; set; }
        public bool BAAK12NoT { get; set; }
        private bool fkagNameBAAK = true;
        public bool FkagNameBAAK
        {
            get
            {
                return fkagNameBAAK;
            }
            set
            {
                fkagNameBAAK = ПроверкаНаНуль(value);
            }
        }
        private bool ПроверкаНаНуль(bool n)
        {   if(n!=null)
            {
                return n;
            }
        else
            {
                return false;
            }

        }

        public static void InstCol()
        {
            _DataColec1 = new ObservableCollection<Bak>();    
        }
        public static void AddKl(string klname1, string KlIp1, String nameBAAK1, bool fBAAK)
        {
            _DataColec1.Add(new Bak { Klname = klname1, KLIP = KlIp1, NameBAAK= nameBAAK1, BAAK12NoT = fBAAK });
        }
        public static void DelKl(int iy)
        {
                if(iy>-1)
            {
                _DataColec1.RemoveAt(iy);
            }           

        }
    }
}
