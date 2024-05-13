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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheHiddenTreasures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerSkinsPage : Page
    {
        public PlayerSkins playerSkins;

        public PlayerSkinsPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlayerName.Text = $"Player: {playerSkins.username}";

            CurrentSkinImg.Source = new BitmapImage()
            {
                UriSource = new Uri($"ms-appx:/Assets/{playerSkins.skins.currentSkin}/idle/tile000.png")
            };

            List<string> skinsImageSources = new List<string>();

            foreach(int skin in playerSkins.skins.skins)
            {
                skinsImageSources.Add($"Assets/{skin}/idle/tile000.png");
            }

            PlayerOwnedSkinsList.DataContext = skinsImageSources;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            playerSkins = e.Parameter as PlayerSkins;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage.PlayClickSound();

            Frame.Navigate(typeof(Statistics));
        }
    }
}
