using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_1
{
    class Elf
    {
        public string name;
        public Element currentJob;

        public Boolean HasJob
        {
            get
            {
                return currentJob != null;
            }
        }

        public Elf(string name)
        {
            this.name = name;
            GetNextJob();
        }

        public void Work()
        {
            if (HasJob)            
                currentJob.TimeSpent++;                            
            else
                GetNextJob();
        }

        public void CheckIfFinished()
        {
            if (currentJob != null && currentJob.Finished)
            {
                Console.WriteLine(String.Format("Elf {0} done job {1} at {2}", name, currentJob.name, Program.time));
                FinishJob();
                GetNextJob();
            }
        } 

        public void FinishJob()
        {
            currentJob.traversed = true;
            currentJob.working = false;
            currentJob = null;
        }

        public void GetNextJob()
        {            
            List<Element> elements = Element.UnWorkedElements();
            if (elements.Count() > 0)
            {
                currentJob = elements.First();
                currentJob.working = true;
                Console.WriteLine(String.Format("Elf {0} picking up job {1} at {2}", name, currentJob.name, Program.time));
            }
        }        

        public static List<Elf> GetElves(int elfCount)
        {
            List<Elf> elves = new List<Elf>();

            for(int x = 0; x < elfCount; ++x)
            {
                elves.Add(new Elf((x+1).ToString()));
            }

            return elves;
        }
    }
}
