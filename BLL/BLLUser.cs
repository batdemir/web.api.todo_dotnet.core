using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using web.api.todo.Models.DB;

namespace web.api.todo.BLL {

    public class BLLUser {

        private TODOContext context;

        public BLLUser(TODOContext context) {
            this.context = context;
        }

        public bool CheckDataExist(Guid userId) {
            return Equals(context.User.Where(user => user.Id == userId).FirstOrDefault(), null);
        }

        public List<User> Get() {
            return context.User.ToList();
        }

        public User GetById(Guid userId) {
            return context.User.Where(user => user.Id.Equals(userId)).FirstOrDefault();
        }

        public User Insert(User model) {
            context.User.Add(model);
            context.SaveChanges();
            return model;
        }

        public User Update(User model) {
            var local = context.Set<User>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));
            if (local != null) {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }

        public bool Delete(Guid userId) {
            context.User.Remove(GetById(userId));
            context.SaveChanges();
            return true;
        }

    }
}
