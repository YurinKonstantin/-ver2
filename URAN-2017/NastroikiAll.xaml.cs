using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

using System.Windows.Navigation;

using Microsoft.Win32;
namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для NastroikiAll.xaml
    /// </summary>
    public partial class NastroikiAll : Window
    {
        private ObservableCollection<Data1> _DataColec;
        //  public ObservableCollection<Bak> _DataColec1;


        // UserSetting set = new UserSetting();


        public NastroikiAll()
        {
            InitializeComponent();


            _DataColec = new ObservableCollection<Data1>
            {
                new Data1 { Name = "Установка", Pyti = "" },
                new Data1 { Name = "Набор/Ran", Pyti = "" },
                new Data1 { Name = "Плата BAAK", Pyti = "" },
                new Data1 { Name = "Данные", Pyti = "" },
                new Data1 { Name = "Синхронизация", Pyti = "" },
                new Data1 { Name = "Параметры плат", Pyti = "" },
                new Data1 { Name = "Методический набор", Pyti = "" },
                new Data1 { Name = "Метод отбора", Pyti = "" }
            };
            listView1.ItemsSource = _DataColec;

            //frameName.NavigationService.Navigate(new Uri("PageSetYstan.xaml", UriKind.Relative));
            // SetYstanovka();
        }

        public struct Data1
        {
            public string Name { get; set; }
            public string Pyti { get; set; }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (listView1.SelectedIndex)
            {
                case 0:
                    SetYstanovka();
                    break;
                case 1:
                    SetRan();
                    break;
                case 2:
                    SetBAAK();
                    break;
                case 3:
                    SetData();
                    break;
                case 5:
                    SetParam();
                    break;
                case 4:
                    SetClok();
                    break;
                case 6:
                    SetTestRAN();
                    break;
                case 7:
                    SetOtbor();
                    break;

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();


        }
    }
}
