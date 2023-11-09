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
    internal class Wall : GameObject
    {
        public Wall(int canvasX, int canvasY, int width, int height, ref Canvas gameCanvas) :
            base(canvasX, canvasY, width, height, new SolidColorBrush(Colors.Blue), ref gameCanvas)
        {
        }
    }
}
