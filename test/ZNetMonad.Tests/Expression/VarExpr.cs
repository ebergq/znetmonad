using System.Collections.Generic;

namespace ZNetMonad.Tests.Expression
{
    internal sealed class VarExpr : Expr
    {
        public VarExpr(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override int Eval(IDictionary<string, int> ctx) => ctx[Name];
    }
}
