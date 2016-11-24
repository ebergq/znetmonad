using System;

namespace ZNetMonad.Either
{
    public abstract class Either<E, A>
    {
        public Either<E, B> Select<B>(Func<A, B> s)
        {
            return Bind(a => Return(s(a)));
        }

        public Either<E, C> SelectMany<B, C>(
            Func<A, Either<E, B>> k,
            Func<A, B, C> s)
        {
            return Bind(a => k(a).Bind(b => Return(s(a, b))));
        }

        protected abstract Either<E, B> Bind<B>(Func<A, Either<E, B>> k);

        private static Either<E, B> Return<B>(B x) => new Right<E, B>(x);
    }
}
