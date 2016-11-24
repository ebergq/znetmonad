using System;

namespace ZNetMonad.Either
{
    public sealed class Right<E, A> : Either<E, A>
    {
        public Right(A value)
        {
            Value = value;
        }

        public A Value { get; }

        protected override Either<E, B> Bind<B>(Func<A, Either<E, B>> k)
            => k(Value);
    }
}
