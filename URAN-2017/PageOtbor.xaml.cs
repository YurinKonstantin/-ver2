using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageOtbor.xaml
    /// </summary>
    public partial class PageOtbor : Page
    {
        ClassOtborNeutron otb = new ClassOtborNeutron();
        public PageOtbor()
        {
            InitializeComponent();
            DeSerial();
            DlitNeu.Text = otb.Dlit.ToString();
            PorogNeutrona.Text = otb.Porog.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
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
            using (fs = new FileStream(md + "\\UranSetUp\\" + "ClassOtborNeutron.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, otb);
                System.Windows.MessageBox.Show("Сохранено");

            }
            fs.Close();
           

        }
        private void DeSerial()
        {
            try
            {
                
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                FileStream fs = new FileStream(md + "\\UranSetUp\\" + "ClassOtborNeutron.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    otb = (ClassOtborNeutron)bf.Deserialize(fs);
                    fs.Close();

                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                    fs.Close();
                }
                finally
                {
                    fs.Close();
                }
              
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }

        }

        private void DlitNeu_TextChanged(object sender, TextChangedEventArgs e)
        {
            otb.Dlit = Convert.ToInt32(DlitNeu.Text);
        }

        private void PorogNeutrona_TextChanged(object sender, TextChangedEventArgs e)
        {
            otb.Porog = Convert.ToInt32(PorogNeutrona.Text);
        }
    }
}
