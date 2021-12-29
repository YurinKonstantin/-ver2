using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace URAN_2017
{
    public partial class MainWindow
    {
        DateTime alarmNewRun { get; set; }
        private void NewRun()
        {
            if (DateTime.Compare(DateTime.UtcNow, alarmNewRun) > 0)
            {
               // Task task= Task.Run(() => NewRanWork());
            }
            NewRanWork();

        }
        delegate void ParametrizedMethodInvoker1();
        void Log_left_accs()
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Dispatcher?.Invoke(new ParametrizedMethodInvoker1(Log_left_accs)" + "\n"); //допишет текст в конец файла
                Dispatcher?.Invoke(new ParametrizedMethodInvoker1(Log_left_accs));
                return;
            }
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Stop.IsEnabled = false" + "\n"); //допишет текст в конец файла
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop.IsEnabled = false; }));
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "contextTestRan.IsEnabled = false" + "\n"); //допишет текст в конец файла
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { contextTestRan.IsEnabled = false; }));
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Идет процесс завершения работы" + "\n"); //допишет текст в конец файла
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Идет процесс завершения работы"; }));
            try
            {
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "cancellationTokenSource " + "\n"); //допишет текст в конец файла
                if (cancellationTokenSource != null)
                {
                    File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "BAAK12T.StopURANDelegate" + "\n"); //допишет текст в конец файла
                    BAAK12T.StopURANDelegate?.Invoke();
                    File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " cancellationTokenSource.Cancel()" + "\n"); //допишет текст в конец файла
                    cancellationTokenSource.Cancel();
                }
            }

            catch (Exception ex)
            {
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " error"+ex.Message + "\n"); //допишет текст в конец файла
                MessageBox.Show("Произошла ошибка при Остановке" + "  " + "Имя ошибки" + ex.ToString(), "Ошибка");
            }
            finally
            {
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " Stop.IsEnabled = false" + "\n"); //допишет текст в конец файла
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Stop.IsEnabled = false; }));
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " Start.IsEnabled = true;" + "\n"); //допишет текст в конец файла
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Start.IsEnabled = true; }));
            
                
            }
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Дожидаемся остановки набора;" + "\n"); //допишет текст в конец файла
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Дожидаемся остановки набора"; }));
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " myZapicDataTascTask.Wait(;" + "\n"); //допишет текст в конец файла
            try
            {


                myZapicDataTascTask.Wait();//Дожидаемся остановки набора
            }
            catch(Exception e)
            {
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " error1"+e.Message + "\n"); //допишет текст в конец файла
            }
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Дожидаемся разблокировки кнопки обновить, точно набор завершен"; }));
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " Дожидаемся разблокировки кнопки обновить, точно набор завершен" + "\n"); //допишет текст в конец файла
            while (!Obnoviti.IsEnabled)//Дожидаемся разблокировки кнопки обновить, точно набор завершен
            {
                Thread.Sleep(500);
            }
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "вызываем событие кнопки обновить"; }));
            //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { Button_Click_3(null, null); }));
            //  Button_Click_3(null, null);//вызываем событие кнопки обновить
            rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "запускаем новый набор"; }));
            File.AppendAllText("D:\\DiagnosticMGVS_file.txt", " Start_Click(null, null" + "\n"); //допишет текст в конец файла
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { StartRunOldFile(); }));
            //  Start_Click(null, null);//запускаем новый набор
        }
        private void NewRanWork()
        {
            Log_left_accs();
          //  Dispatcher.Invoke(new ParametrizedMethodInvoker1(this.Log_left_accs));
        }
       
      
    }
}
