using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace URAN_2017
{
    [Serializable]
    class ClassOtborNeutron
    {
        private int porog = 6;
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

        private int dlit = 2;
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
       

    }
}
