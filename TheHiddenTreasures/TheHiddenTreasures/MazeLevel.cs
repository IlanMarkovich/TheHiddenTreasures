using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Windows.Gaming.XboxLive.Storage;

namespace TheHiddenTreasures
{
    enum Tile
    {
        Wall,
        Path
    }

    class GridCell
    {
        public bool IsVisited { get; set; }
        public Queue<Point> NextPoints { get; set; }

        public GridCell()
        {
            IsVisited = false;
            NextPoints = new Queue<Point>();
        }
    }

    internal class MazeLevel
    {
        private Tile[,] layout;
        private Point startPoint;
        private Point endPoint;
        private int width, height;

        private bool didSetEndPoint;
        private List<Point> startEndPath;

        public MazeLevel(int width, int height)
        {
            this.width = width;
            this.height = height;

            layout = new Tile[width, 2 * height];

            // Set tile matrix
            for(int i = 0; i < layout.GetLength(0); i++)
                for(int j = 0; j < layout.GetLength(1); j++)
                    layout[i, j] = Tile.Wall;

            // Set random a start point
            Random rand = new Random();
            startPoint.X = rand.Next(rand.Next(0, width - 1));
            startPoint.Y = rand.Next(rand.Next(0, height - 1));

            didSetEndPoint = false;
            startEndPath = new List<Point>();
        }

        public Tile[,] GetLayout()
        {
            return layout;
        }

        public Point GetStartPoint()
        {
            return startPoint;
        }

        public Point GetEndPoint()
        {
            return endPoint;
        }

        public List<Point> GetStartEndPath()
        {
            return startEndPath;
        }

        public void GenerateMaze()
        {
            GridCell[,] grid = new GridCell[width, height];
            Stack<Point> posStack = new Stack<Point>();

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    grid[i, j] = new GridCell();

            BuildMazeGrid(startPoint, ref grid, ref posStack);
            CreateMazeLayout(startPoint, ref grid);
        }

        private void BuildMazeGrid(Point currPoint, ref GridCell[,] grid, ref Stack<Point> posStack, int count = 0)
        {
            int x = currPoint.X, y = currPoint.Y;
            grid[x, y].IsVisited = true;

            // u - up, r - right, d - down, l - left
            List<char> directions = new List<char>(new char[] { 'u', 'r', 'd', 'l' });

            // Remove unavaliable directions
            if (x == 0 || grid[x - 1, y].IsVisited)
                directions.Remove('l');

            if (x == width - 1 || grid[x + 1, y].IsVisited)
                directions.Remove('r');

            if (y == 0 || grid[x, y - 1].IsVisited)
                directions.Remove('u');

            if (y == height - 1 || grid[x, y + 1].IsVisited)
                directions.Remove('d');

            // If there are no cells to go to next
            if(directions.Count == 0)
            {
                // If not all the cells been visited yet, go back and visit them
                if (!AreAllVisited(grid))
                {
                    if (!didSetEndPoint)
                        startEndPath.RemoveAt(startEndPath.Count - 1);

                    BuildMazeGrid(posStack.Pop(), ref grid, ref posStack, count);
                }
                
                // If didn't already set an end point, and the current point is far enough from the start point, set this point as the end point
                if(!didSetEndPoint && count >= grid.GetLength(0) * 3)
                {
                    didSetEndPoint = true;
                    endPoint = currPoint;
                }

                return;
            }

            // Figure out what point is the next point, based on the direction choosen
            Random rand = new Random();
            char randDir = directions[rand.Next(0, directions.Count)];
            Point newPoint;

            switch(randDir)
            {
                case 'u':
                    newPoint = new Point(x, y - 1);
                    break;
                case 'r':
                    newPoint = new Point(x + 1, y);
                    break;
                case 'd':
                    newPoint = new Point(x, y + 1);
                    break;
                case 'l':
                    newPoint = new Point(x - 1, y);
                    break;
            }

            if(!didSetEndPoint)
                startEndPath.Add(newPoint);

            // Set this point as a next point (for the layout algorithem to know where to put paths)
            grid[x, y].NextPoints.Enqueue(newPoint);

            // Save this point in the "history stack"
            posStack.Push(currPoint);

            BuildMazeGrid(newPoint, ref grid, ref posStack, count + 1);
        }

        private bool AreAllVisited(GridCell[,] grid)
        {
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!grid[i, j].IsVisited)
                        return false;
                }
            }

            return true;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private void CreateMazeLayout(Point currPoint, ref GridCell[,] grid)
        {
            if (grid[currPoint.X, currPoint.Y].NextPoints.Count == 0)
                return;

            Point nextPoint = grid[currPoint.X, currPoint.Y].NextPoints.Dequeue();

            // Change the wall to a path in the connection between the grid cells
            if (currPoint.X == nextPoint.X)
                layout[currPoint.X, 2 * Math.Max(currPoint.Y, nextPoint.Y) - 1] = Tile.Path;
            else
                layout[Math.Min(currPoint.X, nextPoint.X), 2 * currPoint.Y] = Tile.Path;

            // Continue in the "path" of the next point
            CreateMazeLayout(nextPoint, ref grid);

            // Take care of the other next points of this current point, if there are any
            while (grid[currPoint.X, currPoint.Y].NextPoints.Count > 0)
            {
                CreateMazeLayout(currPoint, ref grid);
            }
        }
    }
}
