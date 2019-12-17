using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017.WorkBD.ViewTaiblBDData
{
   public class ClassTablRun
   {
        public string НомерRun { get; set; }
        public int Синхронизация { get; set; }
        public int ОбщийПорог { get; set; }
        public int Порог { get; set; }
        public int Триггер { get; set; }
        public string ЗначениеТаймер { get; set; }
        public string ВремяЗапуска { get; set; }
        public string ВремяСтоп { get; set; }
                
   }
}
