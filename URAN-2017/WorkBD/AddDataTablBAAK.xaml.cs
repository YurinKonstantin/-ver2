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

namespace URAN_2017.WorkBD
{
    /// <summary>
    /// Логика взаимодействия для AddDataTablBAAK.xaml
    /// </summary>
    public partial class AddDataTablBAAK : Window
    {
        public AddDataTablBAAK()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DataAccesBDBAAK.AddDataTablPlats(new ViewTaiblBDBAAK.ClassTablPSBBAAK() {namePSB=name.Text, Coment=coment.Text, IpPSB=ip.Text, nomerKlastera=Convert.ToInt32(nomerKl.Text), tipPSB=tip.Text });
            this.DialogResult = true;
        }
    }
}
