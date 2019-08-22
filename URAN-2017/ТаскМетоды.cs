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
        private void ВремяОтобрTask()
        {
           
            Task myReadDateTask = Task.Run(() => TimeTask());
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
