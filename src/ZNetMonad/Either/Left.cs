using System;

namespace ZNetMonad.Either
{
    public sealed class Left<E, A> : Either<E, A>
    {
        public Left(E error)
        {
            Error = error;
        }

        public E Error { get; }

        protected override Either<E, B> Bind<B>(Func<A, Either<E, B>> k)
            => new Left<E, B>(Error);
    }
}
