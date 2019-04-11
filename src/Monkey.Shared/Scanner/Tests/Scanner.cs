using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey.Shared.Scanner;

namespace Monkey.Tests
{
    [TestClass]
    public class Scanning
    {
        [TestMethod]
        public void NonLetterCharacters()
        {
            var actual = new Scanner().Scan("=+(){},;");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.NonLetter);
        }

        [TestMethod]
        public void Statements()
        {
            var actual = new Scanner().Scan(@"
                let five = 5;
                let ten = 10;
                let add = fn(x, y) {
                    x + y;
                };
                let _result = add(five, ten);
            ");

            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Statements);
        }

        [TestMethod]
        public void Operators()
        {
            var actual = new Scanner().Scan("! - / * < >");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Operators);
        }

        [TestMethod]
        public void Keywords()
        {
            var actual = new Scanner().Scan("true false if else fn let return");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Keywords);
        }

        [TestMethod]
        public void StickyOperators()
        {
            var actual = new Scanner().Scan(@"
                10 == 10;
                10 != 9;
            ");
            
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.StickyOperators);
        }

        [TestMethod]
        public void PrefixOperators()
        {
            var actual = new Scanner().Scan("!5;!true;!!true;");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.PrefixOperators);
        }

        [TestMethod]
        public void Strings()
        {
            var actual = new Scanner().Scan("\"foo bar\"");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Strings);
        }

        [TestMethod]
        public void Arrays()
        {
            var actual = new Scanner().Scan("[1, 2];");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Arrays);
        }

        [TestMethod]
        public void Hashes()
        {
            var actual = new Scanner().Scan("{ \"foo\": \"bar\" }");
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Hashes);
        }
    }
}
