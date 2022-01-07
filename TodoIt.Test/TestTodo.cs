using Xunit;

namespace TodoIt.Test
{
    public class TestTodo
    {
        [Theory]
        [InlineData(0, "Description one", true)]
        [InlineData(1, "Description two", false)]
        public void TestGetSetProperties(
            int id, string description, bool done)
        {
            Todo todo = new Todo(id, description);
            todo.Done = done;
            todo.Assignee = new Person(0, "Chuck", "Norris");

            Assert.Equal(id, todo.TodoId);
            Assert.NotNull(todo.Decription);
            Assert.Equal(description, todo.Decription);
            Assert.Equal(done, todo.Done);
            Assert.NotNull(todo.Assignee);
        }
    }
}
