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
        public enum State
        {
            NOT_LOGGED_IN, LOGGED_IN
        }

        public static State state = State.NOT_LOGGED_IN;
        public static string username;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (state != State.NOT_LOGGED_IN)
            {
                state = State.NOT_LOGGED_IN;
                ChangeUserState();
            }
        }

        private void ChangeUserState()
        {
            UsernameTB.Text = String.Empty;
            PasswordPB.Password = String.Empty;

            // Change visibility of stack panels based of the switch of the state
            foreach(UIElement item in MainPageGrid.Children)
            {
                if (!(item is StackPanel))
                    continue;

                StackPanel sp = item as StackPanel;

                if ((string)sp.Tag == "NOT_LOGGED_IN_TAG")
                    item.Visibility = state == State.NOT_LOGGED_IN ? Visibility.Collapsed : Visibility.Visible;
                else if ((string)(sp.Tag) == "LOGGED_IN_TAG")
                    item.Visibility = state == State.LOGGED_IN ? Visibility.Collapsed : Visibility.Visible;
            }

            // Change the state to the opposite one
            state = state == State.NOT_LOGGED_IN ? State.LOGGED_IN : State.NOT_LOGGED_IN;
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
                ChangeUserState();
                username = user.username;
                return;
            }

            var dialog = new MessageDialog("Error while trying to Login!");
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
                ChangeUserState();
                username = user.username;
                return;
            }

            var dialog = new MessageDialog("Error while trying to Register!");
            await dialog.ShowAsync();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game));
        }

        private void ShopBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Shop));
        }

        private void StatisticsBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Statistics));
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserState();
        }
    }
}
