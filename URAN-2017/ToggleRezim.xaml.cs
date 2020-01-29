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
    /// Логика взаимодействия для ToggleRezim.xaml
    /// </summary>
    public partial class ToggleRezim : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush Off = new SolidColorBrush(Colors.Red);
        SolidColorBrush On = new SolidColorBrush(Colors.Green);
        private bool Toggled = false;
        public bool On1 = false;
        public ToggleRezim()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }
        public bool Toggled1
        {
            get
            {
                return Toggled;


            }

            set
            {
                Toggled = value;
                Dot_MouseLeftButtonDown(null, null);
            }
        }

        public void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = LeftSide;
                On1 = true;


            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
               
                Dot.Margin = RightSide;
                On1 = false;


            }




        }

        public void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;
                On1 = true;


            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;
                On1 = false;


            }

        }
    }
}
