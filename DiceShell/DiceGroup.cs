namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;

    public class DiceGroup
    {
        private readonly List<Dice> diceList;

        public int Value { get; private set; }

        public int Count { get { return this.diceList.Count; } }

        public bool HasBeenRolled { get; private set; }

        public IImmutableList<Dice> DiceList { get { return ImmutableList.Create(this.diceList.ToArray()); } }

        public DiceGroup()
        {
            this.diceList = new List<Dice>();
            this.Value = 0;
            this.HasBeenRolled = false;
        }

        public DiceGroup(IEnumerable<Dice> dice) : this()
        {
            this.diceList.AddRange(dice);
        }

        public void Roll()
        {
            this.diceList.ForEach(d => d.Roll());
            this.Value = this.diceList.Sum(d => d.Value);
            this.HasBeenRolled = true;
        }
    }
}
