using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
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
                   
                    string d = chit.GetValue(1).ToString();
                    
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
        private string BDselect11(string zaproc)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + BAAK12T.wayDataBD;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {
                // Открываем подключение
                podg.Open();
                var camand = new OleDbCommand
                {
                    Connection = podg,
                   // CommandText = "select * from [Нулевая линия] order by Код desc"
                   CommandText= zaproc
                };
                var chit = camand.ExecuteReader();
                string textBD=String.Empty;
                while (chit.Read() == true)
                {
                    
                    int x = chit.FieldCount;
                    for (int i = 0; i < x; i++)
                    {

                        textBD += chit.GetValue(i).ToString() +"\t";
                 
                       
                    }
                    textBD += "\n";
                  
                }
           
                return textBD;
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
            return String.Empty;
        }
        private  byte[] BDselect112(string zaproc)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + BAAK12T.wayDataBD;

            string sql = zaproc;
            DataTable results = new DataTable();
            string ss = String.Empty;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet("Datas");
                DataTable dt = new DataTable("Data");
                ds.Tables.Add(dt);
                adapter.Fill(ds.Tables["Data"]);

               

             
                ds.WriteXml(@"C:\Setup URAN\usersdb.xml");
                var buffer = File.ReadAllBytes(@"C:\\Setup URAN\usersdb.xml");
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    int x = 0;
                    foreach (object cell in row.ItemArray)
                    { }
                }
                        //results.WriteXml(@"C:\Setup URAN\usersdb.xml");
                        //  ss += results.TableName;

                        // перебор всех столбцов
                        //  foreach (DataColumn column in results.Columns)
                        //    {
                        // ss += column.ColumnName;
                        //  }

                        // ss += "\n";
                        // перебор всех строк таблицы
                        //   foreach (DataRow row in results.Rows)
                        //  {
                        // получаем все ячейки строки
                        //  var cells = row.ItemArray;
                        //  foreach (object cell in cells)
                        //  {
                        //     ss += cell + "\t"; ;
                        //   }

                        //  ss += "\n";
                        // }
                        return buffer;
                //  MessageBox.Show("Конец");
            }
            return null;


    
          
        }

        private string BDselect113(string zaproc)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + BAAK12T.wayDataBD;

            // Создание подключения
            var podg = new OleDbConnection(connectionString);
            try
            {
                String ss = String.Empty;
                // Открываем подключение
                podg.Open();
                var camand = new OleDbCommand
                {
                    Connection = podg,
                    // CommandText = "select * from [Нулевая линия] order by Код desc"
                    CommandText = zaproc
                };
                var chit = camand.ExecuteReader();
                string textBD = String.Empty;
                while (chit.Read() == true)
                {

                    int x = chit.FieldCount;
                    for (int i = 0; i < x; i++)
                    {
                        ss+= chit.GetValue(i).ToString() + "\t";
                        textBD += chit.GetValue(i).ToString() + "\t";


                    }
                    ss += "\n";
                    textBD += "\n";

                }

                MessageBox.Show("Конец");
                return textBD;
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
            return String.Empty;
        }
        private void BDReadСобытие(string nameFile, string nameBAAK, string time, string nameRan, int[] Amp, string nameklaster, int[] Nnut, int[] Nl, Double[] sig, bool test)
        {
            if (set.FlagSaveBD)
            {
                string connectionString;
                if (test)
                {
                    connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" +BAAK12T.wayDataTestBD;
                }
                else
                {
                    connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + BAAK12T.wayDataBD;
                }


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
                        CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1, АмплитудаКанал2, АмплитудаКанал3, АмплитудаКанал4, АмплитудаКанал5, АмплитудаКанал6, АмплитудаКанал7, АмплитудаКанал8, АмплитудаКанал9," +
                                        "АмплитудаКанал10, АмплитудаКанал11, АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                        "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                        + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                        + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                        + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                        + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                        + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                        + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1, АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                        "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                        "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                        + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                        + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                        + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                        + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                        + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                        + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                        "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12) VALUES (" +
                                        "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                        + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                        + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                        + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                        + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                        + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                        + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ")"
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Dispose();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "BDReadСобытие");
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();
                }
            }
        }
        private void BDselect114(string zaproc)
        {
            string connectionString = @"Data Source =" + BAAK12T.wayDataBD;

           

         
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(zaproc, connection);

                DataSet ds = new DataSet();
                string ss = String.Empty;
                adapter.Fill(ds);
                // перебор всех таблиц
                foreach (DataTable dt in ds.Tables)
                {
                    ss += dt.TableName; // название таблицы
                                        // перебор всех столбцов
                    foreach (DataColumn column in dt.Columns)
                    {
                        ss += column.ColumnName;
                    }

                    ss += "\n";
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        foreach (object cell in cells)
                            ss += cell;
                        ss += "\n";
                    }

                    MessageBox.Show("Конец");
                }
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
            if (set.FlagSaveBD)
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
        }
        /// <summary>
        /// добавление информации ран о времени пуска
        /// </summary>
        /// <param name="nameRan"></param>
        /// <param name="time"></param>
        private void BdAddRANTimeПуск(string nameRan, string time)
        {
            if (set.FlagSaveBD)
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
        }
        /// <summary>
        /// добавление информации ран о времени останова
        /// </summary>
        /// <param name="nameRan"></param>
        /// <param name="time"></param>
        private void BdAddRANTimeСтоп(string nameRan, string time)
        {
            if (set.FlagSaveBD)
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
}
