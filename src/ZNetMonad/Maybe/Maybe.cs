using System;

namespace ZNetMonad.Maybe
{
    public abstract class Maybe<A>
    {
        public Maybe<B> Select<B>(Func<A, B> s)
        {
            return Bind(a => s(a).ToMaybe());
        }

        public Maybe<C> SelectMany<B, C>(
            Func<A, Maybe<B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => s(a, b).ToMaybe()));
        }

        protected abstract Maybe<B> Bind<B>(Func<A, Maybe<B>> k);
    }
}
