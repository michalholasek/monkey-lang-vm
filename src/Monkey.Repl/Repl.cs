using System;
using System.Collections.Generic;

namespace Monkey.Repl
{
    class Program
    {
        private static List<string> commands;

        static Program()
        {
            commands = new List<string>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to monkey-lang REPL!");

            var condition = true;

            while (condition)
            {
                Promt();
                condition = HandleCommand(Console.ReadLine());
            }

            Console.WriteLine("Exiting monkey-lang REPL...");
        }

        private static bool HandleCommand(string command)
        {
            commands.Add(command);

            if (commands.Count == 1 && (command == "q" || command == "quit"))
            {
                return false;
            }

            return true;
        }

        private static void Promt()
        {
            if (commands.Count == 0)
            {
                Console.Write("> ");
            }
        }
    }
}
