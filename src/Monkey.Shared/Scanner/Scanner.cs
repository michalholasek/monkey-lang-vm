using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Monkey.Shared.Scanner
{
    public class Scanner
    {
        private abstract class InternalState
        {
            public StringBuilder Buffer { get; set; }
            public StringReader Characters { get; set; }
            public int Column { get; set; }
            public char CurrentCharacter { get; set; }
            public int Line { get; set; }
            public List<Token> Tokens { get; set; }
        }

        private class Options : InternalState {}

        private class State : InternalState
        {
            public State(Options opts)
            {
                Buffer = opts.Buffer;
                Characters = opts.Characters;
                Column = opts.Column;
                CurrentCharacter = opts.CurrentCharacter;
                Line = opts.Line;
                Tokens = opts.Tokens;
            }
        }

        private readonly State initialState;

        public Scanner()
        {
            var options = new Options
            {
                Buffer = new StringBuilder(),
                Column = 2,
                CurrentCharacter = char.MinValue,
                Line = 1,
                Tokens = new List<Token>()
            };

            initialState = new State(options);
        }

        public List<Token> Scan(string source)
        {
            var currentState = new State(new Options
            {
                Buffer = initialState.Buffer,
                Characters = new StringReader(source),
                Column = initialState.Column,
                CurrentCharacter = initialState.CurrentCharacter,
                Line = initialState.Line,
                Tokens = initialState.Tokens
            });
            
            currentState.Buffer.Clear();

            while (currentState.Characters.Peek() > -1)
            {
                currentState.CurrentCharacter = (char)currentState.Characters.Read();

                if (Utilities.IsQuote(currentState.CurrentCharacter))
                {
                    currentState = TokenizeString(currentState);
                }
                else if (Utilities.IsValidLetterCharacter(currentState.CurrentCharacter)|| Char.IsNumber(currentState.CurrentCharacter))
                {
                    currentState.Buffer.Append(currentState.CurrentCharacter);
                }
                else if (Utilities.IsStickyOperator(currentState.CurrentCharacter))
                {
                    currentState = TokenizeStickyOperator(currentState);
                }
                else
                {
                    currentState = TokenizeBuffer(currentState);

                    if (!Char.IsWhiteSpace(currentState.CurrentCharacter))
                    {
                        currentState.Tokens.Add(Utilities.CreateToken(
                            currentState.CurrentCharacter.ToString(), currentState.Column, currentState.Line)
                        );
                    }
                }

                currentState = DetermineNextStatePositions(currentState);
            }

            currentState.Characters.Close();
            
            // Flush buffered characters
            if (currentState.Buffer.Length > 0)
            {
                currentState.Tokens.Add(Utilities.CreateToken(
                    String.Join(String.Empty, currentState.Buffer), currentState.Column - currentState.Buffer.Length, currentState.Line)
                );
            }

            // Create SyntaxKind.EOF token
            currentState.Tokens.Add(Utilities.CreateToken(String.Empty, currentState.Column, currentState.Line));

            return currentState.Tokens;
        }

        private State DetermineNextStatePositions(State previousState)
        {
            var newState = new State(new Options
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            });

            if (Utilities.IsNewlineOrReturnCharacter(newState.CurrentCharacter)) 
            {
                // Handle CRLF
                if (Utilities.IsNewlineOrReturnCharacter(PeekCharacter(newState)))
                {
                    // Skip peeked LF character
                    newState.Characters.Read();
                }
                newState.Column = 2;
                newState.Line++;
            }
            else
            {
                newState.Column++;
            }

            return newState;
        }

        private char PeekCharacter(State currentState)
        {
            return (char)currentState.Characters.Peek();
        }

        private State TokenizeBuffer(State previousState)
        {
            var newState = new State(new Options
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            });

            if (newState.Buffer.Length > 0)
            {
                newState.Tokens.Add(Utilities.CreateToken(
                    String.Join(String.Empty, newState.Buffer),
                    newState.Column - newState.Buffer.Length,
                    newState.Line
                ));
                newState.Buffer.Clear();
            }

            return newState;
        }

        private State TokenizeStickyOperator(State previousState)
        {
            var newState = new State(new Options
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            });

            if (Utilities.IsValidStickyOperator(newState.CurrentCharacter, PeekCharacter(newState)))
            {
                newState.Tokens.Add(Utilities.CreateToken(
                    String.Join(String.Empty, newState.CurrentCharacter, PeekCharacter(newState)), newState.Column, newState.Line)
                );
                newState.Column++;

                // Skip peeked character
                newState.Characters.Read();
            }
            else
            {
                newState.Tokens.Add(Utilities.CreateToken(newState.CurrentCharacter.ToString(), newState.Column, newState.Line));
            }

            return newState;
        }

        private State TokenizeString(State previousState)
        {
            var newState = new State(new Options
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            });

            StringBuilder buffer = new StringBuilder("\"");

            newState.Column++;
            newState.CurrentCharacter = (char)newState.Characters.Read();

            while (!Utilities.IsQuote(newState.CurrentCharacter) && newState.Characters.Peek() > -1)
            {
                buffer.Append(newState.CurrentCharacter);
                newState.Column++;
                newState.CurrentCharacter = (char)newState.Characters.Read();
            }

            buffer.Append("\"");

            newState.Tokens.Add(Utilities.CreateToken(buffer.ToString(), previousState.Column, newState.Line));

            return newState;
        }
    }
}
