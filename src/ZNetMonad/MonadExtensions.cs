namespace ZNetMonad
{
    public static class MonadExtensions
    {
        public static Identity<T> ToIdentity<T>(this T value)
            => new Identity<T>(value);
    }
}
