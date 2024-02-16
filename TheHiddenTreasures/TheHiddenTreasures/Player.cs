using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTreasures
{
    public class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 7;
        public const int PLAYER_SIZE = 75;

        private int idleTile, animationTile;
        private DispatcherTimer animationTimer;

        public Player(System.Drawing.Point startPoint, ref Canvas gameCanvas, Handler handler)
            : base(startPoint.X, startPoint.Y, PLAYER_SIZE, PLAYER_SIZE, GetImage("idle/tile000.png"), ref gameCanvas, handler)
        {
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(250);
            animationTimer.Tick += AnimationTimer_Tick;
        }

        public static ImageBrush GetImage(string imgPath)
        {
            var img = new BitmapImage();
            img.UriSource = new Uri($"ms-appx:/Assets/1/{imgPath}");

            var brush = new ImageBrush();
            brush.ImageSource = img;
            return brush;
        }

        public bool DidAnimationDirectionChange()
        {
            return animationTile / 4 != idleTile;
        }

        public void ChangeIdleDirection(int tile)
        {
            if (idleTile == tile)
                return;

            idleTile = tile;
            animationTile = idleTile * 4;
            Rect.Fill = GetImage($"idle/tile{idleTile:D3}.png");
        }

        public void StartAnimation()
        {
            if (animationTimer.IsEnabled)
                return;

            animationTimer.Start();
        }

        public void StopAnimation()
        {
            if (!animationTimer.IsEnabled)
                return;

            animationTimer.Stop();
            Rect.Fill = GetImage($"idle/tile{idleTile:D3}.png");
        }

        private void AnimationTimer_Tick(object sender, object e)
        {
            animationTile = ((animationTile + 1) % 4) == 0 ? animationTile - 3 : animationTile + 1;
            Rect.Fill = GetImage($"move/tile{animationTile:D3}.png");
        }

        public void Move(VirtualKey pressedKey, int movementSpeed = MOVEMENT_SPEED)
        {
            double x = Canvas.GetLeft(Rect),
                y = Canvas.GetTop(Rect);

            switch (pressedKey)
            {
                case VirtualKey.W:
                    y -= movementSpeed;
                    break;
                case VirtualKey.A:
                    x -= movementSpeed;
                    break;
                case VirtualKey.S:
                    y += movementSpeed;
                    break;
                case VirtualKey.D:
                    x += movementSpeed;
                    break;
            }

            if (WillCollide(x, y))
            {
                // Decrease the movement speed to allow smaller movement to occur
                if(movementSpeed - 1 > 0)
                    Move(pressedKey, movementSpeed - 1);

                return;
            }

            Canvas.SetLeft(Rect, x);
            Canvas.SetTop(Rect, y);

            X = (int) Math.Round((x - Handler.GAP_SIZE - ((Handler.CELL_WIDTH - Rect.Width) / 2)) / (Handler.CELL_WIDTH + Handler.GAP_SIZE));
            Y = (int) Math.Round((y - Handler.GAP_SIZE - ((Handler.CELL_HEIGHT - Rect.Height) / 2)) / (Handler.CELL_HEIGHT + Handler.GAP_SIZE));

            handler.UpdateOnPlayerMove();
        }

        private bool WillCollide(double x, double y)
        {
            foreach(var obj in handler.RenderObjectList)
            {
                if (!(obj is Wall) && !(obj is Trap) && !(obj is Coin))
                    continue;

                var wallRect = new Rect(Canvas.GetLeft(obj.Rect), Canvas.GetTop(obj.Rect), obj.Rect.Width, obj.Rect.Height);
                var playerRect = new Rect(x + Rect.Width / 3, y + Rect.Height / 4, Rect.Width * 0.375, Rect.Height / 2);

                playerRect.Intersect(wallRect);

                if (!playerRect.IsEmpty)
                {
                    if (obj is Trap)
                        handler.GameOver();

                    if (obj is Coin)
                        handler.AddCoin(obj as Coin);

                    return true;
                }
            }

            return false;
        }
    }
}
