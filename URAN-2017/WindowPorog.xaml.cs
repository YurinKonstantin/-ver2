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
    /// Логика взаимодействия для WindowPorog.xaml
    /// </summary>
    public partial class WindowPorog : Window
    {
        public WindowPorog()
        {
            InitializeComponent();
        }
       public ClassBAAK12_100 classBAAK12_100 = new ClassBAAK12_100();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            classBAAK12_100.newPorog(Convert.ToInt32(newporog.Text));
        }
    }
}
