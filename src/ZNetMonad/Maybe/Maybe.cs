using System;

namespace ZNetMonad.Maybe
{
    public abstract class Maybe<A>
    {
        public Maybe<B> Select<B>(Func<A, B> s)
        {
            return Bind(a => Return(s(a)));
        }

        public Maybe<C> SelectMany<B, C>(
            Func<A, Maybe<B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => Return(s(a, b))));
        }

        protected abstract Maybe<B> Bind<B>(Func<A, Maybe<B>> k);

        private static Maybe<B> Return<B>(B x) => new Just<B>(x);
    }
}
