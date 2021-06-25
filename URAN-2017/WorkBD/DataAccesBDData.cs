using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using URAN_2017.WorkBD.ViewTaiblBDData;

namespace URAN_2017.WorkBD
{
    public static class DataAccesBDData
    {
        public static string Path { get; set; }
      
        static public async void CreateDB()
        {
            SQLiteConnection.CreateFile(Path);
        }

        public static void InitializeDatabase()
        {
            
            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);
        
            db.Open();
            String tableCommand =String.Empty;
            //  tableCommand = @"CREATE TABLE [workers] (
            //     [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
            //   [name] char(100) NOT NULL,
            //   [family] char(100) NOT NULL,
            //   [age] int NOT NULL,
            //   [profession] char(100) NOT NULL
            //   );"
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS Run (Primary_Key INTEGER PRIMARY KEY, " +
                "НомерRun char(30) NOT NULL, Синхронизация int NOT NULL, ОбщийПорог int NOT NULL, Порог int NOT NULL, Триггер int NOT NULL, ЗначениеТаймера char(25), " +
                "ВремяЗапуска char(25), ВремяСтоп char(25))";
                createTable.ExecuteReader();// 18.12.2019 14:18:52:818
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                tableCommand = "CREATE TABLE IF NOT " +
                  "EXISTS Файлы (Primary_Key INTEGER PRIMARY KEY, " +
                  "ИмяФайла char(30) NOT NULL, Плата char(2) NOT NULL, ВремяСоздания char(25), ВремяЗакрытия char(25), Run char(30))";
                createTable.ExecuteReader();//19.12.2019 00.10.23
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS События (Primary_Key INTEGER PRIMARY KEY, Время NVARCHAR(256) NOT NULL, ИмяФайла char(25) NOT NULL, Плата char(25), Кластер char(25), СумАмп int, СумN int, " +
                "АмпCh1 int, АмпCh2 int, АмпCh3 int, АмпCh4 int, АмпCh5 int, АмпCh6 int, АмпCh7 int, АмпCh8 int, АмпCh9 int, АмпCh10 int, АмпCh11 int, АмпCh12 int, " +
                "NCh1 int, NCh2 int, NCh3 int, NCh4 int, NCh5 int, NCh6 int, NCh7 int, NCh8 int, NCh9 int, NCh10 int, NCh11 int, NCh12 int, " +
                "Nul1 int, Nul2 int, Nul3 int, Nul4 int, Nul5 int, Nul6 int, Nul7 int, Nul8 int, Nul9 int, Nul10 int, Nul11 int, Nul12 int, " +
                "s1 char(10), s2 char(10), s3 char(10), s4 char(10), s5 char(10), s6 char(10), s7 char(10), s8 char(10), s9 char(10), s10 char(10), s11 char(10), s12 char(10), bad int)";
                createTable.ExecuteReader();
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {

                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS Нейтроны (Primary_Key INTEGER PRIMARY KEY, " +
                "ИмяФайла char(2) NOT NULL, Время NVARCHAR(256) NOT NULL, Dn int, Амп int, TimeFirst int," +
                "int, TimeFirst3 int, TimeEnd3 int, TimeAmp int, bad int)";
                createTable.ExecuteReader();
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {

                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS Темп (Primary_Key INTEGER PRIMARY KEY, " +
                "Плата char(25) NOT NULL, Кластер char(25) NOT NULL, Temp char(25) NOT NULL, Data NVARCHAR(256) NOT NULL, Time NVARCHAR(256) NOT NULL)";
                createTable.ExecuteReader();
            }
           
            db.Close();
        }
        //AmpCh1 - Максимальное значение аплитуды канала 1
        //TmaxCh1 - Время максимума амплитуды в отсчетов АЦП
        //TfACh1 - Время начала амплитуды
        //TeACh1 - Время конца амплитуды
        //TA1/2Ch1 - Время половины максимума амплитуды
        //TA1/4Ch1 - Время одной четвертой амплитуды
        //TA3/4Ch1 - Время три четверти амплитуды
        public static void InitializeDatabase100()
        {

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

            db.Open();
            String tableCommand = String.Empty;
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS Run (Primary_Key INTEGER PRIMARY KEY, " +
                "НомерRun char(30) NOT NULL, Синхронизация int NOT NULL, ОбщийПорог int NOT NULL, Порог int NOT NULL, Триггер int NOT NULL, ЗначениеТаймера char(25), " +
                "ВремяЗапуска char(25), ВремяСтоп char(25))";
                createTable.ExecuteReader();// 18.12.2019 14:18:52:818
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                tableCommand = "CREATE TABLE IF NOT " +
                  "EXISTS Файлы (Primary_Key INTEGER PRIMARY KEY, " +
                  "ИмяФайла char(30) NOT NULL, Плата char(2) NOT NULL, ВремяСоздания char(25), ВремяЗакрытия char(25), Run char(30))";
                createTable.ExecuteReader();//19.12.2019 00.10.23
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {

                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS События (Primary_Key INTEGER PRIMARY KEY, Время NVARCHAR(256) NOT NULL, Run char(25) NOT NULL, ИмяФайла char(25) NOT NULL, Плата char(25), Кластер char(25), СумD int, " +
                "АмпCh1 int, АмпCh2 int, АмпCh3 int, АмпCh4 int, АмпCh5 int, АмпCh6 int, АмпCh7 int, АмпCh8 int, АмпCh9 int, АмпCh10 int, АмпCh11 int, АмпCh12 int, " +
                "TmaxACh1 int, TmaxACh2 int, TmaxACh3 int, TmaxACh4 int, TmaxACh5 int, TmaxACh6 int, TmaxACh7 int, TmaxACh8 int, TmaxACh9 int, TmaxACh10 int, TmaxACh11 int, TmaxACh12 int, " +
                "TfACh1 int, TfACh2 int, TfACh3 int, TfACh4 int, TfACh5 int, TfACh6 int, TfACh7 int, TfACh8 int, TfACh9 int, TfACh10 int, TfACh11 int, TfACh12 int, " +
                "TeACh1 int, TeACh2 int, TeACh3 int, TeACh4 int, TeACh5 int, TeACh6 int, TeACh7 int, TeACh8 int, TeACh9 int, TeACh10 int, TeACh11 int, TeACh12 int, " +
                "TpmACh1 int, TpmACh2 int, TpmACh3 int, TpmACh4 int, TpmACh5 int, TpmACh6 int, TpmACh7 int, TpmACh8 int, TpmACh9 int, TpmACh10 int, TpmACh11 int, TpmACh12 int, " +
                "TA1/4Ch1 int, TA1/4Ch2 int, TA1/4Ch3 int, TA1/4Ch4 int, TA1/4Ch5 int, TA1/4Ch6 int, TA1/4Ch7 int, TA1/4Ch8 int, TA1/4Ch9 int, TA1/4Ch10 int, TA1/4Ch11 int, TA1/4Ch12 int, " +
                "TA3/4Ch1 int, TA3/4Ch2 int, TA3/4Ch3 int, TA3/4Ch4 int, TA3/4Ch5 int, TA3/4Ch6 int, TA3/4Ch7 int, TA3/4Ch8 int, TA3/4Ch9 int, TA3/4Ch10 int, TA3/4Ch11 int, TA3/4Ch12 int, " +
                "QCh1 int, QCh2 int, QCh3 int, QCh4 int, QCh5 int, QCh6 int, QCh7 int, QCh8 int, QCh9 int, QCh10 int, QCh11 int, QCh12 int, " +
                "SBCh1 int, SBCh2 int, SBCh3 int, SBCh4 int, SBCh5 int, SBCh6 int, SBCh7 int, SBCh8 int, SBCh9 int, SBCh10 int, SBCh11 int, SBCh12 int, " +
                "SCh1 int, SCh2 int, SCh3 int, SCh4 int, SCh5 int, SCh6 int, SCh7 int, SCh8 int, SCh9 int, SCh10 int, SCh11 int, SCh12 int, " +
                "Nul1 int, Nul2 int, Nul3 int, Nul4 int, Nul5 int, Nul6 int, Nul7 int, Nul8 int, Nul9 int, Nul10 int, Nul11 int, Nul12 int, " +
                "s1 char(10), s2 char(10), s3 char(10), s4 char(10), s5 char(10), s6 char(10), s7 char(10), s8 char(10), s9 char(10), s10 char(10), s11 char(10), s12 char(10), bad int)";
                createTable.ExecuteReader();
            }
   
            

            db.Close();
        }
        public static void AddDataTablRun(ClassTablRun classTablPSBRun)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO Run VALUES (NULL, @НомерRun, @Синхронизация, @ОбщийПорог, @Порог, @Триггер, @ЗначениеТаймера," +
                "NULL, NULL);";
            insertCommand.Parameters.AddWithValue("@НомерRun", classTablPSBRun.НомерRun);
            insertCommand.Parameters.AddWithValue("@Синхронизация", classTablPSBRun.Синхронизация);
            insertCommand.Parameters.AddWithValue("@ОбщийПорог", classTablPSBRun.ОбщийПорог);
            insertCommand.Parameters.AddWithValue("@Порог", classTablPSBRun.Порог);
            insertCommand.Parameters.AddWithValue("@Триггер", classTablPSBRun.Триггер);
            insertCommand.Parameters.AddWithValue("@ЗначениеТаймера", classTablPSBRun.ЗначениеТаймер);


            insertCommand.ExecuteReader();
            db.Close();

        }

        public static void updateTimeZapuskDataTablRun(string time, string ran)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path, true);

            db.Open();
           // UPDATE COMPANY SET ADDRESS = NULL, SALARY = NULL where ID IN(6, 7);
            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "UPDATE Run SET ВремяЗапуска ='"+time+ "' where НомерRun = '" + ran+"';";

            insertCommand.Parameters.AddWithValue("@ВремяЗапуска", time);
           // insertCommand.Parameters.AddWithValue("@Синхронизация", classTablPSBRun.Синхронизация);
           // insertCommand.Parameters.AddWithValue("@ОбщийПорог", classTablPSBRun.ОбщийПорог);
           // insertCommand.Parameters.AddWithValue("@Порог", classTablPSBRun.Порог);
           // insertCommand.Parameters.AddWithValue("@Триггер", classTablPSBRun.Триггер);
           // insertCommand.Parameters.AddWithValue("@ЗначениеТаймера", classTablPSBRun.ЗначениеТаймер);
           // insertCommand.Parameters.AddWithValue("@ВремяЗапуска", classTablPSBRun.ВремяЗапуска);
            // insertCommand.Parameters.AddWithValue("@ВремяСтоп", classTablPSBRun.ВремяСтоп);

            insertCommand.ExecuteReader();
            db.Close();

        }

        public static void updateTimeStopDataTablRun(string time, string ran)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "UPDATE Run SET ВремяСтоп = '" + time + "' where НомерRun ='" + ran + "';";

             insertCommand.Parameters.AddWithValue("@ВремяСтоп", time);


            insertCommand.ExecuteReader();
            db.Close();

        }

        public static List<ClassTablRun> GetDataRun()
        {
            List<ClassTablRun> entries = new List<ClassTablRun>();
         
            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path, true);

            db.Open();
            
            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT НомерRun, Синхронизация, ОбщийПорог, Порог, Триггер, ЗначениеТаймера, ВремяЗапуска, ВремяСтоп  from Run", db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            { 
                var cl= new ClassTablRun()
                {
                НомерRun = query.GetString(0),
                Синхронизация = query.GetInt32(1),
                ОбщийПорог = query.GetInt32(2),
                Порог = query.GetInt32(3),
                Триггер = query.GetInt32(4),
                ЗначениеТаймер = query.GetString(5),
                ВремяЗапуска = query[6] as string,
                
                };
                string st = String.Empty;
               
               
                cl.ВремяСтоп = query[7] as string;
               // Debug.WriteLine(query.GetString(1)+"\n"+ query.GetString(2));
                entries.Add(cl);
            }

            db.Close();


            return entries;
        }
        public static List<ClassTablSob> GetDataSob()
        {
            List<ClassTablSob> entries = new List<ClassTablSob>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT * from События", db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                int[] masAmp = new int[12];
                for(int i=7; i<19; i++)
                {
                    masAmp[i - 7] = query.GetInt32(i);
                }
                int[] masN = new int[12];
                for (int i = 19; i < 31; i++)
                {
                    masN[i - 19] = query.GetInt32(i);
                }
                int[] masNull = new int[12];
                for (int i = 31; i < 43; i++)
                {
                    masNull[i - 31] = query.GetInt32(i);
                }
                double[] masS = new double[12];
          
                for (int i = 43; i < 55; i++)
                {
                    try
                    {


                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        masS[i - 43] =Convert.ToDouble(query[i], formatter);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString()+i.ToString() + "\t" + query.GetString(i)+"\t"+ query.GetString(i).Replace(",", "."));
                    }
                }
                int badint = query.GetInt32(55);
                bool bad = false;
                if(badint==1)
                {
                    bad = true;
                }

                var cl = new ClassTablSob()
                {
                    Time= query.GetString(1),
                    ИмяФайла= query.GetString(2),
                    Плата= query.GetString(3),
                    Кластер= query.GetString(4),
                    СумАмп= query.GetInt32(5),
                    СумN = query.GetInt32(6),
                   АмпCh=masAmp,
                   NCh=masN,
                   Nul=masNull,
                   sig=masS,
                   bad=bad
                    

               
            };
               


               
              
                entries.Add(cl);
            }

