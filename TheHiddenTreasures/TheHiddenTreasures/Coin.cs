using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTreasures
{
    public class Coin : GameObject
    {
        private const int COIN_TILES = 15;

        private int tile;
        private DispatcherTimer timer;

        public Coin(System.Drawing.Point point, int width, int height, ref Canvas gameCanvas, Handler handler)
            : base(point.X, point.Y, width, height, CoinImage(0), ref gameCanvas, handler)
        {
            tile = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static ImageBrush CoinImage(int tile)
        {
            var img = new BitmapImage();
            img.UriSource = new Uri($"ms-appx:/Assets/coin/tile{tile:D3}.png");

            var imgBrush = new ImageBrush();
            imgBrush.ImageSource = img;
            return imgBrush;
        }

        private void Timer_Tick(object sender, object e)
        {
            tile = (tile + 1) % COIN_TILES;
            Rect.Fill = CoinImage(tile);
        }
    }
}
