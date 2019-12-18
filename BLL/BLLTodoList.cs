using System.Collections.Generic;
using web.api.todo.DAL;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using System.Linq;
using System;

namespace web.api.todo.BLL {

    public class BLLTodoList {

        private TODOContext context;

        public BLLTodoList(TODOContext context) {
            this.context = context;
        }

        public List<CustomTodoList> Get() {
            return context.TodoList
                .Join(context.User, todoList => todoList.UserId, user => user.Id, (todoList, user) => new { todoList, user })
                .Join(context.Todo, todoList2 => todoList2.todoList.TodoId, todo => todo.Id, (todoList2, todo) => new { todoList2, todo })
                .Select(select => new CustomTodoList {
                    Id = select.todoList2.todoList.Id,
                    UserId = select.todoList2.todoList.UserId,
                    TodoId = select.todoList2.user.Id,
                    UserName = select.todoList2.user.Name,
                    TodoName = select.todo.Name
                }).ToList();

        }

        public List<CustomTodoList> GetByTodo(Guid todoId) {
            return context.TodoList
                .Join(context.User, todoList => todoList.UserId, user => user.Id, (todoList, user) => new { todoList, user })
                .Join(context.Todo, todoList2 => todoList2.todoList.TodoId, todo => todo.Id, (todoList2, todo) => new { todoList2, todo })
                .Where(where => where.todoList2.todoList.TodoId.Equals(todoId))
                .Select(select => new CustomTodoList {
                    Id = select.todoList2.todoList.Id,
                    UserId = select.todoList2.todoList.UserId,
                    TodoId = select.todoList2.user.Id,
                    UserName = select.todoList2.user.Name,
                    TodoName = select.todo.Name
                }).ToList();
        }

        public List<CustomTodoList> GetByUser(Guid userId) {
            return context.TodoList
                .Join(context.User, todoList => todoList.UserId, user => user.Id, (todoList, user) => new { todoList, user })
                .Join(context.Todo, todoList2 => todoList2.todoList.TodoId, todo => todo.Id, (todoList2, todo) => new { todoList2, todo })
                .Where(where => where.todoList2.todoList.UserId.Equals(userId))
                .Select(select => new CustomTodoList {
                    Id = select.todoList2.todoList.Id,
                    UserId = select.todoList2.todoList.UserId,
                    TodoId = select.todoList2.user.Id,
                    UserName = select.todoList2.user.Name,
                    TodoName = select.todo.Name
                }).ToList();
        }
    }
}
