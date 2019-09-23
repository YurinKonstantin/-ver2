using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace URAN_2017
{
    class NetworkManagement
    {

        public static void SetIP(string ip_address, string subnet_mask)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ip_address };
                        newIP["SubnetMask"] = new string[] { subnet_mask };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
        }
        /// <summary>
        /// Set's a new Gateway address of the local machine
        /// </summary>
        /// <param name="gateway">The Gateway IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public static void SetGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// Set's the DNS Server of the local machine
        /// </summary>
        /// <param name="NIC">NIC address</param>
        /// <param name="DNS">DNS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public static void SetDNS(string NIC, string DNS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject newDNS =
                                objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                            ManagementBaseObject setDNS =
                                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Set's WINS of the local machine
        /// </summary>
        /// <param name="NIC">NIC Address</param>
        /// <param name="priWINS">Primary WINS server address</param>
        /// <param name="secWINS">Secondary WINS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public static void SetWINS(string NIC, string priWINS, string secWINS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject setWINS;
                            ManagementBaseObject wins =
                            objMO.GetMethodParameters("SetWINSServer");
                            wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                            wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                            setWINS = objMO.InvokeMethod("SetWINSServer", wins, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
      static  public void NetInfo()
        {
            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                // перебираем все сетевые интерфейсы
                foreach (NetworkInterface nic in adapters)
                {
                    string strInterfaceName = nic.Name; // наименование интерфейса
                    string strPhysicalAddress = nic.GetPhysicalAddress().ToString(); //МАС - адрес

                    string strAddr = nic.Name + "\n" + strPhysicalAddress + "\n";

                    // перебираем IP адреса
                    IPInterfaceProperties properties = nic.GetIPProperties();
                    foreach (UnicastIPAddressInformation unicast in properties.UnicastAddresses)
                    {
                        strAddr += unicast.Address.ToString() + " / " + unicast.IPv4Mask + "\n";
                    }

                    // перебираем днс-сервера
                    foreach (IPAddress dnsAddress in properties.DnsAddresses)
                    {
                        strAddr += dnsAddress.ToString() + "\n";
                    }

                    // перебираем шлюзы
                    foreach (GatewayIPAddressInformation gatewayIpAddressInformation in properties.GatewayAddresses)
                    {
                        strAddr += gatewayIpAddressInformation.Address.ToString() + "\n";
                    }
                    MessageBox.Show(strAddr);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
        public void PingDNS()
        {

             // IAsyncResult asyncResult = Dns.BeginGetHostEntry("192.168.2.16", new AsyncCallback(GetHostAddressesCallback), d);

            //  if (!asyncResult.AsyncWaitHandle.WaitOne(1000, false))
            {

                //    MessageBox.Show("false" + addressList.ToString());
            }
            //  else
            {
                //     MessageBox.Show("true" + addressList.ToString());

            }
            try
            {


                // allDone = new ManualResetEvent(false);
                // Create an instance of the RequestState class.
                //RequestState myRequestState = new RequestState();

                // Begin an asynchronous request for information like host name, IP addresses, or 
                // aliases for specified the specified URI.
                //  IAsyncResult asyncResult = Dns.BeginGetHostEntry("192.168.2.166", new AsyncCallback(RespCallback), myRequestState);
                // MessageBox.Show("Host nssamehh : ");
                // Wait until asynchronous call completes.
                //   allDone.WaitOne(100);
                // MessageBox.Show("Host namesss111hh : "); 
                GetHostEntryFinished.Reset();
                ResolveState ioContext = new ResolveState("192.168.1.1");

                Dns.BeginGetHostEntry(ioContext.host,
                    new AsyncCallback(GetHostEntryCallback), ioContext);

                // Wait here until the resolve completes (the callback 
                // calls .Set())

                //  asyncResult.AsyncWaitHandle.WaitOne(1000);
                if (!GetHostEntryFinished.WaitOne(2000, false))
                {

                    MessageBox.Show("false");
                }
                else
                {
                    MessageBox.Show("true");

                }
                Console.WriteLine("EndGetHostEntry({0}) returns:", ioContext.host);

                //foreach (IPAddress address in ioContext.IPs.AddressList)
                {
                    //     Console.WriteLine($"    {address}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("dfdgd");
            }
            
        }
        public class ResolveState
        {
            string hostName;
            IPHostEntry resolvedIPs;

            public ResolveState(string host)
            {
                hostName = host;
            }

            public IPHostEntry IPs
            {
                get { return resolvedIPs; }
                set { resolvedIPs = value; }
            }

            public string host
            {
                get { return hostName; }
                set { hostName = value; }
            }
        }

        // Record the IPs in the state object for later use.
        public static void GetHostEntryCallback(IAsyncResult ar)
        {
            ResolveState ioContext = (ResolveState)ar.AsyncState;
            try
            {

                ioContext.IPs = Dns.EndGetHostEntry(ar);
                GetHostEntryFinished.Set();
            }
            catch (Exception ex)
            {
                ioContext = null;
                Debug.WriteLine("Errorr");

                //   GetHostEntryFinished.Set();
            }
        }

        public static ManualResetEvent GetHostEntryFinished =
    new ManualResetEvent(false);


    }
}
