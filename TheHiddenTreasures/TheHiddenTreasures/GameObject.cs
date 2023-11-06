using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected Rectangle rect;

        public GameObject(int x, int y, int width, int height, Brush content)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            rect = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = content
            };
        }
    }
}
