using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.IO;

namespace ICQ
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        private IPEndPoint serverEndPoint;
        private UdpClient udpClient;
        private ChatUser chatUser;
        private MessageInfo lastChatTime = new();
        private SoundPlayer notificationSound;

        int serverPort = 11000;
        string serverAddress = "239.0.0.1";

        public ChatView(ChatUser user)
        {
            InitializeComponent();
            Connection();
            chatUser = user;
            userTextBlock.Text = chatUser.Username;

            Stream stream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/Notification.wav")).Stream;
            notificationSound = new(stream);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageTextBox.Text != null)
            {
                lastChatTime.DateTime = DateTime.Now;

                string message = $"[{lastChatTime.DateTime:G}] [{chatUser.Username}]: {messageTextBox.Text}";

                messageTextBox.AppendText(message);

                byte[] data = Encoding.UTF8.GetBytes(message);
                udpClient.Send(data, data.Length, serverEndPoint);

                messageTextBox.Text = "";
            }
        }

        private void Connection()
        {
            udpClient = new();
            IPAddress serverIpAddress = IPAddress.Parse(serverAddress);
            udpClient.JoinMulticastGroup(serverIpAddress);

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient.Client.Bind(localEndPoint);

            int localPort = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port;

            serverEndPoint = new(serverIpAddress, serverPort);

            var receiveThread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    byte[] receivedData = udpClient.Receive(ref serverEndPoint);
                    string message = Encoding.UTF8.GetString(receivedData);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        chatTextBox.Text += message + "\n";

                        PlayNotificationSound();
                    });
                }
            });

            receiveThread.Start();
        }

        private void PlayNotificationSound()
        {
            notificationSound.Play();
        }
    }
}
