namespace DiceShell
{
    using Antlr4.Runtime;
    using DiceShell.Gen;
    using Mono.Terminal;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            LineEditor lineEditor = new LineEditor("DiceShell");

            string input;
            while ((input = lineEditor.Edit("DiceShell $ ", "")) != null)
            {
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                try
                {
                    AntlrInputStream inputStream = new AntlrInputStream(input);
                    DiceLexer diceLexer = new DiceLexer(inputStream);
                    CommonTokenStream commonTokenStream = new CommonTokenStream(diceLexer);
                    DiceParser diceParser = new DiceParser(commonTokenStream);
                    DiceParser.ShellContext context = diceParser.shell();
                    DiceVisitor visitor = new DiceVisitor();

                    int result = (int)visitor.Visit(context);

                    Console.WriteLine(string.Format("[{0:HH:mm:ss}] {1}\n", DateTime.Now, result));
                }
                catch (Exception)
                {
                    Console.WriteLine("Parsing Error\n");
                }
            }
        }
    }
}
