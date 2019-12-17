using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;
using MessageBox = System.Windows.MessageBox;

namespace URAN_2017.WorkBD
{
    /// <summary>
    /// Логика взаимодействия для PageBDData.xaml
    /// </summary>
    public partial class PageBDData : Page
    {
        public PageBDData()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {

                var dir = new System.IO.DirectoryInfo(folderBrowser.SelectedPath);
                string pp = folderBrowser.SelectedPath + @"\BD_Data.db";
               DataAccesBDData.Path = folderBrowser.SelectedPath + @"\BD_Data.db";
                Path.Text = folderBrowser.SelectedPath;
                DataAccesBDData.CreateDB();
                DataAccesBDData.InitializeDatabase();
                RanGrid.ItemsSource = DataAccesBDData.GetDataRun();

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                Filter = "База данных(*.MDB;*.MDB;*.accdb; *.db; *.db3)|*.MDB;*.MDB;*.ACCDB;*DB; *DB3;" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                Path.Text = myDialog.FileName;
                DataAccesBDData.Path = myDialog.FileName;
               
                RanGrid.ItemsSource = DataAccesBDData.GetDataRun();
               


            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {



            DataAccesBDData.AddDataTablRun(new ViewTaiblBDData.ClassTablRun() { НомерRun = "d1dd", ВремяСтоп="dddgf", ВремяЗапуска="sss", ЗначениеТаймер="hhg",
                ОбщийПорог=1, Порог=10, Синхронизация=0, Триггер=2});
         
            RanGrid.ItemsSource = DataAccesBDData.GetDataRun();





        }
        private void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            Stream fs;
            using (fs = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
                System.Windows.MessageBox.Show("Сохранено");

            }
            fs.Close();
            UserSetting.Serial();

        }
        UserSetting set = new UserSetting();
        private void DeSerial()
        {
            try
            {


                Bak.InstCol();
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                FileStream fs = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    set = (UserSetting)bf.Deserialize(fs);

                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                }
                finally
                {
                    fs.Close();
                }
                try
                {


                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    StreamReader wr;
                    using (wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);

                    }
                    wr.Close();
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
    }
}
