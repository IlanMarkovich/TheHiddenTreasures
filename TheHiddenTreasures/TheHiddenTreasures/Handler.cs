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
        public static int cellWidth = 20, cellHeight = 20, gapSize = 4;

        private Canvas gameCanvas;
        private MazeLevel currLevel;

        private List<Windows.UI.Xaml.Shapes.Rectangle> wallsLst;
        private List<Windows.UI.Xaml.Shapes.Rectangle> gameRects;
        private Player player;

        public Handler(Canvas gameCanvas)
        {
            this.gameCanvas = gameCanvas;
            currLevel = new MazeLevel(30, 30);
            wallsLst = new List<Windows.UI.Xaml.Shapes.Rectangle>();

            RenderMaze(30, 30);

            player = new Player(currLevel.GetStartPoint(), 15, 15, ref gameCanvas);

            gameRects = new List<Windows.UI.Xaml.Shapes.Rectangle>();
            gameRects.AddRange(wallsLst);
            gameRects.Add(player.Rect);
        }

        public Player GetPlayer()
        {
            return player;
        }

        public void RenderMaze(int width, int height)
        {
            // Render start point and end point
            RenderCell(currLevel.GetStartPoint(), Colors.White);
            RenderCell(currLevel.GetEndPoint(), Colors.Yellow);

            // Add the top wall
            var topWall = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = width * cellWidth + (width + 1) * gapSize,
                Height = gapSize,
                Fill = new SolidColorBrush(Colors.Blue)
            };

            AddWall(topWall, -gapSize, -gapSize);

            // Add the left wall
            var leftWall = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = gapSize,
                Height = height * cellHeight + (height + 1) * gapSize,
                Fill = new SolidColorBrush(Colors.Blue)
            };

            AddWall(leftWall, -gapSize, -gapSize);

            // Add gaps to the walls
            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    var rect = new Windows.UI.Xaml.Shapes.Rectangle
                    {
                        Width = gapSize,
                        Height = gapSize,
                        Fill = new SolidColorBrush(Colors.Blue)
                    };

                    AddWall(rect, x * cellWidth + (x - 1) * gapSize, y * cellHeight + (y - 1) * gapSize);
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
                            Width = gapSize,
                            Height = cellHeight,
                            Fill = new SolidColorBrush(Colors.Blue)
                        };

                        AddWall(rect, x * gapSize + (x + 1) * cellWidth, (y / 2) * cellHeight + (y / 2) * gapSize);
                    }
                    // Add horizontal walls
                    else
                    {
                        var rect = new Windows.UI.Xaml.Shapes.Rectangle
                        {
                            Width = cellWidth,
                            Height = gapSize,
                            Fill = new SolidColorBrush(Colors.Blue)
                        };

                        AddWall(rect, x * (cellWidth + gapSize), (y / 2) * gapSize + (y / 2 + 1) * cellHeight);
                    }
                }
            }
        }

        private void AddWall(Windows.UI.Xaml.Shapes.Rectangle rect, int x, int y)
        {
            Canvas.SetLeft(rect, gapSize + x);
            Canvas.SetTop(rect, gapSize + y);

            gameCanvas.Children.Add(rect);
            wallsLst.Add(rect);
        }

        private void RenderCell(Point p, Windows.UI.Color c)
        {
            int x = p.X * (cellWidth + gapSize),
                y = p.Y * (cellWidth + gapSize);

            var rect = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = cellWidth,
                Height = cellHeight,
                Fill = new SolidColorBrush(c)
            };

            AddWall(rect, x, y);
        }
    }
}
