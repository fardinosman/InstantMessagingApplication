using ChatInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ChattingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChatService Server;
        private static DuplexChannelFactory<IChatService> _channelFactory;
        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallBack(), "ChatServiceEndPoint");
            Server = _channelFactory.CreateChannel();

           
        }
        public void TakeMessage(string message, string userName)
        {
            TextBox.Text += userName + ": " + message+"\n";
            TextBox.ScrollToEnd();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text.Length ==0)
            {
                return;
            }
            Server.SendMessageToAll(MessageTextBox.Text, UserNameTextBox.Text);
            TakeMessage(MessageTextBox.Text, "you");
            MessageTextBox.Text = "";
            //MessageTextBox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = Server.Login(UserNameTextBox.Text)
;
            if (returnValue ==1)
            {
                MessageBox.Show("You are already logged in. Try agin");
            }
            else if (returnValue ==0)
            {
                MessageBox.Show("You logged in!");
                WelcomeLabel.Content = "Welcome " + UserNameTextBox.Text + "!";
                UserNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
                //this  will load our users
                LoadUserList(Server.GetCurrentUsers());

                
            }
        }
        private void LoadUserList(List<string> users)
        {
            foreach (var user in users)
            {
                AddUSerToList(user);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Server.Logout();
        }
        public void AddUSerToList(string userName)
        {
            if (UsersListBox.Items.Contains(userName))
            {
                return;
            }
            UsersListBox.Items.Add(userName);
        }
        public void RemoveUserFromList(string userName)
        {
            UsersListBox.Items.Remove(userName);
        }
    }
}
