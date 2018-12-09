using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem9
{
    class Program
    {
        
        static long[] playerScores = new long[459];        
        static int turn = 0;
        static int goal = 7210300;

        static void Main(string[] args)
        {
            PlayGame();
            Console.WriteLine(playerScores.Max());
            Console.ReadKey();
        }


        private static void PlayGame()
        {
            //Initial Marble
            Marble marbel = new Marble(0);
            marbel.SetLeftRight(marbel, marbel);
            Marble currentMarbel = marbel;
            
            while (turn < goal)
            {
                turn++;
                if (turn % 23 == 0)
                {                    
                    playerScores[turn % playerScores.Count()] += turn;
                    for (int i = 0; i < 7; i++)
                    {
                        currentMarbel = Marble.GoLeft(currentMarbel);                          
                    }
                    playerScores[turn % playerScores.Count()] += currentMarbel.Value;
                    currentMarbel = Marble.Remove(currentMarbel);
                }
                else
                {                    
                    currentMarbel = Marble.GoRight(currentMarbel);                                              
                    currentMarbel = Marble.Add(turn, currentMarbel);
                }                
            }
        }
    }
}
