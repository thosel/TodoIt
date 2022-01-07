using Xunit;

namespace TodoIt.Test
{
    public class TestTodoSequencer
    {
        public TestTodoSequencer()
        {
            TodoSequencer.Reset();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void TestNextTodoId(int expected)
        {
            int id = 0;
            for (int i = 0; i < expected; i++)
                id = TodoSequencer.NextTodoId();

            Assert.Equal(expected - 1, id);
        }

        [Fact]
        public void TestReset()
        {
            TodoSequencer.NextTodoId();
            TodoSequencer.Reset();
            int id = TodoSequencer.NextTodoId();

            Assert.Equal(0, id);
        }
    }
}
