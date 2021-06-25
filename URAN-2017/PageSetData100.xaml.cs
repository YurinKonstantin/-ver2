using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
using URAN_2017.FolderSetUp;
using URAN_2017.WorkBD;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetData100.xaml
    /// </summary>
    public partial class PageSetData100 : Page
    {
        UserSetting set = new UserSetting();
        public PageSetData100()
        {
            InitializeComponent();
            DeSerial();
            Way.Text = set.WayDATA;
            WayBd.Text = set.WayDATABd;
            TestWayBd.Text = set.TestWayDATABd;
            interval.Text = Convert.ToString(set.IntervalFile);
            BuchekTogleBinSave.Toggled1 = !set.FlagSaveBin;
            if (BuchekTogleBinSave.Toggled1 == true)
            {

              
                //  LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }
            
            BuchekTogleBinSave1.Toggled1 = !set.FlagSaveBD;
            if (BuchekTogleBinSave1.Toggled1 == true)
            {

                
                //  LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }
        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting100(set);
            UserSetting.SerialBAA12_100();

        }
        private void DeSerial()
        {
            try
            {


                Bak.InstCol();
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                ClassSerilization.DeSerialUserSetting100(out set);
                try
                {


                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "setting100.xml"))
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
                Filter = "База данных(*.MDB;*.MDB;*.accdb; *.db; *.db3)|*.MDB;*.MDB;*.ACCDB;*DB; *DB3;" + "|Все файлы (*.*)|*.* ",
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
                Filter = "База данных(*.MDB;*.MDB;*.accdb; *.db; *.db3)|*.MDB;*.MDB;*.ACCDB;*DB; *DB3;" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                TestWayBd.Text = myDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                var dir = new System.IO.DirectoryInfo(folderBrowser.SelectedPath);
                string pp = folderBrowser.SelectedPath + @"\BD_Data_100.db";
                DataAccesBDData.Path = folderBrowser.SelectedPath + @"\BD_Data_100.db";
                WayBd.Text = folderBrowser.SelectedPath;
                set.WaySetup = WayBd.Text;
                DataAccesBDData.CreateDB();
                DataAccesBDData.InitializeDatabase100();

            }
            System.Windows.MessageBox.Show("База данных создана");
        }







        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuchekTogleBinSave.Toggled1 == true)
            {

                set.FlagSaveBin = true;
                //  LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                set.FlagSaveBin = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }


        }
        private void Bu_MouseLeftButtonDownFMR1(object sender, MouseButtonEventArgs e)
        {
            if (BuchekTogleBinSave1.Toggled1 == true)
            {

                set.FlagSaveBD = true;
                // LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                set.FlagSaveBD = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }


        }
    }
}
