using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017
{
    class ClassZapicBD
    {
        public Boolean tipDataSob = true;//Есди тру то это данные события, если фолс то нейтрон
        public Boolean tipDataTest = true;
        public string nameFileBD = "1";
        public string nameBAAKBD = "У1";
        public string timeBD = "00.00";
        public string nameRanBD = "Ran00";
       public int[] AmpBD = new int[12];
        public void AmpNew(int[] Amp1)
        {
            AmpBD = new int[12];
            Array.Copy(Amp1, AmpBD, 12);
        }
        public string nameklasterBD = "кл1";
      public  int[] NnutBD = new int[12];
        public void NnutNew(int[] Nnut1)
        {
            NnutBD = new int[12];
            Array.Copy(Nnut1, NnutBD, 12);
        }
        public int[] NlBD = new int[12];
        public void NlNew(int[] Nl1)
        {
            NlBD = new int[12];
            Array.Copy(Nl1, NlBD, 12);
        }
        public Double[] sigBDnew= new Double[12];
       
        public int DBD = 0;
        public int AmpSobBD = 0;
        public int TimeFirstBD = 0;
        public int TimeEndBD = 0;
        public int TimeAmpBD = 0;
        public int TimeFirst3BD = 0;
        public int TimeEnd3BD = 0;
    }
}
