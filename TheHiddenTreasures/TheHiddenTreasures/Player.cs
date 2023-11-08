using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheHiddenTreasures
{
    internal class Player : GameObject
    {
        private const int MOVEMENT_SPEED = 3;

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

        public void Move(VirtualKey key)
        {
            int newCanvasX = CanvasX, newCanvasY = CanvasY;

            switch(key)
            {
                case VirtualKey.W:
                    newCanvasY -= MOVEMENT_SPEED;
                    break;
                case VirtualKey.A:
                    newCanvasX -= MOVEMENT_SPEED;
                    break;
                case VirtualKey.S:
                    newCanvasY += MOVEMENT_SPEED;
                    break;
                case VirtualKey.D:
                    newCanvasX += MOVEMENT_SPEED;
                    break;
            }

            foreach(var obj in Handler.gameObjectLst)
            {
                if (!(obj is Wall))
                    continue;

                var wallRect = new Rect(obj.CanvasX, obj.CanvasY, obj.Width, obj.Height);
                wallRect.Intersect(new Rect(newCanvasX, newCanvasY, Width, Height));

                if(!wallRect.IsEmpty)
                {
                    newCanvasX = CanvasX;
                    newCanvasY = CanvasY;
                }
            }

            CanvasX = newCanvasX;
            CanvasY = newCanvasY;

            Canvas.SetLeft(Rect, CanvasX);
            Canvas.SetTop(Rect, CanvasY);
        }
    }
}
