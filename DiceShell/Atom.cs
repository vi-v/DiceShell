namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Atom : Rollable
    {
        public Atom()
        {
            this.DiceGroupInstance = null;
            this.ModifierInstance = 0;
            this.Sign = AtomSign.Plus;
        }

        public DiceGroup DiceGroupInstance { get; private set; }

        public bool IsDiceGroup { get; private set; }

        public int ModifierInstance { get; private set; }

        public bool IsModifier { get; private set; }

        public AtomSign Sign { get; set; }

        public void SetDiceGroup(DiceGroup dg)
        {
            this.DiceGroupInstance = dg;
            this.IsDiceGroup = true;
        }

        public void SetModifier(int m)
        {
            this.ModifierInstance = Math.Abs(m);
            this.IsModifier = true;

            if (m < 0)
            {
                this.Sign = AtomSign.Minus;
            }
        }

        protected override int ExecuteRoll(Random r = null)
        {
            if (!this.IsModifier && !this.IsDiceGroup)
            {
                throw new InvalidOperationException("Atom not initialized");
            }

            int sign = this.Sign == AtomSign.Plus ? 1 : -1;

            if (this.IsModifier)
            {
                return this.ModifierInstance * sign;
            }

            this.DiceGroupInstance.Roll();
            return this.DiceGroupInstance.Value * sign;
        }
    }
}
