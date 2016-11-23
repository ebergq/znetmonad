using Xunit;

namespace ZNetMonad.Tests
{
    public class IdentityTests
    {
        [Theory]
        [InlineData(1)]
        public void SelectBehavesAsExpected(int a)
        {
            var m = from x in a.ToIdentity()
                    select x;

            Assert.Equal(a, m.Value);
        }

        [Theory]
        [InlineData(1, 1)]
        public void SumInIdentityBehavesAsExpected(int a, int b)
        {
            var m = from x in a.ToIdentity()
                    from y in b.ToIdentity()
                    select x + y;

            Assert.Equal(a + b, m.Value);
        }
    }
}
