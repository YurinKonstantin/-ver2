using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URAN_2017.WorkBD.ViewTaiblBDBAAK;

namespace URAN_2017.WorkBD
{
   public static class DataAccesBDBAAK
    {
      public  static string Path { get; set; }
        static public async void CreateDBBAAK()
        {

            SQLiteConnection.CreateFile(Path);
        }
        public static void InitializeDatabase()
        {


         
            //  SQLiteConnection.CreateFile(dbPath);
            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);


            //  db.ConnectionString = "Dataname=sqliteSample.db3";

            db.Open();
            String tableCommand = String.Empty;
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {



                tableCommand = "CREATE TABLE IF NOT " +
           "EXISTS ПлатыБААК (Primary_Key INTEGER PRIMARY KEY, " +
           "ИмяПлаты NVARCHAR(16) NOT NULL, ТипПлаты char(100) NOT NULL, IP char(100) NOT NULL, Кластер int NOT NULL, Коментарии NVARCHAR(256) NULL)";
                createTable.ExecuteReader();

            }

       
            //  tableCommand = @"CREATE TABLE [workers] (
            //     [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
            //   [name] char(100) NOT NULL,
            //   [family] char(100) NOT NULL,
            //   [age] int NOT NULL,
            //   [profession] char(100) NOT NULL
            //   );";

            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {



               
                tableCommand = "CREATE TABLE IF NOT " +
                  "EXISTS НулевыеЛинии (Primary_Key INTEGER PRIMARY KEY, " +
                  "ИмяПлаты NVARCHAR(16) NOT NULL, Канал1 int NOT NULL, Канал2 int NOT NULL, Канал3 int NOT NULL, Канал4 int NOT NULL, Канал5 int NOT NULL, Канал6 int NOT NULL, Канал7 int NOT NULL," +
                  "Канал8 int NOT NULL, Канал9 int NOT NULL, Канал10 int NOT NULL, Канал11 int NOT NULL, Канал12 int NOT NULL)";
                createTable.ExecuteReader();

            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
             
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS Пороги (Primary_Key INTEGER PRIMARY KEY, " +
                "ИмяПлаты NVARCHAR(16) NOT NULL, Канал1 int NOT NULL, Канал2 int NOT NULL, Канал3 int NOT NULL, Канал4 int NOT NULL, Канал5 int NOT NULL, Канал6 int NOT NULL, Канал7 int NOT NULL," +
                "Канал8 int NOT NULL, Канал9 int NOT NULL, Канал10 int NOT NULL, Канал11 int NOT NULL, Канал12 int NOT NULL)";
                createTable.ExecuteReader();
            }

            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {


              

                tableCommand = "CREATE TABLE IF NOT " +
             "EXISTS Изминиения (Primary_Key INTEGER PRIMARY KEY, " +
                    "ИмяПлаты NVARCHAR(16) NOT NULL, ТипПлаты char(100) NOT NULL, IP char(100) NOT NULL, Кластер int NOT NULL, Коментарии NVARCHAR(256) NULL)";
               // createTable = new SQLiteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
            db.Close();

        }

        public static void AddDataTablPlats(ClassTablPSBBAAK classTablPSBBAAK)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO ПлатыБААК VALUES (NULL, @ИмяПлаты, @Tip, @IPB, @KL, @Коментарий);";
            insertCommand.Parameters.AddWithValue("@ИмяПлаты", classTablPSBBAAK.namePSB);
            insertCommand.Parameters.AddWithValue("@Tip", classTablPSBBAAK.tipPSB);
            insertCommand.Parameters.AddWithValue("@IPB", classTablPSBBAAK.IpPSB);
            insertCommand.Parameters.AddWithValue("@KL", classTablPSBBAAK.nomerKlastera);
            insertCommand.Parameters.AddWithValue("@Коментарий", classTablPSBBAAK.Coment);

            insertCommand.ExecuteReader();
            db.Close();

        }
        public static void updateDataTablPlats(ClassTablPSBBAAK classTablPSBBAAK)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();
           

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            insertCommand.CommandText = "INSERT INTO ПлатыБААК VALUES (NULL, @ИмяПлаты, @Tip, @IPB, @KL, @Коментарий);";
            insertCommand.Parameters.AddWithValue("@ИмяПлаты", classTablPSBBAAK.namePSB);
            insertCommand.Parameters.AddWithValue("@Tip", classTablPSBBAAK.tipPSB);
            insertCommand.Parameters.AddWithValue("@IPB", classTablPSBBAAK.IpPSB);
            insertCommand.Parameters.AddWithValue("@KL", classTablPSBBAAK.nomerKlastera);
            insertCommand.Parameters.AddWithValue("@Коментарий", classTablPSBBAAK.Coment);

