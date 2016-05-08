using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Socket sckCommunication;
        EndPoint epLocal, epRemote;

        byte[] buffer;

        private String getLocalIp()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }

            }
            return "127.0.0.1";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            epLocal = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(textBox4.Text));
            sckCommunication.Bind(epLocal);
            epRemote = new IPEndPoint(IPAddress.Parse(textBox2.Text), Convert.ToInt32(textBox5.Text));
            sckCommunication.Connect(epRemote);

            buffer = new byte[1464];
            sckCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(OperatorCallBack), buffer);
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sckCommunication = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sckCommunication.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            textBox1.Text = getLocalIp();
            textBox2.Text = getLocalIp();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Text.ASCIIEncoding enc =
            new System.Text.ASCIIEncoding();
            byte[] msg = new byte[1464];
            msg = enc.GetBytes(textBox3.Text);
        
            // sending the message
            sckCommunication.Send(msg);

            // add to listbox
            listBox1.Items.Add("You: " + textBox3.Text);

            // clear textBox3
            textBox3.Clear();
        }

        private void OperatorCallBack(IAsyncResult ar)
        {
            try
            {
                int size = sckCommunication.EndReceiveFrom(ar, ref epRemote);

                if(size>0)
                {
                    byte[] aux = new byte[1464];
                    aux = (byte[])ar.AsyncState;
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    string msg = enc.GetString(aux);
                    listBox1.Items.Add("Friend: " + msg);
                }
                buffer = new byte[1464];
                sckCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(OperatorCallBack), buffer);
            

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
    }
}
