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

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageTestRAN100.xaml
    /// </summary>
    public partial class PageTestRAN100 : Page
    {
        UserSetting set = new UserSetting();
        ClassTestRan test = new ClassTestRan();
        public PageTestRAN100()
        {
            InitializeComponent();
            DeSerial();
            BuFlagTestRan.Toggled1 = !set.FlagTestRan;
            listView2.ItemsSource = ClassTestRan._DataColec2;
        }
        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuFlagTestRan.Toggled1 == true)
            {

                set.FlagTestRan = true;
                
                // LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {

                set.FlagTestRan = false;
               
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }


        }

        private void Serial()
        {
            ClassSerilization.SerialUserSetting100(set);
            // UserSetting.Serial();
            ClassTestRan.Serial100();//сохраняем настройки методического набора и коллекцию

        }

        private void DeSerial()
        {
            try
            {
                
                ClassTestRan.InstCol();
            
                ClassSerilization.DeSerialUserSetting100(out set);

                try
                {
                    string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                    XmlSerializer xs1 = new XmlSerializer(typeof(ObservableCollection<ClassTestRan>));
                    using (StreamReader wr1 = new StreamReader(md + "\\UranSetUp\\" + "ClassTestRanSetting100.xml"))
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


    }

}
