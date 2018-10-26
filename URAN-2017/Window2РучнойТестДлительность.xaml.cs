using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Window2РучнойТестДлительность.xaml
    /// </summary>
    public partial class Window2РучнойТестДлительность : Window
    {
        public Window2РучнойТестДлительность()
        {
            InitializeComponent();
        }
        public int DlitTestRan
        {
            get { return Convert.ToInt32(dlit1.Text); }
        }
        public int Porog
        {
            get { return Convert.ToInt32(porog1.Text); }
        }
        public int Trig
        {
            get { return Convert.ToInt32(trig1.Text); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
