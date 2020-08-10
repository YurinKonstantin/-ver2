using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для WindowArduinoMenedger.xaml
    /// </summary>
    public partial class WindowArduinoMenedger : Window
    {
        public WindowArduinoMenedger()
        {
            InitializeComponent();
            //Button_Click_7(null, null);
            classArduinos.Add(new ClassArduino("Arduino 1", "Кластер 1", "192.168.2.201", 80, "192.168.2.161", "192.168.2.171", "192.168.2.181"));
            classArduinos.Add(new ClassArduino("Arduino 2", "Кластер 2", "192.168.2.203", 80, "192.168.2.162", "192.168.2.172", "192.168.2.182"));
            classArduinos.Add(new ClassArduino("Arduino 3", "Кластер 3", "192.168.2.202", 80, "192.168.2.163", "192.168.2.173", "192.168.2.183"));
            classArduinos.Add(new ClassArduino("Arduino 4", "Кластер 4", "192.168.2.205", 80, "192.168.2.164", "192.168.2.174", "192.168.2.184"));
            classArduinos.Add(new ClassArduino("Arduino 5", "Кластер 5", "192.168.2.204", 80, "192.168.2.165", "192.168.2.175", "192.168.2.185"));
            classArduinos.Add(new ClassArduino("Arduino 6", "Кластер 6", "192.168.2.206", 80, "192.168.2.166", "192.168.2.176", "192.168.2.186"));
            listArduino.ItemsSource = classArduinos;
        }
        const string quote = "\"";
        List<ClassArduino> classArduinos = new List<ClassArduino>();
        public string zapros(string iP, string port, string zapros)
        {
            string otvet = String.Empty;
            try
            {
                const string quote = "\"";


                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + iP + ":" + 80 + "/api/v1/URAN_Temp_and_Pressure/" + zapros) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";


                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        otvet = output.ToString();

                        response.Close();

                    }
                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    MessageBox.Show(ez.ToString());
                }

                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
                //labekSystemMessage.Items.Add("Ошибка: " + ex);
                MessageBox.Show(ex.ToString());
            }
            return otvet;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
          
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            classArduinos.Clear();
            classArduinos.Add(new ClassArduino("Arduino 1", "Кластер 1", "192.168.2.201", 80, "192.168.2.161", "192.168.2.171", "192.168.2.181"));
            classArduinos.Add(new ClassArduino("Arduino 2", "Кластер 2", "192.168.2.203", 80, "192.168.2.162", "192.168.2.172", "192.168.2.182"));
            classArduinos.Add(new ClassArduino("Arduino 3", "Кластер 3", "192.168.2.202", 80, "192.168.2.163", "192.168.2.173", "192.168.2.183"));
            classArduinos.Add(new ClassArduino("Arduino 4", "Кластер 4", "192.168.2.205", 80, "192.168.2.164", "192.168.2.174", "192.168.2.184"));
            classArduinos.Add(new ClassArduino("Arduino 5", "Кластер 5", "192.168.2.204", 80, "192.168.2.165", "192.168.2.175", "192.168.2.185"));
            classArduinos.Add(new ClassArduino("Arduino 6", "Кластер 6", "192.168.2.206", 80, "192.168.2.166", "192.168.2.176", "192.168.2.186"));
            foreach(var d in classArduinos)
            {
                MessageBox.Show(d.zaprosStatusStringOtvet());
            }
            listArduino.ItemsSource = classArduinos;
        }

        private void Bord1_Click(object sender, RoutedEventArgs e)
        {       try
                {
               
                var ard = (Button)sender;
                var dd = (ClassArduino)ard.DataContext;
               bool z= dd.restartBAAKAll();
                if(z)
                {
                    MessageBox.Show(dd.ArduinoName+" успешна перегрузила питания всех плат "+dd.Klaster);
                }
                else
                {
                   // MessageBox.Show(ard.ArduinoName + " не смогла выполнить перегрузку всех плат " + ard.Klaster);
                }


                }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var ard1 = (Button)sender;
                var ard = (ClassArduino)ard1.DataContext;
                bool z = ard.restartBAAK(1);
                if (z)
                {
                    MessageBox.Show(ard.ArduinoName + " успешна перегрузила питания платы " + ard.Klaster +" канала 1");
                }
                else
                {
                    MessageBox.Show(ard.ArduinoName + " не смогла выполнить перегрузку платы " + ard.Klaster + " канала 1");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var ard1 = (Button)sender;
                var ard = (ClassArduino)ard1.DataContext;
                bool z = ard.restartBAAK(2);
                if (z)
                {
                    MessageBox.Show(ard.ArduinoName + " успешна перегрузила питания платы " + ard.Klaster + " канала 2");
                }
                else
                {
                    MessageBox.Show(ard.ArduinoName + " не смогла выполнить перегрузку платы " + ard.Klaster + " канала 2");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                var ard1 = (Button)sender;
                var ard = (ClassArduino)ard1.DataContext;
                bool z = ard.restartBAAK(3);
                if (z)
                {
                    MessageBox.Show(ard.ArduinoName + " успешна перегрузила питания платы " + ard.Klaster + " канала 3");
                }
                else
                {
                    MessageBox.Show(ard.ArduinoName + " не смогла выполнить перегрузку платы " + ard.Klaster + " канала 3");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
