using System.Collections.Generic;

namespace ZNetMonad.Tests.Expression
{
    internal abstract class Expr
    {
        public abstract int Eval(IDictionary<string, int> ctx);
    }
}
