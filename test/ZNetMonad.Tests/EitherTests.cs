using System;
using Xunit;
using ZNetMonad.Either;

namespace ZNetMonad.Tests
{
    public class EitherTests
    {
        [Theory]
        [InlineData(1)]
        public void SelectBehavesAsExpected(int a)
        {
            var m = from x in a.ToEither<int, Exception>()
                    select x;

            var result = Assert.IsType<Right<Exception, int>>(m);
            Assert.Equal(a, result.Value);
        }

        [Theory]
        [InlineData(1)]
        public void DivByZeroShortCircuitsWithException(int a)
        {
            var m = from x in a.ToEither<int, Exception>()
                    from y in Div(x, 0)
                    select y;

            var result = Assert.IsType<Left<Exception, int>>(m);
            Assert.IsType<DivideByZeroException>(result.Error);
        }

        private static Either<Exception, int> Div(int a, int b)
        {
            try
            {
                return (a / b).ToEither<int, Exception>();
            }
            catch (Exception e)
            {
                return new Left<Exception, int>(e);
            }
        }
    }
}
