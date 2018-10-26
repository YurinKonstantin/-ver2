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
    /// Логика взаимодействия для PageSetRan.xaml
    /// </summary>
    public partial class PageSetRan : Page
    {
        UserSetting set = new UserSetting();
        public PageSetRan()
        {
            InitializeComponent();
            DeSerial();
            Allporog.IsChecked = Convert.ToBoolean(set.FlagPorog);
            FlagIspol.IsChecked = Convert.ToBoolean(set.FlagOtob);
            porog.Text = set.Porog.ToString();
            porogNO.Text = set.PorogNO.ToString();
            AllTrg.IsChecked = Convert.ToBoolean(set.FlagTrg);
            trg.Text = set.Trg.ToString();
            trgNO.Text = set.TrgNO.ToString();
            FlagMS.IsChecked = set.FlagMS;
            lenght.Text = set.DataLenght.ToString();
            FlagTestRan.IsChecked = set.FlagTestRan;     
            list.ItemsSource = Bak._DataColec1;
            dickTail.Text = Convert.ToString(set.Discret);
        }

        private void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(md + "\\UranSetUp\\"+"setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    bf.Serialize(fs, set);
                    System.Windows.MessageBox.Show("Сохранено");
                }
                catch
                {

                }
                finally
                {
                    fs.Close();
                }

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
                    System.Windows.MessageBox.Show("ошибка");
                }
                finally
                {
                    fs.Close();
                   
                }
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка серилизации");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка серилизации");
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagPorog = Convert.ToBoolean(Allporog.IsChecked);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
        }

        private void AllTrg_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagTrg = Convert.ToBoolean(AllTrg.IsChecked);
        }

        private void Trg_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.Trg = Convert.ToUInt32(trg.Text);
            }
            catch
            {

            }
        }

        private void Porog_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
              set.Porog = Convert.ToInt32(porog.Text);
            }
                catch (Exception )
            {

            }
        }

        private void Lenght_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.DataLenght = Convert.ToInt32(lenght.Text);
            }
            catch (Exception )
            {

            }
        }

        private void FlagMS_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagMS = true;
        }
        private void FlagMS_UnChecked(object sender, RoutedEventArgs e)
        {
            set.FlagMS = false;
        }

        private void FlagTestRan_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagTestRan = true;
        }
        private void FlagTestRan_UnChecked(object sender, RoutedEventArgs e)
        {
            set.FlagTestRan = false;
        }
        private void HorizontalToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            set.FlagPorog = Convert.ToBoolean(Allporog.IsChecked);
        }

        private void HorizontalToggleSwitch_Unchecked_1(object sender, RoutedEventArgs e)
        {
            set.FlagTrg = Convert.ToBoolean(AllTrg.IsChecked);
        }

        private void dickTail_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.Discret = Convert.ToUInt32(dickTail.Text);
        }

        private void porogNO_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.PorogNO = Convert.ToInt32(porogNO.Text);
            }
            catch (Exception)
            {

            }
        }

        private void trgNO_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                set.TrgNO = Convert.ToUInt32(trgNO.Text);
            }
            catch
            {

            }
        }

        private void FlagIspol_Checked(object sender, RoutedEventArgs e)
        {
            set.FlagOtob = true;
        }

        private void FlagIspol_Unchecked(object sender, RoutedEventArgs e)
        {
            set.FlagOtob = false;
        }
    }
}
