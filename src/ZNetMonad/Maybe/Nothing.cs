using System;

namespace ZNetMonad.Maybe
{
    public sealed class Nothing<A> : Maybe<A>
    {
        protected override Maybe<B> Bind<B>(Func<A, Maybe<B>> k)
            => new Nothing<B>();
    }
}
