using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace Day15
{
    class Program
    {

        static List<Player> _players = new List<Player>();
        static char[,] _board;
        static int sleepTime = 0;
        static string[] board = Inputs.real;        
        static Boolean unitDetails = false;
        static int ElfPower = 3;
        static int ElfCount;
        static bool ElvesGood = true;
        static int turn = 0;

        static void Main(string[] args)
        {
            GetElves();
            Console.ReadKey();
        }


        static void GetElves()
        {
            bool ElfVictory = false;
            while (!ElfVictory)
            {
                ElfPower++;
                Console.WriteLine(String.Format("Elf Power {0}", ElfPower));
                ElfVictory = Run();
                CheckElvesGood();
            }

            ResetPlayers();
            DrawBoard(turn);
            int hitPointsLeft = _players.Sum(x => x._hitpoints);
            Console.WriteLine(String.Format("Done.  Turns: {0} HpLeft: {1}", turn, hitPointsLeft));
            Console.WriteLine(String.Format("Score {0}", turn * hitPointsLeft));
        }        

        static bool Run()
        {
            bool Continue = true;
            CreateBoard();
            turn = 0;
            while (Continue)
            {                
                Continue = Go(turn);
                if (!Continue)
                    break;                
                CheckElvesGood();
                if (!ElvesGood)
                {
                    Console.WriteLine("Elves died!");
                    return false;
                }

                Continue = BothSidesRemain();
                int hitPointsLeft = _players.Sum(x => x._hitpoints);
                turn++;
            }
            CheckElvesGood();
            if (!ElvesGood)
                return false;

            return true;
        }

        public static Boolean BothSidesRemain()
        {
            var elves = (from player in _players
                    where player._type == 'E' && player._hitpoints > 0
                    select player).ToList<Player>();

            var goblins = (from player in _players
                      where player._type == 'G' && player._hitpoints > 0
                           select player).ToList<Player>();

            return goblins.Count() > 0 && elves.Count() > 0;
        }

        static void CreateBoard()
        {
            turn = 0;
            ElfCount = 0;
            _players = new List<Player>();
            _board = new char[board[0].Length, board.Length];
            for (int y = 0; y < _board.GetLength(0); y++)
            {
                for (int x = 0; x < _board.GetLength(1); x++)
                {
                    _board[y, x] = board[y][x];
                    if (_board[y, x] == 'E')
                    {
                        ElfCount++;
                        _players.Add(new Player(new Point(x, y), _board[y, x], ElfPower));
                    }
                    else if  (_board[y, x] == 'G')
                        _players.Add(new Player(new Point(x, y), _board[y, x]));
                }
            }
            SortPlayers();
        }

        private static void CheckElvesGood()
        {
            var elves = (from player in _players
                         where player._type == 'E' && player._hitpoints > 0
                         select player).ToList<Player>();

            ElvesGood = elves.Count() == ElfCount;
        }

        static void DrawBoard(int turn)
        {            
                Console.Clear();
                Console.WriteLine(String.Format("Elf power {0}. Turn {1}", ElfPower, turn));
                for (int y = 0; y < _board.GetLength(0); y++)
                {
                    for (int x = 0; x < _board.GetLength(1); x++)
                    {
                        Console.Write(_board[y, x]);
                    }
                    Console.Write('\n');
                }

                if (unitDetails)
                { 
                    foreach (Player player in _players)
                    {
                        Console.WriteLine(String.Format("{0}({1})", player._type, player._hitpoints));
                    }
                }
                Thread.Sleep(sleepTime);            
        }

        public static void SortPlayers()
        {
            RemoveDead();
            _players = _players.OrderBy(o => o._location.Y).ThenBy(o => o._location.X).ToList<Player>();           
        }

        public static void RemoveDead()
        {
            List<Player> removeList = (from player in _players where player._hitpoints < 1 select player).ToList<Player>();
            foreach (Player i in removeList)            
                i.RemovePlayer(_board, _players);            
        }

        static void ResetPlayers()
        {
            foreach (Player p in _players)            
                p._gone = false;

            RemoveDead();
        }

        static Boolean HasUnDonePlayers()
        {
            List<Player> players = (from player in _players
                           where player._gone == false
                           select player).ToList<Player>();
            SortPlayers();
            if (players.Count() > 0)
                return true;

            return false;
        }

        static Player GetNextPlayer()
        {
            SortPlayers();
            List<Player> players = (from player in _players
                                    where player._gone == false
                                    select player).ToList<Player>();            
            return players.First();
        }

        static Boolean Go(int turn)
        {           
            SortPlayers();
            //DrawBoard(turn);            
            while (HasUnDonePlayers())
            {
                SortPlayers();
                Player p = GetNextPlayer();
                if (p._gone == false)
                {
                    if (!p.Go(_board, _players))
                        return false;
                    CheckElvesGood();                    
                    SortPlayers();
                }                
            }
            ResetPlayers();
            return true;
        }
    }
}
