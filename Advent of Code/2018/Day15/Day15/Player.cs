using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Day15
{
    struct MoveChoice
    {
        public int distance;
        public Point move;

        public MoveChoice(int m, Point p)
        {
            distance = m;
            move = p;
        }
    }

    class Player
    {
        const int maxLength = 64;
        public char _type;
        public Point _location;
        public int _hitpoints = 200;
        int attackPower;
        public Boolean _gone = false;


        public Player(Point location, char type, int Power = 3)
        {
            this._location = location;
            this._type = type;
            attackPower = (type == 'G') ? 3 : Power; 

        }

        public void Hit(int attackPower)
        {
            _hitpoints -= attackPower;
        }

        Boolean CheckPossible(Point possiblePoint, char[,] board, List<Point> travelled)
        {
            char character = board[possiblePoint.Y, possiblePoint.X];
            if (!travelled.Contains(possiblePoint) && character == '.')
                return true;
            return false;
        }

        public int Traverse(Point location, Point goal, char[,] board)
        {
            return BreadthFirstSearch.ShortestPathFunction(board, location, goal);        
        }

        public MoveChoice RouteToPoint(Point location, Point goal, char[,] board, int MaxLength)
        {
            MoveChoice bestChoice = new MoveChoice(int.MaxValue, new Point(int.MaxValue, int.MaxValue));

            int result = 0;

            Point newMove = new Point(location.X, location.Y - 1);
            if (CheckPossible(newMove, board, new List<Point>()))
            {
                result = Traverse(newMove, goal, board);
                if (result != -1 && result < bestChoice.distance)
                {
                    bestChoice.distance = result;
                    bestChoice.move = newMove;
                }
            }
            newMove = new Point(location.X - 1, location.Y);
            if (CheckPossible(newMove, board, new List<Point>()))
            {
                result = Traverse(newMove, goal, board);
                if (result != -1 && result < bestChoice.distance)
                {
                    bestChoice.distance = result;
                    bestChoice.move = newMove;
                }
            }
            newMove = new Point(location.X + 1, location.Y);
            if (CheckPossible(newMove, board, new List<Point>()))
            {
                result = Traverse(newMove, goal, board);
                if (result != -1 && result < bestChoice.distance)
                {
                    bestChoice.distance = result;
                    bestChoice.move = newMove;
                }
            }
            newMove = new Point(location.X, location.Y + 1);
            if (CheckPossible(newMove, board, new List<Point>()))
            {
                result = Traverse(newMove, goal, board);
                if (result != -1 && result < bestChoice.distance)
                {
                    bestChoice.distance = result;
                    bestChoice.move = newMove;
                }
            }

            return bestChoice;
        }

        public Point FindMove(List<Player> players, char[,] board)
        {
            List<Point> attackPoints = new List<Point>();
            Point bestAttackPoint = new Point(-1, -1);
            int bestLength = maxLength;

            foreach (Player p in players)
            {
                if (p._type != _type)
                {
                    int y = p._location.Y;
                    int x = p._location.X;
                    if (p._hitpoints > 0)
                    {
                        if (board[y - 1, x] == '.')
                            attackPoints.Add(new Point(x, y - 1));
                        if (board[y, x - 1] == '.')
                            attackPoints.Add(new Point(x - 1, y));
                        if (board[y, x + 1] == '.')
                            attackPoints.Add(new Point(x + 1, y));
                        if (board[y + 1, x] == '.')
                            attackPoints.Add(new Point(x, y + 1));
                    }
                }
            }
            attackPoints = attackPoints.OrderBy(x => x.Y).ThenBy(x => x.X).ToList<Point>();

            foreach (Point p in attackPoints)
            {
                MoveChoice m = RouteToPoint(_location, p, board, bestLength);
                if (m.distance < bestLength)
                {
                    bestAttackPoint = m.move;
                    bestLength = m.distance;
                }
            }

            return bestAttackPoint;
        }

        public Player FindPlayer(List<Player> players, Point location)
        {
            var victims = from player in players
                          where player._location == location && player._type != _type && player._hitpoints > 0
                          select player;
            if (victims.Count() == 1)
                return victims.First();

            return null;
        }


        public Boolean Attack(List<Player> players, char[,] board)
        {
            Program.SortPlayers();

            int y = _location.Y;
            int x = _location.X;

            List<Player> victims = new List<Player>();
            victims.Add(FindPlayer(players, new Point(x, y - 1)));
            victims.Add(FindPlayer(players, new Point(x - 1, y)));
            victims.Add(FindPlayer(players, new Point(x + 1, y)));
            victims.Add(FindPlayer(players, new Point(x, y + 1)));

            victims.RemoveAll(i => i == null || i._hitpoints <= 0);            
            if (victims.Count > 0)
            {
                Player victim = victims.OrderBy(i => i._hitpoints).ThenBy(i => i._location.Y).ThenBy(i => i._location.X).First();
                if (victim != null)
                {
                    victim.Hit(attackPower);
                    return true;
                }
            }
            return false;
        }

        public void RemovePlayer(char[,] board, List<Player> players)
        {
            if (this._hitpoints <= 0)
            {
                board[_location.Y, _location.X] = '.';
                players.Remove(this);
            }
        }

        public Boolean CheckTargets(List<Player> players)
        {
            var victims = from player in players
                          where player._type != _type && player._hitpoints > 0
                          select player;
            if (victims.Count() > 0)
                return true;
            return false;
        }

        public Boolean Go(char[,] board, List<Player> players)
        {
            _gone = true;
            if (this._hitpoints <= 0)
            {
                RemovePlayer(board, players);
                Program.SortPlayers();
            }
            else
            {
                if (!CheckTargets(players))
                {
                    return false;
                }

                if (!Attack(players, board))
                {
                    Point newLocation = FindMove(players, board);
                    if (newLocation.X != -1 && newLocation.Y != -1)
                    {
                        board[_location.Y, _location.X] = '.';
                        this._location = newLocation;
                        board[newLocation.Y, newLocation.X] = _type;                        
                        Attack(players, board);
                    }
                }
            }

            return true;
        }

    }
}
