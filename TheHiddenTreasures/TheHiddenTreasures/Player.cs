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
            : base(startPoint, width, height, new SolidColorBrush(Colors.Red), ref gameCanvas)
        {
            CanvasX = Handler.gapSize + X * (Handler.cellWidth + Handler.gapSize) +
                ((Handler.cellWidth - width) / 2);

            CanvasY = Handler.gapSize + Y * (Handler.cellHeight + Handler.gapSize) +
                ((Handler.cellHeight - height) / 2);

            Canvas.SetLeft(Rect, CanvasX);
            Canvas.SetTop(Rect, CanvasY);

            gameCanvas.Children.Add(Rect);
        }

        public void MoveUp()
        {
            if (WillCollide((int)CanvasX, (int)CanvasY - MOVEMENT_SPEED))
                return;

            CanvasY -= MOVEMENT_SPEED;
            Canvas.SetTop(Rect, CanvasY);
        }

        public void MoveLeft()
        {
            if (WillCollide((int)CanvasX - MOVEMENT_SPEED, (int)CanvasY))
                return;

            CanvasX -= MOVEMENT_SPEED;
            Canvas.SetLeft(Rect, CanvasX);
        }

        public void MoveDown()
        {
            if (WillCollide((int)CanvasX, (int)CanvasY + MOVEMENT_SPEED))
                return;

            CanvasY += MOVEMENT_SPEED;
            Canvas.SetTop(Rect, CanvasY);
        }

        public void MoveRight()
        {
            if (WillCollide((int)CanvasX + MOVEMENT_SPEED, (int)CanvasY))
                return;

            CanvasX += MOVEMENT_SPEED;
            Canvas.SetLeft(Rect, CanvasX);
        }

        private bool WillCollide(int x, int y)
        {
            foreach(var obj in Handler.gameObjectLst)
            {
                if (!(obj is Wall))
                    continue;

                var wallRect = new Rect(obj.CanvasX, obj.CanvasY, obj.Width, obj.Height);
                var playerRect = new Rect(x, y, Width, Height);

                playerRect.Intersect(wallRect);

                if (!playerRect.IsEmpty)
                    return true;
            }

            return false;
        }
    }
}
