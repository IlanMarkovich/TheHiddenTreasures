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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheHiddenTreasures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shop : Page
    {
        private List<Button> btns;

        public Shop()
        {
            this.InitializeComponent();
            btns = new List<Button>() { Btn_1, Btn_2, Btn_3, Btn_4, Btn_5 };
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            int coins = await proxy.GetPlayerCoinsAsync(MainPage.username);
            CoinsTB.Text = $"Coins: {coins}";

            LoadSkins(proxy);
        }

        private async void LoadSkins(ServiceReference1.Service1Client proxy)
        {
            List<int> skins = (await proxy.GetUserItemsAsync(MainPage.username)).ToList();

            foreach(int skin in skins)
            {
                btns[skin - 1].Content = "Equip";
                btns[skin - 1].IsEnabled = true;
            }

            int currentSkin = await proxy.GetPlayerCurrentSkinAsync(MainPage.username);
            btns[currentSkin - 1].Content = "Equipped";
            btns[currentSkin - 1].IsEnabled = false;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new ServiceReference1.Service1Client();
            int skin = int.Parse((sender as Button).Name.Replace("Btn_", ""));

            if((sender as Button).Content is string && (string)(sender as Button).Content == "Equip")
            {
                int currentSkin = await proxy.GetPlayerCurrentSkinAsync(MainPage.username);
                btns[currentSkin - 1].Content = "Equip";
                btns[currentSkin - 1].IsEnabled = true;

                (sender as Button).Content = "Equipped";
                (sender as Button).IsEnabled = false;
                await proxy.UpdatePlayerCurrentSkinAsync(MainPage.username, skin);
            }
            else
            {
                int price = int.Parse((sender as Button).Tag.ToString());
                int coins = await proxy.GetPlayerCoinsAsync(MainPage.username);
                
                if(price > coins)
                {
                    var dialog = new MessageDialog("Not enough coins to buy this!");
                    await dialog.ShowAsync();
                    return;
                }

                coins -= price;
                CoinsTB.Text = $"Coins: {coins}";
                await proxy.BuyPlayerSkinAsync(MainPage.username, skin, price);

                (sender as Button).Content = "Equip";
            }
        }
    }
}
