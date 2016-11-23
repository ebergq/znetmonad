using System;

namespace ZNetMonad.Maybe
{
    public sealed class Just<A> : Maybe<A>
    {
        internal Just(A value)
        {
            Value = value;
        }

        public A Value { get; }

        protected override Maybe<B> Bind<B>(Func<A, Maybe<B>> k)
            => k(Value);
    }
}
