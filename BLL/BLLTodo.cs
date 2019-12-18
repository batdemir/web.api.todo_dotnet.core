using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using web.api.todo.Models.DB;

namespace web.api.todo.BLL {

    public class BLLTodo {

        private TODOContext context;

        public BLLTodo(TODOContext context) {
            this.context = context;
        }

        public bool CheckDataExist(Guid todoId) {
            return Equals(context.Todo.Where(todo => todo.Id.Equals(todoId)).FirstOrDefault(), null);
        }

        public List<Todo> Get() {
            return context.Todo.ToList();
        }

        public Todo GetById(Guid todoId) {
            return context.Todo.Where(todo => todo.Id.Equals(todoId)).FirstOrDefault();
        }

        public Todo Insert(Todo model) {
            context.Todo.Add(model);
            context.SaveChanges();
            return model;
        }

        public Todo Update(Todo model) {
            var local = context.Set<Todo>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));
            if (local != null) {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }

        public bool Delete(Guid todoId) {
            context.Todo.Remove(GetById(todoId));
            context.SaveChanges();
            return false;
        }
    }
}
