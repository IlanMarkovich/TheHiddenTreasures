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
        private const int MOVEMENT_SPEED = 5;

        public Player(System.Drawing.Point startPoint, int width, int height, ref Canvas gameCanvas, Handler handler) 
            : base(startPoint.X, startPoint.Y, width, height, new SolidColorBrush(Colors.Red), ref gameCanvas, handler)
        {
            Canvas.SetLeft(Rect, Handler.GAP_SIZE + X * (Handler.CELL_WIDTH + Handler.GAP_SIZE) +
                ((Handler.CELL_WIDTH - width) / 2));

            Canvas.SetTop(Rect, Handler.GAP_SIZE + Y * (Handler.CELL_HEIGHT + Handler.GAP_SIZE) +
                ((Handler.CELL_HEIGHT - height) / 2));

            gameCanvas.Children.Add(Rect);
        }

        public void MoveUp()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect), (int)Canvas.GetTop(Rect) - MOVEMENT_SPEED))
                return;

            Canvas.SetTop(Rect, Canvas.GetTop(Rect) - MOVEMENT_SPEED);
            handler.FocusOnPlayer();
        }

        public void MoveLeft()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect) - MOVEMENT_SPEED, (int)Canvas.GetTop(Rect)))
                return;

            Canvas.SetLeft(Rect, Canvas.GetLeft(Rect) - MOVEMENT_SPEED);
            handler.FocusOnPlayer();
        }

        public void MoveDown()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect), (int)Canvas.GetTop(Rect) + MOVEMENT_SPEED))
                return;

            Canvas.SetTop(Rect, Canvas.GetTop(Rect) + MOVEMENT_SPEED);
            handler.FocusOnPlayer();
        }

        public void MoveRight()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect) + MOVEMENT_SPEED, (int)Canvas.GetTop(Rect)))
                return;

            Canvas.SetLeft(Rect, Canvas.GetLeft(Rect) + MOVEMENT_SPEED);
            handler.FocusOnPlayer();
        }

        private bool WillCollide(int x, int y)
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
