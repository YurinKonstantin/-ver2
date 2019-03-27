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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetClok100.xaml
    /// </summary>
    public partial class PageSetClok100 : Page
    {
        UserSetting set = new UserSetting();
        public PageSetClok100()
        {
            InitializeComponent();
            try
            {
                DeSerial();
            }
            catch(Exception )
            {
                Serial();
                DeSerial();
                MessageBox.Show("Произошла ошибка серилизации, задайте параметры в меню настройки");
            }
            BuFlafMGVS.Toggled1 = !set.FlagClok;
        
            delay.Text = set.DelayClok.ToString();
            linc.Text = set.LincClok.ToString();
            IpMGVS.Text = set.IpMGVS;
            PortMGVS.Text = set.PortMGVS;
            
        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting100(set);

        }
        private void DeSerial()
        {

            ClassSerilization.DeSerialUserSetting100(out set);



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
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
        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuFlafMGVS.Toggled1 == true)
            {

                set.FlagClok = true;
                PortMGVS.IsEnabled = true;
                IpMGVS.IsEnabled = true;
                delay.IsEnabled = true;
                linc.IsEnabled = true;
                // LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                
                set.FlagClok = false;
                delay.IsEnabled = false;
                linc.IsEnabled = false;
                PortMGVS.IsEnabled = false;
                IpMGVS.IsEnabled = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }


        }
    }
}
