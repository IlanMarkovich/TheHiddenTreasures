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
    internal class Wall : RenderObject
    {
        public Wall(int x, int y, int width, int height, ref Canvas gameCanvas) :
            base(x, y, width, height, new SolidColorBrush(Colors.Blue), ref gameCanvas)
        {
        }
    }
}
