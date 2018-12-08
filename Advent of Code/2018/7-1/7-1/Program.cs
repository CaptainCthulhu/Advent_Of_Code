using System;
using System.Collections.Generic;

namespace _7_1
{
    class Program
    {
        public static int time = 0;

        static void Main(string[] args)
        { 
            Element.FindPath();
            WorkShop();
            Console.ReadKey();
        }      

        static void WorkShop()
        {
            Element.elements = null;
            Element.PopulateElements();
            List<Elf> elves = Elf.GetElves(input.Elves);            

            while (Element.HasUnfinishedElements)
            {
                foreach (Elf elf in elves)
                {
                    elf.CheckIfFinished();
                }

                foreach (Elf elf in elves)
                {
                    if (!elf.HasJob)
                        elf.GetNextJob();
                }

                foreach (Elf elf in elves)
                {                    
                    elf.Work();                   
                }
                
                ++time;
            }

            Console.WriteLine(time.ToString());
        }


        
        

    }
}
