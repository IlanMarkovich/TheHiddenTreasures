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
        public Windows.UI.Xaml.Shapes.Rectangle Rect { get; set; }

        protected Canvas gameCanvas;

        // C'tor for when game coordinates are known
        // but the canvas coordinates aren't known and need to be calculated in the children
        public GameObject(Point point, int width, int height, Brush content, ref Canvas gameCanvas)
        {
            X = point.X;
            Y = point.Y;

            Rect = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = width,
                Height = height,
                Fill = content
            };

            this.gameCanvas = gameCanvas;
        }

        // C'tor for game object who don't need game coordinates (puts zero in 'X' and 'Y')
        // and does know its canvas coordinates
        public GameObject(int canvasX, int canvasY, int width, int height, Brush content, ref Canvas gameCanvas)
            : this(new Point(0, 0), width, height, content, ref gameCanvas)
        {
            Canvas.SetLeft(Rect, canvasX);
            Canvas.SetTop(Rect, canvasY);

            gameCanvas.Children.Add(Rect);
        }
    }
}
