namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class DiceAlreadyRolledException : Exception
    {
        public DiceAlreadyRolledException(int value) : base($"Dice has already been rolled with value {value}")
        {
        }
    }
}
