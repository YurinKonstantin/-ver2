using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        int xTime = 0;
        public void DoReset(object sender, EventArgs e)
        {
            // Dispatcher.Invoke(DispatcherPriority.Render,)
            tecSos.Text = "До обнавления состояния" + (60 - xTime).ToString();
            xTime++;
            if (xTime >= 60)
            {
                xTime = 0;
       
                controlDetector.TempBD();
         
            }

        }
        private void Button_ClickStartD(object sender, RoutedEventArgs e)
        {
            try
            {

                controlDetector.pathBD = BAAK12T.wayDataBD;
                Button_ClickResetDetectors(null, null);
                dispatcherTimer1.Tick += new EventHandler(DoReset);
                dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_ClickResetDetectors(object sender, RoutedEventArgs e)
        {
            controlDetector.pathBD = BAAK12T.wayDataBD;
            controlDetector.TempBD();
           
        }
    }
}
