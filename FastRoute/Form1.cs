using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastRoute
{
    public partial class Form1 : Form
    {
        private byte ForwardIfIndex;
        private uint forwardMetric;
        byte[] gateip = new byte[4];

        [DllImport("Iphlpapi.dll")]

        [return: MarshalAs(UnmanagedType.U4)]

        public static extern int CreateIpForwardEntry(ref MIB_IPFORWARDROW pRoute);

        public struct MIB_IPFORWARDROW

        {

            public UInt32 dwForwardDest;        //destination IP address.

            public UInt32 dwForwardMask;        //Subnet mask

            public UInt32 dwForwardPolicy;      //conditions for multi-path route. Unused, specify 0.

            public UInt32 dwForwardNextHop;     //IP address of the next hop. Own address?

            public UInt32 dwForwardIfIndex;     //index of interface

            public UInt32 dwForwardType;        //route type

            public UInt32 dwForwardProto;       //routing protocol.

            public UInt32 dwForwardAge;         //age of route.

            public UInt32 dwForwardNextHopAS;   //autonomous system number. 0 if not relevant

            public int dwForwardMetric1;     //-1 if not used (goes for all metrics)

            public int dwForwardMetric2;

            public int dwForwardMetric3;

            public int dwForwardMetric4;

            public int dwForwardMetric5;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_IPINTERFACE_ROW
        {
            public uint Family;
            public ulong InterfaceLuid;
            public uint InterfaceIndex;
            public uint MaxReassemblySize;
            public ulong InterfaceIdentifier;
            public uint MinRouterAdvertisementInterval;
            public uint MaxRouterAdvertisementInterval;
            public byte AdvertisingEnabled;
            public byte ForwardingEnabled;
            public byte WeakHostSend;
            public byte WeakHostReceive;
            public byte UseAutomaticMetric;
            public byte UseNeighborUnreachabilityDetection;
            public byte ManagedAddressConfigurationSupported;
            public byte OtherStatefulConfigurationSupported;
            public byte AdvertiseDefaultRoute;
            public uint RouterDiscoveryBehavior;
            public uint DadTransmits;
            public uint BaseReachableTime;
            public uint RetransmitTime;
            public uint PathMtuDiscoveryTimeout;
            public uint LinkLocalAddressBehavior;
            public uint LinkLocalAddressTimeout;
            public uint ZoneIndice0, ZoneIndice1, ZoneIndice2, ZoneIndice3, ZoneIndice4, ZoneIndice5, ZoneIndice6, ZoneIndice7,
             ZoneIndice8, ZoneIndice9, ZoneIndice10, ZoneIndice11, ZoneIndice12, ZoneIndice13, ZoneIndice14, ZoneIndice15;
            public uint SitePrefixLength;
            public uint Metric;
            public uint NlMtu;
            public byte Connected;
            public byte SupportsWakeUpPatterns;
            public byte SupportsNeighborDiscovery;
            public byte SupportsRouterDiscovery;
            public uint ReachableTime;
            public byte TransmitOffload;
            public byte ReceiveOffload;
            public byte DisableDefaultRoutes;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_IPFORWARDTABLE
        {
            public int dwNumEntries; //number of route entries in the table.
            public MIB_IPFORWARDROW[] table;
        }


        [DllImport("Iphlpapi.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int GetIpForwardTable(UInt32[] pIpForwardTable, out int pdwSize, bool bOrder); // Use a byte[] and instead of a pointer, and 'out' as well.

        [DllImport("Iphlpapi.dll")]

        public static extern uint GetIpInterfaceEntry(ref MIB_IPINTERFACE_ROW pRoute);

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto)]

        public static extern int GetBestInterface(UInt32 DestAddr, out UInt32 BestIfIndex);


        [DllImport("Iphlpapi.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        private static extern int DeleteIpForwardEntry(ref MIB_IPFORWARDROW pRoute);


        public static int createIpForwardEntry(UInt32 destIPAddress, UInt32 destMask, UInt32 nextHopIPAddress, UInt32 ifIndex, int metric)

        {

            MIB_IPFORWARDROW mifr = new MIB_IPFORWARDROW();

            mifr.dwForwardDest = destIPAddress;

            mifr.dwForwardMask = destMask;

            mifr.dwForwardNextHop = nextHopIPAddress;

            mifr.dwForwardIfIndex = ifIndex;

            mifr.dwForwardPolicy = Convert.ToUInt32(0);

            mifr.dwForwardType = Convert.ToUInt32(4);

            mifr.dwForwardProto = Convert.ToUInt32(3);

            mifr.dwForwardAge = Convert.ToUInt32(0);

            mifr.dwForwardNextHopAS = Convert.ToUInt32(0);

            mifr.dwForwardMetric1 = metric;

            mifr.dwForwardMetric2 = -1;

            mifr.dwForwardMetric3 = -1;

            mifr.dwForwardMetric4 = -1;

            mifr.dwForwardMetric5 = -1;

            return CreateIpForwardEntry(ref mifr);

        }


        public Form1()
        {
            InitializeComponent();
            int pdwSize = 1;


            UInt32[] pIPForwardTable = new uint[1];

            GetIpForwardTable(pIPForwardTable, out pdwSize, true);

            pIPForwardTable = new uint[pdwSize/4+1];
            GetIpForwardTable(pIPForwardTable, out pdwSize, true);

            int index = 1;
            uint ip = 192 + (168 << 8) + (1 << 16) + (1 << 24);
            for (int i = 0; i < pIPForwardTable[0]; i++)
            {
                if ((pIPForwardTable[index] == pIPForwardTable[index + 1]) && (pIPForwardTable[index + 1] == 0))
                {
                    ForwardIfIndex = (byte)pIPForwardTable[index+4];
                    ip = pIPForwardTable[index + 3];
                    break;
                }
            }

            //uint ip = 192 + (168 << 8) + (1 << 16) + (1 << 24);
            gateiptext.Text = ""+ (ip&0xff)+"."+ ((ip>>8) & 0xff)+"."+((ip >> 16) & 0xff) + "."+((ip >> 24) & 0xff);
            //GetBestInterface(pIPForwardTable[4],out interfaceIndex);
            var row = new MIB_IPINTERFACE_ROW();
            row.Family = 2;
            row.InterfaceLuid = 0;
            row.InterfaceIndex = ForwardIfIndex;
            var error=GetIpInterfaceEntry(ref row);
            forwardMetric = row.Metric;
            if (error != 0)
            {
                AddRoute.Enabled = false;
                DelRoute.Enabled = false;
            }
        }

        public static int deleteIpForwardEntry(UInt32 destIPAddress, UInt32 destMask, UInt32 nextHopIPAddress, UInt32 ifIndex)

        {

            MIB_IPFORWARDROW mifr = new MIB_IPFORWARDROW();

            mifr.dwForwardDest = destIPAddress;

            mifr.dwForwardMask = destMask;

            mifr.dwForwardNextHop = nextHopIPAddress;

            mifr.dwForwardIfIndex = ifIndex;

            mifr.dwForwardPolicy = Convert.ToUInt32(0);

            mifr.dwForwardType = Convert.ToUInt32(4);

            mifr.dwForwardProto = Convert.ToUInt32(3);

            mifr.dwForwardAge = Convert.ToUInt32(0);

            mifr.dwForwardNextHopAS = Convert.ToUInt32(0);

            mifr.dwForwardMetric1 = -1;

            mifr.dwForwardMetric2 = -1;

            mifr.dwForwardMetric3 = -1;

            mifr.dwForwardMetric4 = -1;

            mifr.dwForwardMetric5 = -1;

            return DeleteIpForwardEntry(ref mifr);

        }


        void createRoute(byte b1, byte b2, byte b3, byte b4, byte m1, byte m2, byte m3, byte m4, byte t1, byte t2, byte t3, byte t4)
        {
            uint srcaddr = b4; srcaddr <<= 8;
            srcaddr |= b3; srcaddr <<= 8;
            srcaddr |= b2; srcaddr <<= 8;
            srcaddr |= b1; 

            uint dstaddr = t4; dstaddr <<= 8;
            dstaddr |= t3; dstaddr <<= 8;
            dstaddr |= t2; dstaddr <<= 8;
            dstaddr |= t1; 

            uint maskaddr = m4;maskaddr <<= 8;
            maskaddr |= m3; maskaddr <<= 8;
            maskaddr |= m2; maskaddr <<= 8;
            maskaddr |= m1; 

            

            var error=createIpForwardEntry(srcaddr,maskaddr,dstaddr,ForwardIfIndex,(int)forwardMetric+5);
        }

        void deleteRoute(byte b1, byte b2, byte b3, byte b4, byte m1, byte m2, byte m3, byte m4, byte t1, byte t2, byte t3, byte t4)
        {
            uint srcaddr = b4; srcaddr <<= 8;
            srcaddr |= b3; srcaddr <<= 8;
            srcaddr |= b2; srcaddr <<= 8;
            srcaddr |= b1;

            uint dstaddr = t4; dstaddr <<= 8;
            dstaddr |= t3; dstaddr <<= 8;
            dstaddr |= t2; dstaddr <<= 8;
            dstaddr |= t1;

            uint maskaddr = m4; maskaddr <<= 8;
            maskaddr |= m3; maskaddr <<= 8;
            maskaddr |= m2; maskaddr <<= 8;
            maskaddr |= m1;



            var error = deleteIpForwardEntry(srcaddr, maskaddr, dstaddr, ForwardIfIndex);
        }

        bool getgateip()
        {
            bool newbit = false;
            int b = 0;
            gateip[b] = 0;
            for (int i = 0; i < gateiptext.Text.Length; i++)
            {
                int v = gateiptext.Text[i];
                if (v >= '0' && v <= '9')
                {
                    gateip[b] *= 10;
                    gateip[b] += (byte)(v - '0');
                    newbit = true;
                }
                else if (newbit)
                {
                    newbit = false;
                    if (b < 3)
                    {
                        b++;
                        gateip[b] = 0;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return b == 3;
        }

        private void AddRoute_Click(object sender, EventArgs e)
        {
            
            //deleteRoute(1, 0, 1, 0, 255, 255, 255, 0, 192, 168, 1, 1);
            //createRoute(192, 168, 2, 0, 255, 255, 255, 0, 192, 168, 1, 1);
            if (!getgateip())
            {
                MessageBox.Show("请输入正确网关IP");
                return;
            }
            if (RoutePath.Text.Length == 0)
            {
                MessageBox.Show("路径不能为空");
                return;
            }
            if (!File.Exists(RoutePath.Text))
            {
                MessageBox.Show("文件路径错误");
                return;
            }
            var sr = new StreamReader(RoutePath.Text);
            string val = sr.ReadToEnd();
            sr.Close();
            var ips = val.Substring(val.IndexOf("route add")).Split('\n');
            pross.Visible = true;
            new Thread(
            () =>
             {
                 byte[] ip_mask = new byte[9];
                 int max = ips.Length;
                 int count = 0;
                 foreach (string s in ips)
                 {
                     count++;
                     if (count % (max / 10) == 0)
                     {
                         Invoke(new MethodInvoker(()=> {
                             pross.Value = count * 100 / max;
                         }));
                     }
                     bool newbit = false;
                     int b = 0;
                     ip_mask[b] = 0;
                     for (int i = 0; i < s.Length; i++)
                     {
                         int v = s[i];
                         if (v >= '0' && v <= '9')
                         {
                             ip_mask[b] *= 10;
                             ip_mask[b] += (byte)(v - '0');
                             newbit = true;
                         }
                         else if (newbit)
                         {
                             newbit = false;
                             if (b < 7)
                             {
                                 b++;
                                 ip_mask[b] = 0;
                             }
                             else
                             {
                                 createRoute(ip_mask[0], ip_mask[1], ip_mask[2], ip_mask[3], ip_mask[4], ip_mask[5], ip_mask[6], ip_mask[7], gateip[0], gateip[1], gateip[2], gateip[3]);
                                 break;
                             }
                         }
                     }
                     //return;
                 }
                 Invoke(new MethodInvoker(() => {
                     pross.Value = 100;
                 }));
             }
            ).Start();
            
        }

        OpenFileDialog of = new OpenFileDialog();
        private bool needsave;

        private void SelectFile_Click(object sender, EventArgs e)
        {
            of.Filter = "所有文件|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                RoutePath.Text = of.FileName;
            }
        }

        private void DelRoute_Click(object sender, EventArgs e)
        {
            if (!getgateip())
            {
                MessageBox.Show("请输入正确网关IP");
                return;
            }
            if (RoutePath.Text.Length == 0)
            {
                MessageBox.Show("路径不能为空");
                return;
            }
            if (!File.Exists(RoutePath.Text))
            {
                MessageBox.Show("文件路径错误");
                return;
            }
            var sr = new StreamReader(RoutePath.Text);
            string val = sr.ReadToEnd();
            sr.Close();
            var ips = val.Substring(val.IndexOf("route add")).Split('\n');
            pross.Visible = true;
            new Thread(
            () =>
            {
                byte[] ip_mask = new byte[9];
                int max = ips.Length;
                int count = 0;
                foreach (string s in ips)
                {
                    count++;
                    if (count % (max / 10) == 0)
                    {
                        Invoke(new MethodInvoker(() => {
                            pross.Value = count * 100 / max;
                        }));
                    }
                    bool newbit = false;
                    int b = 0;
                    ip_mask[b] = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        int v = s[i];
                        if (v >= '0' && v <= '9')
                        {
                            ip_mask[b] *= 10;
                            ip_mask[b] += (byte)(v - '0');
                            newbit = true;
                        }
                        else if (newbit)
                        {
                            newbit = false;
                            if (b < 7)
                            {
                                b++;
                                ip_mask[b] = 0;
                            }
                            else
                            {
                                deleteRoute(ip_mask[0], ip_mask[1], ip_mask[2], ip_mask[3], ip_mask[4], ip_mask[5], ip_mask[6], ip_mask[7], gateip[0], gateip[1], gateip[2], gateip[3]);
                                break;
                            }
                        }
                    }
                    //return;
                }
                Invoke(new MethodInvoker(() => {
                    pross.Value = 100;
                }));
            }
            ).Start();
        }

        private void RoutePath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoutePath.SelectedIndex == RoutePath.Items.Count - 1)
            {
                of.Filter = "所有文件|*.*";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    if (!RoutePath.Items.Contains(of.FileName))
                    {
                        needsave = true;
                        RoutePath.Items.Insert(0, of.FileName);
                        RoutePath.SelectedIndex = 0;
                    }
                }
                else
                {
                    RoutePath.SelectedIndex = RoutePath.Items.Count - 2;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SavePathFile.Checked && needsave)
            {
                if (RoutePath.Items.Count != 1) { 
                var sw = new StreamWriter(System.Environment.CurrentDirectory + "\\fastroute.cfg");
                sw.WriteLine(RoutePath.SelectedIndex);
                sw.WriteLine(RoutePath.Items.Count-1);
                for (int i = 0; i < RoutePath.Items.Count-1; i++)
                    sw.WriteLine(RoutePath.Items[i].ToString());
                sw.Flush();
                sw.Close();
                }
            }
            base.OnClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(System.Environment.CurrentDirectory + "\\fastroute.cfg"))
            {
                var sr = new StreamReader(System.Environment.CurrentDirectory + "\\fastroute.cfg");
                var istr = sr.ReadLine();
                var cstr = sr.ReadLine();
                var paths = sr.ReadToEnd().Split('\n');
                sr.Close();
                SavePathFile.Checked = true;
                try
                {
                    int index = int.Parse(istr);
                    int count = int.Parse(cstr);
                    for (int i = 0; i < paths.Length-1; i++)
                        RoutePath.Items.Insert(0, paths[paths.Length - i - 2].Replace("\r",""));
                    RoutePath.SelectedIndex = index;
                }
                catch { }
            }
        }

        private void GithubLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/KeyMove/FastRoute");
        }
    }
}
