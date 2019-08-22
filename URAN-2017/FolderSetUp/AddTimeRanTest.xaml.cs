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
using URAN_2017;

namespace URAN_2017.FolderSetUp
{
    /// <summary>
    /// Логика взаимодействия для AddTimeRanTest.xaml
    /// </summary>
    public partial class AddTimeRanTest : Window
    {
        ClassTestRan test1 = new ClassTestRan();
        public AddTimeRanTest()
        {
            InitializeComponent();
            kolSob.IsEnabled = false;
            interval.IsEnabled = false;
            dlit.IsEnabled = true;
            porog.IsEnabled = true;
            trig.IsEnabled = true;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            test1.Hors = hors.Text;
            test1.Mins = min.Text;
            test1.IncAlam();
            if (Convert.ToBoolean(rad1.IsChecked))
            {
                test1.TipTest = "По длительности";
                test1.Dlit = Convert.ToInt32(dlit.Text);
                test1.Porog = Convert.ToInt32(porog.Text);
                test1.Trig = Convert.ToInt32(trig.Text);
                test1.ProgramTrigTest = false;
            }
            else
            {
                test1.TipTest = "По количеству";
                test1.Kolsob = Convert.ToInt32(kolSob.Text);
                test1.Interval = Convert.ToInt32(interval.Text);
                test1.ProgramTrigTest = true;
            }
            ClassTestRan.AddTestRan(test1.Alam, test1.Hors, test1.Mins, test1.TipTest, test1.Dlit, test1.Porog, test1.Trig, test1.Kolsob, test1.Interval, test1.ProgramTrigTest);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            kolSob.IsEnabled = false;
            interval.IsEnabled = false;
            dlit.IsEnabled = true;
            porog.IsEnabled = true;
            trig.IsEnabled = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            kolSob.IsEnabled = true;
            interval.IsEnabled = true;
            dlit.IsEnabled = false;
            porog.IsEnabled = false;
            trig.IsEnabled = false;
        }
    }
}
