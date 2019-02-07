using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace URAN_2017
{
  public partial class NastroikiAll
    {
        
        public void SetBAAK()
        {
            frameName.NavigationService.Navigate(new Uri("PageSetBAAK.xaml", UriKind.Relative));
        }
        public void SetRan()
        {
            frameName.NavigationService.Navigate(new Uri("PageSetRan.xaml", UriKind.Relative));
        }

        public void SetYstanovka()
        {

            frameName.NavigationService.Navigate(new Uri("PageSetYstan.xaml", UriKind.Relative));
        }

        public void SetData()
        {
            frameName.NavigationService.Navigate(new Uri("PageSetData.xaml", UriKind.Relative));
        }
        public void SetParam()
        {
            frameName.NavigationService.Navigate(new Uri("PageParametersBAAK.xaml", UriKind.Relative));
        }
        public void SetClok()
        {
            frameName.NavigationService.Navigate(new Uri("PageSetClok.xaml", UriKind.Relative));
        }
        public void SetTestRAN()
        {
            frameName.NavigationService.Navigate(new Uri("PageTestRAN.xaml", UriKind.Relative));
        }
        public void SetOtbor()
        {
            frameName.NavigationService.Navigate(new Uri("PageOtbor.xaml", UriKind.Relative));
        }
    }
}

