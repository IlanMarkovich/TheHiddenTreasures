using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTreasures
{
    internal class Wall : RenderObject
    {
        public Wall(int x, int y, int width, int height, ref Canvas gameCanvas) :
            base(x, y, width, height, GetImage(width, height), ref gameCanvas)
        {
        }

        public static ImageBrush GetImage(int width, int height)
        {
            var img = new BitmapImage();

            if (width == height)
                img.UriSource = new Uri("ms-appx:/Assets/wall/Gap.png");
            else if (width > height)
                img.UriSource = new Uri("ms-appx:/Assets/wall/Horizontal.png");
            else
                img.UriSource = new Uri("ms-appx:/Assets/wall/Vertical.png");

            var brush = new ImageBrush();
            brush.ImageSource = img;
            return brush;
        }
    }
}
