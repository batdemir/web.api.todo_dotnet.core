using System;
using System.Collections.Generic;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.DAL {

    public interface IUserService {

        ResponseModel<List<Person>> Get();

        ResponseModel<Person> GetById(Guid userId);

        ResponseModel<Person> Insert(Person model);

        ResponseModel<Person> Update(Person model);

        ResponseModel<bool> Delete(Guid userId);

    }
}
