using System;
using System.Collections.Generic;
using web.api.todo.Models;
using web.api.todo.Models.DB;

namespace web.api.todo.DAL {

    public interface IUserService {

        ResponseModel<List<User>> Get();

        ResponseModel<User> GetById(Guid userId);

        ResponseModel<User> Insert(User model);

        ResponseModel<User> Update(User model);

        ResponseModel<bool> Delete(Guid userId);

    }
}
