using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
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
                "НомерRun char(30) NOT NULL, Синхронизация int NOT NULL, ОбщийПорог int NOT NULL, Порог int NOT NULL, Триггер int NOT NULL, ЗначениеТаймера char(15), " +
                "ВремяЗапуска char(25), ВремяСтоп char(25))";
                createTable.ExecuteReader();
            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                tableCommand = "CREATE TABLE IF NOT " +
                  "EXISTS Файлы (Primary_Key INTEGER PRIMARY KEY, " +
                  "ИмяФайла char(30) NOT NULL, Плата char(2) NOT NULL, ВремяСоздания char(25), ВремяЗакрытия char(25), Run char(30))";
                createTable.ExecuteReader();

            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS События (Primary_Key INTEGER PRIMARY KEY, " +
                "Время NVARCHAR(256) NOT NULL, ИмяФайла char(2) NOT NULL, Плата char(25), Кластер char(25), СумАмп int, СумN int," +
                "АмпCh1 int, АмпCh2 int, АмпCh3 int, АмпCh4 int, АмпCh5 int, АмпCh6 int, АмпCh7 int, АмпCh8 int, АмпCh9 int, АмпCh10 int, АмпCh11 int" +
                " АмпCh12 int, NCh1 int, NCh2 int, NCh3 int, NCh4 int, NCh5 int, NCh6 int, NCh7 int, NCh8 int, NCh9 int, NCh10 int, NCh11 int" +
                " NCh12 int, Nul1 int, Nul2 int, Nul3 int, Nul4 int, Nul5 int, Nul6 int, int, Nul8 int, Nul9 int," +
                " Nul10 int, Nul11 int, Nul12 int," +
                " s1 char(10), s2 char(10), s3 char(10), s4 char(10), s5 char(10), s6 char(10), s7 char(10), s8 char(10), s9 char(10), s10 char(10), s11 char(10), s12 char(10))";
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
        public static void AddDataTablRun(ClassTablRun classTablPSBRun)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

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
           // insertCommand.Parameters.AddWithValue("@ВремяЗапуска", classTablPSBRun.ВремяЗапуска);
           // insertCommand.Parameters.AddWithValue("@ВремяСтоп", classTablPSBRun.ВремяСтоп);

            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void updateTimeZapuskDataTablRun(string time, string ran)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();
           // UPDATE COMPANY SET ADDRESS = NULL, SALARY = NULL where ID IN(6, 7);
            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "UPDATE Run SET ВремяЗапуска="+time+" where НомерRun =" + ran+";";

           // insertCommand.Parameters.AddWithValue("@НомерRun", classTablPSBRun.НомерRun);
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
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();
            // UPDATE COMPANY SET ADDRESS = NULL, SALARY = NULL where ID IN(6, 7);
            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "UPDATE Run SET ВремяСтоп=" + time + " where НомерRun =" + ran + ";";

            // insertCommand.Parameters.AddWithValue("@НомерRun", classTablPSBRun.НомерRun);
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

        public static List<ClassTablRun> GetDataRun()
        {
            List<ClassTablRun> entries = new List<ClassTablRun>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

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
                Debug.WriteLine(query.GetString(1)+"\n"+ query.GetString(2));
                entries.Add(cl);
            }

            db.Close();


            return entries;
        }


        public static void AddDataTablФайлы(string nameFile, string nameBAAK, string timeFile, string nameRan)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO Файлы VALUES (NULL, @ИмяФайла, @Плата, @ВремяСоздания, NULL);";

            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Плата", nameBAAK);
            insertCommand.Parameters.AddWithValue("@ВремяСоздания", timeFile);
            insertCommand.Parameters.AddWithValue("@Run", nameRan);
          

            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void updateTimeStopDataTablФайл(string time, string nameF)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();
            // UPDATE COMPANY SET ADDRESS = NULL, SALARY = NULL where ID IN(6, 7);
            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "UPDATE Файлы SET ВремяЗакрытия=" + time + " where ИмяФайла =" + nameF + ";";

            // insertCommand.Parameters.AddWithValue("@НомерRun", classTablPSBRun.НомерRun);
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
        public static void AddDataTablSob(string nameFile, string nameBAAK, string time, int[] Amp, string nameklaster, int[] Nnut, int[] Nl, double[] sig)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;
           

            insertCommand.CommandText = "INSERT INTO События VALUES (NULL, @ИмяФайла, @Плата, @Кластер, СумАмп, СумN, АмпCh1, АмпCh2, " +
                "АмпCh3, АмпCh4, АмпCh5, АмпCh6, АмпCh7, АмпCh8, АмпCh9, АмпCh10, АмпCh11, АмпCh12, NCh1, NCh2, NCh3, NCh4, NCh5, NCh6" +
                ", NCh7, NCh8, NCh9, NCh10, NCh11, NCh12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12);";

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


            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void AddDataTablSobNeutron(string nameFile, int D, int Amp, int TimeFirst, int TimeEnd, string time, int TimeAmp, int TimeFirst3, int TimeEnd3, int bad)
        {
            SQLiteConnection db =
                   new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO Нейтроны VALUES (NULL, @ИмяФайла, @Время, @Dn, Амп, TimeFirst, TimeEnd, TimeFirst3, " +
                "TimeEnd3, TimeAmp, bad);";

            insertCommand.Parameters.AddWithValue("@ИмяФайла", nameFile);
            insertCommand.Parameters.AddWithValue("@Время", time);
            insertCommand.Parameters.AddWithValue("@Dn", D);
            insertCommand.Parameters.AddWithValue("@Амп", Amp);
            insertCommand.Parameters.AddWithValue("@TimeFirst", TimeFirst);
            insertCommand.Parameters.AddWithValue("@TimeEnd", TimeEnd);
            insertCommand.Parameters.AddWithValue("@TimeFirst3", TimeFirst3);
            insertCommand.Parameters.AddWithValue("@TimeEnd3", TimeEnd3);
            insertCommand.Parameters.AddWithValue("@TimeAmp", TimeAmp);
          
            insertCommand.Parameters.AddWithValue("@TimeAmp", bad);

            insertCommand.ExecuteReader();
            db.Close();

        }

    }
}
