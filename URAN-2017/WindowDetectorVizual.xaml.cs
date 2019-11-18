using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для WindowDetectorVizual.xaml
    /// </summary>
    public partial class WindowDetectorVizual : Window
    {
        public string pathBD=String.Empty;
        public WindowDetectorVizual(string pp)
        {
            InitializeComponent();
            pathBD = pp;
            controlDetector.pathBD = pathBD;
            Button_Click(null, null);
       
        
       

            dispatcherTimer1.Tick += new EventHandler(DoReset);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer1.Start();
        }
    
        DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        int x = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            prog.Visibility = Visibility.Visible;
            controlDetector.TempBD();
            prog.Visibility = Visibility.Collapsed;
        }
      
        public void DoReset(object sender, EventArgs e)
        {
           // Dispatcher.Invoke(DispatcherPriority.Render,)
            tecSos.Text ="До обнавления состояния"+ (60 - x).ToString();
            x++;
            if(x>=60)
            {
                x = 0;
                prog.Visibility = Visibility.Visible;
                controlDetector.TempBD();
                prog.Visibility = Visibility.Collapsed;
            }

        }
    }
}
