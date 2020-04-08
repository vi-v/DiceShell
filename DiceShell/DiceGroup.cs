namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;

    public class DiceGroup : Rollable
    {
        private readonly List<Dice> diceList;

        public int Count { get { return this.diceList.Count; } }

        public IImmutableList<Dice> DiceList { get { return ImmutableList.Create(this.diceList.ToArray()); } }

        public DiceGroup()
        {
            this.diceList = new List<Dice>();
        }

        public DiceGroup(IEnumerable<Dice> dice) : this()
        {
            this.diceList.AddRange(dice);
        }

        protected override int ExecuteRoll(Random r)
        {
            this.diceList.ForEach(d => d.Roll(r));
            return this.diceList.Sum(d => d.Value);
        }
    }
}
