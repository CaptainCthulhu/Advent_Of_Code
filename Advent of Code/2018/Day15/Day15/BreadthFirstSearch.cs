using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Day15
{
    class BreadthFirstSearch
    {       

        static public List<Point> GetAdjacent(Point location, char[,] board)
        {
            List<Point> items = new List<Point>();
            items.Add(new Point(location.X, location.Y - 1));
            items.Add(new Point(location.X - 1, location.Y));
            items.Add(new Point(location.X + 1, location.Y));
            items.Add(new Point(location.X, location.Y + 1));

            items.RemoveAll(x => board[location.Y, location.X] != '.');

            return items;
        }

        public static int ShortestPathFunction(char[,] board, Point start, Point goal)
        {
            var previous = new Dictionary<Point, Point>();

            var queue = new Queue<Point>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                foreach (var neighbor in GetAdjacent(point, board))
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = point;
                    queue.Enqueue(neighbor);
                }
            }

            List<Point> path = new List<Point> { };

            Point current = goal;
            while (!current.Equals(start))
            {
              path.Add(current);
                if (previous.ContainsKey(current))                
                    current = previous[current];                
                else
                    return int.MaxValue;
            };

            path.Add(start);
            path.Reverse();
            return path.Count();
            
        }
    }
}
