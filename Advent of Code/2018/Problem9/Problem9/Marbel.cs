using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem9
{
    class Marbel
    {
        public int Value;
        public Marbel Left;
        public Marbel Right;

        public Marbel(int value)
        {
            this.Value = value;
        }


        public void SetLeftRight(Marbel left, Marbel right)
        {
            this.Left = left;
            this.Right = right;
        }

        public static Marbel Add(int value, Marbel currentMarbel)
        {
            Marbel newMarbel = new Marbel(value);
            newMarbel.Left = currentMarbel;
            newMarbel.Right = newMarbel.Left.Right;
            newMarbel.Right.Left = newMarbel;            
            newMarbel.Left.Right = newMarbel;
            return newMarbel;
        }

        public static Marbel Remove(Marbel currentMarbel)
        {
            currentMarbel.Left.Right = currentMarbel.Right;
            currentMarbel.Right.Left = currentMarbel.Left;
            return currentMarbel.Right;
        }

        public static Marbel GoLeft(Marbel marbel)
        {
            return marbel.Left;
        }

        public static Marbel GoRight(Marbel marbel)
        {
            return marbel.Right;
        }

    }
}
