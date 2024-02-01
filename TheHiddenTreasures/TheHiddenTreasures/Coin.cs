using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheHiddenTreasures
{
    public class Coin : GameObject
    {
        public Coin(System.Drawing.Point point, int width, int height, ref Canvas gameCanvas, Handler handler)
            : base(point.X, point.Y, width, height, new SolidColorBrush(Colors.Green), ref gameCanvas, handler)
        { }
    }
}
