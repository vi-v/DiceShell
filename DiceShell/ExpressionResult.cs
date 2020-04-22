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
            this.AtomList = new List<Atom>();
        }

        public List<Atom> AtomList { get; private set; }

        public void AddAtom(Atom a)
        {
            this.AtomList.Add(a);
        }

        public void AddAtoms(IEnumerable<Atom> atoms)
        {
            this.AtomList.AddRange(atoms);
        }

        public void Print()
        {

            IEnumerable<Atom> diceGroupAtoms = this.AtomList.Where(a => a.IsDiceGroup);
            IEnumerable<Atom> modifierAtoms = this.AtomList.Where(a => a.IsModifier);

            foreach (Atom a in diceGroupAtoms)
            {
                DiceGroup dg = a.DiceGroupInstance;

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
            }

            Console.Write("Modifiers: ");
            Console.WriteLine(string.Join(", ", modifierAtoms.Select(a => a.ModifierInstance.ToString())));
        }

        protected override int ExecuteRoll(Random r = null)
        {
            this.AtomList.ForEach(a => a.Roll());

            return this.AtomList
                    .Select(a => a.Value)
                    .Sum();
        }
    }
}
