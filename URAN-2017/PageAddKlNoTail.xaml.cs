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
    /// Логика взаимодействия для PageAddKlNoTail.xaml
    /// </summary>
    public partial class PageAddKlNoTail : Window
    {
        bool BAA12NoT;
        public PageAddKlNoTail()
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
        public bool BAAK12NoTail
        {
            get { return BAA12NoT; }
        }


    }
}
