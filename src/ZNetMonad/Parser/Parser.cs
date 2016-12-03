using System;
using System.Collections.Generic;
using System.Linq;

namespace ZNetMonad.Parser
{
    public sealed class Parser<S, A>
    {
        private readonly Func<IEnumerable<S>, IEnumerable<Tuple<A, IEnumerable<S>>>> _runParser;

        internal Parser(Func<IEnumerable<S>, IEnumerable<Tuple<A, IEnumerable<S>>>> runParser)
        {
            _runParser = runParser;
        }

        public Tuple<A, IEnumerable<S>> Parse(IEnumerable<S> input)
        {
            return (from result in _runParser(input)
                    orderby result.Item2.Count()
                    select result).FirstOrDefault();
        }

        public Parser<S, A> Where(Func<A, bool> f)
        {
            return Bind(a => f(a) ? Return(a) : Fail());
        }

        public Parser<S, B> Select<B>(Func<A, B> s)
        {
            return Bind(a => Return(s(a)));
        }

        public Parser<S, C> SelectMany<B, C>(
            Func<A, Parser<S, B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => Return(s(a, b))));
        }

        internal Parser<S, A> Plus(Parser<S, A> q)
        {
            return new Parser<S, A>(s => _runParser(s).Concat(q._runParser(s)));
        }

        private static Parser<S, A> Fail()
            => new Parser<S, A>(_ => Enumerable.Empty<Tuple<A, IEnumerable<S>>>());

        private static Parser<S, B> Return<B>(B x)
            => new Parser<S, B>(s => new [] { Tuple.Create(x, s) });

        private Parser<S, B> Bind<B>(Func<A, Parser<S, B>> k)
        {
            return new Parser<S, B>(s =>
                from t1 in _runParser(s)
                from t2 in k(t1.Item1)._runParser(t1.Item2)
                select t2);
        }
    }
}
