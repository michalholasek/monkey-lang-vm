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
            var actual = new Scanner("=+(){},;").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.NonLetter);
        }

        [TestMethod]
        public void Statements()
        {
            var actual = new Scanner(@"
                let five = 5;
                let ten = 10;
                let add = fn(x, y) {
                    x + y;
                };
                let _result = add(five, ten);
            ").Scan();

            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Statements);
        }

        [TestMethod]
        public void Operators()
        {
            var actual = new Scanner("! - / * < >").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Operators);
        }

        [TestMethod]
        public void Keywords()
        {
            var actual = new Scanner("true false if else fn let return").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Keywords);
        }

        [TestMethod]
        public void StickyOperators()
        {
            var actual = new Scanner(@"
                10 == 10;
                10 != 9;
            ").Scan();
            
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.StickyOperators);
        }

        [TestMethod]
        public void PrefixOperators()
        {
            var actual = new Scanner("!5;!true;!!true;").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.PrefixOperators);
        }

        [TestMethod]
        public void Strings()
        {
            var actual = new Scanner("\"foo bar\"").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Strings);
        }

        [TestMethod]
        public void Arrays()
        {
            var actual = new Scanner("[1, 2];").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Arrays);
        }

        [TestMethod]
        public void Hashes()
        {
            var actual = new Scanner("{ \"foo\": \"bar\" }").Scan();
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Tokens.Hashes);
        }
    }
}
