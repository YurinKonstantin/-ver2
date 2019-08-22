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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageTestRAN.xaml
    /// </summary>
    public partial class PageTestRAN : Page
    {
        UserSetting set = new UserSetting();
        ClassTestRan test = new ClassTestRan();
        public PageTestRAN()
        {
            InitializeComponent();
            DeSerial();
            FlagTestRan.IsChecked = set.FlagTestRan;
            listView2.ItemsSource = ClassTestRan._DataColec2;

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
            // UserSetting.Serial();
            ClassTestRan.Serial();//сохраняем настройки методического набора и коллекцию

        }
        private void DeSerial()
        {
            try
            {
                Bak.InstCol();
                ClassTestRan.InstCol();
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


                    XmlSerializer xs1 = new XmlSerializer(typeof(ObservableCollection<ClassTestRan>));
                    using (StreamReader wr1 = new StreamReader(md + "\\UranSetUp\\" + "ClassTestRanSetting1.xml"))
                    {
                        ClassTestRan._DataColec2 = (ObservableCollection<ClassTestRan>)xs1.Deserialize(wr1);

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка серилизации настроек тестового набора");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка серилизации общих настроик");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // test.Hors = "10";
            // test.Mins = "3";
            // test.IncAlam();
            // ClassTestRan.AddKl(test.Alam, "10", "3", "Fuf");
            AddTimeRanTest winAddTest = new AddTimeRanTest();

            if (winAddTest.ShowDialog() == true)
            {

                //Bak.AddKl(winAddKl.Name2, winAddKl.IP, winAddKl.NameB);
            }
            else
            {

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int eh = listView2.SelectedIndex;

            ClassTestRan.DelTestRan(eh);
        }

        private void FlagTestRan_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FlagTestRan_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