            insertCommand.ExecuteReader();
            db.Close();

        }

        public static List<ClassTablPSBBAAK> GetDataBAAK()
        {
            List<ClassTablPSBBAAK> entries = new List<ClassTablPSBBAAK>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT ИмяПлаты, ТипПлаты, IP, Кластер, Коментарии from ПлатыБААК", db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                entries.Add(new ClassTablPSBBAAK() {namePSB= query.GetString(0), tipPSB= query.GetString(1), IpPSB= query.GetString(2), nomerKlastera=query.GetInt32(3), Coment =query.GetString(4) });
            }

            db.Close();


            return entries;
        }
        public static void AddDataTablNullLine(ClassNullLine classTablPSBBAAK)
        {
            SQLiteConnection db =
                 new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand insertCommand = new SQLiteCommand();
            insertCommand.Connection = db;

            // Use parameterized query to prevent SQL injection attacks
            insertCommand.CommandText = "INSERT INTO НулевыеЛинии VALUES (NULL, @ИмяПлаты, @Конал1, @Конал2, @Конал3, @Конал4, @Конал5, @Конал6, @Конал7, @Конал8, @Конал9, @Конал10, @Конал11, @Конал12);";
            insertCommand.Parameters.AddWithValue("@ИмяПлаты", classTablPSBBAAK.namePSB);
            insertCommand.Parameters.AddWithValue("@Конал1", classTablPSBBAAK.nullLine[0]);
            insertCommand.Parameters.AddWithValue("@Конал2", classTablPSBBAAK.nullLine[1]);
            insertCommand.Parameters.AddWithValue("@Конал3", classTablPSBBAAK.nullLine[2]);
            insertCommand.Parameters.AddWithValue("@Конал4", classTablPSBBAAK.nullLine[3]);

            insertCommand.Parameters.AddWithValue("@Конал5", classTablPSBBAAK.nullLine[4]);
            insertCommand.Parameters.AddWithValue("@Конал6", classTablPSBBAAK.nullLine[5]);
            insertCommand.Parameters.AddWithValue("@Конал7", classTablPSBBAAK.nullLine[6]);
            insertCommand.Parameters.AddWithValue("@Конал8", classTablPSBBAAK.nullLine[7]);
            insertCommand.Parameters.AddWithValue("@Конал9", classTablPSBBAAK.nullLine[8]);
            insertCommand.Parameters.AddWithValue("@Конал10", classTablPSBBAAK.nullLine[9]);
            insertCommand.Parameters.AddWithValue("@Конал11", classTablPSBBAAK.nullLine[10]);
            insertCommand.Parameters.AddWithValue("@Конал12", classTablPSBBAAK.nullLine[11]);

            insertCommand.ExecuteReader();

            db.Close();


        }

        public static List<ClassNullLine> GetDataNullLine()
        {
            List<ClassNullLine> entries = new List<ClassNullLine>();

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT ИмяПлаты, Канал1, Канал2, Канал3, Канал4, Канал5, Канал6, Канал7, Канал8, Канал9, Канал10, Канал11, Канал12 from НулевыеЛинии", db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                


                    int[] mas = new int[12];
                    mas[0] = query.GetInt32(1);
                mas[1] = query.GetInt32(2);
                mas[2] = query.GetInt32(3);
                mas[3] = query.GetInt32(4);
                mas[4] = query.GetInt32(5);
                mas[5] = query.GetInt32(6);
                mas[6] = query.GetInt32(7);
                mas[7] = query.GetInt32(8);
                mas[8] = query.GetInt32(9);
                mas[9] = query.GetInt32(10);
                mas[10] = query.GetInt32(11);
                mas[11] = query.GetInt32(12);

                entries.Add(new ClassNullLine() { namePSB = query.GetString(0), nullLine = mas });
                
            }

            db.Close();


            return entries;
        }
        public static void DeleteNullLine(string Key)
        {
          

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("Delete from НулевыеЛинии where ИмяПлаты=" + "'"+Key+"'", db);

           // SQLiteDataReader query = selectCommand.ExecuteReader();


            db.Close();


           
        }
        public static void DeletePSB(string Key)
        {
          

            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);

            db.Open();

            SQLiteCommand selectCommand = new SQLiteCommand
                ("Delete from ПлатыБААК where ИмяПлаты=" + "'" + Key + "'", db);

          //  SQLiteDataReader query = selectCommand.ExecuteReader();


            db.Close();



        }
    }
}
