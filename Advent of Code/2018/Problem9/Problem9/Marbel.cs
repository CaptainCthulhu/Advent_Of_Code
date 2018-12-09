using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem9
{
    class Marble
    {
        public int Value;
        public Marble Left;
        public Marble Right;

        public Marble(int value)
        {
            this.Value = value;
        }


        public void SetLeftRight(Marble left, Marble right)
        {
            this.Left = left;
            this.Right = right;
        }

        public static Marble Add(int value, Marble currentMarbel)
        {
            Marble newMarbel = new Marble(value);
            newMarbel.Left = currentMarbel;
            newMarbel.Right = newMarbel.Left.Right;
            newMarbel.Right.Left = newMarbel;            
            newMarbel.Left.Right = newMarbel;
            return newMarbel;
        }

        public static Marble Remove(Marble currentMarbel)
        {
            currentMarbel.Left.Right = currentMarbel.Right;
            currentMarbel.Right.Left = currentMarbel.Left;
            return currentMarbel.Right;
        }

        public static Marble GoLeft(Marble marbel)
        {
            return marbel.Left;
        }

        public static Marble GoRight(Marble marbel)
        {
            return marbel.Right;
        }

    }
}
