using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для WindowОзапуске.xaml
    /// </summary>
    public partial class WindowОзапуске : Window
    {
        Thread t;
        public WindowОзапуске()
        {
            InitializeComponent();
           // gh();
        }
       
        public void gh(string v)
        {
            lab.Content = v;
            

        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }
        public void fg()
        {
            this.Close();
        }
    }
}
