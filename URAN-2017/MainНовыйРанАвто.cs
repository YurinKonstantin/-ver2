using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    public partial class MainWindow
    {
        DateTime alarmNewRun;
        private void NewRun()
        {
            if (DateTime.Compare(DateTime.UtcNow, alarmNewRun) > 0)
            {
                Task task= Task.Run(() => NewRanWork());
            }


        }
        delegate void ParametrizedMethodInvoker1();
        void Log_left_accs()
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher?.Invoke(new ParametrizedMethodInvoker1(Log_left_accs));
                return;
            }
            Stop_Click(null, null);//вызываем событие кнопки стоп, останавливаем набор
            myZapicDataTascTask.Wait();//Дожидаемся остановки набора
            while (!Obnoviti.IsEnabled)//Дожидаемся разблокировки кнопки обновить, точно набор завершен
            {
                Thread.Sleep(500);
            }
            Button_Click_3(null, null);//вызываем событие кнопки обновить
            Start_Click(null, null);//запускаем новый набор
        }
        private void NewRanWork()
        {
          //  Dispatcher.Invoke(new ParametrizedMethodInvoker1(this.Log_left_accs));
        }
       
      
    }
}
