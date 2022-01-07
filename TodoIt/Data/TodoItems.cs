using System;
using System.Linq;

namespace TodoIt
{
    public class TodoItems
    {
        private static Todo[] todoItems = new Todo[0];

        public int Size()
        {
            return todoItems.Length;
        }

        public Todo[] FindAll()
        {
            return todoItems;
        }

        public Todo FindById(int todoId)
        {
            foreach (Todo todo in todoItems)
                if (todo.TodoId == todoId)
                    return todo;

            throw new Exception("No Todo with that Id found.");
        }

        public Todo AddTodo(string description)
        {
            Todo todo = new Todo(TodoSequencer.NextTodoId(), description);
            todoItems = todoItems.Append<Todo>(todo).ToArray();
            return todo;
        }

        public void Clear()
        {
            todoItems = new Todo[0];
        }

        public Todo[] FindByDoneStatus(bool doneStatus)
        {
            Todo[] todos = new Todo[0];

            foreach (Todo todo in todoItems)
                if (todo.Done == doneStatus)
                    todos = todos.Append<Todo>(todo).ToArray();

            return todos;
        }

        public Todo[] FindByAssignee(int personId)
        {
            Todo[] todos = new Todo[0];

            foreach (Todo todo in todoItems)
                if (todo.Assignee != null && todo.Assignee.PersonId == personId)
                    todos = todos.Append<Todo>(todo).ToArray();

            return todos;
        }

        public Todo[] FindByAssignee(Person assignee)
        {
            Todo[] todos = new Todo[0];

            foreach (Todo todo in todoItems)
                if (todo.Assignee != null && Equals(todo.Assignee, assignee))
                    todos = todos.Append<Todo>(todo).ToArray();

            return todos;
        }

        public Todo[] FindUnassignedTodoItems()
        {
            Todo[] todos = new Todo[0];

            foreach (Todo todo in todoItems)
                if (todo.Assignee != null)
                    todos = todos.Append<Todo>(todo).ToArray();

            return todos;
        }

        public void Remove(Todo itemToRemove)
        {
            int itemIndexToRemove = 0;

            if (itemToRemove == null)
                throw new ArgumentException();

            for (int i = 0; i < Size(); i++)
            {
                if (todoItems[i] == itemToRemove)
                {
                    itemIndexToRemove = i;
                }
            }
            todoItems[itemIndexToRemove] = todoItems[Size() - 1];

            Array.Resize(ref todoItems, Size() - 1);
        }
    }
}




