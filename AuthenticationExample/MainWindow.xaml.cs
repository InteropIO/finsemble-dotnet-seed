using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;

namespace AuthenticationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Finsemble finsemble;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!loggedIn) finsemble.ShutdownApplication();
        }

        private bool loggedIn = false;

        public MainWindow(string[] args)
        {
            //Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
            finsemble = new Finsemble(args, this);
            finsemble.Connect();
            finsemble.Connected += Finsemble_Connected;
        }

        private void Finsemble_Connected(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate //main thread
            {
                // Initialize this Window and show it
                InitializeComponent();
                this.Show();

            });
        }

        private void PublishCredentials_Click(object sender, RoutedEventArgs e)
        {
            finsemble.AuthenticationClient.PublishAuthorization(UserName.Text, JObject.FromObject(new Credentials(Guid.NewGuid().ToString())));
        }
    }
}
