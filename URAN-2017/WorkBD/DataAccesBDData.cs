using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



            //  SQLiteConnection.CreateFile(dbPath);
            SQLiteConnection db =
                new SQLiteConnection("Data Source = " + Path);


            //  db.ConnectionString = "Dataname=sqliteSample.db3";

            db.Open();


            String tableCommand =String.Empty;
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
                "EXISTS Run (Primary_Key INTEGER PRIMARY KEY, " +
                "НомерRun char(30) NOT NULL, Синхронизация int NOT NULL, ОбщийПорог int NOT NULL, Порог int NOT NULL, Триггер int NO NULL, ЗначениеТаймера char(15) NULL, " +
                "ВремяЗапуска char(25) NULL, ВремяЗапуска char(25) NULL)";
                createTable.ExecuteReader();


            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {



               
                tableCommand = "CREATE TABLE IF NOT " +
                  "EXISTS Файлы (Primary_Key INTEGER PRIMARY KEY, " +
                  "ИмяФайла char(30) NOT NULL, Плата char(2) NOT NULL, ВремяСоздания char(25) NULL, ВремяЗакрытия char(25) NULL, Run char(30) NULL)";
                createTable.ExecuteReader();


            }
            using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, db))
            {
                
                tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS События (Primary_Key INTEGER PRIMARY KEY, " +
                "Время NVARCHAR(256) NOT NULL, ИмяФайла char(2) NOT NULL, Плата char(25) NULL, Кластер char(25) NULL, СумАмп char(30) NULL, СумN char(30) NULL)";
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


    }
}
