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
        private const int CELL_WIDTH = 35, CELL_HEIGHT = 35, GAP_SIZE = 7;

        private Canvas gameCanvas;
        private MazeLevel currLevel;

        private List<Windows.UI.Xaml.Shapes.Rectangle> gameRectLst;

        public Handler(Canvas gameCanvas)
        {
            this.gameCanvas = gameCanvas;
            currLevel = new MazeLevel(30, 30);

            gameRectLst = new List<Windows.UI.Xaml.Shapes.Rectangle>();

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

            AddRectanlge(topWall, -GAP_SIZE, -GAP_SIZE);

            // Add the left wall
            var leftWall = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = GAP_SIZE,
                Height = height * CELL_HEIGHT + (height + 1) * GAP_SIZE,
                Fill = new SolidColorBrush(Colors.Blue)
            };

            AddRectanlge(leftWall, -GAP_SIZE, -GAP_SIZE);

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

                    AddRectanlge(rect, x * CELL_WIDTH + (x - 1) * GAP_SIZE, y * CELL_HEIGHT + (y - 1) * GAP_SIZE);
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

                        AddRectanlge(rect, x * GAP_SIZE + (x + 1) * CELL_WIDTH, (y / 2) * CELL_HEIGHT + (y / 2) * GAP_SIZE);
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

                        AddRectanlge(rect, x * (CELL_WIDTH + GAP_SIZE), (y / 2) * GAP_SIZE + (y / 2 + 1) * CELL_HEIGHT);
                    }
                }
            }
        }

        private void AddRectanlge(Windows.UI.Xaml.Shapes.Rectangle rect, int x, int y)
        {
            Canvas.SetLeft(rect, GAP_SIZE + x);
            Canvas.SetTop(rect, GAP_SIZE + y);

            gameCanvas.Children.Add(rect);
            gameRectLst.Add(rect);
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

            AddRectanlge(rect, x, y);
        }
    }
}
