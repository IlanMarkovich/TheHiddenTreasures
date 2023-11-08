using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheHiddenTreasures
{
    internal class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 5;

        public Player(Point startPoint, int width, int height, ref Canvas gameCanvas) 
            : base(startPoint.X, startPoint.Y, width, height, new SolidColorBrush(Colors.Red), ref gameCanvas)
        {
            canvasX = Handler.gapSize + startPoint.X * (Handler.cellWidth + Handler.gapSize) +
                ((Handler.cellWidth - width) / 2);

            canvasY = Handler.gapSize + startPoint.Y * (Handler.cellHeight + Handler.gapSize) +
                ((Handler.cellHeight - height) / 2);

            Canvas.SetLeft(Rect, canvasX);
            Canvas.SetTop(Rect, canvasY);

            gameCanvas.Children.Add(Rect);
        }

        public void Move(VirtualKey key)
        {
            switch(key)
            {
                case VirtualKey.W:
                    canvasY -= MOVEMENT_SPEED;
                    Canvas.SetTop(Rect, canvasY);
                    break;
                case VirtualKey.A:
                    canvasX -= MOVEMENT_SPEED;
                    Canvas.SetLeft(Rect, canvasX);
                    break;
                case VirtualKey.S:
                    canvasY += MOVEMENT_SPEED;
                    Canvas.SetTop(Rect, canvasY);
                    break;
                case VirtualKey.D:
                    canvasX += MOVEMENT_SPEED;
                    Canvas.SetLeft(Rect, canvasX);
                    break;
            }
        }
    }
}
