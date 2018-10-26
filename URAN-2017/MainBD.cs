using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    public partial class MainWindow
    {
        /// <summary>
        /// получение нулевых линий из бд
        /// </summary>
        private void BDselect1()
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + set.WaySetup;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {
                // Открываем подключение
                podg.Open();
                var camand = new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "select * from [Нулевая линия] order by Код desc"
                };
                var chit = camand.ExecuteReader();
                while (chit.Read() == true)
                {
                    MessageBox.Show(chit.FieldCount.ToString());
                    string d = chit.GetValue(1).ToString();
                    MessageBox.Show(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }
        /// <summary>
        /// Запис в бд информации о ран
        /// </summary>
        /// <param name="nameRan"></param>
        /// <param name="sinx"></param>
        /// <param name="allPorog"></param>
        /// <param name="porog"></param>
        /// <param name="trg"></param>
        /// <param name="time"></param>
        private void BDReadRAN(string nameRan, bool sinx, bool allPorog, uint porog, UInt32 trg, string time)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + set.WayDATABd;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {

                // Открываем подключение
                podg.Open();
                // MessageBox.Show("Подключение открыто");
                new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер, ЗначениеТаймера) VALUES (" + "'" + nameRan + "'" + "," + sinx + ", " + allPorog + "," + porog + ", " + trg + "," + "'" + time + "'" + ") "
                    // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                }.Connection = podg;
                new OleDbCommand
                {
                    Connection = podg,
                    CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер, ЗначениеТаймера) VALUES (" + "'" + nameRan + "'" + "," + sinx + ", " + allPorog + "," + porog + ", " + trg + "," + "'" + time + "'" + ") "
                    // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                }.ExecuteNonQuery();



            }
            catch 
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }
        /// <summary>
        /// добавление информации ран о времени пуска
        /// </summary>
        /// <param name="nameRan"></param>
        /// <param name="time"></param>
        private void BdAddRANTimeПуск(string nameRan, string time)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + set.WayDATABd;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {

                // Открываем подключение
                podg.Open();
                // MessageBox.Show("Подключение открыто");
                new OleDbCommand
                {
                    Connection = podg,
                    //CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер, ЗначениеТаймера) VALUES (" + "'" + nameRan + "'" + "," + sinx + ", " + allPorog + "," + porog + ", " + trg + "," + "'" + time + "'" + ") "
                    CommandText = "update [RAN] set ВремяЗапуска=" + "'" + time + "'" + " where НомерRAN=" + "'" + nameRan + "'" + ""
                }.Connection = podg;
                new OleDbCommand
                {
                    Connection = podg,
                    //CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер, ЗначениеТаймера) VALUES (" + "'" + nameRan + "'" + "," + sinx + ", " + allPorog + "," + porog + ", " + trg + "," + "'" + time + "'" + ") "
                    CommandText = "update [RAN] set ВремяЗапуска=" + "'" + time + "'" + " where НомерRAN=" + "'" + nameRan + "'" + ""
                }.ExecuteNonQuery();



            }
            catch 
            {
              
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }
        /// <summary>
        /// добавление информации ран о времени останова
        /// </summary>
        /// <param name="nameRan"></param>
        /// <param name="time"></param>
        private void BdAddRANTimeСтоп(string nameRan, string time)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + set.WayDATABd;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {

                // Открываем подключение
                podg.Open();
                // MessageBox.Show("Подключение открыто");
                var camand = new OleDbCommand
                {
                    Connection = podg,
                    //CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер, ЗначениеТаймера) VALUES (" + "'" + nameRan + "'" + "," + sinx + ", " + allPorog + "," + porog + ", " + trg + "," + "'" + time + "'" + ") "
                    CommandText = "update [RAN] set ВремяОстанова=" + "'" + time + "'" + " where НомерRAN=" + "'" + nameRan + "'" + ""
                };
                camand.Connection = podg;
                camand.ExecuteNonQuery();



            }
            catch 
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                podg.Close();

            }
        }
    }
}
