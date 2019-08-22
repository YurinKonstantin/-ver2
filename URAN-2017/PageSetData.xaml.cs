using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using ToggleSwitch;
using URAN_2017.FolderSetUp;


namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetData.xaml
    /// </summary>
    public partial class PageSetData : Page
    {
        UserSetting set = new UserSetting();
        public PageSetData()
        {
            InitializeComponent();
            DeSerial();
            Way.Text = set.WayDATA;
            WayBd.Text = set.WayDATABd;
            TestWayBd.Text = set.TestWayDATABd;
            interval.Text = Convert.ToString(set.IntervalFile);
            chekTogleBinSave.IsChecked = set.FlagSaveBin;
            chekTogleBinSave1.IsChecked = set.FlagSaveBD;

        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting200(set);
            UserSetting.Serial();

        }
        private void DeSerial()
        {
            try
            {


                Bak.InstCol();
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                ClassSerilization.DeSerialUserSetting200(out set);
                try
                {


                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);
                        wr.Close();

                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }

        }
        private void ButWay_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {

                var dir = new System.IO.DirectoryInfo(folderBrowser.SelectedPath);

                Way.Text = folderBrowser.SelectedPath;
            }
        }

        private void Way_TextChanged(object sender, TextChangedEventArgs e)
        {

            set.WayDATA = Way.Text;
        }
        private void WayBd_TextChanged(object sender, TextChangedEventArgs e)
        {

            set.WayDATABd = WayBd.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
        }

        private void Interval_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.IntervalFile = Convert.ToInt32(interval.Text);
            }
            catch
            {

            }
        }

        private void ButWayBd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                Filter = "База данных(*.MDB;*.MDB;*.accdb)|*.MDB;*.MDB;*.ACCDB" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                WayBd.Text = myDialog.FileName;
            }
        }

        private void TestWayBd_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.TestWayDATABd = TestWayBd.Text;
        }

        private void TestButWayBd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                Filter = "База данных(*.MDB;*.MDB;*.accdb)|*.MDB;*.MDB;*.ACCDB" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                TestWayBd.Text = myDialog.FileName;
            }
        }

        private void HorizontalToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {

            ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
            set.FlagSaveBin = rb.IsChecked;
            if (rb != null && Way != null)
            {
                Way.IsEnabled = true;
                ButWay.IsEnabled = true;
                labP.IsEnabled = true;
                labS.IsEnabled = true;
            }
        }



        private void ChekTogleBinSave_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
            if (rb != null && Way != null)
            {
               set.FlagSaveBin = rb.IsChecked;

            }
        }

        private void ChekTogleBinSave1_Checked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
            set.FlagSaveBD = rb.IsChecked;
            if (rb != null && WayBd != null)
            {
                WayBd.IsEnabled = true;

            }
        }

        private void ChekTogleBinSave1_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
            if (rb != null && Way != null)
            {
                set.FlagSaveBD = rb.IsChecked;
                WayBd.IsEnabled = false;

            }
        }
    }
}
