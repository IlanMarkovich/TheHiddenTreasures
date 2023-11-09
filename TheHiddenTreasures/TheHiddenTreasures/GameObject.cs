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
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Windows.UI.Xaml.Shapes.Rectangle Rect { get; set; }
        public double CanvasX { get; set; }
        public double CanvasY { get; set; }

        protected Canvas gameCanvas;

        public GameObject(Point point, int width, int height, Brush content, ref Canvas gameCanvas)
        {
            X = point.X;
            Y = point.Y;
            Width = width;
            Height = height;

            Rect = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = width,
                Height = height,
                Fill = content
            };

            this.gameCanvas = gameCanvas;
        }

        public GameObject(int canvasX, int canvasY, int width, int height, Brush content, ref Canvas gameCanvas)
            : this(new Point(0, 0), width, height, content, ref gameCanvas)
        {
            this.CanvasX = canvasX;
            this.CanvasY = canvasY;
        }
    }
}
