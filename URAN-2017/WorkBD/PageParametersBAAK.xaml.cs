using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;
using URAN_2017;
using System.Windows.Forms;
using URAN_2017.WorkBD.ViewTaiblBDBAAK;

namespace URAN_2017.WorkBD
{
    /// <summary>
    /// Логика взаимодействия для PageParametersBAAK.xaml
    /// </summary>
    public partial class PageParametersBAAK : Page
    {
        UserSetting set = new UserSetting();
        public PageParametersBAAK()
        {
            InitializeComponent();
           // DeSerial();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           
            AddDataTablBAAK addDataTablBAAK = new AddDataTablBAAK();


            if (addDataTablBAAK.ShowDialog() == true)
            {

                BAAKGrid.ItemsSource = DataAccesBDBAAK.GetDataBAAK();
              


            }
            else
            {

            }

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
                DataAccesBDBAAK.Path = myDialog.FileName;
                BAAKGrid.ItemsSource = DataAccesBDBAAK.GetDataBAAK();
                BAAKGrid1.ItemsSource = DataAccesBDBAAK.GetDataNullLine();


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {

                var dir = new System.IO.DirectoryInfo(folderBrowser.SelectedPath);
                string pp = folderBrowser.SelectedPath + @"\BD_BAAK12.db";
                DataAccesBDBAAK.Path = folderBrowser.SelectedPath + @"\BD_BAAK12.db";
                Path.Text = folderBrowser.SelectedPath;
                DataAccesBDBAAK.CreateDBBAAK();
                DataAccesBDBAAK.InitializeDatabase();
                BAAKGrid.ItemsSource = DataAccesBDBAAK.GetDataBAAK();

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ClassTablPSBBAAK d = (ClassTablPSBBAAK)BAAKGrid.SelectedItem;
            if(d!=null)
            {
                DataAccesBDBAAK.DeletePSB(d.namePSB);
            }
            else
            {
                System.Windows.MessageBox.Show("Выделенных строк для удаления нет", "Процесс удаления строки");
            }
            BAAKGrid.ItemsSource = DataAccesBDBAAK.GetDataBAAK();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            AddDataTablNullLine addDataTablBAAK = new AddDataTablNullLine();


            if (addDataTablBAAK.ShowDialog() == true)
            {

                BAAKGrid1.ItemsSource = DataAccesBDBAAK.GetDataNullLine();



            }
            else
            {

            }
        
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ClassNullLine d = (ClassNullLine)BAAKGrid1.SelectedItem;
            if (d != null)
            {
                DataAccesBDBAAK.DeleteNullLine(d.namePSB);
            }
            else
            {
                System.Windows.MessageBox.Show("Выделенных строк для удаления нет", "Процесс удаления строки");
            }
            BAAKGrid1.ItemsSource = DataAccesBDBAAK.GetDataNullLine();
        }
    }
}
