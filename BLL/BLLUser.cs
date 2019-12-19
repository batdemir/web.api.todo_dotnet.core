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
            return Equals(context.Person.Where(user => user.Id == userId).FirstOrDefault(), null);
        }

        public List<Person> Get() {
            return context.Person.ToList();
        }

        public Person GetById(Guid userId) {
            return context.Person.Where(user => user.Id.Equals(userId)).FirstOrDefault();
        }

        public Person Insert(Person model) {
            context.Person.Add(model);
            context.SaveChanges();
            return model;
        }

        public Person Update(Person model) {
            var local = context.Set<Person>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));
            if(local != null) {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }

        public bool Delete(Guid userId) {
            context.Person.Remove(GetById(userId));
            context.SaveChanges();
            return true;
        }

    }
}
