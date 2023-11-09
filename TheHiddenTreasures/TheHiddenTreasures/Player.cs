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
    internal class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 2;

        public Player(System.Drawing.Point startPoint, int width, int height, ref Canvas gameCanvas) 
            : base(startPoint.X, startPoint.Y, width, height, new SolidColorBrush(Colors.Red), ref gameCanvas)
        {
            Canvas.SetLeft(Rect, Handler.gapSize + X * (Handler.cellWidth + Handler.gapSize) +
                ((Handler.cellWidth - width) / 2));

            Canvas.SetTop(Rect, Handler.gapSize + Y * (Handler.cellHeight + Handler.gapSize) +
                ((Handler.cellHeight - height) / 2));

            gameCanvas.Children.Add(Rect);
        }

        public void MoveUp()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect), (int)Canvas.GetTop(Rect) - MOVEMENT_SPEED))
                return;

            Canvas.SetTop(Rect, Canvas.GetTop(Rect) - MOVEMENT_SPEED);
        }

        public void MoveLeft()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect) - MOVEMENT_SPEED, (int)Canvas.GetTop(Rect)))
                return;

            Canvas.SetLeft(Rect, Canvas.GetLeft(Rect) - MOVEMENT_SPEED);
        }

        public void MoveDown()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect), (int)Canvas.GetTop(Rect) + MOVEMENT_SPEED))
                return;

            Canvas.SetTop(Rect, Canvas.GetTop(Rect) + MOVEMENT_SPEED);
        }

        public void MoveRight()
        {
            if (WillCollide((int)Canvas.GetLeft(Rect) + MOVEMENT_SPEED, (int)Canvas.GetTop(Rect)))
                return;

            Canvas.SetLeft(Rect, Canvas.GetLeft(Rect) + MOVEMENT_SPEED);
        }

        private bool WillCollide(int x, int y)
        {
            foreach(var obj in Handler.renderObjectLst)
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
