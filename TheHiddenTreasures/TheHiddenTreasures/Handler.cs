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
            // Render start point and end point
            RenderPoint(currLevel.GetStartPoint(), Colors.White);
            RenderPoint(currLevel.GetEndPoint(), Colors.Yellow);

            // Add the top wall
            var topWall = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = width * CELL_WIDTH + (width + 1) * GAP_SIZE,
                Height = GAP_SIZE,
                Fill = new SolidColorBrush(Colors.Blue)
            };

            Canvas.SetTop(topWall, 0);
            Canvas.SetLeft(topWall, 0);
            gameCanvas.Children.Add(topWall);

            // Add the left wall
            var leftWall = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = GAP_SIZE,
                Height = height * CELL_HEIGHT + (height + 1) * GAP_SIZE,
                Fill = new SolidColorBrush(Colors.Blue)
            };

            Canvas.SetTop(leftWall, 0);
            Canvas.SetLeft(leftWall, 0);
            gameCanvas.Children.Add(leftWall);

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

                    Canvas.SetLeft(rect, GAP_SIZE + x * CELL_WIDTH + (x - 1) * GAP_SIZE);
                    Canvas.SetTop(rect, GAP_SIZE + y * CELL_HEIGHT + (y - 1) * GAP_SIZE);

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

                        Canvas.SetLeft(rect, GAP_SIZE + x * GAP_SIZE + (x + 1) * CELL_WIDTH);
                        Canvas.SetTop(rect, GAP_SIZE + (y / 2) * CELL_HEIGHT + (y / 2) * GAP_SIZE);

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

                        Canvas.SetLeft(rect, GAP_SIZE + x * (CELL_WIDTH + GAP_SIZE));
                        Canvas.SetTop(rect, GAP_SIZE + (y / 2) * GAP_SIZE + (y / 2 + 1) * CELL_HEIGHT);

                        gameCanvas.Children.Add(rect);
                    }
                }
            }
        }

        private void RenderPoint(Point p, Windows.UI.Color c)
        {
            int x = GAP_SIZE + p.X * (CELL_WIDTH + GAP_SIZE),
                y = GAP_SIZE + p.Y * (CELL_WIDTH + GAP_SIZE);

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
