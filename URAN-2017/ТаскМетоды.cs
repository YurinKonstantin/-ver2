using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        Task myZapicDataTascTask;
        Task РежимСинхИлиНетТаскT;
        public async Task ЗапускНастройкиТаск()
        {
            Task myНастройкаTask = Task.Run(() => BAAK12T.НастройкаURANDelegate?.Invoke());
            await myНастройкаTask;
        }
        public async Task ПускURANDТаск()
        {
            Task myПускURANDTask = Task.Run(() => BAAK12T.ПускURANDelegate?.Invoke());
            await myПускURANDTask;
        }

        public async Task РежимСинхИлиНетТаск(int t)
        {
             РежимСинхИлиНетТаскT = Task.Run(() => РежимСинхИлиНет(t));
            await РежимСинхИлиНетТаскT;
        }
        public DispatcherTimer dispatcherTimer;
        private void ВремяОтобрTask()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
           // Task myReadDateTask = Task.Run(() => TimeTask());
        }
        public void ЗапускРеадТаск(CancellationToken cancellationToken)
        {
            Task myReadDataTask = Task.Run(() => ReadDataTask(cancellationToken));
            
        }
        public async Task ZapicDataTasc1(CancellationToken cancellationToken)
        {
            myZapicDataTascTask = Task.Run(() => ZapicDataTasc(cancellationToken));
            await myZapicDataTascTask;
        }
        public void ZapicDataBDTasc1(CancellationToken cancellationToken)
        {
            Task myZapicDataBDTasc = Task.Run(() => ZapicDataBDTasc(cancellationToken));
            
        }

    }
}
