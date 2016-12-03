using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZNetMonad.Parser;
using ZNetMonad.Tests.Expression;
using static ZNetMonad.Parser.ParserCombinators;

namespace ZNetMonad.Tests
{
    public sealed class ParserTests
    {
        private static readonly IDictionary<char, Func<int, int, int>> _addOpMap = new Dictionary<char, Func<int, int, int>>
        {
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b }
        };

        private static readonly IDictionary<char, Func<int, int, int>> _mulOpMap = new Dictionary<char, Func<int, int, int>>
        {
            { '*', (a, b) => a * b },
            { '/', (a, b) => a / b }
        };

        private static readonly IDictionary<string, int> _ctx = new Dictionary<string, int>
        {
            { "x", 3 }
        };

        private Parser<char, Expr> Atom;
        private Parser<char, Expr> Factor;
        private Parser<char, Expr> Term;
        private Parser<char, Expr> ExprP;

        [Theory]
        [InlineData("9", 9)]
        [InlineData("x*x*x", 27)]
        [InlineData("3+2*4+1", 12)]
        [InlineData("3+2*(4+1)", 13)]
        public void ParserWorks(string expression, int expected)
        {
            var ident = from c in Sat<char>(char.IsLetter)
                        from cs in Many(Sat<char>(char.IsLetterOrDigit))
                        select (Expr)new VarExpr(new string(new [] {c}.Concat(cs).ToArray()));

            var integer = from cs in Many1(Sat<char>(char.IsDigit))
                          select (Expr)new NumExpr(int.Parse(new string(cs.ToArray())));

            Atom = ident.Or(integer).Or(Between('(', ')', () => ExprP));
            Factor = ChainLeft(BinOp(_mulOpMap), Atom);
            Term = ChainLeft(BinOp(_addOpMap), Factor);
            ExprP = Term;

            Assert.Equal(expected, Parse(ExprP, expression).Eval(_ctx));
        }

        private static A Parse<A>(Parser<char, A> parser, string input)
            => parser.Parse(input).Item1;

        private Parser<char, Func<Expr, Expr, Expr>> BinOp(
            IDictionary<char, Func<int, int, int>> map)
        {
            return from op in Sat<char>(map.ContainsKey)
                   select new Func<Expr, Expr, Expr>((e1, e2) => new BinExpr(e1, map[op], e2));
        }
    }
}
