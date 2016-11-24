using Xunit;
using ZNetMonad.Maybe;

namespace ZNetMonad.Tests
{
    public class MaybeTests
    {
        [Theory]
        [InlineData(1)]
        public void SelectBehavesAsExpected(int a)
        {
            var m = from x in a.ToMaybe()
                    select x;

            var result = Assert.IsType<Just<int>>(m);
            Assert.Equal(a, result.Value);
        }

        [Theory]
        [InlineData(1, 1)]
        public void SumInMaybeBehavesAsExpected(int a, int b)
        {
            var m = from x in a.ToMaybe()
                    from y in b.ToMaybe()
                    select x + y;

            var result = Assert.IsType<Just<int>>(m);
            Assert.Equal(a + b, result.Value);
        }

        [Theory]
        [InlineData(1)]
        public void DivisionByZeroReturnsNothing(int a)
        {
            var m = from x in a.ToMaybe()
                    from y in Div(a, 0)
                    select y;

            Assert.IsType<Nothing<int>>(m);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public void DivisionByTwoBehavesAsExpected(int a)
        {
            var m = from x in a.ToMaybe()
                    from y in Div(a, 2)
                    select y;

            var result = Assert.IsType<Just<int>>(m);
            Assert.Equal(a / 2, result.Value);
        }

        private static Maybe<int> Div(int a, int b)
            => b == 0
                  ? new Nothing<int>()
                  : (a / b).ToMaybe();
    }
}
