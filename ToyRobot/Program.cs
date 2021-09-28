using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(6, 6);
            Console.WriteLine("Enter command (Type EXIT to terminate)");
            while (true)
            {
                var commandString = Console.ReadLine();
                if (commandString.ToUpper() == "EXIT")
                {
                    return;
                }
                try
                {
                    var interpretedCommand = CommandInterpreter.GetCommandFromString(commandString);
                    var result = board.ExecuteCommand(interpretedCommand);
                    if (result.Result)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(result.Message);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(result.Message);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
