using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Globalization;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;
using URAN_2017.WorkBD.ViewTaiblBDData;
using static URAN_2017.UserControlURAN;
using MessageBox = System.Windows.MessageBox;


namespace URAN_2017.WorkBD
{
    /// <summary>
    /// Логика взаимодействия для PageBDData.xaml
    /// </summary>
    public partial class PageBDData : Page
    {
        public PageBDData()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                var dir = new System.IO.DirectoryInfo(folderBrowser.SelectedPath);
                string pp = folderBrowser.SelectedPath + @"\BD_Data.db";
                DataAccesBDData.Path = folderBrowser.SelectedPath + @"\BD_Data.db";
                Path.Text = folderBrowser.SelectedPath;
                DataAccesBDData.CreateDB();
                DataAccesBDData.InitializeDatabase();
                RanGrid.ItemsSource = DataAccesBDData.GetDataRun();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          OpenFileDialog myDialog = new OpenFileDialog
            {
                Filter = "База данных(*.MDB;*.MDB;*.accdb; *.db; *.db3)|*.MDB;*.MDB;*.ACCDB;*DB; *DB3;" + "|Все файлы (*.*)|*.* ",
                CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                Path.Text = myDialog.FileName;
                DataAccesBDData.Path = myDialog.FileName;              
                RanGrid.ItemsSource = DataAccesBDData.GetDataRun();
                SobGrid1.ItemsSource = DataAccesBDData.GetDataSob();
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccesBDData.AddDataTablRun(new ViewTaiblBDData.ClassTablRun() 
            { 
                НомерRun = "18.12.2019 14:18:14:597", 
                ВремяЗапуска= "18.12.2019 14:18:52:818",
                ЗначениеТаймер= "18 14:18:30:0",
                ОбщийПорог=1,
                Порог=10,
                Синхронизация=0,
                Триггер=2
            });
           // DataAccesBDData.updateTimeZapuskDataTablRun("18.12.2019 14:18:52:818", "18.12.2019 14:18:14:597");
           // DataAccesBDData.updateTimeStopDataTablRun("18.12.2019 14:18:52:818", "18.12.2019 14:18:14:597");
            RanGrid.ItemsSource = DataAccesBDData.GetDataRun();
            // DataAccesBDData.AddDataTablФайлы("1_19.12.2019 00.10.23_N", "У6", "19.12.2019 00.10.23", "18.12.2019 14:18:14:597");
            //  DataAccesBDData.updateTimeStopDataTablФайл("19.12.2019 00.10.23", "18.12.2019 14:18:14:597");
            int[] amp = new int[12];
            double[] sig = new double[12];
            DataAccesBDData.AddDataTablSob("1_19.12.2019 00.10.23_N", "Y4", "18.12.2019 14:18:14:597", amp, "1", amp, amp, sig, 1);
            DataAccesBDData.AddDataTablSobNeutron("1_19.12.2019 00.10.23_N", 1, 10, 5, 9, "18.12.2019 14:18:14:597", 7, 4, 8, 1);
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
            using (fs = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, set);
                System.Windows.MessageBox.Show("Сохранено");
            }
            fs.Close();
            UserSetting.Serial();

        }
        UserSetting set = new UserSetting();
        private void DeSerial()
        {
            try
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
                    using (wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);
                    }
                    wr.Close();
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }

        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ClassSob> classSobs1 = new ObservableCollection<ClassSob>();
            var classRazvertka = await GetDataSob("Кластер ==6", classSobs1, Path.Text);
           // MessageBox.Show(classSobs1.Count.ToString());

          //  var ss=await TempSob(classSobs1.ToList(), 1);
            
           
            var nn = await TempNeutron(classSobs1.ToList(), 1);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            
           /* if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
               
                using (StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath+@"\"+"kl6.txt", false, System.Text.Encoding.Default))
                {
                    foreach (var d in ss)
                    {
                        await sw.WriteLineAsync(d.dateTime+"\t"+d.colSob.ToString()+"\t"+d.mTemp[0].ToString() + "\t" + d.mTemp[1].ToString()
                            + "\t" + d.mTemp[2].ToString() + "\t" + d.mTemp[3].ToString() + "\t" + d.mTemp[4].ToString() + "\t" + d.mTemp[5].ToString()
                            + "\t" + d.mTemp[6].ToString() + "\t" + d.mTemp[7].ToString() + "\t" + d.mTemp[8].ToString() + "\t" + d.mTemp[9].ToString()
                            + "\t" + d.mTemp[10].ToString() + "\t" + d.mTemp[11].ToString());
                    }
                }

            }
            */
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                using (StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath + @"\" + "klN6.txt", false, System.Text.Encoding.Default))
                {
                    foreach (var d in nn)
                    {
                        await sw.WriteLineAsync(d.dateTime + "\t" + d.colSob.ToString() + "\t" + d.mTemp[0].ToString() + "\t" + d.mTemp[1].ToString()
                            + "\t" + d.mTemp[2].ToString() + "\t" + d.mTemp[3].ToString() + "\t" + d.mTemp[4].ToString() + "\t" + d.mTemp[5].ToString()
                            + "\t" + d.mTemp[6].ToString() + "\t" + d.mTemp[7].ToString() + "\t" + d.mTemp[8].ToString() + "\t" + d.mTemp[9].ToString()
                            + "\t" + d.mTemp[10].ToString() + "\t" + d.mTemp[11].ToString());
                    }
                }

            }
            MessageBox.Show("Файлы сохранены");
        }
        public async Task<List<ClassTemp>> TempSob(List<ClassSob> classRazvertka1, int cloc)
        {

            List<ClassTemp> classTemps = new List<ClassTemp>();
         
            try
            {


                var orderedNumbers = from ClassSob in classRazvertka1 where ClassSob.badS==false
                                     orderby ClassSob.dateUR.DateTimeString()
                                     select ClassSob;
            
                DateTime dateTime = new DateTime(orderedNumbers.ElementAt(0).dateUR.GG, orderedNumbers.ElementAt(0).dateUR.MM, orderedNumbers.ElementAt(0).dateUR.DD, orderedNumbers.ElementAt(0).dateUR.HH, 0, 0, 0);
                DateTime dateTimeFirst = new DateTime(orderedNumbers.ElementAt(0).dateUR.GG, orderedNumbers.ElementAt(0).dateUR.MM, orderedNumbers.ElementAt(0).dateUR.DD, orderedNumbers.ElementAt(0).dateUR.HH, 0, 0, 0);

                DateTime dateTime1 = dateTime; //new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0);
                DateTime dateTimeEnd = new DateTime(orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.GG, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.MM, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.DD, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.HH, 0, 0, 0);

                dateTime1 = dateTime1.AddHours(cloc);
                int col = 0;
                double[] mas = new double[12];

                for (int i = 0; i <= dateTimeEnd.Subtract(dateTimeFirst).TotalHours; i += cloc)
                {
                    int xx = 1;

                    foreach (ClassSob classSob in orderedNumbers)
                    {
                        try
                        {
                            DateTime dateTimeTec = new DateTime(classSob.dateUR.GG, classSob.dateUR.MM, classSob.dateUR.DD, classSob.dateUR.HH, classSob.dateUR.Min, classSob.dateUR.CC, 0);
                            if (dateTimeTec.Subtract(dateTime).TotalHours >= 0 && dateTimeTec.Subtract(dateTime).TotalHours < cloc)
                            {
                                for (int ii = 0; ii < 12; ii++)
                                {
                                    if (classSob.mAmp[ii] >= 10)
                                    {
                                        mas[ii] = mas[ii] + 1;
                                    }

                                }
                                xx = (int)dateTimeTec.Subtract(dateTime).TotalHours + 1;
                                col++;
                            }
                            if (dateTimeTec.Subtract(dateTime).TotalHours > cloc)
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                          //  MessageBox.Show(ex.ToString());
                        }
                    }
                    //await new MessageDialog(xx.ToString()).ShowAsync();
                    if (col > 0)
                    {
                        //col = col / xx;
                        for (int ii = 0; ii < 12; ii++)
                        {

                            mas[ii] = mas[ii] / col;


                        }

                    }
                    classTemps.Add(new ClassTemp() { dateTime = dateTime, mTemp = mas, colSob = col });
                    //DataColecN.Add(new ClassTemp() { dateTime = dateTime, mTemp = masN });


                    col = 0;
                    mas = new double[12];

                    dateTime = dateTime1;
                    dateTime1 = dateTime1.AddHours(cloc);

                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return classTemps;

        }
        public async Task<List<ClassTemp>> TempNeutron(List<ClassSob> classRazvertka1, int cloc)
        {
            List<ClassTemp> classTemps = new List<ClassTemp>();
            try
            {


                var orderedNumbers = from ClassSob in classRazvertka1
                                     orderby ClassSob.dateUR.DateTimeString()
                                     select ClassSob;
                DateTime dateTime = new DateTime(orderedNumbers.ElementAt(0).dateUR.GG, orderedNumbers.ElementAt(0).dateUR.MM, orderedNumbers.ElementAt(0).dateUR.DD, orderedNumbers.ElementAt(0).dateUR.HH, 0, 0, 0);
                DateTime dateTimeFirst = new DateTime(orderedNumbers.ElementAt(0).dateUR.GG, orderedNumbers.ElementAt(0).dateUR.MM, orderedNumbers.ElementAt(0).dateUR.DD, orderedNumbers.ElementAt(0).dateUR.HH, 0, 0, 0);

                DateTime dateTime1 = dateTime; //new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0);
                DateTime dateTimeEnd = new DateTime(orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.GG, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.MM, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.DD, orderedNumbers.ElementAt(orderedNumbers.Count() - 1).dateUR.HH, 0, 0, 0);

                dateTime1 = dateTime1.AddHours(cloc);
                int col = 0;
                double[] mas = new double[12];
                double[] masN = new double[12];
                for (int i = 0; i <= dateTimeEnd.Subtract(dateTimeFirst).TotalHours; i += cloc)
                {

                    try
                    {


                        foreach (ClassSob classSob in orderedNumbers)
                        {
                            DateTime dateTimeTec = new DateTime(classSob.dateUR.GG, classSob.dateUR.MM, classSob.dateUR.DD, classSob.dateUR.HH, classSob.dateUR.Min, classSob.dateUR.CC, 0);

                            if (dateTimeTec.Subtract(dateTime).TotalHours >= 0 && dateTimeTec.Subtract(dateTime).TotalHours < cloc)
                            {
                                for (int ii = 0; ii < 12; ii++)
                                {
                                    if (classSob.mAmp[ii] >= 10)
                                    {
                                        mas[ii] = mas[ii] + 1;
                                    }

                                }
                                masN[0] = masN[0] + classSob.Nnut0;
                                masN[1] = masN[1] + classSob.Nnut1;
                                masN[2] = masN[2] + classSob.Nnut2;
                                masN[3] = masN[3] + classSob.Nnut3;
                                masN[4] = masN[4] + classSob.Nnut4;
                                masN[5] = masN[5] + classSob.Nnut5;
                                masN[6] = masN[6] + classSob.Nnut6;
                                masN[7] = masN[7] + classSob.Nnut7;
                                masN[8] = masN[8] + classSob.Nnut8;
                                masN[9] = masN[9] + classSob.Nnut9;
                                masN[10] = masN[10] + classSob.Nnut10;
                                masN[11] = masN[11] + classSob.Nnut11;
                                col++;
                            }
                            if (dateTimeTec.Subtract(dateTime).TotalHours > cloc)
                            {
                                break;
                            }
                        }

                        for (int ii = 0; ii < 12; ii++)
                        {
                            if (col > 0)
                            {
                                masN[ii] = masN[ii] / col;
                            }
                            else
                            {
                                masN[ii] = 0;
                            }


                        }
                        classTemps.Add(new ClassTemp() { dateTime = dateTime, mTemp = masN, colSob = col });


                        col = 0;
                        mas = new double[12];
                        masN = new double[12];
                        dateTime = dateTime1;
                        dateTime1 = dateTime1.AddHours(cloc);
                    }
                    catch(Exception )
                    {

                    }

                }
            }
            catch(Exception)
            {

            }
            return classTemps;


        }
        public double[] sredTemp(List<ClassTemp> classTempSred)
        {
            double[] mas = new double[12];
            foreach (var d in classTempSred)
            {
                for (int i = 0; i < 12; i++)
                {
                    mas[i] += d.mTemp[i];

                }
            }
            for (int i = 0; i < 12; i++)
            {
                int coun = (from ss in classTempSred.AsParallel() where ss.mTemp[i] == 0 select ss).Count();
                mas[i] = mas[i] / ((double)classTempSred.Count() - coun);

            }
            return mas;

        }
        public double[] sredTempN(List<ClassTemp> classTempSred)
        {
            double[] mas = new double[12];
            foreach (var d in classTempSred)
            {
                for (int i = 0; i < 12; i++)
                {
                    mas[i] += d.mTemp[i];

                }
            }
            for (int i = 0; i < 12; i++)
            {
                int coun = (from ss in classTempSred.AsParallel() where ss.mTemp[i] == 0 select ss).Count();

                mas[i] = mas[i] / ((double)classTempSred.Count() - coun);

            }
            return mas;

        }

        public class ClassTemp
        {
            public double[] mTemp { get; set; }

            public double Temp
            {
                get
                {
                    return mTemp.Sum();
                }
            }
            public double TempPro(int i)
            {

                return mTemp[i] * 100;

            }
            public double TempPro1
            {
                get
                {
                    return mTemp[0] * 100;
                }



            }
            public int colSob { get; set; }
            public DateTime dateTime { get; set; }
            public string date()
            {
                return dateTime.Date.ToString();
            }

        }
        public class ClassSob
        {


            public int count { get; set; }
            public string nameTip;
            public string nameFile { get; set; }
            public string nameklaster { get; set; }
            public string nameBAAK { get; set; }

            int sumAmp = 0;
            public int SumAmp
            {
                get
                {
                    return mAmp.Sum();
                }

                set
                {
                    sumAmp = value;
                }
            }
            public DataTimeUR dateUR { get; set; }
            public string DataTimeV
            {
                get
                {
                    return dateUR.DateString();
                }
            }
            int sumNeu = 0;
            public int SumNeu
            {

                get
                {
                    return classSobNeutronsList.Count;
                }
                set
                {
                    classSobNeutronsList = new List<ClassSobNeutron>(value);


                }
            }
            public string time { get; set; }
            int[] _mAmp = new int[12];
            public int[] mAmp
            {
                get
                {
                    return _mAmp;
                }
                set
                {
                    _mAmp = value;
                }
            }

            int[] _mTimeD = new int[12];
            public int[] mTimeD { get; set; }
            public int[] mCountN
            {
                get
                {
                    int[] bb = new int[12];
                    for (int i = 0; i < 12; i++)
                    {
                        var dd = from ClassSobNeutron in classSobNeutronsList.AsParallel()
                                 where ClassSobNeutron.D == (i + 1)
                                 select ClassSobNeutron;
                        bb[i] = bb[i] + dd.Count(); count = count + dd.Count();

                        // foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                        {
                            // if (classSobNeutron.D == i+1)
                            {
                                //   bb[i] = bb[i] + 1; count++;
                            }
                        }
                    }
                    return bb;

                }
            }
            public short Nnut0
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 1)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }

            public short Nnut1
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 2)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut2
            {


                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 3)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut3
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 4)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut4
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 5)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut5
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 6)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut6
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 7)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut7
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 8)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut8
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 9)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut9
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 10)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut10
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 11)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public short Nnut11
            {
                get
                {
                    short count = 0;
                    foreach (ClassSobNeutron classSobNeutron in classSobNeutronsList)
                    {
                        if (classSobNeutron.D == 12)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }
            public double sig0 { get; set; }
            public double sig1 { get; set; }
            public double sig2 { get; set; }
            public double sig3 { get; set; }
            public double sig4 { get; set; }
            public double sig5 { get; set; }
            public double sig6 { get; set; }
            public double sig7 { get; set; }
            public double sig8 { get; set; }
            public double sig9 { get; set; }
            public double sig10 { get; set; }
            public double sig11 { get; set; }
            public double[] mSig
            {
                get
                {
                    double[] m = new double[12];
                    m[0] = sig0;
                    m[1] = sig1;
                    m[2] = sig2;
                    m[3] = sig3;
                    m[4] = sig4;
                    m[5] = sig5;
                    m[6] = sig6;
                    m[7] = sig7;
                    m[8] = sig8;
                    m[9] = sig9;
                    m[10] = sig10;
                    m[11] = sig11;
                    return m;
                }
            }
            public int[] mNull
            {
                get
                {
                    int[] m = new int[12];
                    m[0] = Convert.ToInt32(Nnull0);
                    m[1] = Convert.ToInt32(Nnull1);
                    m[2] = Convert.ToInt32(Nnull2);
                    m[3] = Convert.ToInt32(Nnull3);
                    m[4] = Convert.ToInt32(Nnull4);
                    m[5] = Convert.ToInt32(Nnull5);
                    m[6] = Convert.ToInt32(Nnull6);
                    m[7] = Convert.ToInt32(Nnull7);
                    m[8] = Convert.ToInt32(Nnull8);
                    m[9] = Convert.ToInt32(Nnull9);
                    m[10] = Convert.ToInt32(Nnull10);
                    m[11] = Convert.ToInt32(Nnull11);
                    return m;
                }
            }
            public short Nnull0 { get; set; }
            public short Nnull1 { get; set; }
            public short Nnull2 { get; set; }
            public short Nnull3 { get; set; }
            public short Nnull4 { get; set; }
            public short Nnull5 { get; set; }
            public short Nnull6 { get; set; }
            public short Nnull7 { get; set; }
            public short Nnull8 { get; set; }
            public short Nnull9 { get; set; }
            public short Nnull10 { get; set; }
            public short Nnull11 { get; set; }

            public string TimeS0 { get; set; }
            public string TimeS1 { get; set; }
            public string TimeS2 { get; set; }
            public string TimeS3 { get; set; }
            public string TimeS4 { get; set; }
            public string TimeS5 { get; set; }
            public string TimeS6 { get; set; }
            public string TimeS7 { get; set; }
            public string TimeS8 { get; set; }
            public string TimeS9 { get; set; }
            public string TimeS10 { get; set; }
            public string TimeS11 { get; set; }

            public int[] masTime()
            {
                int[] mas = new int[12];
                mas[0] = Convert.ToInt32(TimeS0);
                mas[1] = Convert.ToInt32(TimeS1);
                mas[2] = Convert.ToInt32(TimeS2);
                mas[3] = Convert.ToInt32(TimeS3);
                mas[4] = Convert.ToInt32(TimeS4);
                mas[5] = Convert.ToInt32(TimeS5);
                mas[6] = Convert.ToInt32(TimeS6);
                mas[7] = Convert.ToInt32(TimeS7);
                mas[8] = Convert.ToInt32(TimeS8);
                mas[9] = Convert.ToInt32(TimeS9);
                mas[10] = Convert.ToInt32(TimeS10);
                mas[11] = Convert.ToInt32(TimeS11);

                return mas;
            }
            //  public short[] AmpSum()
            // {
            //  return new short[12] { Amp0, Amp1, Amp2, Amp3, Amp4, Amp5, Amp6, Amp7, Amp8, Amp9, Amp10, Amp11 };
            //  }
            public List<string> ShovSelect()
            {
                List<string> vs = new List<string>();
                vs.Add(vs.Count.ToString() + " " + mAmp[0].ToString() + " " + sig0.ToString());

                return vs;
            }



            public List<ClassSobNeutron> classSobNeutronsList { get; set; }

            public bool badS = false;





        }
        public struct ClassSobNeutron
        {

            public int Amp { get; set; }
            public int D { get; set; }

            public int TimeFirst { get; set; }
            public int TimeFirst3 { get; set; }
            public int TimeAmp { get; set; }
            public int TimeEnd3 { get; set; }
            public int TimeEnd { get; set; }
        }
        public struct DataTimeUR
        {
            public int GG { get; set; }
            public int MM { get; set; }
            public int DD { get; set; }
            public int HH { get; set; }
            public int Min { get; set; }
            public int CC { get; set; }
            public int Mil { get; set; }
            public int ML { get; set; }
            public int NN { get; set; }
            public DataTimeUR(int gg, int mm, int dd, int hh, int min, int s, int mil, int ml, int nn)
            {
                GG = gg;
                MM = mm;
                DD = dd;
                HH = hh;
                Min = min;
                CC = s;
                Mil = mil;
                ML = ml;
                NN = nn;
            }
            public void corectTime(string time)
            {
                string[] t = time.Split('_')[1].Split(' ')[0].Split('.');

                DateTime g = new DateTime(Convert.ToInt32(t[2]), Convert.ToInt32(t[1]), Convert.ToInt32(t[0]));

                DateTime f = new DateTime(Convert.ToInt32(t[2]), Convert.ToInt32(t[1]), Convert.ToInt32(t[0]));

                if (DD != f.Day || DD != g.AddDays(-1).Day)
                {
                    DD = f.Day;
                    MM = f.Month;
                    GG = f.Year;

                }
                else
                {
                    if (DD == f.Day)
                    {
                        MM = f.Month;
                        GG = f.Year;
                    }
                    else
                    {
                        MM = g.Month;
                        GG = g.Year;
                    }


                }

            }
            public string TimeString()
            {
                return DD.ToString("00") + "." + HH.ToString("00") + "." + Min.ToString("00") + "." + CC.ToString("00") + "." + Mil.ToString("00") + "." + ML.ToString("00") + "." + NN.ToString("00");
            }
            /// <summary>
            /// Возращает (String) дату и время события гггг.мм.дд.чч.мм.сс.мм.мм.нн
            /// </summary>
            /// <returns></returns>
            public string DateTimeString()
            {
                return GG.ToString("0000") + "." + MM.ToString("00") + "." + DD.ToString("00") + "." + HH.ToString("00") + "." + Min.ToString("00") + "." + CC.ToString("00") + "." + Mil.ToString("00") + "." + ML.ToString("00") + "." + NN.ToString("00");
            }
            /// <summary>
            /// Возращает (String) дату события гггг.мм.дд
            /// </summary>
            /// <returns></returns>
            public string DateString()
            {
                return GG.ToString("0000") + "." + MM.ToString("00") + "." + DD.ToString("00");
            }
        }
        public async static Task<List<ClassSob>> GetDataSob(string uslovie, ObservableCollection<ClassSob> classSobs, string Path)
        {
            List<ClassSob> entries = new List<ClassSob>();
            SQLiteConnection db =
                  new SQLiteConnection("Data Source = " + Path, true);
          
            db.Open();
        
            SQLiteCommand selectCommand = new SQLiteCommand
                ("SELECT * from События where " + uslovie, db);

            SQLiteDataReader query = selectCommand.ExecuteReader();

            try
            {


                while (query.Read())
                {
                    
                    List<ClassSobNeutron> cll = new List<ClassSobNeutron>();
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

                            masS[i - 43] = Convert.ToDouble(query.GetString(i));
                        }
                        catch (Exception ex)
                        {

                            //   await new MessageDialog(ex.ToString() + i.ToString() + "\t" + query.GetString(i) + "\t" + query.GetString(i).Replace(",", ".")).ShowAsync();
                        }
                    }
                    int badint = query.GetInt32(55);
                    bool bad = false;
                    if (badint == 1)
                    {
                        bad = true;
                    }
                    if (masN.Sum() > 0)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            if (masN[i] > 0)
                            {
                                for (int y = 0; y < masN[i]; y++)
                                {


                                    cll.Add(new ClassSobNeutron()
                                    {
                                        D = i + 1,
                                        Amp = 5,
                                        TimeAmp = 2,
                                        TimeEnd = 4,
                                        TimeEnd3 = 2,
                                        TimeFirst = 0,
                                        TimeFirst3 = 1
                                    });
                                }
                            }

                        }

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
                    string[] strTime = query.GetString(1).Split('.');


                    DataTimeUR dataTimeUR = new DataTimeUR(0, 0, Convert.ToInt16(strTime[0]), Convert.ToInt16(strTime[1]), Convert.ToInt16(strTime[2]), Convert.ToInt16(strTime[3]),
                                 Convert.ToInt16(strTime[4]), Convert.ToInt16(strTime[5]), Convert.ToInt16(strTime[6]));
                    if (query.GetString(2).Contains("Test"))
                    {
                        string[] vs = query.GetString(2).Split('_');
                        string gg = vs[0] + "_" + vs[2];
                        dataTimeUR.corectTime(gg);
                    }
                    else
                    {
                        dataTimeUR.corectTime(query.GetString(2));
                    }

                    string time1 = strTime[0] + "." + strTime[1] + "." + (Convert.ToInt32(strTime[2])).ToString("00") + "." + strTime[3] + "." + strTime[4] + "." + strTime[5] + "." + strTime[6];
                    classSobs.Add(new ClassSob()
                    {
                        nameFile = query.GetString(2),
                        nameklaster = query.GetString(4),
                        nameBAAK = query.GetString(3),
                        time = time1,
                        mAmp = masAmp,
                        dateUR = dataTimeUR,
                        //   dateUR = dataTimeUR,
                        //TimeS0 = timeS[0].ToString(),
                        // TimeS1 = timeS[1].ToString(),
                        // TimeS2 = timeS[2].ToString(),
                        // TimeS3 = timeS[3].ToString(),
                        // TimeS4 = timeS[4].ToString(),
                        // TimeS5 = timeS[5].ToString(),
                        // TimeS6 = timeS[6].ToString(),
                        // TimeS7 = timeS[7].ToString(),
                        //  TimeS8 = timeS[8].ToString(),
                        // TimeS9 = timeS[9].ToString(),
                        // TimeS10 = timeS[10].ToString(),
                        // TimeS11 = timeS[11].ToString(),
                        //  mTimeD = timeS,
                        sig0 = masS[0],
                        sig1 = masS[1],
                        sig2 = masS[2],
                        sig3 = masS[3],
                        sig4 = masS[4],
                        sig5 = masS[5],
                        sig6 = masS[6],
                        sig7 = masS[7],
                        sig8 = masS[8],
                        sig9 = masS[9],
                        sig10 = masS[10],
                        sig11 = masS[11],

                        Nnull0 = Convert.ToInt16(masNull[0]),
                        Nnull1 = Convert.ToInt16(masNull[1]),
                        Nnull2 = Convert.ToInt16(masNull[2]),
                        Nnull3 = Convert.ToInt16(masNull[3]),
                        Nnull4 = Convert.ToInt16(masNull[4]),
                        Nnull5 = Convert.ToInt16(masNull[5]),
                        Nnull6 = Convert.ToInt16(masNull[6]),
                        Nnull7 = Convert.ToInt16(masNull[7]),
                        Nnull8 = Convert.ToInt16(masNull[8]),
                        Nnull9 = Convert.ToInt16(masNull[9]),
                        Nnull10 = Convert.ToInt16(masNull[10]),
                        Nnull11 = Convert.ToInt16(masNull[11]),
                        classSobNeutronsList = cll,
                        badS=bad

                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                db.Close();
            




            return entries;
        }
    }
}
