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
    /// Логика взаимодействия для AddKl100.xaml
    /// </summary>
    public partial class AddKl100 : Window
    {
        public AddKl100()
        {
            InitializeComponent();
        }
    
     

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public string Name2
        {
            get { return name.Text; }
        }
        public string IP
        {
            get { return ip.Text; }
        }
        public string NameB
        {
            get { return nameB.Text; }
        }
        

     
    }
}
