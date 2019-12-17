using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using URAN_2017.WorkBD;
using URAN_2017.WorkBD.ViewTaiblBDBAAK;

namespace URAN_2017
{
    public partial class BAAK12T : ClassParentsBAAK
    {
        public bool FlagSaveBD = true;
        public virtual void BDReadFile(string nameFile, string nameBAAK, string timeFile, string nameRan)
        {
            if (FlagSaveBD)
            {


                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
               
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    DataAccesBDBAAK.Path = wayDataBD;
                    DataAccesBDData.AddDataTablФайлы(nameFile, nameBAAK, timeFile, nameRan);
                }
                else
                {


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
                            CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Файлы](" + "ИмяФайла, Плата, ВремяСоздания, НомерRAN) VALUES (" + "'" + nameFile + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + timeFile + "'" + ", " + "'" + nameRan + "'" + ") "
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Dispose();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "BDReadFile");
                    }
                    finally
                    {
                        // закрываем подключение
                        podg.Close();
                        podg.Dispose();


                    }
                }
            }
        }
        private void BDReadNeutron(string nameFile, int D, int Amp, int TimeFirst, int TimeEnd, string time, int TimeAmp, int TimeFirst3, int TimeEnd3, bool test, bool bad)
        {
            if (FlagSaveBD)
            {
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    if (!test)
                    {


                        DataAccesBDBAAK.Path = wayDataBD;
                        int x = 0;
                        if(bad)
                        {
                            x = 1;
                        }
                        DataAccesBDData.AddDataTablSobNeutron(nameFile, D, Amp, TimeFirst, TimeEnd, time, TimeAmp, TimeFirst3, TimeEnd3, x);
                    }
                }
                else
                {
                    string connectionString;
                    if (test)
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataTestBD;
                    }
                    else
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                    }

                    // Создание подключения
                    var podg = new OleDbConnection(connectionString);
                    try
                    {

                        // Открываем подключение
                        podg.Open();
                        // MessageBox.Show("Подключение открыто");
                        OleDbCommand oleDbCommand = new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp, bad) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        };
                        oleDbCommand.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp, bad) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Нейтроны](" + "ИмяФайла, Dn, Amp, TimeFirst, TimeEnd, Время, TimeFirst3, TimeEnd3, TimeAmp, bad) VALUES (" + "'" + nameFile + "'" + "," + "'" + D + "'" + ", " + "'" + Amp + "'" + ", " + "'" + TimeFirst + "'" + ", " + "'" + TimeEnd + "'" + ", " + "'" + time + "'" + ", " + "'" + TimeFirst3 + "'" + ", " + "'" + TimeEnd3 + "'" + ", " + "'" + TimeAmp + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Dispose();

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        // закрываем подключение
                        podg.Close();

                    }
                }
            }
        }

        public virtual void BDReadСобытие(string nameFile, string nameBAAK, string time, int[] Amp, string nameklaster, int[] Nnut, double[] Nl, double[] sig, bool test, bool bad)
        {
            if (FlagSaveBD)
            {
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    if (!test)
                    {


                        DataAccesBDBAAK.Path = wayDataBD;
                        int x = 0;
                        if (bad)
                        {
                            x = 1;
                        }
                        int[] nll = new int[12];
                        for(int i=0; i<12; i++)
                        {
                            nll[i] = Convert.ToInt32(Nl[i]);
                        }
                        DataAccesBDData.AddDataTablSob(nameFile, nameBAAK, time, Amp, nameklaster, Nnut, nll, sig);
                    }
                }
                else
                {
                    string connectionString;
                    if (test)
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataTestBD;
                    }
                    else
                    {
                        connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
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
                                            "АмплитудаКанал10, АмплитудаКанал11, АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, bad) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1, АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                            "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, bad) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "INSERT INTO[Событие](" + "ИмяФайла, Кластер, Плата, Время, АмплитудаКанал1,АмплитудаКанал2,АмплитудаКанал3,АмплитудаКанал4,АмплитудаКанал5,АмплитудаКанал6,АмплитудаКанал7,АмплитудаКанал8,АмплитудаКанал9," +
                                            "АмплитудаКанал10,АмплитудаКанал11,АмплитудаКанал12, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, Nul1, Nul2, Nul3, Nul4, Nul5, Nul6, Nul7, Nul8, Nul9, Nul10, Nul11, Nul12, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, bad) VALUES (" +
                                            "'" + nameFile + "'" + "," + "'" + nameklaster + "'" + "," + "'" + nameBAAK + "'" + ", " + "'" + time + "'" + ", " + "'" + Amp[0] + "'" + ", " + "'" + Amp[1] + "'" + ", " + "'" + Amp[2] + "'" + ", " + "'" + Amp[3] + "'" + ", " + "'"
                                            + Amp[4] + "'" + ", " + "'" + Amp[5] + "'" + ", " + "'" + Amp[6] + "'" + ", " + "'" + Amp[7] + "'" + ", " + "'" + Amp[8] + "'" + ", " + "'" + Amp[9] + "'" + ", " + "'" + Amp[10] + "'" + ", " + "'" + Amp[11] + "'" + ", " + "'" + Nnut[0] + "'"
                                            + ", " + "'" + Nnut[1] + "'" + ", " + "'" + Nnut[2] + "'" + ", " + "'" + Nnut[3] + "'" + ", " + "'" + Nnut[4] + "'" + ", " + "'" + Nnut[5] + "'" + ", " + "'" + Nnut[6] + "'" + ", " + "'" + Nnut[7] + "'" + ", " + "'" + Nnut[8]
                                            + "'" + ", " + "'" + Nnut[9] + "'" + ", " + "'" + Nnut[10] + "'" + ", " + "'" + Nnut[11] + "'" + ", " + "'" + Nl[0] + "'" + ", " + "'" + Nl[1] + "'" + ", " + "'" + Nl[2] + "'" + ", " + "'" + Nl[3] + "'" + ", " + "'" + Nl[4]
                                            + "'" + ", " + "'" + Nl[5] + "'" + ", " + "'" + Nl[6] + "'" + ", " + "'" + Nl[7] + "'" + ", " + "'" + Nl[8] + "'" + ", " + "'" + Nl[9] + "'" + ", " + "'" + Nl[10] + "'" + ", " + "'" + Nl[11] + "'" + ", " + "'" + sig[0].ToString("0.000") + "'"
                                            + ", " + "'" + sig[1].ToString("0.000") + "'" + ", " + "'" + sig[2].ToString("0.000") + "'" + ", " + "'" + sig[3].ToString("0.000") + "'" + ", " + "'" + sig[4].ToString("0.000") + "'" + ", " + "'" + sig[5].ToString("0.000") + "'" + ", " + "'" + sig[6].ToString("0.000") + "'" + ", " + "'" + sig[7].ToString("0.000") + "'" + ", " + "'" + sig[8].ToString("0.000") + "'"
                                            + ", " + "'" + sig[9].ToString("0.000") + "'" + ", " + "'" + sig[10].ToString("0.000") + "'" + ", " + "'" + sig[11].ToString("0.000") + "'" + ", " + "'" + bad.ToString() + "'" + ")"
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Dispose();

                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message+ "BDReadСобытие");
                    }
                    finally
                    {
                        // закрываем подключение
                        podg.Close();
                    }
                }
            }
        }

        public virtual void BDReadCloseFile(string nameFile, string time)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;
                DataAccesBDBAAK.Path = wayDataBD;
                if (wayDataBD.Split('.')[1] == "db" || wayDataBD.Split('.')[1] == "db3")
                {
                    DataAccesBDData.updateTimeStopDataTablФайл(time, nameFile);
                }
                else
                {


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
                            CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Connection = podg;
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.ExecuteNonQuery();
                        new OleDbCommand
                        {
                            Connection = podg,
                            CommandText = "update [Файлы] set ВремяЗакрытия=" + "'" + time + "'" + " where ИмяФайла=" + "'" + nameFile + "'" + ""
                            // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                        }.Dispose();



                    }
                    catch
                    {

                    }
                    finally
                    {

                        podg.Close();

                    }
                }
            }
        }
        public virtual void BDReadTemP(string nameBAAK, int temp)
        {
            if (FlagSaveBD)
            {
                string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + wayDataBD;

                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();
                    // MessageBox.Show("Подключение открыто");
                    DateTime taimer2 = DateTime.UtcNow;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Темп](" + "Кластер№, Плата, час, минута, год, месяц, день, темп ) VALUES (" + "'" + NamKl + "'" + "," + "'" + nameBAAK + "'" + "," + "'" + taimer2.Hour + "'" + ", " + "'" + taimer2.Minute + "'" + ", " + "'" + taimer2.Year + "'" + "," + "'" + taimer2.Month + "'" + "," + "'" + taimer2.Day + "'" + "," + "'" + temp + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.Connection = podg;
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "INSERT INTO[Темп](" + "Кластер№, Плата, час, минута, год, месяц, день, темп ) VALUES (" + "'" + NamKl + "'" + "," + "'" + nameBAAK + "'" + "," + "'" + taimer2.Hour + "'" + ", " + "'" + taimer2.Minute + "'" + ", " + "'" + taimer2.Year + "'" + "," + "'" + taimer2.Month + "'" + "," + "'" + taimer2.Day + "'" + "," + "'" + temp + "'" + ") "
                        // CommandText = "INSERT INTO[RAN](" + "НомерRAN, Синхронизация, ОбщийПорог, Порог,Триггер,ЗначениеТаймера,ВремяЗапуска) VALUES (nameRan, sinx, allPorog, porog, trg, time, timeStart)"
                    }.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                finally
                {
                    // закрываем подключение
                    podg.Close();

                }
            }
        }
        public void BDselect(out uint[] masNul)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + NameFileSetUp;
            masNul = new uint[12];
            DataAccesBDBAAK.Path = NameFileSetUp;
            if (NameFileSetUp.Split('.')[1] == "db" || NameFileSetUp.Split('.')[1] == "db3")
            {
                List<ClassNullLine> list = DataAccesBDBAAK.GetDataNullLine();
                var f = from el in DataAccesBDBAAK.GetDataNullLine() where el.namePSB == NameBAAK12 select el;
                int[] mass= f.ElementAt(0).nullLine;
                for (int i = 0; i < 12; i++)
                {
                    masNul[i] = Convert.ToUInt32(mass[i]);
                 
                }
             
            }
            else
            {


                // Создание подключения
                var podg = new OleDbConnection(connectionString);
                try
                {

                    // Открываем подключение
                    podg.Open();

                    var chit = new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "select * from [Нулевая линия] where ИмяПлаты ='" + NameBAAK12 + "'"
                    }.ExecuteReader(CommandBehavior.CloseConnection);
                    while (chit.Read() == true)
                    {

                        for (int i = 2; i < chit.FieldCount; i++)
                        {
                            masNul[i - 2] = Convert.ToUInt32(chit.GetValue(i));


                        }
                    }
                    new OleDbCommand
                    {
                        Connection = podg,
                        CommandText = "select * from [Нулевая линия] where Плата ='" + NameBAAK12 + "'"
                    }.Dispose();

                }
                catch(NullReferenceException ee)
                {
                    Debug.WriteLine("Error 428");
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
        }
    }
}
