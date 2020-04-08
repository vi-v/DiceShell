namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Dice : Rollable
    {
        public int Size { get; }

        public Dice(int size)
        {
            this.Size = size;
        }

        public override string ToString()
        {
            return $"d{this.Size}";
        }

        protected override int ExecuteRoll(Random r = null)
        {
            return r.Next(1, this.Size + 1);
        }
    }
}
