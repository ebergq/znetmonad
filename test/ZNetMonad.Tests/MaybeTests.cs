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
        public void SumInIdentityBehavesAsExpected(int a, int b)
        {
            var m = from x in a.ToMaybe()
                    from y in b.ToMaybe()
                    select x + y;

            var result = Assert.IsType<Just<int>>(m);
            Assert.Equal(a + b, result.Value);
        }
    }
}
