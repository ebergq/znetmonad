using System;
using System.Collections.Generic;
using System.Linq;

namespace ZNetMonad.Parser
{
    public static class ParserCombinators
    {
        public static Parser<S, A> Succeed<S, A>(A result)
        {
            return new Parser<S, A>(input => new[] { Tuple.Create(result, input) });
        }

        public static Parser<S, S> Symbol<S>()
        {
            return new Parser<S, S>(input => {
                return input.Any()
                  ? new [] { Tuple.Create(input.First(), input.Skip(1)) }
                  : Enumerable.Empty<Tuple<S, IEnumerable<S>>>();
            });
        }

        public static Parser<S, S> Sat<S>(Func<S, bool> f)
        {
            return from x in Symbol<S>()
                   where f(x)
                   select x;
        }

        public static Parser<S, S> This<S>(S s)
            where S : IEquatable<S>
        {
            return Sat<S>(x => x.Equals(s));
        }

        public static Parser<S, A> Or<S, A>(this Parser<S, A> p, Parser<S, A> q)
        {
            return p.Plus(q);
        }

        public static Parser<S, IEnumerable<A>> Many1<S, A>(Parser<S, A> p)
        {
            return from x in p
                   from xs in Many(p)
                   select new[] { x }.Concat(xs);
        }

        public static Parser<S, IEnumerable<A>> Many<S, A>(Parser<S, A> p)
        {
            return Many1(p).Or(Succeed<S, IEnumerable<A>>(Enumerable.Empty<A>()));
        }

        public static Parser<S, A> Between<S, A>(S open, S close, Func<Parser<S, A>> p)
            where S : IEquatable<S>
        {
            return from o in This(open)
                   from x in p()
                   from c in This(close)
                   select x;
        }

        public static Parser<S, A> ChainLeft<S, A>(
            Parser<S, Func<A, A, A>> op,
            Parser<S, A> term)
        {
            return from e in term
                   from r in Chain(e, op, term)
                   select r;
        }

        private static Parser<S, A> Chain<S, A>(
            A e,
            Parser<S, Func<A, A, A>> op,
            Parser<S, A> term)
        {
            return Succeed<S, A>(e).Or(
                    from f in op
                    from eprim in term
                    from r in Chain(f(e, eprim), op, term)
                    select r);
        }
    }
}
