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
//settingProg - информация маин режим или нет
//ClassOtborNeutron - параметры отбора нейтрона в хвосте
//ClassTestRanSetting1 - набор тестовых наборов не вариации
//ClassTestRanSetting100 - набор тестовых наборов вариации
//settingBAAK12-100 - набор плат 100 для работы класс Bak
//setting100 -  настройки работы с вариацией, порог, триггер и тд

namespace URAN_2017
{
   public class ClassSerilization
    {
        public static void SerialProg(ClassSetUpProgram set)
        {
           
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(md + "\\UranSetUp\\" + "settingProg.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
               
                

            }

           

        }
      public static void DeSerial( out ClassSetUpProgram set)
        {
            set = new ClassSetUpProgram();
            try
            {
               
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                FileStream fs = new FileStream(md + "\\UranSetUp\\" + "settingProg.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                 set = (ClassSetUpProgram)bf.Deserialize(fs);
                 
                   
                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                }
                finally
                {
                    fs.Close();

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка серилизации"+ex.ToString());
            }

        }
        public static void SerialUserSetting200(UserSetting set)
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


        }
        public static void DeSerialUserSetting200(out UserSetting set)
        {
            set = new UserSetting();
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

        }

        /// <summary>
        /// Сохраняет настройки работы установки с платой 100 (порог, тригер, интервал и тд.)
        /// </summary>
        /// <param name="set"></param>
        public static void SerialUserSetting100(UserSetting set)
        {

            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(md + "\\UranSetUp\\" + "setting100.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);

                fs.Close();

            }


        }
        /// <summary>
        /// Извлекает из памяти настройки установки при работе с плаой 100
        /// </summary>
        /// <param name="set"></param>
        public static void DeSerialUserSetting100(out UserSetting set)
        {
            set = new UserSetting();
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            FileStream fs = new FileStream(md + "\\UranSetUp\\" + "setting100.dat", FileMode.Open);
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

        }
    }
}
