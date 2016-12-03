using System;
using System.Collections.Generic;

namespace ZNetMonad.Tests.Expression
{
    internal sealed class BinExpr : Expr
    {
        public BinExpr(Expr lhs, Func<int, int, int> op, Expr rhs)
        {
            Lhs = lhs;
            Op = op;
            Rhs = rhs;
        }

        public Expr Lhs { get; }

        public Func<int, int, int> Op { get; }

        public Expr Rhs { get; }

        public override int Eval(IDictionary<string, int> ctx)
            => Op(Lhs.Eval(ctx), Rhs.Eval(ctx));
    }
}
