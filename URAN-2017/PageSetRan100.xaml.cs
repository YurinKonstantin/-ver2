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
    /// Логика взаимодействия для PageSetRan100.xaml
    /// </summary>
    public partial class PageSetRan100 : Page
    {
        UserSetting set = new UserSetting();
        ClassSetUpProgram setP = new ClassSetUpProgram();
        public PageSetRan100()
        {
            InitializeComponent();
            ClassSerilization.DeSerial(out setP);
            DeSerial();
           
        
          
            porogNO.Text = set.Porog.ToString();
       
           
            trgNO.Text = set.Trg.ToString();
          
            BuFlagMS.Toggled1 = !set.FlagMS;
            if (BuFlagMS.Toggled1 == true)
            {

                set.FlagMS = true;
                //  LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                set.FlagMS = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }
            lenght.Text = set.DataLenght.ToString();
         
            BuFlagMS.Toggled1= !set.FlagMS;

            if (BuFlagTestRan.Toggled1 == true)
            {

               // set.FlagMainRezim = true;
                // LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
              //  set.FlagMainRezim = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }
            list.ItemsSource = Bak._DataColecBAAK100;
            
        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting100(set);
            UserSetting.SerialBAA12_100();
            MessageBox.Show("Сохранено"+set.Trg.ToString());

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

                    md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
                    {
                        Bak._DataColecBAAK100 = (ObservableCollection<Bak>)xs.Deserialize(wr);

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка серилизации");
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

      

        private void Lenght_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.DataLenght = Convert.ToInt32(lenght.Text);
            }
            catch (Exception)
            {

            }
        }


        private void porogNO_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.Porog = Convert.ToInt32(porogNO.Text);
            }
            catch (Exception)
            {

            }
        }

        private void trgNO_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.Trg = Convert.ToUInt32(trgNO.Text);
            }
            catch
            {

            }
        }
        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuFlagMS.Toggled1 == true)
            {

                set.FlagMS = true;
                //  LabFlagMainR.Content = "Вкл";
                // LabFlagMainR.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                set.FlagMS = false;
                //  LabFlagMainR.Content = "Выкл";
                //  LabFlagMainR.Foreground = System.Windows.Media.Brushes.Red;

            }
          

        }
        private void Bu_MouseLeftButtonDownFMR1(object sender, MouseButtonEventArgs e)
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



    }
}
