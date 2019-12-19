using System;
using System.Collections.Generic;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.DAL {

    public interface ITodoService {

        ResponseModel<List<Todo>> Get();

        ResponseModel<Todo> GetById(Guid todoId);

        ResponseModel<Todo> Insert(Todo model);

        ResponseModel<Todo> Update(Todo model);

        ResponseModel<bool> Delete(Guid todoId);

    }
}
