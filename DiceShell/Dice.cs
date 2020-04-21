namespace DiceShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Dice : Rollable
    {
        public int Size { get; }

        public Dice(int size)
        {
            this.Size = size;
        }

        public override string ToString()
        {
            return $"d{this.Size}";
        }

        protected override int ExecuteRoll(Random r = null)
        {
            int result = r.Next(1, this.Size + 1);

            if (this.Size == 20)
            {
                if (result == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Natural 1");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (result == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Natural 20");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            return result;
        }
    }
}
