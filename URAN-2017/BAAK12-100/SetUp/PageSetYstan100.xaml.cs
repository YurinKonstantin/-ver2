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
    /// Логика взаимодействия для PageSetYstan100.xaml
    /// </summary>
    public partial class PageSetYstan100 : Page
    {
        public PageSetYstan100()
        {
            InitializeComponent();
            DeSerial();


            listNoTail100.ItemsSource = Bak._DataColecBAAK100;

            NameMS.Text = set.MS;
            if (set.MS1 == null)
            {
                set.MS1 = "192.168.2.191";
            }
            NameMS1.Text = set.MS1;
          
        }

        UserSetting set = new UserSetting();
     

      
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
               
                fs.Close();

            }
            UserSetting.Serial();
            MessageBox.Show("Сохранено");

        }
        private void DeSerial()
        {
            try
            {


                Bak.InstCol();
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                
               
                try
                {

                    md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
                    {
                        Bak._DataColecBAAK100 = (ObservableCollection<Bak>)xs.Deserialize(wr);

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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            AddKl100 winAddKl100 = new AddKl100();

            if (winAddKl100.ShowDialog() == true)
            {

                Bak.AddKl100(winAddKl100.Name2, winAddKl100.IP, winAddKl100.NameB);
             
                listNoTail100.ItemsSource = Bak._DataColecBAAK100;

            }
            else
            {

            }


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

    

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            object f = listNoTail100.SelectedItem;
            Bak bak = (Bak)f;

            int xx = Bak._DataColec1.IndexOf(bak);
            Bak.DelKl100(xx);
           
            listNoTail100.ItemsSource = Bak._DataColecBAAK100;
        }
    }
}