            db.Close();


            return entries;
        }
        public static List<ClassTablSob> GetDataSob(string uslovie)
        {
            List<ClassTablSob> entries = new List<ClassTablSob>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT * from События where "+ uslovie, db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                int[] masAmp = new int[12];
                for (int i = 7; i < 19; i++)
                {
                    masAmp[i - 7] = query.GetInt32(i);
                }
                int[] masN = new int[12];
                for (int i = 19; i < 31; i++)
                {
                    masN[i - 19] = query.GetInt32(i);
                }
                int[] masNull = new int[12];
                for (int i = 31; i < 43; i++)
                {
                    masNull[i - 31] = query.GetInt32(i);
                }
                double[] masS = new double[12];

                for (int i = 43; i < 55; i++)
                {
                    try
                    {


                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        masS[i - 43] = Convert.ToDouble(query[i], formatter);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + i.ToString() + "\t" + query.GetString(i) + "\t" + query.GetString(i).Replace(",", "."));
                    }
                }
                int badint = query.GetInt32(55);
                bool bad = false;
                if (badint == 1)
                {
                    bad = true;
                }

                var cl = new ClassTablSob()
                {
                    Time = query.GetString(1),
                    ИмяФайла = query.GetString(2),
                    Плата = query.GetString(3),
                    Кластер = query.GetString(4),
                    СумАмп = query.GetInt32(5),
                    СумN = query.GetInt32(6),
                    АмпCh = masAmp,
                    NCh = masN,
                    Nul = masNull,
                    sig = masS,
                    bad = bad



                };





