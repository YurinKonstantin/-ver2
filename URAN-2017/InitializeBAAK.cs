using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URAN_2017
{
   public partial class MainWindow
    {
        private void InitializeMS(string host)//Функция производит подписку на все необходимые действия для работы
        {
            try
            {
                MS = new ClassMC() { Host = set.MS, CтатусБААК12 = "МС доступен", flagMGVS=set.FlagClok};
                stMS.Content = "МС подключен";
                ConnnectURANDelegate += MS.Conect3000; //подписка на конект
                НастройкаURANDelegate += MS.ОбщаяНастройка; //подписка на запуск(загрузка регистров начало, создание файла и тд )
                DiscConnnectURANDelegate += MS.DicsConectAll;
                ДеИнсталяцияDelegate += DeInitializeMS;
            }
            catch (Exception)
            {
                DeInitializeMS();

            }
        }
        public void DeInitializeMS()//Функция производит отписку от всех  действия для работы
        {
            try
            {
                ConnnectURANDelegate -= MS.Conect3000;//подписка на конект
                DiscConnnectURANDelegate -= MS.DicsConectAll;
                ДеИнсталяцияDelegate -= DeInitializeMS;
            }
            catch
            {

            }
        }

        private void InitializeMS1(string host)//Функция производит подписку на все необходимые действия для работы
        {
            try
            {
                MS1 = new ClassMC() { Host = set.MS1, CтатусБААК12 = "МС доступен", flagMGVS = set.FlagClok };
                stMS.Content = "МС подключен";
                ConnnectURANDelegate += MS1.Conect3000; //подписка на конект
                НастройкаURANDelegate += MS1.ОбщаяНастройка; //подписка на запуск(загрузка регистров начало, создание файла и тд )
                DiscConnnectURANDelegate += MS1.DicsConectAll;
                ДеИнсталяцияDelegate += DeInitializeMS1;
            }
            catch (Exception)
            {
                DeInitializeMS1();

            }
        }
        public void DeInitializeMS1()//Функция производит отписку от всех  действия для работы
        {
            try
            {
                ConnnectURANDelegate -= MS1.Conect3000;//подписка на конект
                DiscConnnectURANDelegate -= MS1.DicsConectAll;
                ДеИнсталяцияDelegate -= DeInitializeMS1;
            }
            catch
            {

            }
        }

    }
}
