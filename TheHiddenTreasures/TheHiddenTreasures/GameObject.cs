using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TheHiddenTreasures
{
    public abstract class GameObject : RenderObject
    {
        // Those coordinates are reffering the game coordinates
        // which are the grid cells of the maze (ex. top left corner cell is 0,0)
        public int X { get; set; }
        public int Y { get; set; }

        public GameObject(int x, int y, int width, int height, Brush content, ref Canvas gameCanvas)
            : base(width, height, content, ref gameCanvas)
        {
            X = x;
            Y = y;
        }
    }
}
