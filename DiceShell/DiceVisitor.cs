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
        public override object VisitExpression([NotNull] DiceParser.ExpressionContext context)
        {
            int atomValue = (int)this.VisitSignedAtom(context.signedAtom());

            if (context.expression() != null)
            {
                int value = (int)this.VisitExpression(context.expression());
                return value + atomValue;
            }
            else
            {
                return atomValue;
            }
        }

        public override object VisitSignedAtom([NotNull] DiceParser.SignedAtomContext context)
        {
            int mod = 1;

            if (context.SIGN() != null && context.SIGN().GetText() == "-")
            {
                mod = -1;
            }

            return mod * (int)this.VisitAtom(context.atom());
        }

        public override object VisitAtom([NotNull] DiceParser.AtomContext context)
        {
            if (context.diceGroup() == null)
            {
                return int.Parse(context.NUMBER().GetText());
            }
            else
            {
                DiceGroup diceGroup = (DiceGroup)this.VisitDiceGroup(context.diceGroup());
                diceGroup.Roll();

                return diceGroup.Value;
            }
        }

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
