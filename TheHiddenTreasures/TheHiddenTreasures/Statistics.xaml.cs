using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            ObservableCollection<ServiceReference1.PlayerStatistics> DBstatistics = await proxy.GetPlayerStatisticsAsync();
            proxy.Close();

            ObservableCollection<PlayerStatistics> statistics = new ObservableCollection<PlayerStatistics>();

            foreach(var player in DBstatistics)
            {
                statistics.Add(new PlayerStatistics(player.username, player.gamesPlayed, player.gamesWon, player.minTime));
            }

            PlayersLst.DataContext = statistics;
        }
    }
}
