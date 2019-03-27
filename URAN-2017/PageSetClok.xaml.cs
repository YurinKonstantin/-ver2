using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetClok.xaml
    /// </summary>
    public partial class PageSetClok : Page
    {
        UserSetting set = new UserSetting();
        public PageSetClok()
        {
            InitializeComponent();
            try
            {
                DeSerial();
            }
            catch
            {
                Serial();
                DeSerial();
                MessageBox.Show("Произошла ошибка серилизации, задайте параметры в меню настройки");
            }
            FlafMGVS.IsChecked = set.FlagClok;
            delay.Text = set.DelayClok.ToString();
            linc.Text = set.LincClok.ToString();
            IpMGVS.Text = set.IpMGVS;
            PortMGVS.Text = set.PortMGVS;
        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting200(set);

        }
        private void DeSerial()
        {

            ClassSerilization.DeSerialUserSetting200(out set);



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
        }
        private void FlagMGVS_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagClok = true;
            PortMGVS.IsEnabled = true;
            IpMGVS.IsEnabled = true;
            delay.IsEnabled = true;
            linc.IsEnabled = true;

        }

        private void FlafMGVS_Unchecked(object sender, RoutedEventArgs e)
        {
            set.FlagClok = false;
            delay.IsEnabled = false;
            linc.IsEnabled = false;
            PortMGVS.IsEnabled = false;
            IpMGVS.IsEnabled = false;
        }

        private void delay_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.DelayClok = Convert.ToInt32(delay.Text);
        }

        private void linc_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.LincClok = Convert.ToInt32(linc.Text);
        }

        private void IpMGVS_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.IpMGVS = IpMGVS.Text;

        }

        private void PortMGVS_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.PortMGVS = PortMGVS.Text;
        }
    }
}
