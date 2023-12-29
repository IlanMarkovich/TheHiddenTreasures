using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheHiddenTreasures
{
    internal class Trap : GameObject
    {
        public Trap(System.Drawing.Point point, int width, int height, ref Canvas gameCanvas, Handler handler)
            : base(point.X, point.Y, width, height, new SolidColorBrush(Colors.Orange), ref gameCanvas, handler)
        {
            Canvas.SetLeft(Rect, Handler.GAP_SIZE + X * (Handler.CELL_WIDTH + Handler.GAP_SIZE) +
            ((Handler.CELL_WIDTH - width) / 2));

            Canvas.SetTop(Rect, Handler.GAP_SIZE + Y * (Handler.CELL_HEIGHT + Handler.GAP_SIZE) +
                ((Handler.CELL_HEIGHT - height) / 2));

            gameCanvas.Children.Add(Rect);
        }
    }
}
