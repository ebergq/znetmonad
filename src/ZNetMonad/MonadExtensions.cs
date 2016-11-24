using ZNetMonad.Either;
using ZNetMonad.Identity;
using ZNetMonad.Maybe;

namespace ZNetMonad
{
    public static class MonadExtensions
    {
        public static Identity<T> ToIdentity<T>(this T value)
            => new Identity<T>(value);

        public static Maybe<T> ToMaybe<T>(this T value)
            => new Just<T>(value);

        public static Either<E, A> ToEither<A, E>(this A right)
            => new Right<E, A>(right);
    }
}
