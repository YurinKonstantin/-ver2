using System;
using System.Net.Sockets;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Windows.Media.Animation;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

using LiveCharts.Events;
using System.ComponentModel;
using System.Net;
using System.Drawing;
using System.IO.Ports;
using InteractiveDataDisplay.WPF;
using URAN_2017.FolderSetUp;
using System.Reactive;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // MyGrafic nf;
        double[] x;
        double[] y;
        public ObservableCollection<int> LabelsRaz { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        

            _DataColec1 = new ObservableCollection<Bak>();
            _DataColecBAAK12100 = new ObservableCollection<Bak>();
            _DataColecViev = new ObservableCollection<BAAK12T>();
            _DataColecVievList2 = new ObservableCollection<ClassBAAK12NoTail>();
            _DataColecVievList3 = new ObservableCollection<ClassBAAK12_100>();
            List1.ItemsSource = _DataColecViev;
            List2.ItemsSource = _DataColecVievList2;
            List3.ItemsSource = _DataColecVievList3;
            ListEror.ItemsSource = ListEr;
            //DataContext = customer;
            nf = new MyGrafic { };
            nf.NewCol();
            DataContext = nf;
            comport.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            foreach(MainWindow window in App.Current.Windows)
            {
                MyGrafic.MainWindow = window;
            }
            ChatMain.MouseDoubleClick += Auto;
        }

        private void Auto(object sender, MouseButtonEventArgs e)
        {
            ToggleAutoX.Toggled1 = false;
            ToggleAutoY.Toggled1 = false;
            ToggleButton_MouseLeftButtonDown_1(null, null);
                ToggleButton_MouseLeftButtonDown_2(null, null);
        }

        ObservableCollection<ClassErrorStartAndIspravlenie> ListEr = new ObservableCollection<ClassErrorStartAndIspravlenie>();
        /// <summary>
        /// зупускает набор
        /// </summary>
        /// 


        private async void StartRun()
        {
            if (BAAK12T.ConnnectURANDelegate != null)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { progres.IsIndeterminate = true; }));
                 Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Start.IsEnabled = false; }));
               
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Obnoviti.IsEnabled = false; }));

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Foreground = System.Windows.Media.Brushes.Red; }));

                try
                {
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Подготовка к запуску"; }));

                    cancellationTokenSource = new CancellationTokenSource();
                    CancellationToken cancellationToken = cancellationTokenSource.Token;
                    RanName();
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Загрузка регистров плат"; }));

                    await ЗапускНастройкиТаск();
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Start.IsEnabled = false; }));
                
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка режима синхронизации"; }));

                    await РежимСинхИлиНетТаск(set.DelayClok);
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Разрешаем работу"; }));

                    await ПускURANDТаск();

                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Запись в БД"; }));
                    BDReadRAN(BAAK12T.NameRan, ClassParentsBAAK.Синхронизация, true, BAAK12T.PorogAll, BAAK12T.TrgAll, TimeTaimer1);
                    BdAddRANTimeПуск(BAAK12T.NameRan, TimeПуск());
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Foreground = System.Windows.Media.Brushes.Black; }));
                    Task.Run(() => MainYpravlenia(IntervalNewFile, set.КолТригТест, set.ИнтервалТригТест, set.TimeRanHors, set.TimeRanMin, cancellationToken));
                    ЗапускРеадТаск(cancellationToken);
                
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { contextTestRan.IsEnabled = true; }));

                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop.IsEnabled = true; }));

                   // ZapicDataBDTasc1(cancellationToken);
                   if(set.FlagSaveBD)
                    {
                        Task.Run(() => ZapicDataBDTasc(cancellationToken));
                    }
                  
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка УРАН запущена"; }));
                    await ZapicDataTasc1(cancellationToken);
                
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("Нет доступных плат" + e.ToString(), "Ошибка");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при СТАРТЕ" + "  " + "Имя ошибки" + ex.ToString(), "Ошибка");
                }
                finally
                {
                    Stop.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop.IsEnabled = false; }));//To Do
             
                    Start.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Start.IsEnabled = true; }));


                   
                    Obnoviti.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Obnoviti.IsEnabled = true; }));
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Foreground = System.Windows.Media.Brushes.Red; }));
                    
                }
                try
                {

                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Foreground = System.Windows.Media.Brushes.Red; }));
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Идет процесс завершения работы"; }));
                    try
                    {


                        if (ClassParentsBAAK.Синхронизация)
                        {
                            try
                            {
                                MS.АвтономныйКлокРазрешен(0);
                            }
                            catch
                            {
                                MessageBox.Show("Произошла ошибка при Остановке MS1" + "  " + "Имя ошибки", "Ошибка");
                            }
                            try
                            {
                                MS1.АвтономныйКлокРазрешен(0);
                            }
                            catch
                            {
                                MessageBox.Show("Произошла ошибка при Остановке MS1" + "  " + "Имя ошибки", "Ошибка");
                            }

                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка при Остановке MS" + "  " + "Имя ошибки", "Ошибка");
                    }
                    
                    BdAddRANTimeСтоп(BAAK12T.NameRan, TimeПуск());
                    Start.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Start.IsEnabled = true; }));
                  
                    Stop.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop.IsEnabled = false; }));

                  
                    contextTestRan.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { contextTestRan.IsEnabled = false; }));
                    Obnoviti.IsEnabled = true;
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка УРАН Завершила работу"; }));

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при Остановке" + "  " + "Имя ошибки" + ex.ToString(), "Ошибка");
                }
                finally
                {
                   Stop.IsEnabled = false;
                    Start.IsEnabled = true;
                 
                    contextTestRan.IsEnabled = false;
                    Obnoviti.IsEnabled = true;
                    rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Остановка УРАН"; }));
                }
                rezimYst.Content = "Установка УРАН остановлена";
                progres.IsIndeterminate = false;
            }
            else
            {
                MessageBox.Show("Нет доступных плат", "Остановка программы");
            }

        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(set.DelayClok.ToString());
            StartRun();
        }
        private async void Stop_Click(object sender, RoutedEventArgs e)
        {
            Stop.IsEnabled = false;
        
            contextTestRan.IsEnabled = false;
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Идет процесс завершения работы"; }));
   
            try
            {
                if (cancellationTokenSource != null)
                {
                    BAAK12T.StopURANDelegate?.Invoke();
                 
                    cancellationTokenSource.Cancel();

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при Остановке" + "  " + "Имя ошибки" + ex.ToString(), "Ошибка");


            }
            finally
            {
                Stop.IsEnabled = false;
               Start.IsEnabled = true;
      

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NastroikiAll Window2 = new NastroikiAll
            {
                Owner = this
            };
            this.Hide();
            Window2.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window11 = new Window1
            {
                Owner = this
            };
            Window11.Show();
        }
        private void Inz(BAAK12T dd, string hg)
        {
            dd.Host = hg;
        }
        private Double Sum(int[] n, int x)
        {
            Double res = 0;
            foreach (int i in n)
            {
                res += Math.Pow((i - x), 2);
            }
            return res;
        }
        private SerialPort comport = new SerialPort();
       public List<WindowChart> lstChart = new List<WindowChart>();
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

            try
            {
              string v ="{" + "name" +  ":" +  "reboot_arduino" +  "," +  "ip" +  ":" + "192.168.2.202" + "," +  "port" + ":" + 80 + "," +  "relay_number" + ":" + 1 + "}";
                //   string v = "{" + quote + "name" + quote + ":" + quote + "reboot_arduino" + quote + "," + quote + "ip" + quote + ":" + quote + "192.168.2.202" + quote + "," + quote + "port" + quote + ":" + 80 + "," + quote + "relay_number" + quote + ":" + 1 + "}";
                //  HttpWebRequest httpWebRequest = WebRequest.Create(@"https://api.vk.com/method/video.getAlbums?&access_token=5c38a232d7142501bfb227df273e6b96cffc6f05e263fcb237cd02bca72a0824d2275855bbfce1578acbd") as HttpWebRequest;

                //string v = "{" + quote + "name" + quote + ":" + quote + "sync_device" + quote + "," + quote + "link_mask" + quote + ":" + set.LincClok + "," + quote + "delay" + quote + ":" + t + "}";
                string iP = "192.168.1.78";
                string port = "80";
                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + iP + ":" + 80 + "/api/v1/URAN_Temp_and_Pressure/" + v) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";

                // httpWebRequest.BeginGetResponse((ar) =>
                // {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        Thread.Sleep(50);
                        MessageBox.Show(output.ToString());
                       // MS = JsonConvert.DeserializeObject<ClassMC>(output.ToString());
                        //Dispatcher.BeginInvoke(new Action<string>(s => { Otvet.Items.Add(data.Microseconds.ToString()); }), data.Microseconds.ToString());
                        //MessageBox.Show(data.text.ToString());
                        response.Close();

                    }
                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    MessageBox.Show(ez.ToString());
                }

                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
                //labekSystemMessage.Items.Add("Ошибка: " + ex);
                MessageBox.Show(ex.ToString());
            }
        }


      




        private double _toRaz=10;
        private double _fromRaz=0;
        public double FromRaz
        {
            get { return _fromRaz; }
            set
            {
                _fromRaz = value;
                OnPropertyChanged("FromRaz");
            }
        }

        public double ToRaz
        {
            get { return _toRaz; }
            set
            {
                _toRaz = value;
                OnPropertyChanged("ToRaz");
            }
        } 

        public async Task Xcxc()
        {
            int[,] dd = new int[12, 1024];
            dd[0, 2] = 100;
            dd[0, 2] = 90;
            dd[0, 2] = 105;
            dd[0, 2] = 80;
            MyGrafic.AddPointRaz(dd, "NameKl");
        }

        private void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

    
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GridStartInfoError.Visibility = Visibility.Hidden;
            GridStartInfo.Visibility = Visibility.Visible;
            int i = 0;
            int i1 = 0;
            int i2 = 0;
            int i3 = 0;
            try
            {
                progres.IsIndeterminate = true;
                if (BAAK12T.DiscConnnectURANDelegate != null)
                {
                    BAAK12T.DiscConnnectURANDelegate?.Invoke();
                    i++;
                }

                if (BAAK12T.ДеИнсталяцияDelegate != null)
                {
                    BAAK12T.ДеИнсталяцияDelegate?.Invoke();
                    i1++;
                }

                DeInitializeMS();

   
                _DataColecViev.Clear();
                _DataColecVievList2.Clear();
           
                try
                {
                    MyGrafic.SeriesCollection.Clear();
                    MyGrafic.SeriesCollectionN.Clear();
                }
                catch (NullReferenceException ee)
                {
                    Debug.WriteLine("Error 427");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()+" "+ "MyGrafic.SeriesCollection.Clear();", "Ошибка");
                }
                try
                {
                    MyGrafic.Labels.Clear();
                    MyGrafic.LabelsN.Clear();
                }
                catch (NullReferenceException ee)
                {
                    Debug.WriteLine("Error 440");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()+" "+"MyGrafic.Labels.Clear();", "Ошибка");
                }

                await Запуск();
                progres.IsIndeterminate = false;
           
                GridStartInfo.Visibility = Visibility.Hidden;
                //MessageBox.Show("обновленно");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()+i.ToString()+" "+i1.ToString()+" "+i2.ToString()+" "+i3.ToString(), "Ошибка");
            }
        }

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            ClassParentsBAAK.Синхронизация = true;
            
        }
        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassParentsBAAK.Синхронизация = false;    
        }

        private void OProg_Click(object sender, RoutedEventArgs e)
        {
            WindowOprogramme WindowoPr = new WindowOprogramme
            {
                Owner = this
            };
            WindowoPr.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window2РучнойТестДлительность winTestDl = new Window2РучнойТестДлительность();

            if (winTestDl.ShowDialog() == true)
            {
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {  rezimYst.Content = "Идет подготовка к методическуму набору по длительности"; }));
               
                int p = winTestDl.Porog;
                int t = winTestDl.Trig;
                int d = winTestDl.DlitTestRan;
                BAAK12T.TestRanSetUpDelegate?.Invoke(p, t, false);
                Task myTestRanMan = Task.Run(() => TestRanTask(d));
            }
            else
            {

            }
 
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window2РучнойТестКол winTestKol = new Window2РучнойТестКол();

            if (winTestKol.ShowDialog() == true)
            {
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {rezimYst.Content = "Идет подготовка к методическуму набору по количеству событий(программный триггер)"; }));
                
                int k = winTestKol.KolSobTestRan;
                int i = winTestKol.Interval;
                
                BAAK12T.TestRanSetUpDelegate?.Invoke(10, 10, true);
                Task myTestRanMan = Task.Run(() => TestRanTask1(k, i));
            }
            else
            {

            }
        }
        private void Jj(object sender, RoutedEventArgs e)
        {
 
                    MessageBox.Show(BAAK12T.PorogAll.ToString());         
        }
        private void Jj1(object sender, RoutedEventArgs e)
        {
            BAAK12T dd = _DataColecViev[List1.SelectedIndex];
           string s = dd.NamKl.ToString() + " " + "Плата " + dd.NameBAAK12.ToString() + "\r\n";
            for (int i = 0; i < 12; i++)
            {
                s += dd.masnul[i].ToString() + "\t";
            }
            s += "\r\n";
            MessageBox.Show(s);

        }
        private void Jj2(object sender, RoutedEventArgs e)
        {
          
            MessageBox.Show(BAAK12T.TrgAll.ToString());
      

        }
        int eh=0;
        private void List1_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            if (_DataColecViev.Count>0)
            {
                context.IsEnabled = true;
                context1.Items.Clear();
                context2.Items.Clear();
                
                if (List1.SelectedIndex > -1)
                {
                    
                   
                    context1.Items.Clear();
                    context2.Items.Clear();
                    context3.Items.Clear();
                    BAAK12T dd = _DataColecViev[eh];
                    var menuItemporog = new MenuItem
                    {
                        Header = dd.NamKl,
                    };
                    menuItemporog.Click += Jj;
                    
                    context1.Items.Add(menuItemporog);
                    var menuItNull = new MenuItem
                    {
                        Header = dd.NamKl,
                    };
                    menuItNull.Click += Jj1;
                    context3.Items.Add(menuItNull);
                    var menuItTr = new MenuItem
                    {
                        Header = dd.NamKl,
                    };
                    menuItTr.Click += Jj2;
                    context2.Items.Add(menuItTr);
                }
                else
                {
                    context1.Items.Clear();
                    foreach (BAAK12T dd in _DataColecViev)
                    {
                        
                    }
                    

                }
            }

        }

        private void List1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var ff = (ListView)sender;
            if (ff.SelectedIndex >= 0)
            {


                BAAK12T bAAK12T = (BAAK12T)ff.SelectedItem;
                BAAK12T.otobKl = bAAK12T.NamKl + bAAK12T.BAAKTAIL.ToString();
              
            }

        }

        private void List1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
         
            if (List1.SelectedIndex == eh)
            {
                eh = -1;
                List1.SelectedIndex = -1;
       
            }
            else
            {
                eh = List1.SelectedIndex;
          
            }
        }
        MyGrafic nf;
        private bool первая_активация = true;
        private async void Window_Activated(object sender, EventArgs e)
        {
            if(первая_активация)
            {
                первая_активация = false;
                await Запуск();
              
                ВремяОтобрTask();
                первая_активация = false;
                try
                {
                    Task task = Task.Run(() => StartServer());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка");
                }
            }
            
        }
        private void ChartOnDataClick(object sender, ChartPoint p)
        {
            var asPixels = Chart.ConvertToPixels(p.AsPoint());
            MessageBox.Show("[EVENT] You clicked (" + p.X + ", " + p.Y + ") in pixels (" +
                            asPixels.X + ", " + asPixels.Y + ")");
            
            Console.WriteLine("[EVENT] You clicked (" + p.X + ", " + p.Y + ") in pixels (" +
                            asPixels.X + ", " + asPixels.Y + ")");
        }

        private void Chart_OnDataHover(object sender, ChartPoint p)
        {
            Console.WriteLine("[EVENT] you hovered over " + p.X + ", " + p.Y);
        }

        private void ChartOnUpdaterTick(object sender)
        {
            Console.WriteLine("[EVENT] chart was updated");
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            Console.WriteLine("[EVENT] axis range changed");
        }

        private void ChartMouseMove(object sender, MouseEventArgs e)
        {
            var point = Chart.ConvertToChartValues(e.GetPosition(Chart));
            X.Content = point.X.ToString("N");
            Y.Content = point.Y.ToString("N");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
           
            X1.MinValue = double.NaN;
            X1.MaxValue = double.NaN;
            Y1.MinValue = double.NaN;
            Y1.MaxValue = double.NaN;
        }
   
            bool isDataDirty = true;
        private void Window_Closing(object sender, CancelEventArgs e)
        {

            if (!Start.IsEnabled)
            {
                MessageBox.Show("Вданный момент закрытие не возможно. Нажмите ОК, остановите работу программы и повторите закрытие", "Закрытие программы");
                e.Cancel = true;
            }
            else
            {
                // If data is dirty, notify user and ask for a response
                if (this.isDataDirty)
                {
                    string msg = "Программа будет закрыта. Закрыть программу?";
                    MessageBoxResult result =
                      MessageBox.Show(
                        msg,
                        "Data App",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        // If user doesn't want to close, cancel closure
                        e.Cancel = true;
                    }
                    else
                    {try
                        {
                            if (BAAK12T.DiscConnnectURANDelegate != null)
                            {
                                BAAK12T.DiscConnnectURANDelegate?.Invoke();
                            }

                            if (BAAK12T.ДеИнсталяцияDelegate != null)
                            {
                                BAAK12T.ДеИнсталяцияDelegate?.Invoke();
                            }
                            try
                            {
                                MyGrafic.SeriesCollection.Clear();
                            }
                            catch(Exception ex)
                            {

                            }
                            try
                            {
                                MyGrafic.Labels.Clear();
                            }
                            catch (Exception ex)
                            {

                            }
                            ChatMain.MouseDoubleClick -= Auto;
                            DeInitializeMS();
                        }
                        catch
                        {
                            
                        }
                        try
                        {
                            foreach (var v in _DataColecViev)
                            {
                                v.Dispose();
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            foreach (var v in _DataColecVievList2)
                            {
                                v.Dispose();
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                           foreach(var d in lstChart)
                            {
                                d.Close();

                            }
                        }
                        catch
                        {

                        }
                        _DataColecViev.Clear();
                        _DataColecVievList2.Clear();
                    }
                }
            }
        }


        // Запуск сервера и вспомогательного потока акцептирования клиентских подключений
        // т.е. назначения сокетов ответственных за обмен сообщениями 
        // с соответствующим клиентским приложением
       async void  StartServer()
        {
                try
                {
                IPHostEntry iPHost = Dns.GetHostEntry("localhost");
                IPAddress iPAddress = iPHost.AddressList[1];
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8888);
              
                ServisMonitor.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { ServisMonitor.Visibility = Visibility.Visible; }));
               
                while (true)
                {
                   
                    Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(iPEndPoint);
                    socket.Listen(10);
                    // Task task = Task.Run(() => fg(socket));
                    Socket s = socket.Accept();
                    string data = null;
                    byte[] bytes = new byte[1024];
                    byte[] Data;
                    int bytesCount = s.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesCount);
                    string str = "Проблема";
                    string[] ss = data.Split('\t');
                    if (ss[0] == "Stop")
                    {

                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop_Click(null, null); }));
                          
                    }
                    else
                    {
                        if (ss[0] == "BDB!")
                        {
                            Data = await Task<byte>.Run(() => BDselect112(ss[1]));

                            s.Send(Data);
                        }
                        else
                        {
                            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { str = rezimYst.Content.ToString(); }));
                            if (_DataColecViev.Count != 0)
                            {
                                str += " " + (_DataColecViev.Count+ _DataColecVievList2.Count) + "\n\t";
                                foreach (BAAK12T bak in _DataColecViev)
                                {
                                    str += bak.NamKl + "\t" + bak.CтатусБААК12 + "\t" + bak.КолПакетов + "\t" + bak.ТемпПакетов + "\n\t";
                                }
                                foreach (ClassBAAK12NoTail bak in _DataColecVievList2)
                                {
                                    str += bak.NamKl + "\t" + bak.CтатусБААК12 + "\t" + bak.КолПакетов + "\t" + bak.ТемпПакетов + "\n\t";
                                }
                            }
                            s.Send(Encoding.UTF8.GetBytes(str));
                        }
                    }
                   
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                    socket.Close();
                    socket.Dispose();
                }
                }
                catch
                {
              
                }
            
        }

        public void Fg(Socket socket)
        {
            bool f = true;
            while (f)
            {
                Socket s = socket.Accept();
                string str;
                if (_DataColecViev.Count!=0)
                {
                    str = "Запущены БААК12-200Т"+_DataColecViev.Count;
                }
                else
                {
                    str = "Работающих кластеров нет";
                }
                if (_DataColecVievList2.Count != 0)
                {
                    str += " и БААК12-200" + _DataColecVievList2.Count;
                }
           
                s.Send(Encoding.UTF8.GetBytes(str));
                s.Shutdown(SocketShutdown.Both);
                s.Close();
                f = false;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            X11.MinValue = double.NaN;
            X11.MaxValue = double.NaN;
            Y11.MinValue = double.NaN;
            Y11.MaxValue = double.NaN;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            GridStartInfoError.Visibility = Visibility.Hidden;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button.Tag.ToString()=="0")
            {
                comport.PortName = "COM4";
                comport.BaudRate = Convert.ToInt16(9600);
                comport.Open();

                comport.Write("1");
                Thread.Sleep(2000);

                comport.Write("0");
                comport.Close();
                 MessageBox.Show("Питание МС1 и МС2 перегружена, ожидайте загрузок плат и нажмите 'Обновить'", "Управление питанием");
            }
            else
            {
                MessageBox.Show("Извините, на данный момент эта функция находится в разработке", "Управление питанием");
            }
           
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Chart1 != null)
            {


                var vv = (TabControl)sender;
                if (vv.SelectedIndex == 1)
                {

                    BAAK12T.grafOtob = true;
                   
                }
                else
                {
                    BAAK12T.grafOtob = false;
                  
                }
            }
        }

        private void HorizontalToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            setP.FlagMainRezim = true;
        }

        private void HorizontalToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            setP.FlagMainRezim = false;
        }
        private void Bu_MouseLeftButtonDownFMR(object sender, MouseButtonEventArgs e)
        {
            if (BuMC.Toggled1 == true)
            {

                ClassParentsBAAK.Синхронизация = true;
                LabFlagMC.Content = "Вкл";
                LabFlagMC.Foreground = System.Windows.Media.Brushes.Green;



            }
            else
            {
                ClassParentsBAAK.Синхронизация = false;
                LabFlagMC.Content = "Выкл";
                LabFlagMC.Foreground = System.Windows.Media.Brushes.Red;

            }


        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
           
            if(PanSet.Visibility==Visibility.Collapsed)
            {
                PanSet.Visibility = Visibility.Visible;
            }
            else
            {
                PanSet.Visibility = Visibility.Collapsed;
            }
         
        }
        int XOt = 0;
        int YOt = 0;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            ChatMain.PlotOriginX = v;
            XOt = v;
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            ChatMain.PlotWidth = v - XOt;
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            ChatMain.PlotOriginY = v;
            YOt = v;
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            int v = Convert.ToInt32(((TextBox)sender).Text);
            ChatMain.PlotHeight= v - YOt;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
          double x=  ChatMain.PlotOriginY;
            double y = ChatMain.PlotHeight;
          
        }

        private void ToggleButton_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {          
                TextFX.IsEnabled = !ToggleAutoX.On1;
            TextEX.IsEnabled = !ToggleAutoX.On1;
            if(ToggleAutoX.On1)
            {
                ChatMain.IsAutoFitEnabled = true;
                if(!ToggleAutoY.On1)
                {
                    int v = Convert.ToInt32(TextFY.Text);
                    ChatMain.PlotOriginY = v;
                    YOt = v;
                    v = Convert.ToInt32(TextEY.Text);
                    ChatMain.PlotHeight = v - YOt;
                }
            }
            else
            {
                int v = Convert.ToInt32(TextFX.Text);
                ChatMain.PlotOriginX = v;
                XOt = v;
                v = Convert.ToInt32(TextEX.Text);
                ChatMain.PlotWidth = v - XOt;
            }

        }
        
        private void ToggleButton_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            TextFY.IsEnabled = !ToggleAutoY.On1;
            TextEY.IsEnabled = !ToggleAutoY.On1;
            if (ToggleAutoY.On1)
            {
                ChatMain.IsAutoFitEnabled = true;
                if (!ToggleAutoX.On1)
                {
                    int v = Convert.ToInt32(TextFX.Text);
                    ChatMain.PlotOriginX = v;
                    XOt = v;
                    v = Convert.ToInt32(TextEX.Text);
                    ChatMain.PlotWidth = v - XOt;
                }
            }
            else
            {
                int v = Convert.ToInt32(TextFY.Text);
                ChatMain.PlotOriginY = v;
                YOt = v;
                v = Convert.ToInt32(TextEY.Text);
                ChatMain.PlotHeight = v - YOt;
            }
        }

        private void ChartContex1_Click(object sender, RoutedEventArgs e)
        {
            BAAK12T dd = (BAAK12T)List1.SelectedItem;
            var windowChart= dd.openWindowsChart();
            lstChart.Add(windowChart);

        }

        private void ChartContex1_Click_1(object sender, RoutedEventArgs e)
        {
            BAAK12T dd = (BAAK12T)List1.SelectedItem;
            var windowChart = dd.openWindowsChartTail();
            lstChart.Add(windowChart);
        }
    }
    public class VisibilityToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility)value) == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

}
