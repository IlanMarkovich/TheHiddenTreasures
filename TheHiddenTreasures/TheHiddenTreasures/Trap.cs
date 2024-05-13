using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTreasures
{
    internal class Trap : GameObject
    {
        public const int TRAP_SIZE = 75;

        private DispatcherTimer animationTimer;
        private int currentTile;

        public Trap(System.Drawing.Point point, ref Canvas gameCanvas, Handler handler)
            : base(point.X, point.Y, TRAP_SIZE, TRAP_SIZE, GetImage("tile000.png"), ref gameCanvas, handler)
        {
            currentTile = 0;

            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(50);
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public void StartTrap()
        {
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, object e)
        {
            if(currentTile == 3)
            {
                animationTimer.Stop();
                handler.GameOver();
                return;
            }

            currentTile++;
            Rect.Fill = GetImage($"tile{currentTile:D3}.png");
        }

        private static ImageBrush GetImage(string imgPath)
        {
            var img = new BitmapImage();
            img.UriSource = new Uri($"ms-appx:/Assets/trap/{imgPath}");

            var brush = new ImageBrush();
            brush.ImageSource = img;
            return brush;
        }
    }
}
