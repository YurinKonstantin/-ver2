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
    /// Логика взаимодействия для AddKl.xaml
    /// </summary>
    public partial class AddKl : Window
    {
        bool BAA12NoT;
        public AddKl()
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton rb)
            {
                string colorName = rb.Tag.ToString();
                switch (colorName)
                {
                    case "Yes":
                        BAA12NoT = false;
                        break;
                    case "No":
                        BAA12NoT = true;

                        break;
                }
            }

        }
    }
}
