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
        /// <summary>
        ///Запускает набор по длительности
        /// </summary>
        /// <param name="Dlitt">длительность набора</param>
        /// <param name="token11">прерывание набора</param>
        private void TestRanTask(int Dlitt)//набор по длительности
        {


            try
            {


                int min = 0;
                while (min < Dlitt * 60000)
                {
                    rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Тестовый набор длительностью =" + Dlitt + " мин." + " Осталось " + (((Dlitt * 60000) - min) / 1000).ToString() + "сек."; }));
                    Thread.Sleep(1000);
                    min = min + 1000;
                }
                rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Завершение тестовго набора"; }));
                try
                {


                    BAAK12T.СтопТриггерDelegate?.Invoke();
                    if (BAAK12T.TestRanTheEndDelegate != null)
                    {
                        BAAK12T.TestRanTheEndDelegate?.Invoke(false);

                    }
                    else
                    {
                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "TestRanTheEndDelegate равен нул"; }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("erTest" + ex.ToString());
                }
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка УРАН запущена"; rezimYst.Foreground = System.Windows.Media.Brushes.Green; }));
                File.AppendAllText("D:\\DiagnosticMGVS_file.txt", "Начало " + "\n"); //допишет текст в конец файла
                NewRun();
            }
            catch(Exception ex)
            {
                MessageBox.Show("erTestRanTask" + ex.ToString());
            }
        }
        /// <summary>
        /// Запускаем набор п количеству программных триггеров
        /// </summary>
        /// <param name="kTestRan">колличесво программных триггеров</param>
        /// <param name="intTestRan">интервал между триггерами</param>
        /// <param name="token2"></param>
        private void TestRanTask1(int kTestRan, int intTestRan)
        {
            try
            {


                for (int i = 0; i < kTestRan; i++)
                {
                    rezimYst.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Тестовый набор по количеству: " + i.ToString() + " из" + " " + kTestRan; ; }));

                    Thread.Sleep(intTestRan);
                    if (BAAK12T.TestRanStartDelegate != null)
                    {
                        BAAK12T.TestRanStartDelegate?.Invoke();
                    }
                    else
                    {
                        rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "TestRanStartDelegate"; ; }));
                    }

                }
                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Завершение тестовго набора"; ; }));
                BAAK12T.СтопТриггерDelegate?.Invoke();
                BAAK12T.TestRanTheEndDelegate?.Invoke(true);


                rezimYst.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { rezimYst.Content = "Установка УРАН запущена"; rezimYst.Foreground = System.Windows.Media.Brushes.Green; }));
            }
            catch(Exception ex)
            {
                MessageBox.Show("erTestRanTask1" + ex.ToString());
            }
            }
        /// <summary>
        /// Запускает тестовый нобор, по длительности или по количеству триггеров, в определенное время
        /// </summary>
        /// <param name="alarm1"></param>
        /// <param name="kTestRan"></param>
        /// <param name="intTestRan"></param>
        /// <param name="inter"></param>
        /// <param name="token1"></param>
        private void TestRanAndNewFile(int kTestRan, int intTestRan, int inter)
        {
            try
            {


                if (_DataColecClassTestRan.Count != 0)
                {
                    foreach (ClassTestRan test in _DataColecClassTestRan)
                    {
                        if (test.Alam.Subtract(DateTime.UtcNow).TotalMinutes < 5)
                        {
                            DateTime tmp = DateTime.UtcNow;
                            var ff = test.Alam.Subtract(tmp);
                            TimeTestO.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => { TimeTestO.Text = "До тестового набора" + ff.Minutes.ToString("00") + "м" + ff.Seconds.ToString("00") + "c"; }));

                        }
                        else
                        {
                            TimeTestO.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => { TimeTestO.Text = String.Empty; }));

                        }
                        if (DateTime.Compare(DateTime.UtcNow, test.Alam) >= 0)
                        {
                            try
                            {

                                if (!test.ProgramTrigTest)//Тест по времени
                                {
                                    BAAK12T.TestRanSetUpDelegate?.Invoke(test.Porog, test.Trig, test.ProgramTrigTest);
                                    Task myTestRan = Task.Run(() => TestRanTask(test.Dlit));
                                }
                                else
                                {
                                    BAAK12T.TestRanSetUpDelegate?.Invoke(test.Porog, test.Trig, test.ProgramTrigTest);
                                    Task myTestRan = Task.Run(() => TestRanTask1(test.Kolsob, test.Interval));
                                }

                                // }

                            }
                            catch (NullReferenceException ee)
                            {
                                MessageBox.Show("er1" + ee.ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("er2" + ex.ToString());
                            }
                            finally
                            {

                                test.Alam = test.Alam.AddDays(1);

                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("erT" + ex.ToString());
            }
            
          
        }

    }
}
