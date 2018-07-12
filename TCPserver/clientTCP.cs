using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPserver
{
    class clientChat
    {
        public TcpClient client;
        public BinaryReader reading;
        public BinaryWriter writing;
        public NetworkStream ns;
        public IPEndPoint IP;
        public string nick;
        public bool activeCall;
        public BackgroundWorker bw;
        public string message;
        public int room;
        public TreeNode treeNode;

        public void Close()
        {
            client.Close();
        }

        public clientChat()
        {
            client = null;
        }
        public clientChat(TcpListener server)
        {
            client = server.AcceptTcpClient();
            ns = client.GetStream();
            IP = (IPEndPoint)client.Client.RemoteEndPoint;
            reading = new BinaryReader(ns);
            writing = new BinaryWriter(ns);
            bw = new BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bw_DoWork);
            bw.WorkerSupportsCancellation = true;
            activeCall = false;
            message = " ";
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //receive messages
            try
            {
                string messageRecived;
                while (activeCall)
                {
                    messageRecived = reading.ReadString();
                    message = messageRecived;
                    //MessageBox.Show(message);
                }

            }
            catch (Exception ex)
            {
                activeCall = false;
                message = "//command END";
                Close();
                bw.CancelAsync();
                //MessageBox.Show(ex.ToString(), "Błąd Socket "+ message);
            }
            
        }

        private TcpClient GetClient()
        {
            return client;
        }
    }
}
