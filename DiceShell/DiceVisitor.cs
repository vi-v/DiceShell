namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Antlr4.Runtime.Misc;
    using DiceShell.Gen;

    public class DiceVisitor : DiceBaseVisitor<object>
    {
        public override object VisitDiceGroup([NotNull] DiceParser.DiceGroupContext context)
        {
            int count, size;
            if (context.NUMBER().Length == 1)
            {
                count = 1;
                size = int.Parse(context.NUMBER()[0].GetText());
            }
            else
            {
                count = int.Parse(context.NUMBER()[0].GetText());
                size = int.Parse(context.NUMBER()[1].GetText());
            }

            DiceGroup diceGroup = new DiceGroup(Enumerable.Range(1, count).Select((_) => new Dice(size)));

            return diceGroup;
        }
    }
}
