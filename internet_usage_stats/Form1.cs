using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Globalization;

namespace internet_usage_stats
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string s;
        private void button1_Click(object sender, EventArgs e)
        {

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                //NetworkInterface nic = interfaces[0];
                int a = interfaces.Length;
                int i, f = 0;
                //MessageBox.Show(a.ToString());
                for(i=0; i<interfaces.Length;i++)
                {
                    NetworkInterface nic = interfaces[i];
                    if (interfaces[i].OperationalStatus == OperationalStatus.Up && nic.GetIPv4Statistics().BytesReceived/1024 > 20)
                    {
                        IPv4InterfaceStatistics netstats = nic.GetIPv4Statistics();

                        if(nic.Speed > 0)
                        {
                            float down = netstats.BytesReceived / 1024;
                            float up = netstats.BytesSent / 1024;
                            float total = down + up;
                            float lspeed = nic.Speed / 1024;

                            unit(down);
                            label1.Text = "Download: " + s;
                            unit(up);
                            label2.Text = "Upload: " + s;
                            unit(total);
                            label3.Text = "Total: " + s;
                            unit(lspeed);
                            label4.Text = "Link Speed: " + s + "ps";
                            label5.Text = "Interface: " + nic.Name;
                            f = 1;
                            break;
                        }
                    }
                }
                if (f == 0)
                    MessageBox.Show("No active internet connection", "Error !");
            }
        }
        private void unit(float x)
        {
            if(x<1024)
            {
                s = x.ToString("F", CultureInfo.InvariantCulture) + " KB";
            }
            else if(x>1024 && x<1048576)
            {
                x = x / 1024;
                s = x.ToString("F", CultureInfo.InvariantCulture) + " MB";
            }
            else
            {
                x = (x / 1024) / 1024;
                s = x.ToString("F", CultureInfo.InvariantCulture) + " GB";
            }
        }
    }
}
