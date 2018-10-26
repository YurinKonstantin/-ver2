using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Data;
using System.Data.OleDb;
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

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetBAAK.xaml
    /// </summary>
    public partial class PageSetBAAK : Page
    {
        UserSetting set = new UserSetting();
        public PageSetBAAK()
        {
            InitializeComponent();
            DeSerial();
            WaySet.Text = set.WaySetup;
            list.ItemsSource = Bak._DataColec1;

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
            using ( fs = new FileStream(md + "\\UranSetUp\\"+"setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
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
                        wr.Close();

                    }
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Ошибка серилизации");
                }
            }
            catch(Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
        }

        private void WaySet_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.WaySetup = WaySet.Text;
        }

        private void ButWaySet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                Filter = "База данных(*.MDB;*.MDB;*.accdb)|*.MDB;*.MDB;*.ACCDB" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                WaySet.Text = myDialog.FileName;
            }

            
        }
    }
}
