using System;
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

namespace TheHiddenTreasures
{
    public class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 7;

        public Player(System.Drawing.Point startPoint, int width, int height, ref Canvas gameCanvas, Handler handler) 
            : base(startPoint.X, startPoint.Y, width, height, new SolidColorBrush(Colors.Red), ref gameCanvas, handler)
        {
            Canvas.SetLeft(Rect, Handler.GAP_SIZE + X * (Handler.CELL_WIDTH + Handler.GAP_SIZE) +
                ((Handler.CELL_WIDTH - width) / 2));

            Canvas.SetTop(Rect, Handler.GAP_SIZE + Y * (Handler.CELL_HEIGHT + Handler.GAP_SIZE) +
                ((Handler.CELL_HEIGHT - height) / 2));

            gameCanvas.Children.Add(Rect);
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

            handler.FocusOnPlayer();
        }

        private bool WillCollide(double x, double y)
        {
            foreach(var obj in handler.RenderObjectLst)
            {
                if (!(obj is Wall))
                    continue;

                var wallRect = new Rect(Canvas.GetLeft(obj.Rect), Canvas.GetTop(obj.Rect), obj.Rect.Width, obj.Rect.Height);
                var playerRect = new Rect(x, y, Rect.Width, Rect.Height);

                playerRect.Intersect(wallRect);

                if (!playerRect.IsEmpty)
                    return true;
            }

            return false;
        }
    }
}
