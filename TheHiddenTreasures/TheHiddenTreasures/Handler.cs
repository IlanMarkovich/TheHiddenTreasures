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
            currLevel = new MazeLevel(10, 10);

            RenderMaze(10, 10);
        }

        public void RenderMaze(int width, int height)
        {
            // Render start point and end point
            RenderPoint(currLevel.GetStartPoint(), Colors.White);
            RenderPoint(currLevel.GetEndPoint(), Colors.Yellow);

            // Add gaps to the walls
            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    var rect = new Windows.UI.Xaml.Shapes.Rectangle
                    {
                        Width = GAP_SIZE,
                        Height = GAP_SIZE,
                        Fill = new SolidColorBrush(Colors.Blue)
                    };

                    Canvas.SetLeft(rect, x * CELL_WIDTH + (x - 1) * GAP_SIZE);
                    Canvas.SetTop(rect, y * CELL_HEIGHT + (y - 1) * GAP_SIZE);

                    gameCanvas.Children.Add(rect);
                }
            }

            Tile[,] layout = currLevel.GetLayout();

            for(int x = 0; x < layout.GetLength(0); x++)
            {
                for(int y = 0; y < layout.GetLength(1); y++)
                {
                    if (layout[x, y] == Tile.Path)
                    {
                        continue;
                    }

                    // Add vertical walls
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
                    // Add horizontal walls
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

        private void RenderPoint(Point p, Windows.UI.Color c)
        {
            int x = p.X * (CELL_WIDTH + GAP_SIZE),
                y = p.Y * (CELL_WIDTH + GAP_SIZE);

            var rect = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Fill = new SolidColorBrush(c)
            };

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            gameCanvas.Children.Add(rect);
        }
    }
}
