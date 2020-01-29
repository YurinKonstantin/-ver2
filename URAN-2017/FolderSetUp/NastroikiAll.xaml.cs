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
using URAN_2017.FolderSetUp;
using URAN_2017.WorkBD;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для NastroikiAll.xaml
    /// </summary>
    public partial class NastroikiAll : Window
    {
        private ObservableCollection<Data1> _DataColec;
        //  public ObservableCollection<Bak> _DataColec1;


       ClassSetUpProgram set;

      
        public NastroikiAll()
        {
            InitializeComponent();

            ClassSerilization.DeSerial(out set);
          
        
            BuFMR.Toggled1 = !set.FlagMainRezim;
            if (BuFMR.Toggled1 == true)
            {

             

                _DataColec = new ObservableCollection<Data1>
            {
                new Data1 { Name = "Установка", Pyti = "", Img="/ImgSourse/stars18dp.png" },
                new Data1 { Name = "Набор/Ran", Pyti = "", Img="/ImgSourse/bike8dp.png" },
                new Data1 { Name = "Плата BAAK", Pyti = "",  Img="/ImgSourse/board18dp.png"  },
                new Data1 { Name = "Данные", Pyti = "", Img="/ImgSourse/assess18dp.png"  },
                new Data1 { Name = "Синхронизация", Pyti = "", Img="/ImgSourse/alarm18dp.png" },
                new Data1 { Name = "Параметры плат", Pyti = "",  Img="/ImgSourse/list18dp.png"  },
                new Data1 { Name = "Методический набор", Pyti = "",  Img="/ImgSourse/widg18dp.png"  },
                new Data1 { Name = "Метод отбора", Pyti = "",  Img="/ImgSourse/tur18dp.png" },
                new Data1 { Name = "Визуализация", Pyti = "",  Img="/ImgSourse/tur18dp.png" },
                new Data1 { Name = "BD_Data", Pyti = "",  Img="/ImgSourse/list18dp.png" }
            };
                listView1.ItemsSource = _DataColec;

            }
            else
            {
              //  ClassSetUpProgram.FlagMainRezim = false;
               
                
                _DataColec = new ObservableCollection<Data1>
            {
                new Data1 { Name = "Установка", Pyti = "", Img="/ImgSourse/stars18dp.png" },
                new Data1 { Name = "Набор/Ran", Pyti = "", Img="/ImgSourse/bike8dp.png" },
                new Data1 { Name = "Плата BAAK", Pyti = "",  Img="/ImgSourse/board18dp.png"  },
                new Data1 { Name = "Данные", Pyti = "", Img="/ImgSourse/assess18dp.png"  },
                new Data1 { Name = "Синхронизация", Pyti = "", Img="/ImgSourse/alarm18dp.png" },
                new Data1 { Name = "Параметры плат", Pyti = "",  Img="/ImgSourse/list18dp.png"  },
                new Data1 { Name = "Методический набор", Pyti = "",  Img="/ImgSourse/widg18dp.png"  }
                
            };
                listView1.ItemsSource = _DataColec;

            }
          
            //frameName.NavigationService.Navigate(new Uri("PageSetYstan.xaml", UriKind.Relative));
            // SetYstanovka();
        }
      
        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuFMR.Toggled1 == true)
            {

                set.FlagMainRezim = true;
           
                _DataColec = new ObservableCollection<Data1>
            {
                new Data1 { Name = "Установка", Pyti = "", Img="/ImgSourse/stars18dp.png" },
                new Data1 { Name = "Набор/Ran", Pyti = "", Img="/ImgSourse/bike8dp.png" },
                new Data1 { Name = "Плата BAAK", Pyti = "",  Img="/ImgSourse/board18dp.png"  },
                new Data1 { Name = "Данные", Pyti = "", Img="/ImgSourse/assess18dp.png"  },
                new Data1 { Name = "Синхронизация", Pyti = "", Img="/ImgSourse/alarm18dp.png" },
                new Data1 { Name = "Параметры плат", Pyti = "",  Img="/ImgSourse/list18dp.png"  },
                new Data1 { Name = "Методический набор", Pyti = "",  Img="/ImgSourse/widg18dp.png"  },
                new Data1 { Name = "Метод отбора", Pyti = "",  Img="/ImgSourse/tur18dp.png" },
                   new Data1 { Name = "Визуализация", Pyti = "",  Img="/ImgSourse/tur18dp.png" }
            };
                listView1.ItemsSource = _DataColec;


            }
            else
            {
                set.FlagMainRezim = false;
              
                _DataColec = new ObservableCollection<Data1>
            {
                new Data1 { Name = "Установка", Pyti = "", Img="/ImgSourse/stars18dp.png" },
                new Data1 { Name = "Набор/Ran", Pyti = "", Img="/ImgSourse/bike8dp.png" },
                new Data1 { Name = "Плата BAAK", Pyti = "",  Img="/ImgSourse/board18dp.png"  },
                new Data1 { Name = "Данные", Pyti = "", Img="/ImgSourse/assess18dp.png"  },
                new Data1 { Name = "Синхронизация", Pyti = "", Img="/ImgSourse/alarm18dp.png" },
                new Data1 { Name = "Параметры плат", Pyti = "",  Img="/ImgSourse/list18dp.png"  },
                new Data1 { Name = "Методический набор", Pyti = "",  Img="/ImgSourse/widg18dp.png"  }
               
            };
                listView1.ItemsSource = _DataColec;

            }
            ClassSerilization.SerialProg(set);
            ListView1_SelectionChanged(null, null);

        }
        public struct Data1
        {
            public string Name { get; set; }
            public string Pyti { get; set; }
            public string Img { get; set; }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (listView1.SelectedIndex)
            {
                case 0:
                    if(set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetYstan.xaml", UriKind.Relative));
                    }
                  else
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetYstan100.xaml", UriKind.Relative));
                    }
                   


                    break;
                case 1:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetRan.xaml", UriKind.Relative));
                    }
                    else
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetRan100.xaml", UriKind.Relative));
                    }
                   
                    break;
                case 2:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("FolderSetUp/PageSetBAAK.xaml", UriKind.Relative));
                    }
                    else
                    {
                        frameName.NavigationService.Navigate(new Uri("FolderSetUp/PageSetBAAK100.xaml", UriKind.Relative));
                    }
                
                    break;
                case 3:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetData.xaml", UriKind.Relative));
                    }
                    else
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetData100.xaml", UriKind.Relative));
                    }
             
                    break;
                case 5:                
                        frameName.NavigationService.Navigate(new Uri("WorkBD/PageParametersBAAK.xaml", UriKind.Relative));
                    break;
                case 4:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetClok.xaml", UriKind.Relative));
                    }
                    else
                    {
                        frameName.NavigationService.Navigate(new Uri("PageSetClok100.xaml", UriKind.Relative));
                    }
                   
                    break;
                case 6:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("PageTestRAN.xaml", UriKind.Relative));
                    }
                    else
                    {
                        frameName.NavigationService.Navigate(new Uri("PageTestRAN100.xaml", UriKind.Relative));
                    }
             
                    break;
                case 7:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("FolderSetUp/PageOtbor.xaml", UriKind.Relative));
                    }
                    else
                    {

                    }
                 
                    break;
                case 8:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("FolderSetUp/PageVizyal.xaml", UriKind.Relative));
                    }
                    else
                    {

                    }
                    break;
                case 9:
                    if (set.FlagMainRezim)
                    {
                        frameName.NavigationService.Navigate(new Uri("WorkBD/PageBDData.xaml", UriKind.Relative));
                    }
                    else
                    {

                    }

                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();


        }
    }
}
