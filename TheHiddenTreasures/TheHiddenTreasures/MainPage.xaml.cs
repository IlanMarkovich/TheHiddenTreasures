using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheHiddenTreasures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTB.Text == String.Empty || PasswordPB.Password == String.Empty)
                return;

            var proxy = new ServiceReference1.Service1Client();
            var user = new ServiceReference1.User();
            user.username = UsernameTB.Text;
            user.password = PasswordPB.Password;

            if(await proxy.ValidateUserAsync(user))
            {
                Frame.Navigate(typeof(Game));
                return;
            }

            var dialog = new MessageDialog("Unable to Login!");
            await dialog.ShowAsync();
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTB.Text == String.Empty || PasswordPB.Password == String.Empty)
                return;

            var proxy = new ServiceReference1.Service1Client();
            var user = new ServiceReference1.User();
            user.username = UsernameTB.Text;
            user.password = PasswordPB.Password;

            if (await proxy.RegisterUserAsync(user))
            {
                Frame.Navigate(typeof(Game));
                return;
            }

            var dialog = new MessageDialog("Unable to Register!");
            await dialog.ShowAsync();
        }
    }
}
