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
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;

namespace URAN_2017
{
    public partial class MainWindow
    {
        UserSetting set = new UserSetting();
        ClassOtborNeutron otbN = new ClassOtborNeutron();
        public ObservableCollection<URAN_2017.FolderSetUp.Bak> _DataColec1;
        public ObservableCollection<Bak> _DataColecBAAK12100;
        public ObservableCollection<ClassTestRan> _DataColecClassTestRan;

        public async Task DeSerial200()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
      
            // using (FileStream fs = new FileStream("setting.dat", FileMode.Open)) 
     
            
            FileStream fs ;
            try
            {
                ClassSerilization.DeSerialUserSetting200(out set);

                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                {
                    _DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);

                }
                try
                {


                    XmlSerializer xs2 = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
                    {
                        _DataColecBAAK12100 = (ObservableCollection<Bak>)xs2.Deserialize(wr);

                    }
                    
                }
                catch(Exception ex)
                {

                }
                XmlSerializer xs1 = new XmlSerializer(typeof(ObservableCollection<ClassTestRan>));
                using (StreamReader wr1 = new StreamReader(md + "\\UranSetUp\\" + "ClassTestRanSetting1.xml"))
                {
                    _DataColecClassTestRan = (ObservableCollection<ClassTestRan>)xs1.Deserialize(wr1);

                }


            }

            catch (SerializationException)
            {
                MessageBox.Show("ошибка");
            }
            catch (Exception)
            {
                MessageBox.Show("ошибка");
            }
         
            finally
            {
               // fs.Close();


            }

            try
            {

                string md1 = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                FileStream fs1 = new FileStream(md1 + "\\UranSetUp\\" + "ClassOtborNeutron.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    otbN = (ClassOtborNeutron)bf.Deserialize(fs1);
                    

                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                    
                }
                finally
                {
                    fs1.Close();
                }

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }



        }
        public async Task DeSerial100()
        {
            ClassSerilization.DeSerialUserSetting100(out set);
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

            // using (FileStream fs = new FileStream("setting.dat", FileMode.Open)) 


            FileStream fs;
            try
            {
                ClassSerilization.DeSerialUserSetting100(out set);
                try
                {


                    XmlSerializer xs2 = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
                    {
                        _DataColecBAAK12100 = (ObservableCollection<Bak>)xs2.Deserialize(wr);

                    }
                   
                }
                catch (Exception ex)
                {

                }

                XmlSerializer xs1 = new XmlSerializer(typeof(ObservableCollection<ClassTestRan>));
                using (StreamReader wr1 = new StreamReader(md + "\\UranSetUp\\" + "ClassTestRanSetting100.xml"))
                {
                    _DataColecClassTestRan = (ObservableCollection<ClassTestRan>)xs1.Deserialize(wr1);

                }

            }

            catch (SerializationException)
            {
                MessageBox.Show("ошибка");
            }
            catch (Exception)
            {
                MessageBox.Show("ошибка");
            }

            finally
            {
                // fs.Close();


            }

         



        }

    }
}
