using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace URAN_2017
{
   public partial class BAAK12T
    {
        public void StartTime(uint t)
        {
            WreadReg3000(0x200002, t);
        }
        public void SettingNoCloc()
        {

            WreadReg3000(0x20001c, 0);

            WreadReg3000(0x200026, 0); // Generator Clock

            WreadReg3000(0x200024, 1); 
            WreadReg3000(0x200024, 0);//Сброс регистров
            Thread.Sleep(500);
            WreadReg3000(0x20001c, 0);
            WreadReg3000(0x200026, 0); // Generator Clock

        }
        public void SettingCloc()
        {
            WreadReg3000(0x20001c, 1);
            WreadReg3000(0x200026, 1); // optc Clock
            WreadReg3000(0x200002, 1);// Запуск по клоку
            WreadReg3000(0x20001c, 1);//синхронизация таймера
            WreadReg3000(0x200024, 1); //Сброс регистров
            Thread.Sleep(600);
            WreadReg3000(0x200024, 0);// нормальная работа регистров
            WreadReg3000(0x20001c, 1);//синхронизация таймера
            WreadReg3000(0x200026, 1);

        }
        public void SettingAll()
        {
           

            BlocAndPolarnost(9252);
            TriggerStop();           
            WreadReg3000(0x200004 + 8, 0xfff);//маска каналов  
            WreadReg3000(0x200202, 0xfa);
            WreadReg3000(0x200204, 0xfa);
            ADCSetUp();    
            AllSetPorogAll(PorogAll);
            OffSetData();
            DataLengt();
            WreadReg3000(0x200004 + 8, 0xfff);
            for (uint j = 0; j < 12; j++)
            {
                
                WreadReg3000(BaseA_M + AM_FThrBase + j * 2, 2400); // Filter Threshold
            }


            


            if(!trigOtBAAK)
            {
                Trigger(0x200006, TrgAll);
            }
          else
            {
                Trigger(0x200006, 128);
            }
            Winduws(0x20000a, 10);           
            WreadReg3000(0x200208, ДискретностьХвост);//дискретность хвоста
            AllStopDelay(650);            
            for (uint j = 0; j < 12; j++)
            {              
                WreadReg3000(0x200050 + j * 2, 0xfff); // матрица совпадений
            }
            WreadReg3000(0x200020, 0x1);
        }
       public bool trigOtBAAK = false;
       
        public void FirsTime()//Время внутреннего таймера
        {
            
            WreadReg3000(0x200010, Time0x10);
          
            WreadReg3000(0x200012, Time0x12);
            
            WreadReg3000(0x200014, Time0x14);
            
            WreadReg3000(0x200016, Time0x16);
            
        }
        public void BlocAndPolarnost( uint x)//Полярность сигнала 8224 - запрос разрешен и положительная полярность, 9252 - запрос запрешен, и положительная полярность
        {
            WreadReg3000(0x90000, x);

            WreadReg3000(0x98000, x);

            WreadReg3000(0xb0000, x);

            WreadReg3000(0xb8000, x);

            WreadReg3000(0xd0000, x);

            WreadReg3000(0xd8000, x);

        }
        public void AllStopDelay(uint wind)//задержка остановки записи после прихода триггера
        {
            WreadReg3000(0x9000e, wind);
            WreadReg3000(0x9800e, wind);
            WreadReg3000(0xb000e, wind);
            WreadReg3000(0xb800e, wind);
            WreadReg3000(0xd000e, wind);
            WreadReg3000(0xb800e, wind);
        }
        public void StopDelay(uint Reg, uint wind)//задержка остановки записи после прихода триггера в отсчетах
        {
            WreadReg3000(Reg, wind);
        }
        public void Trigger(uint Reg, uint tr)//Кратность совпадений и подверждение
        {
            WreadReg3000(Reg, tr);
        }
        public void Winduws(uint Reg, uint wind)//окно совпадений дискретность 10 нс от 0 до 254
        {
            WreadReg3000(Reg, wind);
        }
        public void SetPorog(uint Reg, uint por)
        {
            WreadReg3000(Reg, por);
        }
       public uint[] masnul = new uint[12];
        public void AllSetPorogAll(uint PorogAll1)
        {
            BDselect(out masnul);
            SetPorog(0x90010, masnul[0] + PorogAll1);//низкий порог для канала 0(первый)
            SetPorog(0x90020, masnul[0]+1 + PorogAll1);//высокий порог для канала 0(первый)

            SetPorog(0x90014, masnul[1] + PorogAll1);//низкий порог для канала 1
            SetPorog(0x90024, masnul[1] + 1 + PorogAll1);//высокий порог для канала 1

            SetPorog(0x98010, masnul[2] + PorogAll1);//низкий порог для канала 2
            SetPorog(0x98020, masnul[2] + 1 + PorogAll1);//высокий порог для канала 2

            SetPorog(0x98014, masnul[3] + PorogAll1);//низкий порог для канала 3
            SetPorog(0x98024, masnul[3] + 1 + PorogAll1);//высокий порог для канала 3

            SetPorog(0xb0010, masnul[4] + PorogAll1);//низкий порог для канала 4
            SetPorog(0xb0020, masnul[4] + 1 + PorogAll1);//высокий порог для канала 4

            SetPorog(0xb0014, masnul[5] + PorogAll1);//низкий порог для канала 5
            SetPorog(0xb0024, masnul[5] + 1 + PorogAll1);//высокий порог для канала 5

            SetPorog(0xb8010, masnul[6] + PorogAll1);//низкий порог для канала 6
            SetPorog(0xb8020, masnul[6] + 1 + PorogAll1);//высокий порог для канала 6

            SetPorog(0xb8014, masnul[7] + PorogAll1);//низкий порог для канала 7
            SetPorog(0xb8024, masnul[7] + 1 + PorogAll1);//высокий порог для канала 7

            SetPorog(0xd0010, masnul[8] + PorogAll1);//низкий порог для канала 8
            SetPorog(0xd0020, masnul[8] + 1 + PorogAll1);//высокий порог для канала 8

            SetPorog(0xd0014, masnul[9] + PorogAll1);//низкий порог для канала 9
            SetPorog(0xd0024, masnul[9] + 1 + PorogAll1);//высокий порог для канала 9

            SetPorog(0xd8010, masnul[10] + PorogAll1);//низкий порог для канала 10
            SetPorog(0xd8020, masnul[10] + 1 + PorogAll1);//высокий порог для канала 10

            SetPorog(0xd8014, masnul[11] + PorogAll1);//низкий порог для канала 11
            SetPorog(0xd8024, masnul[11] + 1 + PorogAll1);//высокий порог для канала 11

        }
        //   +++ Настройка АЦП+++
        public void ADCSetUp()
        {
           WreadReg3000(0x200004 + 8, 0x3ff);// пишем во все АЦП одновременно
            WreadReg3000(0x200000 + 12 * 2, 0x10);// пишем во все АЦП одновременно
           WreadReg3000(0x200000 + 13 * 2, 0x0503); //данные для обоих каналов
            WreadReg3000(0x200000 + 13 * 2, 0xff01); //Update Register
            WreadReg3000(0x200000 + 13 * 2, 0x1404); //Offset Binary
           WreadReg3000(0x200000 + 13 * 2, 0x1792); // DCO Delay  -- (2,5-0,4-0,2) ns = 1,9 ns = (18+1)= 0x12 (+ invert)
            WreadReg3000(0x200000 + 13 * 2, 0x181d);// Full Scale
            WreadReg3000(0x200000 + 13 * 2, 0xff01);// Update Register
        }
        public void OffSetData()
        {
            WreadReg3000(0x90040, 10300);//смешение данных канала 0
            WreadReg3000(0x90042, 10300);//смешение данных канала 1
            WreadReg3000(0x98040, 10300);//смешение данных канала 2
            WreadReg3000(0x98042, 10300);//смешение данных канала 3
            WreadReg3000(0xb0040, 10300);//смешение данных канала 4
            WreadReg3000(0xb0042, 10300);//смешение данных канала 5
            WreadReg3000(0xb8040, 10300);//смешение данных канала 6
            WreadReg3000(0xb8042, 10300);//смешение данных канала 7
            WreadReg3000(0xd0040, 10300);//смешение данных канала 8
            WreadReg3000(0xd0042, 10300);//смешение данных канала 9
            WreadReg3000(0xd8040, 10300);//смешение данных канала 10
            WreadReg3000(0xd8042, 10300);//смешение данных канала 11
        }
        public void DataLengt()
        {
            WreadReg3000(0x90044, 2048 * DataLenght);//длинна данных канала 0
            WreadReg3000(0x90046, 2048 * DataLenght);//длинна данных канала 1
            WreadReg3000(0x98044, 2048 * DataLenght);//длинна данных канала 2
            WreadReg3000(0x98046, 2048 * DataLenght);//длинна данных канала 3
            WreadReg3000(0xb0044, 2048 * DataLenght);//длинна данных канала 4
            WreadReg3000(0xb0046, 2048 * DataLenght);//длинна данных канала 5
            WreadReg3000(0xb8044, 2048 * DataLenght);//длинна данных канала 6
            WreadReg3000(0xb8046, 2048 * DataLenght);//длинна данных канала 7
            WreadReg3000(0xd0044, 2048 * DataLenght);//длинна данных канала 8
            WreadReg3000(0xd0046, 2048 * DataLenght);//длинна данных канала 9
            WreadReg3000(0xd8044, 2048 * DataLenght);//длинна данных канала 10
            WreadReg3000(0xd8046, 2048 * DataLenght);//длинна данных канала 11
        }
        public void TriggerProgramSetap()//внешний триггер
        {
            WreadReg3000(0x200006, 0x101);
        }


    }
}
