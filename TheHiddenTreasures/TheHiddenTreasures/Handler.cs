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
    public class Handler
    {
        public const int CELL_WIDTH = 100, CELL_HEIGHT = 100, GAP_SIZE = 20;
        public const int ZOOM_LEVEL = 250;
        public const double DEFAULT_VISIBILITY = 500, MAX_OPACITY = 0.75;

        public List<RenderObject> RenderObjectLst { get; set; }

        private Canvas gameCanvas;
        private PlaneProjection gameCamera;
        private TextBlock X_tb, Y_tb;

        private MazeLevel currLevel;
        private Player player;

        private int visibilityRadius;

        public Handler(ref Canvas gameCanvas, ref PlaneProjection gameCamera, ref TextBlock X_tb, ref TextBlock Y_tb)
        {
            this.gameCanvas = gameCanvas;
            this.gameCamera = gameCamera;
            this.X_tb = X_tb;
            this.Y_tb = Y_tb;

            visibilityRadius = (int)DEFAULT_VISIBILITY;

            currLevel = new MazeLevel(30, 30);
            RenderObjectLst = new List<RenderObject>();
            player = new Player(currLevel.GetStartPoint(), 25, 25, ref gameCanvas, this);

            currLevel.GenerateMaze();
            RenderMaze(30, 30);

            UpdateOnPlayerMove();
        }

        public Player GetPlayer()
        {
            return player;
        }

        public void RenderMaze(int width, int height)
        {
            // Render end point
            RenderCell(currLevel.GetEndPoint(), Colors.Yellow);


            // Add the top and left walls
            for (int i = 0; i < width; i++)
            {
                AddWall(i * CELL_WIDTH + i * GAP_SIZE, -GAP_SIZE, CELL_WIDTH, GAP_SIZE);
                AddWall(-GAP_SIZE, i * CELL_HEIGHT + i * GAP_SIZE, GAP_SIZE, CELL_HEIGHT);
            }

            // Add gaps to the walls
            for (int x = 0; x <= width; x++)
            {
                for (int y = 0; y <= height; y++)
                {
                    AddWall(x * CELL_WIDTH + (x - 1) * GAP_SIZE, y * CELL_HEIGHT + (y - 1) * GAP_SIZE, GAP_SIZE, GAP_SIZE);
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
                        AddWall(x * GAP_SIZE + (x + 1) * CELL_WIDTH, (y / 2) * CELL_HEIGHT + (y / 2) * GAP_SIZE, GAP_SIZE, CELL_HEIGHT);
                    }
                    // Add horizontal walls
                    else
                    {
                        AddWall(x * (CELL_WIDTH + GAP_SIZE), (y / 2) * GAP_SIZE + (y / 2 + 1) * CELL_HEIGHT, CELL_WIDTH, GAP_SIZE);
                    }
                }
            }
        }

        public void UpdateOnPlayerMove()
        {
            FocusOnPlayer();
            UpdateCoordinates();
            UpdateVisibility();
        }

        private void FocusOnPlayer()
        {
            if (!Game.isCameraOn)
                return;

            double canvasCenterX = gameCanvas.ActualWidth / 2;
            double canvasCenterY = gameCanvas.ActualHeight / 2;

            double playerCenterX = Canvas.GetLeft(player.Rect) + player.Rect.Width / 2;
            double playerCenterY = Canvas.GetTop(player.Rect) + player.Rect.Height / 2;

            gameCamera.GlobalOffsetX = canvasCenterX - playerCenterX;
            gameCamera.GlobalOffsetY = canvasCenterY - playerCenterY;
            gameCamera.GlobalOffsetZ = ZOOM_LEVEL;
        }

        private void UpdateCoordinates()
        {
            X_tb.Text = $"X: {player.X}";
            Y_tb.Text = $"Y: {player.Y}";
        }

        private void UpdateVisibility()
        {
            if (!Game.isVisibilityOn)
                return;

            foreach(var obj in RenderObjectLst)
            {
                // Distance between the object and the player
                double distance = Math.Sqrt(Math.Pow(Canvas.GetLeft(player.Rect) - Canvas.GetLeft(obj.Rect), 2) + Math.Pow(Canvas.GetTop(player.Rect) - Canvas.GetTop(obj.Rect), 2));

                // Formula for calculating the visibility (opacity) based on the distance
                double visibility = (visibilityRadius / 25) / distance;

                // If the distance is greater than the visibility radius, the opacity will be zero
                // and if the visibility exceeds the maximum opacity, set it to the maximum opacity 
                obj.Rect.Opacity = distance > visibilityRadius ? 0 : visibility > MAX_OPACITY ? MAX_OPACITY : visibility;
            }
        }

        private void AddWall(int x, int y, int width, int height)
        {
            RenderObjectLst.Add(new Wall(GAP_SIZE + x, GAP_SIZE + y, width, height, ref gameCanvas));
        }

        // TODO remove temp method
        private void RenderCell(Point p, Windows.UI.Color c)
        {
            int x = p.X * (CELL_WIDTH + GAP_SIZE),
                y = p.Y * (CELL_WIDTH + GAP_SIZE);

            var rect = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Fill = new SolidColorBrush(c)
            };

            Canvas.SetLeft(rect, GAP_SIZE + x);
            Canvas.SetTop(rect, GAP_SIZE + y);

            gameCanvas.Children.Add(rect);
        }
    }
}
