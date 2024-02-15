﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTreasures
{
    public class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 7;
        public const int PLAYER_SIZE = 25;

        public Player(System.Drawing.Point startPoint, ref Canvas gameCanvas, Handler handler)
            : base(startPoint.X, startPoint.Y, PLAYER_SIZE, PLAYER_SIZE, new SolidColorBrush(Colors.Red), ref gameCanvas, handler)
        {}

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
                var playerRect = new Rect(x, y, Rect.Width, Rect.Height);

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
