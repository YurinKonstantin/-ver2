using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    public partial class BAAK12T : ClassParentsBAAK
    {
       
        /// <summary>
        /// подготовка к тестовому набоу по длительности или количеству, если trigPorog=true, то по количеству
        /// </summary>
        /// <param name="porog">порог срабатывания</param>
        /// <param name="trig">триггер</param>
        /// <param name="trigProg">если =true, то по количеству</param>
        public virtual void TestRanПодготовка(int porog, int trig, Boolean trigProg)
        {
            
            CтатусБААК12 = "Подготовка к тестовому набору";
            Thread.Sleep(500);

            TriggerStopОго();
            CтатусБААК12 = "Вычитываем данные";
            Thread.Sleep(500);
           // ВычитываемДанныеНужные();
            ВычитываемДанныеНенужные();

            //TriggerStop();
            CтатусБААК12 = "вычитываем очередь";
           
            Thread.Sleep(500);
            int koloch = 0;
            while (OcherediNaZapic.Count != 0 | koloch < 50)
            {
                koloch++;
                //Thread.Sleep(500);
                CтатусБААК12 = "вычитываем очередь" + " =" + OcherediNaZapic.Count;
            }
            CтатусБААК12 = "Закрытие файла";
            Thread.Sleep(1000);
            CloseFile();
            Flagtest = true;
            CтатусБААК12 = "Открытие тестового файла";
            Thread.Sleep(500);
            //if (Conect300Statys)
            // {
            string tipPl;
            if (!BAAKTAIL)
            {
                tipPl = "N";
            }
            else
            {
                tipPl = "T";
            }
            try
                {
                string path = NameFileWay;
                string subpath = @"Test";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo = new DirectoryInfo(path+@"\"+ subpath);

               if(!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                
                String sd = Time();
                    NameFile = NameFileWay+@"\"+ subpath + @"\" + NamKl + "_" + "Test" + "_" + sd+"_"+tipPl + ".bin";
                    data_fs = new FileStream(NameFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                    data_w = new BinaryWriter(data_fs);
                    BDReadFile(NamKl + "_" + "Test" + "_" + sd, NameBAAK12, sd, BAAK12T.NameRan);
                    NameFileClose = NamKl + "_" + "Test" + "_" + sd + "_" + tipPl;
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка открытия файла" + ex.ToString());
                }
            // }
            КолПакетов = 0;
            КолПакетовEr = 0;
            КолПакетовОчер = 0;
            КолПакетовОчер2 = 0;
            КолПакетовN = 0;
            if (!trigProg)//по длительности
            {
                CтатусБААК12 = "Тестовый набор по длительности";
                Thread.Sleep(500);
                AllSetPorogAll(Convert.ToUInt32(porog));
                Trigger(0x200006, Convert.ToUInt32(trig));               
               //TriggerStart();
              
            }
           else
            {
                CтатусБААК12 = "Тестовый набор по количеству";
                TriggerProgramSetap();
                Thread.Sleep(500);
                TriggerStart();
                //TriggerProgramSetap();
                
            }
            
        }
        /// <summary>
        /// Завершение тестовго набора и возрат настроек обычного набора
        /// </summary>
        public virtual void TestRanTheEnd(Boolean trigProg)
        {
            TriggerStopОго();


            CтатусБААК12 = "Вычитываем данные";
            Thread.Sleep(500);
            ВычитываемДанныеНужные();
            CтатусБААК12 = "вычитываем очередь";
            Thread.Sleep(500);
            int koloch = 0;
            while (OcherediNaZapic.Count != 0 | koloch<50)
            {
                koloch++;
                //Thread.Sleep(500);
                CтатусБААК12 = "вычитываем очередь"+ " ="+OcherediNaZapic.Count;
            }
            Thread.Sleep(500);
            CтатусБААК12 = "Работает";
            //TriggerStop();
            NewFileData();
            if (!trigProg)
            {
                if (!trigOtBAAK)
                {
                    Trigger(0x200006, TrgAll);
                }
                else
                {
                    Trigger(0x200006, 256);
                }
                AllSetPorogAll(PorogAll);
            }
           else
            {
                if (!trigOtBAAK)
                {
                    Trigger(0x200006, TrgAll);
                }
                else
                {
                    Trigger(0x200006, 256);
                }
                //ToDo подготовить к запуску отвнешнего триггера
            }
            
            
            //TriggerStart();
            Thread.Sleep(500);
            КолПакетовОчер = 0;
            КолПакетовОчер2 = 0;
            КолПакетовN = 0;
            КолПакетовEr = 0;
            КолПакетов = 0;
            Пакетов = 0;
            ТемпПакетов = 0;
            ТемпПакетовN = 0;
            ПакетовN = 0;
         
            Flagtest = false;

        }


    }

}
