using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для PageSetYstan.xaml
    /// </summary>
    public partial class PageSetYstan : Page
    {
        UserSetting set = new UserSetting();
        public PageSetYstan()
        {

            InitializeComponent();
            DeSerial();
            var BakGroups1 = from user in Bak._DataColec1
                             where user.BAAK12NoT == false
                             orderby user.KLIP
                             select user;
            list1.ItemsSource = BakGroups1.ToList();
            var BakGroups2 = from user in Bak._DataColec1
                             where user.BAAK12NoT == true
                             select user;

            listNoTail.ItemsSource = BakGroups2.ToList();

            NameMS.Text = set.MS;
            if (set.MS1 == null)
            {
                set.MS1 = "192.168.2.191";
            }
            NameMS1.Text = set.MS1;
            auto.IsChecked = set.FlagAuto;
        }

        private void Auto_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagAuto = Convert.ToBoolean(auto.IsChecked);

        }
        private void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
                MessageBox.Show("Сохранено");
                fs.Close();

            }
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
                    MessageBox.Show("ошибка");
                }
                finally
                {
                    fs.Close();
                }
                try
                {

                    md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
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
                MessageBox.Show("Ошибка серилизации");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
        }
        private void NameMS_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.MS = NameMS.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            AddKl winAddKl = new AddKl();

            if (winAddKl.ShowDialog() == true)
            {

                Bak.AddKl(winAddKl.Name2, winAddKl.IP, winAddKl.NameB, winAddKl.BAAK12NoTail);
                var BakGroups1 = from user in Bak._DataColec1
                                 where user.BAAK12NoT == false
                                 orderby user.KLIP
                                 select user;
                list1.ItemsSource = BakGroups1.ToList();

            }
            else
            {

            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            object f = list1.SelectedItem;
            Bak bak = (Bak)f;

            int xx = Bak._DataColec1.IndexOf(bak);
            Bak.DelKl(xx);
            var BakGroups1 = from user in Bak._DataColec1
                             where user.BAAK12NoT == false
                             orderby user.KLIP
                             select user;
            list1.ItemsSource = BakGroups1.ToList();


        }

        private void auto_Unchecked(object sender, RoutedEventArgs e)
        {
            set.FlagAuto = Convert.ToBoolean(auto.IsChecked);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //  int eh = list.SelectedIndex;

            //Bak.DelKl(eh);
        }

        private void NameMS1_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.MS1 = NameMS1.Text;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PageAddKlNoTail winAddKl1 = new PageAddKlNoTail();

            if (winAddKl1.ShowDialog() == true)
            {

                Bak.AddKl(winAddKl1.Name2, winAddKl1.IP, winAddKl1.NameB, true);
                var BakGroups2 = from user in Bak._DataColec1
                                 where user.BAAK12NoT == true
                                 orderby user.KLIP
                                 select user;
                listNoTail.ItemsSource = BakGroups2.ToList();

            }
            else
            {

            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            object f = listNoTail.SelectedItem;
            Bak bak = (Bak)f;

            int xx = Bak._DataColec1.IndexOf(bak);
            Bak.DelKl(xx);
            var BakGroups2 = from user in Bak._DataColec1
                             where user.BAAK12NoT == true
                             orderby user.KLIP
                             select user;
            listNoTail.ItemsSource = BakGroups2.ToList();
        }
    }
}
