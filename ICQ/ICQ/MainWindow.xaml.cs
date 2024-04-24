using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ICQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatUser user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if(Check())
            {
                ChatControl = new ChatView(user);
                this.Content = ChatControl;
            }
        }

        private bool Check()
        {
            if(usernameTextBox.Text != null && usernameTextBox.Text != "")
            {
                user = new()
                {
                    Username = usernameTextBox.Text,
                };

                return true;
            }

            else
            {
                MessageBox.Show("Enter your username!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }
    }
}
