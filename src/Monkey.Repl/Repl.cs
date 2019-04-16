using System;
using System.Collections.Generic;

using Monkey.Shared.Scanner;
using Monkey.Shared.Parser;
using Monkey.Shared.Evaluator;
using Environment = Monkey.Shared.Evaluator.Environment;

namespace Monkey.Repl
{
    class Repl
    {
        private static List<string> commands;
        private static Environment env;
        private static Scanner scanner;
        private static Parser parser;
        private static Evaluator evaluator;

        static Repl()
        {
            commands = new List<string>();
            env = new Environment();
            scanner = new Scanner();
            parser = new Parser();
            evaluator = new Evaluator();
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
            if (command == String.Empty && commands.Count > 0)
            {
                var source = string.Join(" ", commands);
                var result = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);

                commands.Clear();

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine(result.Value.ToString());
            }
            else
            {
                commands.Add(command);
            }

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
