using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Tile[][] layout;
        private Point startPoint;
        private Point endPoint;

        private int width, height;

        public MazeLevel(int width, int height)
        {
            this.width = width;
            this.height = height;

            // Set tile matrix
            for(int i = 0; i < height; i++)
            {
                int arrLen = i % 2 == 0 ? width - 1 : width;
                layout[i] = new Tile[arrLen];

                for(int j = 0; j < arrLen; j++)
                {
                    layout[i][j] = Tile.Wall;
                }
            }

            // Set random a start point
            Random rand = new Random();
            startPoint.X = rand.Next(rand.Next(0, width - 1));
            startPoint.Y = rand.Next(rand.Next(0, height - 1));
        }

        private void GenerateMaze()
        {
            GridCell[,] grid = new GridCell[width, height];
            Stack<Point> posStack = new Stack<Point>();

            GenerateMaze(startPoint, ref grid, ref posStack);
        }

        private void GenerateMaze(Point currPoint, ref GridCell[,] grid, ref Stack<Point> posStack)
        {
            int x = currPoint.X, y = currPoint.Y;
            grid[x, y].IsVisited = true;

            // u - up, r - right, d - down, l - left
            List<char> directions = new List<char>(new char[] { 'u', 'r', 'd', 'l' });

            // Remove unavaliable directions
            if (x == 0 || grid[x - 1, y].IsVisited)
                directions.Remove('l');
            else if (x == width - 1 || grid[x + 1, y].IsVisited)
                directions.Remove('r');

            if (y == 0 || grid[x, y - 1].IsVisited)
                directions.Remove('u');
            else if (y == height - 1 || grid[x, y + 1].IsVisited)
                directions.Remove('d');

            if(directions.Count == 0)
            {
                if (!areAllVisited(grid))
                    GenerateMaze(posStack.Pop(), ref grid, ref posStack);
                else
                    endPoint = currPoint;

                return;
            }

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

            grid[x, y].NextPoints.Enqueue(newPoint);
            posStack.Push(currPoint);

            GenerateMaze(newPoint, ref grid, ref posStack);
        }

        private bool areAllVisited(GridCell[,] grid)
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
    }
}
