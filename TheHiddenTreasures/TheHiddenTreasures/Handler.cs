using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TheHiddenTreasures
{
    internal class Handler
    {
        private Canvas gameCanvas;
        private MazeLevel currLevel;

        private const int CELL_WIDTH = 35, CELL_HEIGHT = 35, GAP_SIZE = 7;

        public Handler(Canvas gameCanvas)
        {
            this.gameCanvas = gameCanvas;
            currLevel = new MazeLevel(30, 30);

            RenderMaze(30, 30);
        }

        public void RenderMaze(int width, int height)
        {
            width = width * CELL_WIDTH + (width - 1) * GAP_SIZE;
            height = height * CELL_HEIGHT + (height - 1) * GAP_SIZE;

            // Draw grid cells
            for(int x = 0; x < width; x += CELL_WIDTH + GAP_SIZE)
            {
                for(int y = 0; y < height; y += CELL_HEIGHT + GAP_SIZE)
                {
                    Point currPoint = new Point(x / (CELL_WIDTH + GAP_SIZE), y / (CELL_HEIGHT + GAP_SIZE));

                    var rect = new Windows.UI.Xaml.Shapes.Rectangle
                    {
                        Width = CELL_WIDTH,
                        Height = CELL_HEIGHT,
                        Fill = new SolidColorBrush(currPoint == currLevel.GetStartPoint() ? Colors.White : currPoint == currLevel.GetEndPoint() ? Colors.Yellow : Colors.Blue)
                    };

                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);

                    gameCanvas.Children.Add(rect);
                }
            }

            Tile[,] layout = currLevel.GetLayout();

            for(int x = 0; x < layout.GetLength(0); x++)
            {
                for(int y = 0; y < layout.GetLength(1); y++)
                {
                    if (layout[x, y] == Tile.Wall)
                    {
                        continue;
                    }

                    // Add vertical paths
                    if (y % 2 == 0)
                    {
                        var rect = new Windows.UI.Xaml.Shapes.Rectangle
                        {
                            Width = GAP_SIZE,
                            Height = CELL_HEIGHT,
                            Fill = new SolidColorBrush(Colors.Blue)
                        };

                        Canvas.SetLeft(rect, x * GAP_SIZE + (x + 1) * CELL_WIDTH);
                        Canvas.SetTop(rect, (y / 2) * CELL_HEIGHT + (y / 2) * GAP_SIZE);

                        gameCanvas.Children.Add(rect);
                    }
                    // Add horizontal paths
                    else
                    {
                        var rect = new Windows.UI.Xaml.Shapes.Rectangle
                        {
                            Width = CELL_WIDTH,
                            Height = GAP_SIZE,
                            Fill = new SolidColorBrush(Colors.Blue)
                        };

                        Canvas.SetLeft(rect, x * (CELL_WIDTH + GAP_SIZE));
                        Canvas.SetTop(rect, (y / 2) * GAP_SIZE + (y / 2 + 1) * CELL_HEIGHT);

                        gameCanvas.Children.Add(rect);
                    }
                }
            }
        }
    }
}
