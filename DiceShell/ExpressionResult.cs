namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ExpressionResult : Rollable
    {
        public ExpressionResult()
        {
            this.DiceGroupList = new List<DiceGroup>();
            this.ModifierList = new List<int>();
        }

        public List<DiceGroup> DiceGroupList { get; private set; }

        public List<int> ModifierList { get; private set; }

        public void AddDiceGroup(DiceGroup dg)
        {
            this.DiceGroupList.Add(dg);
        }

        public void AddDiceGroups(IEnumerable<DiceGroup> diceGroups)
        {
            this.DiceGroupList.AddRange(diceGroups);
        }

        public void AddModifier(int m)
        {
            this.ModifierList.Add(m);
        }

        public void AddModifiers(IEnumerable<int> modifiers)
        {
            this.ModifierList.AddRange(modifiers);
        }

        public void Print()
        {
            this.DiceGroupList.ForEach(dg =>
            {
                dg.DiceList.ToList().ForEach(d =>
                {
                    if (d.Size == 20)
                    {
                        if (d.Value == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (d.Value == 20)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }

                    Console.WriteLine($"{d}: {d.Value}");
                    Console.ForegroundColor = ConsoleColor.White;
                });
            });

            Console.Write("Modifiers: ");
            Console.WriteLine(string.Join(", ", this.ModifierList.Select(m => m.ToString())));
        }

        protected override int ExecuteRoll(Random r = null)
        {
            this.DiceGroupList.ForEach(dg => dg.Roll());

            return this.DiceGroupList
                    .Select(dg => dg.Value)
                    .Concat(this.ModifierList)
                    .Sum();
        }
    }
}
