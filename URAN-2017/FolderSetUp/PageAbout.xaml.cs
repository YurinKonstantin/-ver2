using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace URAN_2017.FolderSetUp
{
    /// <summary>
    /// Логика взаимодействия для PageAbout.xaml
    /// </summary>
    public partial class PageAbout : Page
    {
        public PageAbout()
        {
            InitializeComponent();
            //var version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
           // VersionNumber.Text = string.Format($"Version: {version.Major}.{version.Minor}.{version.Build}.{version.Revision}");
        }
      

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Process.Start(@"https://github.com/YurinKonstantin/URAN/issues/new/choose");
           // await Launcher.LaunchUriAsync(new Uri(@"https://github.com/YurinKonstantin/FolderFileCommander/issues/new/choose"));
        }
    }
}
