using System;
using System.Linq;
using Xunit;

namespace TodoIt.Test
{
    public class TestTodoItems : IDisposable
    {
        private readonly TodoItems todoItems1;
        private readonly TodoItems todoItems2;
        private readonly int size;
        private readonly People people;

        public TestTodoItems()
        {
            TodoSequencer.Reset();

            todoItems1 = new TodoItems();
            todoItems2 = new TodoItems();

            todoItems1.Clear();

            // size must be even
            size = 6;
            MassAdd(size / 2, todoItems1);
            MassAdd(size / 2, todoItems2);

            people = new People();
            people.CreatePerson("a", "b");
            people.CreatePerson("c", "d");

            todoItems1.FindById(0).Assignee = people.FindById(0);
            todoItems1.FindById(1).Assignee = people.FindById(0);
            todoItems1.FindById(2).Assignee = people.FindById(1);
        }

        public void Dispose()
        {
            people.Clear();
            PersonSequencer.Reset();
            TodoSequencer.Reset();
        }

        private void MassAdd(int size, TodoItems todoItems)
        {
            for (int i = 0; i < size; i++)
                todoItems.AddTodo($"Todo {i}");
        }

        [Fact]
        public void TestSize()
        {
            Assert.Equal(size, todoItems1.Size());
            Assert.Equal(size, todoItems2.Size());
        }

        [Fact]
        public void TestReturnAllAndFindById()
        {
            Todo[] todos = todoItems1.FindAll();

            for (int i = 0; i < todos.Length; i++)
            {
                Assert.Equal(todos[i], todoItems1.FindById(i));
            }
        }

        [Fact]
        public void TestAddTodo()
        {
            string expected = "Description";
            todoItems1.AddTodo(expected);

            Assert.Equal(
                expected,
                todoItems1.FindById(todoItems1.Size() - 1).Decription);
        }

        [Fact]
        public void TestClear()
        {
            todoItems1.Clear();

            Assert.Equal(0, todoItems1.Size());
        }

        [Fact]
        public void TestFindByIdThrowsException()
        {
            Assert.Throws<Exception>(() => todoItems1.FindById(100));
        }

        [Fact]
        public void TestFindByDoneStatus()
        {
            for (int i = 0; i < size / 2; i++)
            {
                todoItems1.FindById(i).Done = true;
            }

            Todo[] done = todoItems1.FindByDoneStatus(true);
            Todo[] open = todoItems1.FindByDoneStatus(false);

            Assert.Equal(done.Length, size / 2);
            Assert.Equal(open.Length, size / 2);
        }

        [Fact]
        public void TestFindByAssignee()
        {
            Todo[] assignee0 = todoItems1.FindByAssignee(people.FindById(0));
            Todo[] assignee1 = todoItems1.FindByAssignee(1);

            Assert.Equal(2, assignee0.Length);
            Assert.Single(assignee1);
        }

        [Fact]
        public void TestFindUnassignedTodoItems()
        {
            Todo[] unassigned = todoItems1.FindUnassignedTodoItems();

            Assert.Equal(3, unassigned.Length);
        }

        [Fact]
        public void TestRemoveTodoItems()
        {
            string itemToFind = "find";

            todoItems1.AddTodo(itemToFind);
            Todo todoToRemove = todoItems1.FindAll().Last();

            todoItems1.Remove(todoToRemove);

            Assert.Throws<Exception>(() => todoItems1.FindById(todoToRemove.TodoId));

        }
        [Fact]
        public void TestRemoveTodoItemThrowsException()
        {
            Assert.Throws<ArgumentException>(() => todoItems2.Remove(null));
        }

    }
}
