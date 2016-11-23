using System;

namespace ZNetMonad
{
    public sealed class Identity<A>
    {
        internal Identity(A value)
        {
            Value = value;
        }

        public A Value { get; }

        public Identity<B> Select<B>(Func<A, B> s)
        {
            return Bind(a => s(a).ToIdentity());
        }

        public Identity<C> SelectMany<B, C>(
            Func<A, Identity<B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => s(a, b).ToIdentity()));
        }

        private Identity<B> Bind<B>(Func<A, Identity<B>> k) => k(Value);
    }
}
