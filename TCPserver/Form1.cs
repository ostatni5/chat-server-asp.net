using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPserver
{


    public partial class MainWindow : Form
    {
        private TcpListener server = null;
        private List<clientChat> consL = new List<clientChat>();
        TreeNode root = new TreeNode("Użytkownicy");
        private TreeNode[] rooms;
        private List<clientChat>[] roomsTab;
        private int visibleItems = 0;
        private string[] commandList = { "help - wyświetla pomoc", "kick [user] - rozłącza użtkownika", "private [user] - prywatana wiadomość", "roomMsg [pokój] - prywatana wiadomość do pokoju" };
        public MainWindow()
        {
            InitializeComponent();
            tbMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);
            Random rnd = new Random();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    lbMessage.Items.Add(ip.ToString());
                    tbHostAddress.Text = ip.ToString();
                }
            }
            //throw new Exception("Local IP Address Not Found!");
        }

        static string Hash(string input)//hasowanie
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        private void lbMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != lbMessage) return;

            if (e.Control && e.KeyCode == Keys.C)
                CopySelectedValuesToClipboard();
        }

        private void CopySelectedValuesToClipboard()
        {
            try
            {
                string items = "";
                foreach (int index in lbMessage.SelectedIndices)
                {
                    items += (lbMessage.Items[index] as string) + '\n';
                }
                Clipboard.SetText(items.Trim()); // trim to remove the remaining '\n'

                //Clipboard.SetText(builder.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                bSend.PerformClick();
            }
        }
        private void GetTcpConnections(clientChat client)
        {

            if (client.client.Client.Poll(0, SelectMode.SelectRead))
            {
                byte[] buff = new byte[1];
                if (client.client.Client.Receive(buff, SocketFlags.Peek) == 0)
                {
                    // Client disconnected
                    lbMessage.Items.Add("Koniec poł z: " + client.IP.ToString());
                    client.activeCall = false;
                }
            }
        }

        private string jsonS(string msg,string sys)
        {
            string[] send = { msg,sys };
            string json = JsonConvert.SerializeObject(send);
            return json;
        }
        private string jsonS(string msg)
        {
            string[] send = { msg };
            string json = JsonConvert.SerializeObject(send);
            return json;
        }
        private string jsonS(string[] msg)
        {
            string json = JsonConvert.SerializeObject(msg);
            return json;
        }
        private string[] jsonD(string msg)
        {
            string[] json = JsonConvert.DeserializeObject<string[]>(msg);
            return json;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            lbMessage.Items.Add("Zaczynam pracę serwera ...");
            lbMessage.Items.Add("Pomoc //command help");
            tbMessage.Focus();
            if (!bwServer.IsBusy)
                bwServer.RunWorkerAsync();
            bStart.Enabled = false;
            bStop.Enabled = true;
            tvUsers.Nodes.Clear();
            root.Nodes.Clear();
            roomsTab = new List<clientChat>[16];
            tvUsers.Nodes.Add(root);
            rooms = new TreeNode[16];
            for (int i = 0; i < rooms.Count(); i++)
            {
                rooms[i] = new TreeNode("Pokój " + i);
                roomsTab[i] = new List<clientChat>();
            }

            root.Nodes.AddRange(rooms);
            

        }

        private void bStop_Click(object sender, EventArgs e)
        {
            //try
            //{
            lbMessage.Items.Add("Zakończono pracę serwera ...");
 
            bwServer.CancelAsync();
            backgroundWorker2.CancelAsync();
            for (int i = 0; i < consL.Count; i++)
            {
                consL[i].Close();
            }
            consL.Clear();
            server.Stop();
            bStart.Enabled = true;
            bStop.Enabled = false;

            
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Błąd");
            //}
        }

        private void bwServer_DoWork(object sender, DoWorkEventArgs e)
        {
            IPAddress adresIP = null;
            try
            {
                adresIP = IPAddress.Parse(tbHostAddress.Text);
            }
            catch
            {
                MessageBox.Show("Błędny format adresu IP!", "Błąd");
                tbHostAddress.Invoke(new MethodInvoker(delegate { tbHostAddress.Text = String.Empty; }));
                //tbHostAddress.Text = String.Empty;
                return;
            }

            int port = System.Convert.ToInt16(nUDPort.Value);

            
            bool activeCall = false;
            //connection: 
            server = new TcpListener(adresIP, port);
            server.Start();
            if (!backgroundWorker2.IsBusy)
                backgroundWorker2.RunWorkerAsync();

            lbMessage.Invoke(new MethodInvoker(delegate {
                visibleItems = lbMessage.ClientSize.Height / lbMessage.ItemHeight;
            }));

            try
            {
                while (true)
                {
                   
                    clientChat client = new clientChat(server);
                    NetworkStream ns = client.ns;
                    IPEndPoint IP = client.IP;
                    BinaryReader reading = client.reading;
                    BinaryWriter writing = client.writing;
                    bool newNick = true;

                    string[] request = JsonConvert.DeserializeObject<string[]>(reading.ReadString());
                    //foreach (string Bar in request)
                    //{
                    //    lbMessage.Invoke(new MethodInvoker(delegate {
                    //        lbMessage.Items.Add(Bar);
                    //    }));
                    //}
                    client.nick = request[1];
                    client.room = System.Convert.ToInt16(request[2]);
                    client.treeNode = new TreeNode(client.nick);
                    client.treeNode.Nodes.Add(new TreeNode(IP.ToString()));
                    for (int i = 0; i < consL.Count; i++)
                    {
                        if (consL[i].nick == client.nick)
                            newNick = false;                        
                    }
                    if(newNick)
                        if (request[0] == Hash(tbPass.Text) && request.Length == 3)
                        {
                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("[" + IP.ToString() + "] :Nawiązano połączenie"); lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));

                            client.activeCall = true;
                            string messageSent = "1";
                            writing.Write(jsonS(messageSent));
                            messageSent = "Witaj w SzymCZAT pokój ogólny to 0";
                            writing.Write(jsonS(messageSent));
                            ;
                        }
                        else
                        {
                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("[" + IP.ToString() + "] :Odrzucono połączenie"); lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));
                            string messageSent = "0";
                            writing.Write(jsonS(messageSent));
                            client.Close();
                            client.activeCall = false;
                        }
                    else
                    {
                        lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("[" + IP.ToString() + "] :Odrzucono połączenie - zajety nick"); lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));
                        string messageSent = "2";
                        writing.Write(jsonS(messageSent));
                        client.Close();
                        client.activeCall = false;
                    }
                    if (client.activeCall)
                    {
                        consL.Add(client);
                        if (!client.bw.IsBusy)
                            client.bw.RunWorkerAsync();

                        tvUsers.Invoke(new MethodInvoker(delegate {
                            rooms[client.room].Nodes.Add(client.treeNode);
                            roomsTab[client.room].Add(client);
                        }));

                        string messageSent = "Zalogowałeś do pokoju {" + client.room + "}";
                        writing.Write(jsonS(messageSent));
                        List<string> users = new List<string>();
                        for (int u = 0; u < roomsTab[client.room].Count; u++)
                        {
                            users.Add(roomsTab[client.room][u].nick);
                        }
                        messageSent = "Witamy " + client.nick;
                        for (int j = 0; j < consL.Count; j++)
                        {
                            if (consL[j].room == client.room)
                                consL[j].writing.Write(jsonS(messageSent, jsonS(users.ToArray())));
                        }
                        //BackgroundWorker bw = new BackgroundWorker();
                        //bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bwCON_DoWork);
                        //bw.WorkerSupportsCancellation = true;
                    }
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString(), "Błąd");
            }


        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //receive messages
            try
            {
                int l = consL.Count;
                while (true)
                {
                    for (int i = 0; i < consL.Count; i++)
                    {
                        
                        lCountUsers.Invoke(new MethodInvoker(delegate { lCountUsers.Text= consL.Count.ToString(); }));
                        
                        //GetTcpConnections(consL[i]);
                        string message = consL[i].message;
                        if (message == "//command END")
                        {

                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("[" + consL[i].IP.ToString() + "] :Koniec połączenia"); lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));
                            tvUsers.Invoke(new MethodInvoker(delegate
                            {
                                rooms[consL[i].room].Nodes.Remove(consL[i].treeNode);
                                roomsTab[consL[i].room].Remove(consL[i]);
                            }));
                            List<string> users = new List<string>();
                            for (int u = 0; u < roomsTab[consL[i].room].Count; u++)
                            {
                                users.Add(roomsTab[consL[i].room][u].nick);
                            }
                            clientChat leaveUser = consL[i];
                            consL[i].activeCall = false;
                            consL[i].bw.CancelAsync();
                            consL[i].Close();
                            consL.RemoveAt(i);
                            string messageSent = "Żegnamy " + leaveUser.nick;
                            for (int j = 0; j < consL.Count; j++)
                            {
                                if (consL[j].room == leaveUser.room)
                                    consL[j].writing.Write(jsonS(messageSent, jsonS(users.ToArray())));
                            }
                            lCountUsers.Invoke(new MethodInvoker(delegate { lCountUsers.Text = consL.Count.ToString(); }));
                            continue;
                        }
                        else if (message != " ")
                        {
                            CultureInfo ci = CultureInfo.InvariantCulture;
                            string time = DateTime.Now.ToString("HH:mm", ci);
                            string messageSent ="["+time+ "]{"+ consL[i].room+ "} <" + consL[i].nick + "> " + message;
                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add(messageSent); lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));
                            List<string> users = new List<string>();
                            for (int u = 0; u < roomsTab[consL[i].room].Count; u++)
                            {
                                users.Add(roomsTab[consL[i].room][u].nick);
                            }
                            for (int j = 0; j < consL.Count; j++)
                            {
                                if(consL[j].room == consL[i].room)
                                consL[j].writing.Write(jsonS(messageSent, jsonS(users.ToArray())));
                            }
                            consL[i].message = " ";
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Błąd BACG 2");
            }

        }
        
        private void bSend_Click(object sender, EventArgs e)
        {
            if(tbMessage.Text!="")
            try
            {
                    string msg = tbMessage.Text;
                    tbMessage.Text = "";
                    String[] command = msg.Split(' ');
                    switch (command[0])
                    {
                        case "//command":
                                if(command.Length>1)
                                switch (command[1])
                                {
                                    case "help":
                                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("Dostepne Komendy"); }));
                                            for(int i = 0;i<commandList.Length;i++)
                                            {
                                                lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add(commandList[i]); }));
                                            }
                                            break;
                                    case "kick":
                                        if (command.Length == 3)
                                            for (int j = 0; j < consL.Count; j++)
                                            {
                                                if (consL[j].nick == command[2])
                                                {
                                                    consL[j].writing.Write(jsonS("Wyrzucono cię"));
                                                    consL[j].message = "//command END";
                                                }
                                                
                                            }
                                        break;
                                    case "private":
                                        if (command.Length > 3)
                                            for (int j = 0; j < consL.Count; j++)
                                            {
                                                if (consL[j].nick == command[2])
                                                {
                                                    command[0] = "";
                                                    command[1] = "";
                                                    command[2] = "";
                                                    string messageSent2 = "<serwer> " + String.Join(" ", command);
                                                    consL[j].writing.Write(jsonS(messageSent2));
                                                    lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add(messageSent2); }));
                                                    //command private nick79 siema eniu
                                                }

                                            }
                                        break;
                                    case "roomMsg":
                                        int room;
                                        bool sended=false;
                                        if (command.Length > 3 && 9999!=(room=Int16.Parse(command[2])) )
                                            for (int j = 0; j < consL.Count; j++)
                                            {
                                                string messageSent2="Złe dane";
                                                if (consL[j].room == room)
                                                {
                                                    command[0] = "";
                                                    command[1] = "";
                                                    command[2] = "";
                                                    messageSent2 = "<serwer> " + String.Join(" ", command);
                                                    consL[j].writing.Write(jsonS(messageSent2));
                                                    
                                                    //command private nick79 siema eniu
                                                }
                                                if (!sended)
                                                { lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add(messageSent2); })); sended = true; }

                                            }
                                        break;
                                    default:
                                        lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add("Brak lub błędna komenda"); }));                                        
                                        break;
                                }
                            break;
                        default:
                            //sending a message:
                            string messageSent = "<serwer> " + msg;
                            lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.Items.Add(messageSent); }));
                            for (int i = 0; i < consL.Count; i++)
                            {
                                //for example//=TextBoxl.text;
                                consL[i].writing.Write(jsonS(messageSent));
                            }
                            break;
                    }
                
                lbMessage.Invoke(new MethodInvoker(delegate { lbMessage.TopIndex = Math.Max(lbMessage.Items.Count - visibleItems + 1, 0); }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Błąd");
                
            }
        }
    }
}
