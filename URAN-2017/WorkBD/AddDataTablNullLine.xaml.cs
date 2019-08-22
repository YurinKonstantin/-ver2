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
    /// Логика взаимодействия для AddDataTablNullLine.xaml
    /// </summary>
    public partial class AddDataTablNullLine : Window
    {
        public AddDataTablNullLine()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            int[] mas = new int[12];
            mas[0] = Convert.ToInt32(Сh1.Text);
            mas[1] = Convert.ToInt32(Сh2.Text);
            mas[2] = Convert.ToInt32(Сh3.Text);
            mas[3] = Convert.ToInt32(Сh4.Text);
            mas[4] = Convert.ToInt32(Сh5.Text);
            mas[5] = Convert.ToInt32(Сh6.Text);
            mas[6] = Convert.ToInt32(Сh7.Text);
            mas[7] = Convert.ToInt32(Сh8.Text);
            mas[8] = Convert.ToInt32(Сh9.Text);
            mas[9] = Convert.ToInt32(Сh10.Text);
            mas[10] = Convert.ToInt32(Сh11.Text);
            mas[11] = Convert.ToInt32(Сh12.Text);
            DataAccesBDBAAK.AddDataTablNullLine(new ViewTaiblBDBAAK.ClassNullLine() { namePSB = name.Text, nullLine=mas});
            this.DialogResult = true;
        }
    }
}
