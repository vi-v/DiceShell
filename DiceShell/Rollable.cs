namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Rollable
    {
        public bool HasBeenRolled = false;

        public int Value = 0;

        protected abstract int ExecuteRoll(Random r = null);

        public void Roll(Random r = null)
        {
            if (this.HasBeenRolled)
            {
                throw new DiceAlreadyRolledException(this.Value);
            }

            r = r ?? new Random();

            this.Value = this.ExecuteRoll(r);

            this.HasBeenRolled = true;
        }
    }
}
