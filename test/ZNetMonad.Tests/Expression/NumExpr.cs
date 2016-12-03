using System.Collections.Generic;

namespace ZNetMonad.Tests.Expression
{
    internal sealed class NumExpr : Expr
    {
        public NumExpr(int literal)
        {
            Literal = literal;
        }

        public int Literal { get; }

        public override int Eval(IDictionary<string, int> ctx) => Literal;
    }
}
