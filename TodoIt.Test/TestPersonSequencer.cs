using Xunit;

namespace TodoIt.Test
{
    public class TestPersonSequencer
    {
        public TestPersonSequencer()
        {
            PersonSequencer.Reset();
        }

        [Fact]
        public void TestPersonId()
        {
            int resultOne = PersonSequencer.NextPersonId();
            int resultTwo = PersonSequencer.NextPersonId();
            Assert.Equal(0, resultOne);
            Assert.Equal(1, resultTwo);
        }

        [Fact]
        public void TestReset()
        {
            int result = 0;
            int id;

            PersonSequencer.Reset();
            id = PersonSequencer.NextPersonId();

            Assert.Equal(id, result);
        }
    }
}
