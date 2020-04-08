namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Atom
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
    }
}
