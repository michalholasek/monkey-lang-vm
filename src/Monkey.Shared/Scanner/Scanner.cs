using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using static Monkey.Shared.Scanner.Utilities;

namespace Monkey.Shared
{
    public partial class Scanner
    {
        private readonly ScannerState defaults;

        public Scanner()
        {
            defaults = new ScannerState
            {
                Buffer = new StringBuilder(),
                Column = 2,
                CurrentCharacter = char.MinValue,
                Line = 1,
                Tokens = new List<Token>()
            };
        }

        public List<Token> Scan(string source)
        {
            var currentState = new ScannerState
            {
                Buffer = new StringBuilder(),
                Characters = new StringReader(source),
                Column = defaults.Column,
                CurrentCharacter = defaults.CurrentCharacter,
                Line = defaults.Line,
                Tokens = new List<Token>()
            };
            
            currentState.Buffer.Clear();

            while (currentState.Characters.Peek() > -1)
            {
                currentState.CurrentCharacter = (char)currentState.Characters.Read();

                if (IsQuote(currentState.CurrentCharacter))
                {
                    currentState = TokenizeString(currentState);
                }
                else if (IsValidLetterCharacter(currentState.CurrentCharacter)|| Char.IsNumber(currentState.CurrentCharacter))
                {
                    currentState.Buffer.Append(currentState.CurrentCharacter);
                }
                else if (IsStickyOperator(currentState.CurrentCharacter))
                {
                    currentState = TokenizeStickyOperator(currentState);
                }
                else
                {
                    currentState = TokenizeBuffer(currentState);

                    if (!Char.IsWhiteSpace(currentState.CurrentCharacter))
                    {
                        currentState.Tokens.Add(Token.Create
                        (
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
                currentState.Tokens.Add(Token.Create
                (
                    String.Join(String.Empty, currentState.Buffer),
                    currentState.Column - currentState.Buffer.Length,
                    currentState.Line
                ));
            }

            // Create SyntaxKind.EOF token
            currentState.Tokens.Add(Token.Create(String.Empty, currentState.Column, currentState.Line));

            return currentState.Tokens;
        }

        private ScannerState DetermineNextStatePositions(ScannerState previousState)
        {
            var newState = new ScannerState
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            };

            if (IsNewlineOrReturnCharacter(newState.CurrentCharacter)) 
            {
                // Handle CRLF
                if (IsNewlineOrReturnCharacter(PeekCharacter(newState)))
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

        private char PeekCharacter(ScannerState currentState)
        {
            return (char)currentState.Characters.Peek();
        }

        private ScannerState TokenizeBuffer(ScannerState previousState)
        {
            var newState = new ScannerState
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            };

            if (newState.Buffer.Length > 0)
            {
                newState.Tokens.Add(Token.Create
                (
                    String.Join(String.Empty, newState.Buffer),
                    newState.Column - newState.Buffer.Length,
                    newState.Line
                ));
                newState.Buffer.Clear();
            }

            return newState;
        }

        private ScannerState TokenizeStickyOperator(ScannerState previousState)
        {
            var newState = new ScannerState
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            };

            if (IsValidStickyOperator(newState.CurrentCharacter, PeekCharacter(newState)))
            {
                newState.Tokens.Add(Token.Create
                (
                    String.Join(String.Empty, newState.CurrentCharacter, PeekCharacter(newState)), newState.Column, newState.Line)
                );
                newState.Column++;

                // Skip peeked character
                newState.Characters.Read();
            }
            else
            {
                newState.Tokens.Add(Token.Create(newState.CurrentCharacter.ToString(), newState.Column, newState.Line));
            }

            return newState;
        }

        private ScannerState TokenizeString(ScannerState previousState)
        {
            var newState = new ScannerState
            {
                Buffer = previousState.Buffer,
                Characters = previousState.Characters,
                Column = previousState.Column,
                CurrentCharacter = previousState.CurrentCharacter,
                Line = previousState.Line,
                Tokens = previousState.Tokens
            };

            StringBuilder buffer = new StringBuilder("\"");

            newState.Column++;
            newState.CurrentCharacter = (char)newState.Characters.Read();

            while (!IsQuote(newState.CurrentCharacter) && newState.Characters.Peek() > -1)
            {
                buffer.Append(newState.CurrentCharacter);
                newState.Column++;
                newState.CurrentCharacter = (char)newState.Characters.Read();
            }

            buffer.Append("\"");

            newState.Tokens.Add(Token.Create(buffer.ToString(), previousState.Column, newState.Line));

            return newState;
        }
    }
}
