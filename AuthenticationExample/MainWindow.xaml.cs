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
using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            finsemble.AuthenticationClient.PublishAuthorization(UserName.Text, JObject.FromObject(new Credentials(Guid.NewGuid().ToString())));
        }
    }
}
