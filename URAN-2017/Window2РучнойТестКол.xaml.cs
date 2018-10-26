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

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для Window2РучнойТестКол.xaml
    /// </summary>
    public partial class Window2РучнойТестКол : Window
    {
        public Window2РучнойТестКол()
        {
            InitializeComponent();
        }
        public int KolSobTestRan
        {
            get { return Convert.ToInt32(kolSob1.Text); }
        }
        public int Interval
        {
            get { return Convert.ToInt32(interval1.Text); }
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
      
    }
}
