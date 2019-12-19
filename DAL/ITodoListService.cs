using System;
using System.Collections.Generic;
using web.api.todo.Models;
using web.api.todo.Models.Response;

namespace web.api.todo.DAL {

    public interface ITodoListService {

        ResponseModel<List<CustomTodoList>> Get();

        ResponseModel<List<CustomTodoList>> GetByUser(Guid userId);

        ResponseModel<List<CustomTodoList>> GetByTodo(Guid todoId);

    }
}
