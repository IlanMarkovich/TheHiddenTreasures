using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
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
using static TheHiddenTreasures.MainPage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheHiddenTreasures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Statistics : Page
    {
        public Statistics()
        {
            this.InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            User_TB.Text = "Current User: " + MainPage.username;

            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            ObservableCollection<ServiceReference1.PlayerStatistics> DBstatistics = await proxy.GetPlayerStatisticsAsync();
            proxy.Close();

            ObservableCollection<PlayerStatistics> statistics = new ObservableCollection<PlayerStatistics>();

            foreach(var player in DBstatistics)
            {
                statistics.Add(new PlayerStatistics(player.username, player.gamesPlayed, player.gamesWon, player.minTime, player.coins));
            }

            PlayersLst.DataContext = statistics;
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "User Delete",
                Content = "Are you sure you want to delete your user?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            if (await confirmDialog.ShowAsync() == ContentDialogResult.Secondary)
                return;

            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            await proxy.DeleteUserAsync(MainPage.username);
            proxy.Close();

            MainPage.state = State.NOT_LOGGED_IN;
            Frame.Navigate(typeof(MainPage));
        }
    }
}
