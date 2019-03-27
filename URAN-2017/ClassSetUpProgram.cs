using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017
{
    [Serializable]
    public class ClassSetUpProgram
    {
        private  bool _FlagMainRezim = true;
        public  bool FlagMainRezim
        {
            get
            {
                return _FlagMainRezim;
            }
            set
            {
                _FlagMainRezim = value;
            }
        }
    }
}
