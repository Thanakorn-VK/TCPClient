using System.Text;
using SimpleTcp;

namespace TCPClient2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                buttonSend.Enabled = true;
                buttonConnect.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(textMessage.Text))
                {
                    client.Send(textMessage.Text);
                    textInfo.Text += $"Me: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                client = new(textIP.Text);
                client.Events.Connected += Events_Connected;
                client.Events.DataReceived += Events_DataReceived;
                client.Events.Disconnected += Events_Disconnected;
                buttonSend.Enabled = false;
            });
        }

        private void Events_Disconnected(object? sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            { 
                textInfo.Text += $"Server disconnected.{Environment.NewLine}";
            });
            
        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            { 
                textInfo.Text += $"Server: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
            
        }

        private void Events_Connected(object? sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            { 
                textInfo.Text += $"Server connected.{Environment.NewLine}";
            });
            
        }

        
    }
}