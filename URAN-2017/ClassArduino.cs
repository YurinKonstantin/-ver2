using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    public class ClassArduino
    {
        public ClassArduino(string arduinoName, string klaster, string ip, int port, string _chenel1IP, string _chenel2IP, string _chenel3IP)
            {
            ArduinoName = arduinoName;
            Klaster = klaster;
            iPAddress = ip;
            Port = port;
            chenel1IP = _chenel1IP;
            chenel2IP = _chenel2IP;
            chenel3IP = _chenel3IP;

        }
       public string ArduinoName { get; set; }
        public string Klaster { get; set; }
        public int Port { get; set; }
        public string iPAddress { get; set; }
        /// <summary>
        /// IP адрес платы которая привязана к каналу 1
        /// </summary>
        public string chenel1IP { get; set; }
        /// <summary>
        /// IP адрес платы которая привязана к каналу 2
        /// </summary>
        public string chenel2IP { get; set; }
        /// <summary>
        /// IP адрес платы которая привязана к каналу 3
        /// </summary>
        public string chenel3IP { get; set; }
        /// <summary>
        /// IP адрес платы которая привязана к каналу 4
        /// </summary>
        public string chenel4IP { get; set; }
        const string quote = "\"";
        public bool restartBAAK(int chanel)
        {
            string zapros = "{" + quote + "name" + quote + ":" + quote + "reboot_arduino" + quote + "," + quote + "ip" + quote + ":" + quote + iPAddress + quote + "," + quote + "port" + quote + ":" + Port + "," + quote + "relay_number" + quote + ":" + chanel + "}";

            string otvet = String.Empty;
            try
            {
                const string quote = "\"";


                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + "192.168.1.78" + ":" + 80 + "/api/v1/URAN_Temp_and_Pressure/" + zapros) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";


                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        otvet = output.ToString();

                        response.Close();

                    }

                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    MessageBox.Show(ez.ToString());
                }
                string[] str = (otvet.Substring(1, otvet.Length - 3)).Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] str1 = str[i].Split(':');
                    if (str1[0].Contains("status"))
                    {
                        bool isBool = Boolean.TryParse(str1[1], out bool res);
                        if (isBool)
                        {
                            return res;
                        }
                        else
                        {
                            bool isInt = int.TryParse(str1[1], out int res1);
                            if (isInt)
                            {
                                if (res1 == 1)
                                {
                                    return true;
                                }
                            }
                            // MessageBox.Show(str1[1]+"\n"+ str1.Length.ToString()+"\t"+ str.Length.ToString());
                            return false;
                        }
                    }


                }
                
                return false;
                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
                //labekSystemMessage.Items.Add("Ошибка: " + ex);
                MessageBox.Show(ex.ToString());
            }


            return false;
        }
        public bool zaprosStatus()
        {
            string zapros = "{" + quote + "name" + quote + ":" + quote + "check_mask_arduino" + quote + "," + quote + "ip" + quote + ":" + quote + iPAddress + quote + "," + quote + "port" + quote + ":" + Port + "}";

            string otvet = String.Empty;
            try
            {
                const string quote = "\"";


                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + "192.168.1.78" + ":" + 80 + "/api/v1/URAN_Temp_and_Pressure/" + zapros) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";


                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        otvet = output.ToString();

                        response.Close();

                    }

                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    //  MessageBox.Show(ez.ToString());
                    return false;
                }
                string[] str = (otvet.Substring(1, otvet.Length - 3)).Split(',');
                
                for(int i=0; i< str.Length; i++)
                {
                    string[] str1 = str[i].Split(':');
                    if(str1[0].Contains("status"))
                    {
                        bool isBool = Boolean.TryParse(str1[1], out bool res);
                        if (isBool)
                        {
                            return res;
                        }
                        else
                        {
                            bool isInt = int.TryParse(str1[1], out int res1);
                            if (isInt)
                            {
                                if (res1 == 1)
                                {
                                    return true;
                                }
                            }
                            // MessageBox.Show(str1[1]+"\n"+ str1.Length.ToString()+"\t"+ str.Length.ToString());
                            return false;
                        }
                    }
                    

                }
               
                return   false;
                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
               // labekSystemMessage.Items.Add("Ошибка: " + ex);
               // MessageBox.Show(ex.ToString());
            }
           
          
                return false;
        }
        public string zaprosStatusStringOtvet()
        {
            string zapros = "{" + quote + "name" + quote + ":" + quote + "check_mask_arduino" + quote + "," + quote + "ip" + quote + ":" + quote + iPAddress + quote + "," + quote + "port" + quote + ":" + Port + "}";

            string otvet = String.Empty;
            try
            {
                const string quote = "\"";


                HttpWebRequest httpWebRequest = WebRequest.Create(@"http://" + "192.168.1.78" + ":" + 80 + "/api/v1/URAN_Temp_and_Pressure/" + zapros) as HttpWebRequest;
                httpWebRequest.Accept = "application/json";


                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        StringBuilder output = new StringBuilder();

                        output.Append(reader.ReadToEnd());
                        otvet = output.ToString();

                        response.Close();

                    }

                }
                catch (WebException ez)
                {
                    //Dispatcher.BeginInvoke(new Action<string>(s => { labekSystemMessage.Items.Add(s); }), ez.Message);
                    //  MessageBox.Show(ez.ToString());
                    return String.Empty;
                }
                
                return otvet.Substring(1, otvet.Length - 3);
                //}, null);
                //labekSystemMessage.Items.Add("Все Ок");
            }
            catch (Exception ex)
            {
                // labekSystemMessage.Items.Add("Ошибка: " + ex);
                // MessageBox.Show(ex.ToString());
            }


            return String.Empty;
        }
        public bool restartBAAK(string ip)
        {
            bool z = zaprosStatus();

            if (z)
            {
                if(ip== chenel1IP)
                {
                   bool v= restartBAAK(1);
                    return v;
                }
                if (ip == chenel2IP)
                {
                    bool v = restartBAAK(2);
                    return v;
                }
                if (ip == chenel3IP)
                {
                    bool v = restartBAAK(3);
                    return v;
                }
            }
            return false;
        }
       
        public bool restartBAAKAll()
        {
            bool z = zaprosStatus();

            if (z)
            {
               
                    bool v = restartBAAK(1);
               if(!v)
                {
                    MessageBox.Show("Ошибка при перезагрузке канала 1, кластера "+Klaster+"\n"+"ip adres перегружаемой платы "+ chenel1IP);
                }
                
                
                    bool v1 = restartBAAK(2);
                if (!v1)
                {
                    MessageBox.Show("Ошибка при перезагрузке канала 2, кластера " + Klaster + "\n" + "ip adres перегружаемой платы " + chenel2IP);
                }


                bool v2 = restartBAAK(3);
                if (!v2)
                {
                    MessageBox.Show("Ошибка при перезагрузке канала 3, кластера " + Klaster + "\n" + "ip adres перегружаемой платы " + chenel3IP);
                }

            }
            return false;
        }
        public string strSost
        {
            get
            {
                return "Состояние " + zaprosStatus().ToString();
            }
        }
        public void Bord1_Click1()
        {
            try
            {
                MessageBox.Show("sss");
                //var ard = (ClassArduino)sender;
                //bool z = ard.restartBAAKAll();
                //if (z)
                {
                   // MessageBox.Show(ard.ArduinoName + " успешна перегрузила питания всех плат " + ard.Klaster);
                }
           //     else
                {
                  //  MessageBox.Show(ard.ArduinoName + " не смогла выполнить перегрузку всех плат " + ard.Klaster);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
    }
}