                entries.Add(cl);
            }

            db.Close();


            return entries;
        }
        public static List<ClassTablSob> GetDataSobTop10(string uslovie)
        {
            List<ClassTablSob> entries = new List<ClassTablSob>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT * from События order by Primary_Key desc limit " + uslovie, db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                int[] masAmp = new int[12];
                for (int i = 7; i < 19; i++)
                {
                    masAmp[i - 7] = query.GetInt32(i);
                }
                int[] masN = new int[12];
                for (int i = 19; i < 31; i++)
                {
                    masN[i - 19] = query.GetInt32(i);
                }
                int[] masNull = new int[12];
                for (int i = 31; i < 43; i++)
                {
                    masNull[i - 31] = query.GetInt32(i);
                }
                double[] masS = new double[12];

                for (int i = 43; i < 55; i++)
                {
                    try
                    {


                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        masS[i - 43] = Convert.ToDouble(query[i], formatter);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + i.ToString() + "\t" + query.GetString(i) + "\t" + query.GetString(i).Replace(",", "."));
                    }
                }
                int badint = query.GetInt32(55);
                bool bad = false;
                if (badint == 1)
                {
                    bad = true;
                }

                var cl = new ClassTablSob()
                {
                    Time = query.GetString(1),
                    ИмяФайла = query.GetString(2),
                    Плата = query.GetString(3),
                    Кластер = query.GetString(4),
                    СумАмп = query.GetInt32(5),
                    СумN = query.GetInt32(6),
                    АмпCh = masAmp,
                    NCh = masN,
                    Nul = masNull,
                    sig = masS,
                    bad = bad



                };





                entries.Add(cl);
            }

            db.Close();


            return entries;
        }
        public static void AddDataTablФайлы(string nameFile, string nameBAAK, string timeFile, string nameRan)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO Файлы VALUES (NULL, @ИмяФайла, @Плата, @ВремяСоздания, NULL, @Run);";

            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Плата", nameBAAK);
            insertCommand.Parameters.AddWithValue("@ВремяСоздания", timeFile);
            insertCommand.Parameters.AddWithValue("@Run", nameRan);
          

            insertCommand.ExecuteReader();
            db.Close();

        }

        public static void updateTimeStopDataTablФайл(string time, string nameF)
        {
            try
            {


                SQLiteConnection db =
                     new SQLiteConnection("Data Source = " + Path, true);

                db.Open();
                // UPDATE COMPANY SET ADDRESS = NULL, SALARY = NULL where ID IN(6, 7);
                SQLiteCommand insertCommand = new SQLiteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "UPDATE Файлы SET ВремяЗакрытия='" + time + "' where ИмяФайла ='" + nameF + "';";

                insertCommand.Parameters.AddWithValue("@ВремяЗакрытия", time);
                // insertCommand.Parameters.AddWithValue("@Синхронизация", classTablPSBRun.Синхронизация);
                // insertCommand.Parameters.AddWithValue("@ОбщийПорог", classTablPSBRun.ОбщийПорог);
                // insertCommand.Parameters.AddWithValue("@Порог", classTablPSBRun.Порог);
                // insertCommand.Parameters.AddWithValue("@Триггер", classTablPSBRun.Триггер);
                // insertCommand.Parameters.AddWithValue("@ЗначениеТаймера", classTablPSBRun.ЗначениеТаймер);
                // insertCommand.Parameters.AddWithValue("@ВремяЗапуска", classTablPSBRun.ВремяЗапуска);
                // insertCommand.Parameters.AddWithValue("@ВремяСтоп", classTablPSBRun.ВремяСтоп);

                insertCommand.ExecuteReader();
                db.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public static void AddDataTablSob(string nameFile, string nameBAAK, string time, int[] Amp, string nameklaster, int[] Nnut, int[] Nl, double[] sig, int bad)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;
           

            insertCommand.CommandText = "INSERT INTO События VALUES (NULL, @Время,  @ИмяФайла, @Плата, @Кластер, @СумАмп, @СумN, " +
                "@АмпCh1, @АмпCh2, @АмпCh3, @АмпCh4, @АмпCh5, @АмпCh6, @АмпCh7, @АмпCh8, @АмпCh9, @АмпCh10, @АмпCh11, @АмпCh12," +
                " @NCh1, @NCh2, @NCh3, @NCh4, @NCh5, @NCh6, @NCh7, @NCh8, @NCh9, @NCh10, @NCh11, @NCh12," +
                " @Nul1, @Nul2, @Nul3, @Nul4, @Nul5, @Nul6, @Nul7, @Nul8, @Nul9, @Nul10, @Nul11, @Nul12," +
                " @s1, @s2, @s3, @s4, @s5, @s6, @s7, @s8, @s9, @s10, @s11, @s12, @bad);";
           
            insertCommand.Parameters.AddWithValue("@Время", time);
            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Плата", nameBAAK);
            insertCommand.Parameters.AddWithValue("@Кластер", nameklaster);
            insertCommand.Parameters.AddWithValue("@СумАмп", Amp.Sum());
            insertCommand.Parameters.AddWithValue("@СумN", Nnut.Sum());
            insertCommand.Parameters.AddWithValue("@АмпCh1", Amp[0]);
            insertCommand.Parameters.AddWithValue("@АмпCh2", Amp[1]);
            insertCommand.Parameters.AddWithValue("@АмпCh3", Amp[2]);
            insertCommand.Parameters.AddWithValue("@АмпCh4", Amp[3]);
            insertCommand.Parameters.AddWithValue("@АмпCh5", Amp[4]);
            insertCommand.Parameters.AddWithValue("@АмпCh6", Amp[5]);
            insertCommand.Parameters.AddWithValue("@АмпCh7", Amp[6]);
            insertCommand.Parameters.AddWithValue("@АмпCh8", Amp[7]);
            insertCommand.Parameters.AddWithValue("@АмпCh9", Amp[8]);
            insertCommand.Parameters.AddWithValue("@АмпCh10", Amp[9]);
            insertCommand.Parameters.AddWithValue("@АмпCh11", Amp[10]);
            insertCommand.Parameters.AddWithValue("@АмпCh12", Amp[11]);
            insertCommand.Parameters.AddWithValue("@NCh1", Nnut[0]);
            insertCommand.Parameters.AddWithValue("@NCh2", Nnut[1]);
            insertCommand.Parameters.AddWithValue("@NCh3", Nnut[2]);
            insertCommand.Parameters.AddWithValue("@NCh4", Nnut[3]);
            insertCommand.Parameters.AddWithValue("@NCh5", Nnut[4]);
            insertCommand.Parameters.AddWithValue("@NCh6", Nnut[5]);
            insertCommand.Parameters.AddWithValue("@NCh7", Nnut[6]);
            insertCommand.Parameters.AddWithValue("@NCh8", Nnut[7]);
            insertCommand.Parameters.AddWithValue("@NCh9", Nnut[8]);
            insertCommand.Parameters.AddWithValue("@NCh10", Nnut[9]);
            insertCommand.Parameters.AddWithValue("@NCh11", Nnut[10]);
            insertCommand.Parameters.AddWithValue("@NCh12", Nnut[11]); 
            insertCommand.Parameters.AddWithValue("@Nul1", Nl[0]);
            insertCommand.Parameters.AddWithValue("@Nul2", Nl[1]);
            insertCommand.Parameters.AddWithValue("@Nul3", Nl[2]);
            insertCommand.Parameters.AddWithValue("@Nul4", Nl[3]);
            insertCommand.Parameters.AddWithValue("@Nul5", Nl[4]);
            insertCommand.Parameters.AddWithValue("@Nul6", Nl[5]);
            insertCommand.Parameters.AddWithValue("@Nul7", Nl[6]);
            insertCommand.Parameters.AddWithValue("@Nul8", Nl[7]);
            insertCommand.Parameters.AddWithValue("@Nul9", Nl[8]);
            insertCommand.Parameters.AddWithValue("@Nul10", Nl[9]);
            insertCommand.Parameters.AddWithValue("@Nul11", Nl[10]);
            insertCommand.Parameters.AddWithValue("@Nul12", Nl[11]);
            insertCommand.Parameters.AddWithValue("@s1", sig[0].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s2", sig[1].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s3", sig[2].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s4", sig[3].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s5", sig[4].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s6", sig[5].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s7", sig[6].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s8", sig[7].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s9", sig[8].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s10", sig[9].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s11", sig[10].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s12", sig[11].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@bad", bad);


            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void AddDataTablSobNeutron(string nameFile, int D, int Amp, int TimeFirst, int TimeEnd, string time, int TimeAmp, int TimeFirst3, int TimeEnd3, int bad)
        {
            SQLiteConnection db =
                   new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO Нейтроны VALUES (NULL, @ИмяФайла, @Время, @Dn, @Амп, @TimeFirst, @TimeEnd, @TimeFirst3, " +
                "@TimeEnd3, @TimeAmp, @bad);";

            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Время", time);
            insertCommand.Parameters.AddWithValue("@Dn", D);
            insertCommand.Parameters.AddWithValue("@Амп", Amp);
            insertCommand.Parameters.AddWithValue("@TimeFirst", TimeFirst);
            insertCommand.Parameters.AddWithValue("@TimeEnd", TimeEnd);
            insertCommand.Parameters.AddWithValue("@TimeFirst3", TimeFirst3);
            insertCommand.Parameters.AddWithValue("@TimeEnd3", TimeEnd3);
            insertCommand.Parameters.AddWithValue("@TimeAmp", TimeAmp);
          
            insertCommand.Parameters.AddWithValue("@bad", bad);

            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void AddDataTablSob100(string Ran, string nameFile, string nameBAAK, string time, int[] SumD, string nameklaster, int[] Amp, int[] TmaxACh,
            int[] TfACh, int[] TeACh, int[] TpmA, int[] TA14Ch, int[] TA34Ch, int[] QCh, int[] SBCh, int[] SCh, int[] Nl, double[] sig, int bad)
        {
            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path, true);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;


            insertCommand.CommandText = "INSERT INTO События VALUES (NULL, @Время, @Run,  @ИмяФайла, @Плата, @Кластер, @СумD, " +
                "@АмпCh1, @АмпCh2, @АмпCh3, @АмпCh4, @АмпCh5, @АмпCh6, @АмпCh7, @АмпCh8, @АмпCh9, @АмпCh10, @АмпCh11, @АмпCh12," +
                "@TmaxACh1, @TmaxACh2, @TmaxACh3, @TmaxACh4, @TmaxACh5, @TmaxACh6, @TmaxACh7, @TmaxACh8, @TmaxACh9, @TmaxACh10, @TmaxACh11, @TmaxACh12," +
                "@TfACh1, @TfACh2, @TfACh3, @TfACh4, @TfACh5, @TfACh6, @TfACh7, @TfACh8, @TfACh9, @TfACh10, @TfACh11, @TfACh12," +
                "@TeACh1, @TeACh2, @TeACh3 @TeACh4, @TeACh5, @TeACh6, @TeACh7, @TeACh8, @TeACh9, @TeACh10, @TeACh11, @TeACh12," +
                "@TpmACh1, @TpmACh2, @TpmACh3, @TpmACh4, @TpmAC5, @TpmACh6, @TpmACh7, @TpmACh8, @TpmACh9, @TpmACh10, @TpmACh11, @TpmACh12," +
                 "@TA1/4Ch1, @TA1/4Ch2, @TA1/4Ch3, @TA1/4Ch4, @TA1/4Ch5, @TA1/4Ch6, @TA1/4Ch7, @TA1/4Ch8, @TA1/4Ch9, @TA1/4Ch10, @TA1/4Ch11, @TA1/4Ch12," +
              "@TA3/4Ch1, @TA3/4Ch2, @TA3/4Ch3, @TA3/4Ch4, @TA3/4Ch5, @TA3/4Ch6, @TA3/4Ch7, @TA3/4Ch8, @TA3/4Ch9, @TA3/4Ch10, @TA3/4Ch11, @TA3/4Ch12," +
              "@QCh1, @QCh2, @QCh3, @QCh4, @QCh5, @QCh6, @QCh7, @QCh8, @QCh9, @QCh10, @QCh11, @QCh12," +
              "@SBCh1, @SBCh2, @SBCh3, @SBCh4, @SBCh5, @SBCh6, @SBCh7, @SBCh8, @SBCh9, @SBCh10, @SBCh11, @SBCh12, " +
              "@SCh1, @SCh2, @SCh3, @SCh4, @SCh5, @SCh6, @SCh7, @SCh8, @SCh9, @SCh10, @SCh11, @SCh12, " +
                " @Nul1, @Nul2, @Nul3, @Nul4, @Nul5, @Nul6, @Nul7, @Nul8, @Nul9, @Nul10, @Nul11, @Nul12," +
                " @s1, @s2, @s3, @s4, @s5, @s6, @s7, @s8, @s9, @s10, @s11, @s12, @bad);";

            insertCommand.Parameters.AddWithValue("@Время", time);
            insertCommand.Parameters.AddWithValue("@Run", Ran);
            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Плата", nameBAAK);
            insertCommand.Parameters.AddWithValue("@Кластер", nameklaster);
            insertCommand.Parameters.AddWithValue("@СумD", SumD.Sum());
            insertCommand.Parameters.AddWithValue("@АмпCh1", Amp[0]);
            insertCommand.Parameters.AddWithValue("@АмпCh2", Amp[1]);
            insertCommand.Parameters.AddWithValue("@АмпCh3", Amp[2]);
            insertCommand.Parameters.AddWithValue("@АмпCh4", Amp[3]);
            insertCommand.Parameters.AddWithValue("@АмпCh5", Amp[4]);
            insertCommand.Parameters.AddWithValue("@АмпCh6", Amp[5]);
            insertCommand.Parameters.AddWithValue("@АмпCh7", Amp[6]);
            insertCommand.Parameters.AddWithValue("@АмпCh8", Amp[7]);
            insertCommand.Parameters.AddWithValue("@АмпCh9", Amp[8]);
            insertCommand.Parameters.AddWithValue("@АмпCh10", Amp[9]);
            insertCommand.Parameters.AddWithValue("@АмпCh11", Amp[10]);
            insertCommand.Parameters.AddWithValue("@АмпCh12", Amp[11]);
            insertCommand.Parameters.AddWithValue("@TmaxACh1", TmaxACh[0]);
            insertCommand.Parameters.AddWithValue("@TmaxACh2", TmaxACh[1]);
            insertCommand.Parameters.AddWithValue("@TmaxACh3", TmaxACh[2]);
            insertCommand.Parameters.AddWithValue("@TmaxACh4", TmaxACh[3]);
            insertCommand.Parameters.AddWithValue("@TmaxACh5", TmaxACh[4]);
            insertCommand.Parameters.AddWithValue("@TmaxACh6", TmaxACh[5]);
            insertCommand.Parameters.AddWithValue("@TmaxACh7", TmaxACh[6]);
            insertCommand.Parameters.AddWithValue("@TmaxACh8", TmaxACh[7]);
            insertCommand.Parameters.AddWithValue("@TmaxACh9", TmaxACh[8]);
            insertCommand.Parameters.AddWithValue("@TmaxACh10", TmaxACh[9]);
            insertCommand.Parameters.AddWithValue("@NTmaxACh11", TmaxACh[10]);
            insertCommand.Parameters.AddWithValue("@TmaxACh12", TmaxACh[11]);
            insertCommand.Parameters.AddWithValue("@TfACh1", TfACh[0]);
            insertCommand.Parameters.AddWithValue("@TfACh2", TfACh[1]);
            insertCommand.Parameters.AddWithValue("@TfACh3", TfACh[2]);
            insertCommand.Parameters.AddWithValue("@TfACh4", TfACh[3]);
            insertCommand.Parameters.AddWithValue("@TfACh5", TfACh[4]);
            insertCommand.Parameters.AddWithValue("@TfACh6", TfACh[5]);
            insertCommand.Parameters.AddWithValue("@TfACh7", TfACh[6]);
            insertCommand.Parameters.AddWithValue("@TfACh8", TfACh[7]);
            insertCommand.Parameters.AddWithValue("@TfACh9", TfACh[8]);
            insertCommand.Parameters.AddWithValue("@TfACh10", TfACh[9]);
            insertCommand.Parameters.AddWithValue("@TfACh11", TfACh[10]);
            insertCommand.Parameters.AddWithValue("@TfACh12", TfACh[11]);
            insertCommand.Parameters.AddWithValue("@TeACh1", TeACh[0]);
            insertCommand.Parameters.AddWithValue("@TeACh2", TeACh[1]);
            insertCommand.Parameters.AddWithValue("@TeACh3", TeACh[2]);
            insertCommand.Parameters.AddWithValue("@TeACh4", TeACh[3]);
            insertCommand.Parameters.AddWithValue("@TeACh5", TeACh[4]);
            insertCommand.Parameters.AddWithValue("@TeACh6", TeACh[5]);
            insertCommand.Parameters.AddWithValue("@TeACh7", TeACh[6]);
            insertCommand.Parameters.AddWithValue("@TeACh8", TeACh[7]);
            insertCommand.Parameters.AddWithValue("@TeACh9", TeACh[8]);
            insertCommand.Parameters.AddWithValue("@TeACh10", TeACh[9]);
            insertCommand.Parameters.AddWithValue("@TeACh11", TeACh[10]);
            insertCommand.Parameters.AddWithValue("@TeACh12", TeACh[11]);
            insertCommand.Parameters.AddWithValue("@TpmACh1", TpmA[0]);
            insertCommand.Parameters.AddWithValue("@TpmACh2", TpmA[1]);
            insertCommand.Parameters.AddWithValue("@TpmACh3", TpmA[2]);
            insertCommand.Parameters.AddWithValue("@TpmACh4", TpmA[3]);
            insertCommand.Parameters.AddWithValue("@TpmACh5", TpmA[4]);
            insertCommand.Parameters.AddWithValue("@TpmACh6", TpmA[5]);
            insertCommand.Parameters.AddWithValue("@TpmACh7", TpmA[6]);
            insertCommand.Parameters.AddWithValue("@TpmACh8", TpmA[7]);
            insertCommand.Parameters.AddWithValue("@TpmACh9", TpmA[8]);
            insertCommand.Parameters.AddWithValue("@TpmACh10", TpmA[9]);
            insertCommand.Parameters.AddWithValue("@TpmACh11", TpmA[10]);
            insertCommand.Parameters.AddWithValue("@TpmACh12", TpmA[11]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch1", TA14Ch[0]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch2", TA14Ch[1]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch3", TA14Ch[2]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch4", TA14Ch[3]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch5", TA14Ch[4]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch6", TA14Ch[5]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch7", TA14Ch[6]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch8", TA14Ch[7]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch9", TA14Ch[8]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch10", TA14Ch[9]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch11", TA14Ch[10]);
            insertCommand.Parameters.AddWithValue("@TA1/4Ch12", TA14Ch[11]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch1", TA34Ch[0]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch2", TA34Ch[1]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch3", TA34Ch[2]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch4", TA34Ch[3]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch5", TA34Ch[4]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch6", TA34Ch[5]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch7", TA34Ch[6]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch8", TA34Ch[7]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch9", TA34Ch[8]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch10", TA34Ch[9]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch11", TA34Ch[10]);
            insertCommand.Parameters.AddWithValue("@TA3/4Ch12", TA34Ch[11]);
            insertCommand.Parameters.AddWithValue("@QCh1", QCh[0]);
            insertCommand.Parameters.AddWithValue("@QCh2", QCh[1]);
            insertCommand.Parameters.AddWithValue("@QCh3", QCh[2]);
            insertCommand.Parameters.AddWithValue("@QCh4", QCh[3]);
            insertCommand.Parameters.AddWithValue("@QCh5", QCh[4]);
            insertCommand.Parameters.AddWithValue("@QCh6", QCh[5]);
            insertCommand.Parameters.AddWithValue("@QCh7", QCh[6]);
            insertCommand.Parameters.AddWithValue("@QCh8", QCh[7]);
            insertCommand.Parameters.AddWithValue("@QCh9", QCh[8]);
            insertCommand.Parameters.AddWithValue("@QCh10", QCh[9]);
            insertCommand.Parameters.AddWithValue("@QCh11", QCh[10]);
            insertCommand.Parameters.AddWithValue("@QCh12", QCh[11]);
            insertCommand.Parameters.AddWithValue("@SBCh1", SBCh[0]);
            insertCommand.Parameters.AddWithValue("@SBCh2", SBCh[1]);
            insertCommand.Parameters.AddWithValue("@SBCh3", SBCh[2]);
            insertCommand.Parameters.AddWithValue("@SBCh4", SBCh[3]);
            insertCommand.Parameters.AddWithValue("@SBCh5", SBCh[4]);
            insertCommand.Parameters.AddWithValue("@SBCh6", SBCh[5]);
            insertCommand.Parameters.AddWithValue("@SBCh7", SBCh[6]);
            insertCommand.Parameters.AddWithValue("@SBCh8", SBCh[7]);
            insertCommand.Parameters.AddWithValue("@SBCh9", SBCh[8]);
            insertCommand.Parameters.AddWithValue("@SBCh10", SBCh[9]);
            insertCommand.Parameters.AddWithValue("@SBCh11", SBCh[10]);
            insertCommand.Parameters.AddWithValue("@SBCh12", SBCh[11]);
            insertCommand.Parameters.AddWithValue("@SCh1", SCh[0]);
            insertCommand.Parameters.AddWithValue("@SCh2", SCh[1]);
            insertCommand.Parameters.AddWithValue("@SCh3", SCh[2]);
            insertCommand.Parameters.AddWithValue("@SCh4", SCh[3]);
            insertCommand.Parameters.AddWithValue("@SCh5", SCh[4]);
            insertCommand.Parameters.AddWithValue("@SCh6", SCh[5]);
            insertCommand.Parameters.AddWithValue("@SCh7", SCh[6]);
            insertCommand.Parameters.AddWithValue("@SCh8", SCh[7]);
            insertCommand.Parameters.AddWithValue("@SCh9", SCh[8]);
            insertCommand.Parameters.AddWithValue("@SCh10", SCh[9]);
            insertCommand.Parameters.AddWithValue("@SCh11", SCh[10]);
            insertCommand.Parameters.AddWithValue("@SCh12", SCh[11]);
            insertCommand.Parameters.AddWithValue("@Nul1", Nl[0]);
            insertCommand.Parameters.AddWithValue("@Nul2", Nl[1]);
            insertCommand.Parameters.AddWithValue("@Nul3", Nl[2]);
            insertCommand.Parameters.AddWithValue("@Nul4", Nl[3]);
            insertCommand.Parameters.AddWithValue("@Nul5", Nl[4]);
            insertCommand.Parameters.AddWithValue("@Nul6", Nl[5]);
            insertCommand.Parameters.AddWithValue("@Nul7", Nl[6]);
            insertCommand.Parameters.AddWithValue("@Nul8", Nl[7]);
            insertCommand.Parameters.AddWithValue("@Nul9", Nl[8]);
            insertCommand.Parameters.AddWithValue("@Nul10", Nl[9]);
            insertCommand.Parameters.AddWithValue("@Nul11", Nl[10]);
            insertCommand.Parameters.AddWithValue("@Nul12", Nl[11]);
            insertCommand.Parameters.AddWithValue("@s1", sig[0].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s2", sig[1].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s3", sig[2].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s4", sig[3].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s5", sig[4].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s6", sig[5].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s7", sig[6].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s8", sig[7].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s9", sig[8].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s10", sig[9].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s11", sig[10].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@s12", sig[11].ToString("0.0000"));
            insertCommand.Parameters.AddWithValue("@bad", bad);


            insertCommand.ExecuteReader();
            db.Close();
        }
    }
}
