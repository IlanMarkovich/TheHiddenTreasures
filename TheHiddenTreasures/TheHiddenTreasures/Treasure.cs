using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.Media.Core;

namespace TheHiddenTreasures
{
    public class Treasure : GameObject
    {
        public const int TREASURE_SIZE = 75;

        private DispatcherTimer animationTimer;
        private int currentTile;

        public Treasure(Point point, ref Canvas gameCanvas, Handler handler)
            : base(point.X, point.Y, TREASURE_SIZE, TREASURE_SIZE, GetImage("tile000.png"), ref gameCanvas, handler)
        {
            currentTile = 0;

            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(50);
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public void Open()
        {
            MainPage.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/sound/nextLevel.wav"));
            MainPage.mediaPlayer.Play();

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, object e)
        {
            if(currentTile == 5)
            {
                animationTimer.Stop();
                handler.NextLevel();
                return;
            }

            currentTile++;
            Rect.Fill = GetImage($"tile{currentTile:D3}.png");
        }

        public static ImageBrush GetImage(string imgPath)
        {
            var img = new BitmapImage();
            img.UriSource = new Uri($"ms-appx:/Assets/chest/{imgPath}");

            var brush = new ImageBrush();
            brush.ImageSource = img;
            return brush;
        }
    }
}
