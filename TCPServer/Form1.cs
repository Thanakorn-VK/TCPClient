using System.Text;
using SimpleTcp;

namespace TCPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            server.Start();
            textInfo.Text += $"Starting...{Environment.NewLine}";
            buttonStart.Enabled = false;
            buttonSend.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonSend.Enabled = false;
            server = new SimpleTcpServer(textIP.Text);
            server.Events.ClientConnected += Event_ChientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;
        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textInfo.Text += $"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
            
        }

        private void Events_ClientDisconnected(object? sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
            textInfo.Text += $"{e.IpPort} disconnected.{Environment.NewLine}";
                listClientIP.Items.Remove(e.IpPort);
            });
            
        }

        private void Event_ChientConnected(object? sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
            textInfo.Text += $"{e.IpPort} connected.{Environment.NewLine}";
                listClientIP.Items.Add(e.IpPort);
            });
            
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                if(!string.IsNullOrEmpty(textMessage.Text) && listClientIP.SelectedItem != null)
                {
                    server.Send(listClientIP.SelectedItem.ToString(), textMessage.Text);
                    textInfo.Text += $"Server: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = String.Empty;
                }
            }
        }
    }
}