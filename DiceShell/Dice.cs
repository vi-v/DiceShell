namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Dice
    {
        public int Size { get; }

        public bool HasBeenRolled { get; private set; }

        public int Value { get; private set; }

        public Dice(int size)
        {
            this.Size = size;
            this.HasBeenRolled = false;
            this.Value = 0;
        }


        public void Roll(Random r = null)
        {
            if (this.HasBeenRolled)
            {
                throw new DiceAlreadyRolledException(this.Value);
            }

            r = r ?? new Random();

            this.Value = r.Next(1, this.Size + 1);
            this.HasBeenRolled = true;
        }
    }
}
