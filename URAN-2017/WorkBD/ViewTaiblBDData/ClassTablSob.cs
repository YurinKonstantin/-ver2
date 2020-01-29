using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017.WorkBD.ViewTaiblBDData
{
  public  class ClassTablSob
    {
        public string Time { get; set; }
        public string ИмяФайла { get; set; }
        public string Плата { get; set; }
        public string Кластер { get; set; }
        public int СумАмп { get; set; }
        public int СумN { get; set; }
        public int[] АмпCh { get; set; }
        public int[] NCh { get; set; }
        public int[] Nul { get; set; }
        public double[] sig { get; set; }
        public bool bad { get; set; }

    }
}
