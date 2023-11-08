using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TheHiddenTreasures
{
    public abstract class GameObject
    {
        protected int X { get; set; }
        protected int Y { get; set; }
        protected int Width { get; set; }
        protected int Height { get; set; }
        public Rectangle Rect { get; set; }

        protected int canvasX, canvasY;

        protected Canvas gameCanvas;

        public GameObject(int x, int y, int width, int height, Brush content, ref Canvas gameCanvas)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            Rect = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = content
            };

            this.gameCanvas = gameCanvas;
        }
    }
}
