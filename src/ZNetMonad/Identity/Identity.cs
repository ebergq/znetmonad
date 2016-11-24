using System;

namespace ZNetMonad.Identity
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
            return Bind(a => Return(s(a)));
        }

        public Identity<C> SelectMany<B, C>(
            Func<A, Identity<B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => Return(s(a, b))));
        }

        private Identity<B> Bind<B>(Func<A, Identity<B>> k) => k(Value);

        private static Identity<B> Return<B>(B x) => new Identity<B>(x);
    }
}
