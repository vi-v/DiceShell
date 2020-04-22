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
            Atom atom = (Atom)this.VisitSignedAtom(context.signedAtom());
            ExpressionResult er;

            if (context.expression() != null)
            {
                er = (ExpressionResult)this.VisitExpression(context.expression());
            }
            else
            {
                er = new ExpressionResult();
            }

            er.AddAtom(atom);

            return er;
        }

        public override object VisitSignedAtom([NotNull] DiceParser.SignedAtomContext context)
        {
            Atom atom = (Atom)this.VisitAtom(context.atom());

            if (context.SIGN() != null && context.SIGN().GetText() == "-")
            {
                atom.Sign = AtomSign.Minus;
            }

            return atom;
        }

        public override object VisitAtom([NotNull] DiceParser.AtomContext context)
        {
            Atom atom = new Atom();
            if (context.diceGroup() == null)
            {
                atom.SetModifier(int.Parse(context.NUMBER().GetText()));
                return atom;
            }
            else
            {
                DiceGroup diceGroup = (DiceGroup)this.VisitDiceGroup(context.diceGroup());
                atom.SetDiceGroup(diceGroup);

                return atom;
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
