using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monkey.Shared.Scanner
{
    public partial class Scanner
    {
        private List<Token> tokens = new List<Token>();

        private char[] characters;
        private StringBuilder buffer;
        private int position = 0;
        private int currentColumn = 2;
        private int currentLine = 1;
        private char currentCharacter;

        public Scanner(string source)
        {
            characters = source.ToCharArray();
        }

        public List<Token> Scan()
        {
            buffer = new StringBuilder();

            while (position < characters.Length)
            {
                currentCharacter = characters[position];

                if (Utilities.IsQuote(currentCharacter))
                {
                    tokens.Add(TokenizeString());
                }
                else if (Utilities.IsValidLetterCharacter(currentCharacter)|| Char.IsNumber(currentCharacter))
                {
                    buffer.Append(currentCharacter);
                }
                else if (Utilities.IsStickyOperator(currentCharacter))
                {
                    tokens.Add(TokenizeStickyOperator());
                }
                else
                {
                    if (buffer.Length > 0)
                    {
                        tokens.Add(Utilities.CreateToken(String.Join(String.Empty, buffer), currentColumn - buffer.Length, currentLine));
                        buffer.Clear();
                    }

                    if (!Char.IsWhiteSpace(currentCharacter)) {
                        tokens.Add(Utilities.CreateToken(currentCharacter.ToString(), currentColumn, currentLine));
                    }
                }

                if (Utilities.IsNewlineOrReturnCharacter(currentCharacter))
                {
                    // Handle CRLF
                    if (Utilities.IsNewlineOrReturnCharacter(PeekCharacter())) position++;

                    currentColumn = 2;
                    currentLine++;
                }
                else
                {
                    currentColumn++;
                }

                position++;
            }

            // Flush buffered characters
            if (buffer.Length > 0)
            {
                tokens.Add(Utilities.CreateToken(String.Join(String.Empty, buffer), currentColumn - buffer.Length, currentLine));
            }

            // Create SyntaxKind.EOF token
            tokens.Add(Utilities.CreateToken(String.Empty, currentColumn, currentLine));

            return tokens;
        }

        private char PeekCharacter()
        {
            return characters[position + 1];
        }

        private Token TokenizeStickyOperator()
        {
            Token token;

            if (Utilities.IsValidStickyOperator(currentCharacter, PeekCharacter()))
            {
                token = Utilities.CreateToken(String.Join(String.Empty, currentCharacter, PeekCharacter()), currentColumn, currentLine);
                currentColumn++;
                position++;
            }
            else
            {
                token = Utilities.CreateToken(currentCharacter.ToString(), currentColumn, currentLine);
            }

            return token;
        }

        private Token TokenizeString()
        {
            StringBuilder buffer = new StringBuilder("\"");
            var column = currentColumn;

            position++;
            currentColumn++;
            currentCharacter = characters[position];

            while (!Utilities.IsQuote(currentCharacter) && position < characters.Length)
            {
                buffer.Append(currentCharacter);
                currentColumn++;
                position++;
                currentCharacter = characters[position];
            }

            buffer.Append("\"");

            return Utilities.CreateToken(buffer.ToString(), column, currentLine);
        }
    }
}
