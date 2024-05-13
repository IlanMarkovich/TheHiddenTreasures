using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TheHiddenTreasures
{
    public abstract class RenderObject
    {
        public Rectangle Rect { get; set; }

        protected Canvas gameCanvas;
        // C'tor for when not providing cooridnates and just creating the rectangle
        public RenderObject(int width, int height, Brush content, ref Canvas gameCanvas)
        {
            Rect = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = content
            };

            this.gameCanvas = gameCanvas;
        }

        // C'tor for when providing coordinates and adding the rectangle to the canvas
        public RenderObject(int x, int y, int width, int height, Brush content, ref Canvas gameCanvas)
            : this(width, height, content, ref gameCanvas)
        {
            Canvas.SetLeft(Rect, x);
            Canvas.SetTop(Rect, y);

            gameCanvas.Children.Add(Rect);
        }
    }
}
