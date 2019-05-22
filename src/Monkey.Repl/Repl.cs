using System;
using System.Collections.Generic;

using Monkey;
using Monkey.Shared;
using Environment = Monkey.Shared.Environment;
using Object = Monkey.Shared.Object;

namespace Monkey.Repl
{
    class Repl
    {
        private static List<string> commands;

        private static Scanner scanner;
        private static Parser parser;
        private static Compiler compiler;
        private static VirtualMachine vm;

        static Repl()
        {
            commands = new List<string>();

            scanner = new Scanner();
            parser = new Parser();
            compiler = new Compiler();
            vm = new VirtualMachine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to monkey-lang REPL!");

            var run = true;

            while (run)
            {
                Promt();
                run = HandleCommand(Console.ReadLine());
            }

            Console.WriteLine("Exiting monkey-lang REPL...");
        }

        private static bool HandleCommand(string command)
        {
            if (command == String.Empty && commands.Count > 0)
            {
                var source = string.Join(" ", commands);

                var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

                commands.Clear();
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                if (compilationResult.Errors.Count > 0)
                {
                    compilationResult.Errors.ForEach(error =>
                    {
                        Console.WriteLine(error.Message);
                    });
                }
                else
                {
                    vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);
                    Console.WriteLine(Stringify.Object((Object)vm.LastStackElement));
                }
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
