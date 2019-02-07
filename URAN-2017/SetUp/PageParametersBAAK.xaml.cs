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

namespace URAN_2017
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
            DeSerial();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // string connectionString = "    Data Source=\"C:\\Users\\yurin\\Document\\Data1.mdb\";User " + "ID=Admin;Provider=\"Microsoft.JET.OLEDB.4.0\";    ";
           // string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\\Users\\yurin\\Documents\\Data1.mdb";
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + set.WaySetup;
            
            // Создание подключения
            // SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                var podg = new OleDbConnection(connectionString);
                // Открываем подключение
                // podg.ConnectionString = connectionString;
                podg.Open();
                MessageBox.Show("Подключение открыто");
                var camand = new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "Select * From [" + "Нулевая линия" + "] order by ИмяПлаты"
                };
                var chit = camand.ExecuteReader(CommandBehavior.CloseConnection);
                while (chit.Read() == true)
                {
                    if (Convert.ToString(chit.GetValue(1)) == "У1")
                    {
                        for (int i = 2; i < chit.FieldCount; i++)
                        {
                            

                                MessageBox.Show(Convert.ToString(chit.GetValue(i)));
                        }
                        break;
                    }
                    MessageBox.Show(Convert.ToString(chit.GetValue(1)));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение

                MessageBox.Show("Подключение закрыто...");
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
            using ( fs = new FileStream(md + "\\UranSetUp\\"+"setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
                System.Windows.MessageBox.Show("Сохранено");

            }
            fs.Close();
            UserSetting.Serial();

        }
        private void DeSerial()
        {try
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
                    using ( wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);

                    }
                    wr.Close();
                }
                catch (Exception)
                {

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка серилизации");
            }

        }

    }
}
