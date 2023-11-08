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

        public static List<GameObject> gameObjectLst;
        private Player player;

        public Handler(Canvas gameCanvas)
        {
            this.gameCanvas = gameCanvas;
            currLevel = new MazeLevel(30, 30);
            gameObjectLst = new List<GameObject>();

            RenderMaze(30, 30);

            player = new Player(currLevel.GetStartPoint(), 10, 10, ref gameCanvas);
            gameObjectLst.Add(player);
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
            AddWall(-gapSize, -gapSize, width * cellWidth + (width + 1) * gapSize, gapSize);

            // Add the left wall
            AddWall(-gapSize, -gapSize, gapSize, height * cellHeight + (height + 1) * gapSize);

            // Add gaps to the walls
            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    AddWall(x * cellWidth + (x - 1) * gapSize, y * cellHeight + (y - 1) * gapSize, gapSize, gapSize);
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
                        AddWall(x * gapSize + (x + 1) * cellWidth, (y / 2) * cellHeight + (y / 2) * gapSize, gapSize, cellHeight);
                    }
                    // Add horizontal walls
                    else
                    {
                        AddWall(x * (cellWidth + gapSize), (y / 2) * gapSize + (y / 2 + 1) * cellHeight, cellWidth, gapSize);
                    }
                }
            }
        }

        private void AddWall(int x, int y, int width, int height)
        {
            Wall newWall = new Wall(gapSize + x, gapSize + y, width, height, ref gameCanvas);
            gameObjectLst.Add(newWall);
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

            Canvas.SetLeft(rect, gapSize + x);
            Canvas.SetTop(rect, gapSize + y);

            gameCanvas.Children.Add(rect);
        }
    }
}
