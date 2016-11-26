using System;

namespace ZNetMonad.State
{
    public sealed class State<S, A>
    {
        private readonly Func<S, Tuple<A, S>> _runState;

        private State(Func<S, Tuple<A, S>> f)
        {
            _runState = f;
        }

        public static State<S, S> Get()
            => new State<S, S>(s => Tuple.Create(s, s));

        public static State<S, object> Put(S s)
            => new State<S, object>(_ => Tuple.Create(default(object), s));

        public A Eval(S state) => _runState(state).Item1;

        public State<S, B> Select<B>(Func<A, B> s)
        {
            return Bind(a => Return(s(a)));
        }

        public State<S, C> SelectMany<B, C>(
            Func<A, State<S, B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => Return(s(a, b))));
        }

        private static State<S, B> Return<B>(B x)
            => new State<S, B>(s => Tuple.Create(x, s));

        private State<S, B> Bind<B>(Func<A, State<S, B>> k)
        {
            return new State<S, B>(s => {
                var result = _runState(s);
                return k(result.Item1)._runState(result.Item2);
            });
        }
    }
}
