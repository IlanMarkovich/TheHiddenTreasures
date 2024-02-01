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

        protected Handler handler;

        public GameObject(int x, int y, int width, int height, Brush content, ref Canvas gameCanvas, Handler handler)
            : base(width, height, content, ref gameCanvas)
        {
            X = x;
            Y = y;

            this.handler = handler;

            Canvas.SetLeft(Rect, Handler.GAP_SIZE + X * (Handler.CELL_WIDTH + Handler.GAP_SIZE) +
                ((Handler.CELL_WIDTH - width) / 2));

            Canvas.SetTop(Rect, Handler.GAP_SIZE + Y * (Handler.CELL_HEIGHT + Handler.GAP_SIZE) +
                ((Handler.CELL_HEIGHT - height) / 2));

            gameCanvas.Children.Add(Rect);
        }
    }
}
