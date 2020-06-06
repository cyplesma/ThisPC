using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThisPc
{
    public partial class frmName : Form
    {

        IPAddress[] arsIPAddresses;
        int iIPAddressCount = 0;
        int iCurrentIPAddress = 0;
        string gsIPAddress = "OOO";
        public frmName()
        {
            InitializeComponent();
        }

        private void frmName_Load(object sender, EventArgs e)
        {
            this.Left = 300;
            this.Top = 0;
            lblName.Text = Environment.MachineName.ToUpper();

            Bitmap bmp = new Bitmap(this.BackgroundImage);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            
            bmp.MakeTransparent(bmp.GetPixel(2, 2));            
            this.BackgroundImage = null;
            this.BackgroundImage = bmp;


            CollectIPAddresses();
            GetHost();
            lblIP.Text = gsIPAddress;
            SetKeyValue();



            tmrKey_Tick(this, null);

        }

        private void CollectIPAddresses()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                arsIPAddresses = host.AddressList;
                iIPAddressCount = host.AddressList.Length;
            }
            else
            {
                lblIP.Text = "Not Connected!!";
            }

        }

        private void lblIP_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tmrKey_Tick(object sender, EventArgs e)
        {

            tmrKey.Enabled = false;

            SetKeyValue();

            //lblIP.Text = arsIPAddresses[iCurrentIPAddress].ToString();
            iCurrentIPAddress++;
            if (iCurrentIPAddress >= iIPAddressCount)
            {
                CollectIPAddresses();
                iCurrentIPAddress = 0;
            }


            tmrKey.Enabled = true;
        }


        private void SetKeyValue()
        {

            Random rSeed = new Random();
            string sKey = "";
            for (int i = 0; i < 32; i++)
            {
                int rChar = rSeed.Next(33, 126);

                sKey += Char.ConvertFromUtf32(rChar);
            }

            lblKey.Text = sKey;
        }

        public void GetHost()
        {
            string gsHostName = Dns.GetHostName();

            

            for(Int16 i = 0; i < System.Net.Dns.GetHostEntry(gsHostName).AddressList.Length ; i++)
            { 
            if( System.Net.Dns.GetHostEntry(gsHostName).AddressList[i].IsIPv6LinkLocal == false )
                    {
                     gsIPAddress = System.Net.Dns.GetHostEntry(gsHostName).AddressList[i].ToString();
                if( gsIPAddress.Contains(".") == true)
                    {
                        return;
                }

            }

        }
        //gsIPAddress = System.Net.Dns.GetHostEntry(gsHostName).AddressList(0).ToString() 


    }
    }
}
