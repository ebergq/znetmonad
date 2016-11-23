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
    }
}
